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
    public partial class FormSingleSeance : Form
    {
        Seance seance;
        int seanceId;
        public FormSingleSeance()
        {
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
        }

        public FormSingleSeance(int seanceId)
        {
            this.seanceId = seanceId;
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
        private void FormSingleSeance_Load(object sender, EventArgs e)
        {
            comboBoxFilm.SelectedIndexChanged += comboBoxFilm_SelectedIndexChanged;

            seance = Helper.DB.Seance.Where(x => x.SeanceId == seanceId).FirstOrDefault();

            comboBoxFilm.DataSource = Helper.DB.Film.Where(x => x.FilmActive).ToList();
            comboBoxFilm.DisplayMember = "FilmName";
            comboBoxFilm.ValueMember = "FilmId";
            comboBoxFilm.SelectedIndex = 0;

            comboBoxHall.DataSource = Helper.DB.Hall.ToList();
            comboBoxHall.DisplayMember = "HallName";
            comboBoxHall.ValueMember = "HallId";
            comboBoxHall.SelectedIndex = 0;

            maskedTextBoxDate.Text = DateTime.Now.ToShortDateString();
            maskedTextBoxTime.Text = DateTime.Now.ToShortTimeString();
            if (DateTime.Now.TimeOfDay.Hours < 10)
            {
                maskedTextBoxTime.Text = "0" + maskedTextBoxTime.Text;
            }

            if (Helper.formEvents == FormEvents.Редактирование)
            {
                maskedTextBoxDate.Text = seance.SeanceDate.ToShortDateString();
                maskedTextBoxTime.Text = seance.SeanceTime.ToString();

                textBoxCost.Text = seance.SeanceCost.ToString();

                comboBoxFilm.SelectedValue = seance.FilmId;
                comboBoxHall.SelectedValue = seance.HallId;
                buttonDelete.Enabled = true;
            }
            Text = "Сеанс (" + Helper.formEvents + ")";
            labelTitle.Text = Helper.formEvents + " сеанса";

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxFilm.SelectedIndex < 0)
            {
                MessageBox.Show("Вы не можете сохранить сеанс с не активным фильмом", Helper.formEvents + " сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(textBoxCost.Text) || String.IsNullOrEmpty(maskedTextBoxDate.Text) || String.IsNullOrEmpty(maskedTextBoxTime.Text))
            {
                MessageBox.Show("Вы заполнили не все данные!", Helper.formEvents + " сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string stringDate = maskedTextBoxDate.Text;
            string stringTime = maskedTextBoxTime.Text;
            string stringCost = textBoxCost.Text;

            DateTime date;
            try
            {
                int day = Convert.ToInt32(stringDate.Split('.')[0]);
                int month = Convert.ToInt32(stringDate.Split('.')[1]);
                int year = Convert.ToInt32(stringDate.Split('.')[2]);
                date = new DateTime(year, month, day);
            }
            catch
            {
                MessageBox.Show("Неправильный формат даты", Helper.formEvents + " сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (date < DateTime.Now.Date)
            {
                MessageBox.Show("Дата не может быть в прошлом", Helper.formEvents + " сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TimeSpan time;
            try
            {
                time = TimeSpan.Parse(stringTime);
            }
            catch
            {
                MessageBox.Show("Неправильный формат времени", Helper.formEvents + " сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (date.Date == DateTime.Now.Date)
                if (time < DateTime.Now.TimeOfDay)
                {
                    MessageBox.Show("Время не может быть в прошлом", Helper.formEvents + " сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            float cost;
            try
            {
                cost = float.Parse(stringCost);
            }
            catch
            {
                MessageBox.Show("Неправильный формат цены", Helper.formEvents + " сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cost <= 0)
            {
                MessageBox.Show("Цена должна быть положительной", Helper.formEvents + " сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int filmId = (int)comboBoxFilm.SelectedValue;
            int hallId = (int)comboBoxHall.SelectedValue;


            var checkSeances = Helper.DB.Seance.Where(x => x.SeanceDate == date && x.HallId == hallId).ToList();
            foreach (var checkSeance in checkSeances)
            {
                if (checkSeance != null)
                {
                    if (seanceId != null && seanceId == checkSeance.SeanceId)
                        continue;
                    Film checkFilm = Helper.DB.Film.Where(x => x.FilmId == checkSeance.FilmId).FirstOrDefault();
                    TimeSpan endTime = checkSeance.SeanceTime + checkFilm.FilmDuration;
                    Film f = Helper.DB.Film.Where(x => x.FilmId == filmId).FirstOrDefault();
                    TimeSpan startTime = checkSeance.SeanceTime - f.FilmDuration;
                    if (time <= endTime && time >= startTime)
                    {
                        MessageBox.Show("На указанную дату и время в этом зале назначен другой сеанс", Helper.formEvents + " сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            Seance s;
            if (Helper.formEvents == FormEvents.Добавление)
            {
                s = new Seance();
                //s.SeanceId = Helper.DB.Seance.Max(x => x.SeanceId) + 1;
            }
            else
            {
                s = seance;
            }

            s.SeanceDate = date;
            s.SeanceTime = time;
            s.SeanceCost = cost;
            s.FilmId = filmId;

            s.HallId = hallId;

            if (Helper.formEvents == FormEvents.Добавление)
                Helper.DB.Seance.Add(s);

            try
            {
                Helper.DB.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Ошибка, попробуйте снова позже", Helper.formEvents + " сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Данные успешно сохранены", Helper.formEvents + " сеанса", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int seanceId = seance.SeanceId;
            if (Helper.DB.Ticket.Where(x => x.SeanceId == seanceId).FirstOrDefault() != null)
            {
                MessageBox.Show("На данный сеанс приобретены билеты", "Удаление сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Helper.DB.Seance.Remove(Helper.DB.Seance.Where(x => x.SeanceId == seanceId).FirstOrDefault());
            Helper.DB.SaveChanges();
            MessageBox.Show("Сеанс удален", "Удаление сеанса", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void comboBoxFilm_SelectedIndexChanged(object sender, EventArgs e)
        {
            Film f = (Film)comboBoxFilm.SelectedItem;
            if (f != null)
            labelDuration.Text = f.FilmDuration.ToString().Substring(0,5);
        }
    }
}
