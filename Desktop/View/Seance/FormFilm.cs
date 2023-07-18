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
    public partial class FormFilm : Form
    {
        Film film;

        public FormFilm()
        {
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
        }

        public FormFilm(int filmId)
        {
            Helper.DB = new DB(Helper.connection);
            film = Helper.DB.Film.Where(x => x.FilmId == filmId).FirstOrDefault();
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
        private void FormFilm_Load(object sender, EventArgs e)
        {
            Text = film.FilmName;
            labelTitle.Text = film.FilmName;

            if (!String.IsNullOrEmpty(film.FilmCover.ToString())) pictureBoxCover.Image = Image.FromStream(new MemoryStream(film.FilmCover));

            labelYear.Text = film.FilmYear.ToString();
            labelCountry.Text = film.Country.CountryName;
            labelAge.Text = film.MinAge.MinAgeValue.ToString() + "+";
            labelDesc.Text = film.FilmDescription;
            labelTime.Text = film.FilmDuration.ToString().Substring(0, film.FilmDuration.ToString().LastIndexOf(':'));

            List<Genre> genres = film.Genre.ToList();
            for (int i = 0; i < genres.Count; i++)
            {
                labelGenres.Text += genres[i].GenreName;
                if (i != (genres.Count - 1)) labelGenres.Text += ", ";
            }
        }

        /// <summary>
        /// Скриншоты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonScreenshots_Click(object sender, EventArgs e)
        {
            if (Helper.DB.Screenshot.Where(x => x.FilmId == film.FilmId).Count() == 0)
            {
                MessageBox.Show("У фильма нет скриншотов", "Скриншоты", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FormScreenshot formScreenshot = new FormScreenshot(film.FilmId);
            formScreenshot.ShowDialog();
        }
    }
}
