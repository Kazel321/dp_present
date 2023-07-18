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
    public partial class FormDataSeance : Form
    {
        public FormDataSeance()
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
        private void FormDataSeance_Load(object sender, EventArgs e)
        {
            List<Hall> halls = Helper.DB.Hall.ToList();
            comboBoxHall.Items.Add("без фильтра");
            comboBoxHall.SelectedIndex = 0;
            for (int i = 0; i < halls.Count; i++)
            {
                comboBoxHall.Items.Add("Зал " + halls[i].HallId);
            }
            update();
        }

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        private void update()
        {
            dataGridView.Rows.Clear();

            var seances = Helper.DB.Seance.ToList();

            if (dateTimePickerDate.Checked) seances = seances.Where(x => x.SeanceDate == dateTimePickerDate.Value.Date).ToList();
            if (comboBoxHall.SelectedIndex != 0)
            {
                int hallId = Convert.ToInt32(comboBoxHall.SelectedItem.ToString().Substring(4));
                seances = seances.Where(x => x.HallId == hallId).ToList();
            }
            if (!String.IsNullOrEmpty(textBoxSearch.Text)) seances = seances.Where(x => x.Film.FilmName.Contains(textBoxSearch.Text)).ToList();

            int ind = -1;

            seances = seances.OrderBy(x => x.SeanceId).ToList();

            foreach (var item in seances)
            {
                ind = dataGridView.Rows.Add();
                dataGridView[0, ind].Value = item.SeanceId;
                dataGridView[1, ind].Value = item.Film.FilmName;
                dataGridView[2, ind].Value = "Зал " + item.HallId;
                dataGridView[3, ind].Value = item.SeanceDate.ToLongDateString();
                dataGridView[4, ind].Value = item.SeanceTime;
                dataGridView[5, ind].Value = item.SeanceCost.ToString("C2");
            }

            labelCount.Text = "Всего: \n" + (ind + 1) + " из " + Helper.DB.Seance.Count();
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
                form = new FormSingleSeance();
                Helper.formEvents = FormEvents.Добавление;
            }
            else
            {
                form = new FormSingleSeance((int)dataGridView[0, dataGridView.CurrentRow.Index].Value);
                Helper.formEvents = FormEvents.Редактирование;
            }

            Hide();
            form.ShowDialog();
            update();
            Show();
        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int seanceId = (int)dataGridView[0, dataGridView.CurrentRow.Index].Value;
            if (Helper.DB.Ticket.Where(x => x.SeanceId == seanceId).Count() > 0)
            {
                MessageBox.Show("В базе данных присутствуют купленные билеты на данный сеанс", "Удаление сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Helper.DB.Seance.Remove(Helper.DB.Seance.Where(X => X.SeanceId == seanceId).FirstOrDefault());
            Helper.DB.SaveChanges();

            MessageBox.Show("Сеанс успешно удален", "Удаление сеанса", MessageBoxButtons.OK, MessageBoxIcon.Information);
            update();
        }

        private void Filter(object sender, EventArgs e)
        {
            update();
        }

        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow.Index >= 0)
            buttonAdd_Click(buttonEdit, e);
        }
    }
}
