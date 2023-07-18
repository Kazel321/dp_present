using Diplom.Classes;
using Diplom.Classes.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class FormDataTicket : Form
    {
        DateTime date;

        public FormDataTicket()
        {
            InitializeComponent();
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
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDataTicket_Load(object sender, EventArgs e)
        {
            update();
        }

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        private void update()
        {
            Helper.DB = new DB(Helper.connection);
            dataGridView.Rows.Clear();

            var tickets = Helper.DB.Ticket.ToList();
            if (dateTimePicker.Checked) tickets = tickets.Where(x => x.TicketDateTime.Date == dateTimePicker.Value.Date).ToList();
            if (!String.IsNullOrEmpty(textBoxSearch.Text)) tickets = tickets.Where(x => x.TicketCode.Contains(textBoxSearch.Text)).ToList();

            int ind = -1;

            tickets = tickets.OrderBy(x => x.TicketId).ToList();

            foreach (var item in tickets)
            {
                ind = dataGridView.Rows.Add();
                dataGridView[0, ind].Value = item.TicketId;
                dataGridView[1, ind].Value = item.SeanceId;
                dataGridView[2, ind].Value = item.Seance.SeanceDate.ToShortDateString() + " " + item.Seance.SeanceTime.ToString();
                dataGridView[3, ind].Value = item.Seance.Film.FilmName;
                dataGridView[4, ind].Value = item.Place.HallId + ", " + item.Place.PlaceRow + ", " + item.Place.PlaceNumber;
                dataGridView[5, ind].Value = item.TicketCost.ToString("C2");
                string user = (item.UserId == null ? "Самостоятельно" : (item.User.UserLastName + " " + item.User.UserFirstName + " " + item.User.UserPatronymic));
                dataGridView[6, ind].Value = user;
                dataGridView[7, ind].Value = item.TicketDateTime.ToString("dd.MM.yyyy HH:mm:ss");
                dataGridView[8, ind].Value = item.TicketCode;
                dataGridView[9, ind].Value = item.TicketActive;
            }

            labelCount.Text = "Всего: " + (ind + 1) + " из " + Helper.DB.Ticket.Count();
        }
        private void Filter(object sender, EventArgs e)
        {
            update();
        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count > 0)
            {
                if (MessageBox.Show("Вы уверены?", "Очистка истории билетов", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                for (int i = 0; i < dataGridView.SelectedCells.Count; i++)
                {
                    int ind = dataGridView.SelectedCells[i].RowIndex;
                    int id = (int)dataGridView[0, ind].Value;
                    Helper.DB.Ticket.Remove(Helper.DB.Ticket.Where(x => x.TicketId == id).FirstOrDefault());
                }
                Helper.DB.SaveChanges();
                update();
            }
        }

        private void buttonChangeActive_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow != null && dataGridView.CurrentRow.Index >= 0)
            {
                int id = (int)dataGridView[0, dataGridView.CurrentRow.Index].Value;
                Ticket t = Helper.DB.Ticket.Where(x => x.TicketId == id).FirstOrDefault();
                t.TicketActive = !t.TicketActive;
                Helper.DB.SaveChanges();
                update();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Вы уверены в том, что хотите очистить истрию?", "Очистка истории билетов", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                Helper.DB.Ticket.RemoveRange(Helper.DB.Ticket);
                Helper.DB.SaveChanges();
                update();
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Application.StartupPath + "\\tickets\\");

                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
                catch
                {
                    return;
                }
            }
        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            update();
        }
    }
}
