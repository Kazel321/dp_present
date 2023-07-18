using Diplom.Classes;
using Diplom.Classes.Entities;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Diplom
{
    public partial class FormManagerMenu : Form
    {
        public FormManagerMenu()
        {
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
        }


        /// <summary>
        /// Вернутся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Переход на следующую форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNext_Click(object sender, EventArgs e)
        {
            Helper.DB = new DB(Helper.connection);
            FormReportDate dateForm = new FormReportDate();
            dateForm.ShowDialog();
            if (dateForm.isSuccess)
            {
                DateTime minDate = dateForm.minDate;
                DateTime maxDate = dateForm.maxDate;
                
                List<KeyValuePair<string, int>> chartValues = new List<KeyValuePair<string, int>>();
                dynamic data = "";
                string title = "";
                string chartTitle = "";
                FormReport nextForm;
                Button button = (Button)sender;
                string next = button.Tag.ToString();
                switch (next)
                {
                    case "Film":
                        title = "Отчет по фильмам от " + minDate.ToShortDateString() + " до " + maxDate.ToShortDateString();
                        chartTitle = "Количество проданных билетов";
                        var seances = Helper.DB.Seance.Where(x => x.SeanceDate >= minDate && x.SeanceDate <= maxDate).ToList();
                        var filmGroup = seances
                            .GroupBy(x => x.Film)
                            .Select(g => new
                            {
                                FilmName = g.Key.FilmName,
                                TicketCount = g.SelectMany(p => p.Ticket).Count()
                            });

                        foreach (var result in filmGroup)
                        {
                            chartValues.Add(new KeyValuePair<string, int>(result.FilmName, result.TicketCount));
                        }

                        data = seances
                            .GroupBy(x => x.Film)
                            .Select(g => new
                            {
                                Фильм = g.Key.FilmName,
                                Сеансы = g.Count(),
                                Зрители = g.SelectMany(p => p.Ticket).Count(),
                                Средняя_посещаемость = Math.Round((double)g.SelectMany(p => p.Ticket).Count() / (double)g.Count()),
                                Сборы = g.Sum(x => x.Ticket.Sum(s => s.TicketCost)).ToString("C2")
                            }).ToList();

                        break;
                    case "User":
                        title = "Отчет по сотрудникам от " + minDate.ToShortDateString() + " до " + maxDate.ToShortDateString();
                        chartTitle = "Количество проданных билетов";
                        maxDate = maxDate.AddHours(23);
                        maxDate = maxDate.AddMinutes(59);
                        maxDate = maxDate.AddSeconds(59);
                        var c = Helper.DB.Ticket.Where(x => x.TicketDateTime <= maxDate).ToList();
                        var tickets = Helper.DB.Ticket.Where(x => x.TicketDateTime >= minDate && x.TicketDateTime <= maxDate).ToList();
                        var userGroup = tickets.Where(x => x.User != null)
                            .GroupBy(x => x.User)
                            .Select(g => new
                            {
                                FIO = g.Key.UserLastName + " " + g.Key.UserFirstName.Substring(0, 1) + "." + g.Key.UserPatronymic.Substring(0, 1) + ".",
                                TicketCount = g.Count()
                            });
                        foreach (var result in userGroup)
                        {
                            chartValues.Add(new KeyValuePair<string, int>(result.FIO, result.TicketCount));
                        }

                        data = tickets.Where(x => x.User != null)
                            .GroupBy(x => x.User)
                            .Select(g => new
                            {
                                ФИО = g.Key.UserLastName + " " + g.Key.UserFirstName + " " + g.Key.UserPatronymic,
                                Входы = g.Key.Login.Where(x => x.LoginDateTime >= minDate && x.LoginDateTime <= maxDate).Count(),
                                Продано_билетов = g.Count(),
                                Общая_сумма = g.Sum(x => x.TicketCost).ToString("C2")
                            }).ToList();

                        break;
                    case "Hall":
                        title = "Отчет по залам от " + minDate.ToShortDateString() + " до " + maxDate.ToShortDateString();
                        chartTitle = "Количество проданных билетов";
                        List<Seance> hallSeances = Helper.DB.Seance.Where(x => x.SeanceDate >= minDate && x.SeanceDate <= maxDate).ToList();
                        var hallGroup = hallSeances
                            .GroupBy(x => x.Hall)
                            .Select(g => new
                            {
                                Hall = g.Key.HallName,
                                TicketCount = g.SelectMany(t => t.Ticket).Count()
                            });
                        foreach (var result in hallGroup)
                        {
                            chartValues.Add(new KeyValuePair<string, int>(result.Hall, result.TicketCount));
                        }

                        data = hallSeances
                            .GroupBy(x => x.Hall)
                            .Select(g => new
                            {
                                Номер = g.Key.HallId,
                                Название = g.Key.HallName,
                                Популярное_место = g.Select(x => x.Ticket.Select(p => p.Place).OrderByDescending(o => o.Ticket.Count)).FirstOrDefault().FirstOrDefault() != null ? 
                                ("Ряд: " + g.Select(x => x.Ticket.Select(p => p.Place).OrderByDescending(o => o.Ticket.Count)).FirstOrDefault().FirstOrDefault().PlaceRow +
                                ", Место: " + g.Select(x => x.Ticket.Select(p => p.Place).OrderByDescending(o => o.Ticket.Count)).FirstOrDefault().FirstOrDefault().PlaceNumber)
                                : "Нет билетов",
                                Количество_сеансов = g.Count(),
                                Общая_сумма = g.SelectMany(x => x.Ticket).Sum(a => a.TicketCost).ToString("C2")

                            }).ToList();

                        break;
                    case "Sell":
                        maxDate = maxDate.AddHours(23);
                        maxDate = maxDate.AddMinutes(59);
                        maxDate = maxDate.AddSeconds(59);
                        title = "Отчет по продажам от " + minDate.ToShortDateString() + " до " + maxDate.ToShortDateString();
                        chartTitle = "Количество проданных билетов";
                        var sellTickets = Helper.DB.Ticket.Where(x => x.TicketDateTime >= minDate && x.TicketDateTime <= maxDate).ToList();
                        var monthSells = sellTickets
                            .GroupBy(x => x.TicketDateTime.Month)
                            .Select(g => new
                            {
                                Month = g.Select(x => x.TicketDateTime.ToString("MMMM")).First(),
                                TicketCount = g.Where(x => x.TicketDateTime.Month == g.Key).Select(x => x.TicketId).Count()
                            });
                        foreach (var result in monthSells)
                        {
                            chartValues.Add(new KeyValuePair<string, int>(result.Month, result.TicketCount));
                        }

                        var ds = sellTickets
                            .Select(g => new
                            {
                                Сеансы = sellTickets.Select(x => x.Seance).Distinct().Count(),
                                Зрители = sellTickets.Count,
                                Средняя_посещаемость = Math.Round((double)sellTickets.Count / (double)sellTickets.Select(x => x.Seance).Distinct().Count()),
                                Самостоятельно = sellTickets.Where(x => x.UserId == null).Count(),
                                На_кассе = sellTickets.Where(x => x.UserId != null).Count(),
                                Популярный_фильм = sellTickets.GroupBy(x => x.Seance.Film).OrderByDescending(d => d.Count()).Select(b => b.Key).FirstOrDefault().FilmName,
                                Продано_VIP = sellTickets.Where(x => x.Place.PlaceTypeId == 2).Select(x => x.TicketId).Count(),
                                Общая_сумма = sellTickets.Sum(x => x.TicketCost).ToString("C2")
                            }).ToList();

                        if (ds.Count > 1)
                        ds.RemoveRange(1, ds.Count - 1);
                        data = ds;

                        break;
                    default:
                        return;
                }
                Hide();
                nextForm = new FormReport(title, chartValues, chartTitle);
                nextForm.DataSource = data;
                nextForm.ShowDialog();
                Show();
            }
        }
    }
}
