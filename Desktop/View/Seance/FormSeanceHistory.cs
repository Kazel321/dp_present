using Diplom.Classes;
using Diplom.Classes.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class FormSeanceHistory : Form
    {
        //string savePath = Application.StartupPath + "\\tickets\\" + "Сеанс-" + seance.Film.FilmName + ", Время-" + seancetime + ", Билет-Ряд" + ticket.Place.PlaceRow + "Место" + ticket.Place.PlaceNumber + ".pdf";
        public FormSeanceHistory()
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

        private void FormSeanceHistory_Load(object sender, EventArgs e)
        {
            dateTimePicker.Value = DateTime.Now;
            dateTimePicker.Checked = false;
            update();
        }

        private void update()
        {
            dataGridView.Rows.Clear();

            var tickets = Helper.DB.Ticket.Where(x => x.UserId == Helper.User.UserId).ToList();
            if (dateTimePicker.Checked) tickets = tickets.Where(x => x.TicketDateTime.Date == dateTimePicker.Value.Date).ToList();
            if (!String.IsNullOrEmpty(textBoxSearch.Text)) tickets = tickets.Where(x => x.Seance.Film.FilmName.Contains(textBoxSearch.Text)).ToList();
            tickets = tickets.OrderBy(x => x.TicketDateTime).ToList();

            int ind = -1;

            foreach (var item in tickets)
            {
                ind = dataGridView.Rows.Add();
                dataGridView[0, ind].Value = item.TicketId;
                dataGridView[1, ind].Value = item.Seance.SeanceDate.ToShortDateString() + " " + item.Seance.SeanceTime.ToString();
                dataGridView[2, ind].Value = item.Seance.Film.FilmName;
                dataGridView[3, ind].Value = item.Place.HallId + ", " + item.Place.PlaceRow + ", " + item.Place.PlaceNumber;
                dataGridView[4, ind].Value = item.TicketCost.ToString("C2");
                string user = (item.UserId == null ? "Самостоятельно" : (item.User.UserLastName + " " + item.User.UserFirstName + " " + item.User.UserPatronymic));
                dataGridView[5, ind].Value = user;
                dataGridView[6, ind].Value = item.TicketDateTime.ToString();
            }

            labelCount.Text = "Всего: " + (ind + 1) + " из " + Helper.DB.Ticket.Where(x => x.UserId == Helper.User.UserId).Count();
        }
        private void Filter(object sender, EventArgs e)
        {
            update();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count <= 0)
            {
                MessageBox.Show("Вы не выбрали билет", "Возврат билета", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < dataGridView.SelectedCells.Count; i++)
            {
                int row = dataGridView.SelectedCells[i].RowIndex;
                int id = (int)dataGridView[0, row].Value;
                Ticket t = Helper.DB.Ticket.Where(x => x.TicketId == id).FirstOrDefault();
                Helper.DB.Ticket.Remove(t);
            }
            //int id = (int)dataGridView[0, dataGridView.CurrentRow.Index].Value;
            //Ticket t = Helper.DB.Ticket.Where(x => x.TicketId == id).FirstOrDefault();
            //Helper.DB.Ticket.Remove(t);
            Helper.DB.SaveChanges();
            update();
        }
    }
}
