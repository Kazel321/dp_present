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
    public partial class FormDataFilm : Form
    {
        public FormDataFilm()
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
        private void FormDataFilm_Load(object sender, EventArgs e)
        {
            var lst = Helper.DB.Genre.Select(x => x.GenreName).ToList();
            lst.Insert(0,"Все жанры");
            comboBoxGenres.DataSource= lst;
            comboBoxGenres.SelectedIndex = 0;

            var st = Helper.DB.Film.Select(x => x.FilmYear.ToString()).Distinct().OrderBy(x => x).ToList();
            st.Insert(0, "Все года");
            comboBoxYear.DataSource = st;
            comboBoxYear.SelectedIndex = 0;

            update();
        }

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        private void update()
        {
            Helper.DB = new DB(Helper.connection);
            dataGridView.Rows.Clear();

            var films = Helper.DB.Film.ToList();

            if (comboBoxGenres.SelectedIndex > 0) films = films.Where(x => x.Genre.Any(g => g.GenreId == comboBoxGenres.SelectedIndex)).ToList();
            if (comboBoxYear.SelectedIndex > 0) films = films.Where(x => x.FilmYear == Convert.ToInt32(comboBoxYear.Text)).ToList();
            if (!String.IsNullOrEmpty(textBoxSearch.Text)) films = films.Where(x => x.FilmName.Contains(textBoxSearch.Text)).ToList();

            films = films.OrderBy(x => x.FilmId).ToList();

            int ind = -1;

            foreach (var f in films)
            {
                ind = dataGridView.Rows.Add();
                dataGridView[0, ind].Value = f.FilmId;
                dataGridView[1, ind].Value = f.FilmName;
                dataGridView[2, ind].Value = f.FilmYear;
                dataGridView[3, ind].Value = f.Country.CountryName;
                dataGridView[4, ind].Value = f.MinAge.MinAgeValue + "+";
                dataGridView[5, ind].Value = f.FilmDuration.ToString();
                dataGridView[6, ind].Value = f.FilmActive;
            }

            labelCount.Text = "Всего: \n" + (ind + 1) + " из " + Helper.DB.Film.Count();
        }

        private void Filter(object sender, EventArgs e)
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
                form = new FormSingleFilm();
                Helper.formEvents = FormEvents.Добавление;
            }
            else
            {
                form = new FormSingleFilm((int)dataGridView[0, dataGridView.CurrentRow.Index].Value);
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
            int filmId = (int)dataGridView[0, dataGridView.CurrentRow.Index].Value;
            if (Helper.DB.Seance.Where(x => x.FilmId == filmId).FirstOrDefault() != null)
            {
                MessageBox.Show("Фильм используется в сеансах", "Удаление фильма", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Film f = Helper.DB.Film.Where(x => x.FilmId == filmId).FirstOrDefault();
            var genres = f.Genre.ToList();
            foreach (var item in genres)
            {
                f.Genre.Remove(item);
            }
            var screens = f.Screenshot.ToList();
            Helper.DB.Screenshot.RemoveRange(screens);
            Helper.DB.Film.Remove(f);
            //catch
            //{
            //    MessageBox.Show("Ошибка", "Удаление фильма", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            Helper.DB.SaveChanges();
            update();
        }

        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow.Index >= 0)
                buttonAdd_Click(buttonEdit, e);
        }

    }
}
