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
    public partial class FormDataHall : Form
    {
        public FormDataHall()
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
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDataHall_Load(object sender, EventArgs e)
        {
            update();
        }

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        private void update()
        {
            dataGridView.Rows.Clear();

            var halls = Helper.DB.Hall.ToList();

            if (!String.IsNullOrEmpty(textBoxSearch.Text)) halls = halls.Where(x => x.HallName.Contains(textBoxSearch.Text)).ToList();

            halls = halls.OrderBy(x => x.HallId).ToList();

            int ind = -1;

            foreach (var hall in halls)
            {
                ind = dataGridView.Rows.Add();
                dataGridView[0, ind].Value = hall.HallId;
                dataGridView[1, ind].Value = hall.HallName;
            }

            labelCount.Text = "Всего: " + (ind + 1) + " из " + Helper.DB.Hall.Count();
        }

        private void Filter(Object sender, EventArgs e)
        {
            update();
        }

        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form form = null;
            Button button = (Button)sender;
            if (button.Text == buttonAdd.Text)
            {
                form = new FormSingleHall();
                Helper.formEvents = FormEvents.Добавление;
            }
            else
            {
                form = new FormSingleHall((int)dataGridView[0, dataGridView.CurrentRow.Index].Value);
                Helper.formEvents = FormEvents.Редактирование;
            }

            Hide();
            form.ShowDialog();
            Show();
            update();
        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                int hallId = (int)dataGridView[0, dataGridView.CurrentRow.Index].Value;
                if (Helper.DB.Seance.Where(x => x.HallId == hallId).Count() == 0 && Helper.DB.Ticket.Where(x => x.Place.HallId == hallId).Count() == 0)
                {
                    Helper.DB.Place.RemoveRange(Helper.DB.Place.Where(x => x.HallId == hallId));
                    Helper.DB.Hall.Remove(Helper.DB.Hall.Where(x => x.HallId == hallId).FirstOrDefault());
                    Helper.DB.SaveChanges();
                    update();
                }
                else
                {
                    MessageBox.Show("Зал используется в сеансах или его места используются в билетах", "Удаление зала", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                buttonAdd_Click(buttonEdit, e);
            }
        }
    }
}
