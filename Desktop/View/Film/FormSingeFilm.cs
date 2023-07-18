using Diplom.Classes;
using Diplom.Classes.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class FormSingleFilm : Form
    {
        Film film;
        int filmId;
        OpenFileDialog openFileDialog = new OpenFileDialog();

        public FormSingleFilm()
        {
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
        }

        public FormSingleFilm(int filmId)
        {
            this.filmId = filmId;
            
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

        private void FormSingleFilm_Load(object sender, EventArgs e)
        {
            comboBoxCountry.DataSource = Helper.DB.Country.OrderBy(x => x.CountryName).ToList();
            comboBoxCountry.DisplayMember = "CountryName";
            comboBoxCountry.ValueMember = "CountryId";
            comboBoxCountry.SelectedIndex = 0;

            comboBoxMinAge.DataSource = Helper.DB.MinAge.OrderBy(x => x.MinAgeValue).ToList();
            comboBoxMinAge.DisplayMember = "MinAgeValue";
            comboBoxMinAge.ValueMember = "MinAgeId";
            comboBoxMinAge.SelectedIndex = 0;

            openFileDialog.Filter = Helper.Filter;

            textBoxId.ReadOnly = true;

            if (Helper.formEvents == FormEvents.Редактирование)
            {
                film = Helper.DB.Film.Where(x => x.FilmId == filmId).FirstOrDefault();

                Text = film.FilmName + " (" + film.FilmYear + ")";
                labelTitle.Text = film.FilmName + " (" + film.FilmYear + ")";

                textBoxId.Text = film.FilmId.ToString();
                textBoxName.Text = film.FilmName;
                textBoxTrailer.Text = film.FilmTrailerLink;
                comboBoxCountry.SelectedValue = film.CountryId;
                comboBoxMinAge.SelectedValue = film.MinAgeId;
                maskedTextBoxDuration.Text = film.FilmDuration.ToString();
                checkBoxActive.Checked = film.FilmActive;
                if (!String.IsNullOrEmpty(film.FilmDescription))
                    textBoxDesc.Text = film.FilmDescription.ToString();
                textBoxYear.Text = film.FilmYear.ToString();
                pictureBoxCover.Image = Image.FromStream(new MemoryStream(film.FilmCover));

                if (film.Screenshot.Count > 0)
                    screens = film.Screenshot.ToList();
                else
                    screens = new List<Screenshot>();
            }
            else
            {
                film = new Film();
                textBoxId.Text = (Helper.DB.Film.Max(x => x.FilmId) + 1) + "";
                Text = Helper.formEvents + " фильма";
                labelTitle.Text = Helper.formEvents + " фильма";
                buttonRemove.Enabled = false;
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog.FileName);
                pictureBoxCover.Image = img;
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                film.FilmCover = ms.ToArray();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            pictureBoxCover.Image = null;
        }

        private void buttonGenre_Click(object sender, EventArgs e)
        {
            FormGenres formGenres = new FormGenres();
            formGenres.genres = new BindingList<Genre>(film.Genre.ToList());
            formGenres.ShowDialog();
            if (formGenres.isSaved)
            {
                film.Genre = formGenres.genres;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string stringId = textBoxId.Text;
            string stringYear = textBoxYear.Text;
            string name = textBoxName.Text;
            string trailer = textBoxTrailer.Text;
            string desc = textBoxDesc.Text;
            string stringDuration = maskedTextBoxDuration.Text;

            int countryId = (int)comboBoxCountry.SelectedValue;
            int minAgeId = (int)comboBoxMinAge.SelectedValue;

            bool active = checkBoxActive.Checked;

            if (String.IsNullOrEmpty(stringId) || String.IsNullOrEmpty(name) || String.IsNullOrEmpty(stringDuration) || String.IsNullOrEmpty(stringYear))
            {
                MessageBox.Show("Вы заполнили не все данные", Helper.formEvents + " фильма", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pictureBoxCover.Image == null)
            {
                MessageBox.Show("Обложка фильма обязательна", Helper.formEvents + " фильма", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int id;
            int year;
            TimeSpan duration;
            try
            {
                id = int.Parse(stringId);
                year = int.Parse(stringYear);
                duration = TimeSpan.Parse(stringDuration);
            }
            catch
            {
                MessageBox.Show("Неверный формат данных", Helper.formEvents + " фильма", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (year > 3000 || year < 1800)
            {
                MessageBox.Show("Неверно задан год", Helper.formEvents + " фильма", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (film.Genre.Count == 0)
            {
                MessageBox.Show("Фильму необходим хотя бы 1 жанр", Helper.formEvents + " фильма", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            film.FilmId = id;
            film.FilmName = name;
            film.CountryId = countryId;
            film.MinAgeId = minAgeId;
            film.FilmDuration = duration;
            film.FilmYear = year;
            film.FilmDescription = desc;
            film.FilmTrailerLink = trailer;
            film.FilmActive = active;

            if (Helper.formEvents == FormEvents.Добавление)
            {
                film.Screenshot = null;
                film.FilmId = (Helper.DB.Film.Max(x => x.FilmId) + 1);
                Helper.DB.Film.Add(film);
                Helper.DB.SaveChanges();
                screens.ForEach(x => x.FilmId = film.FilmId);
                Helper.DB.Screenshot.AddRange(screens);
            }
            else
            {
                Helper.DB.Screenshot.RemoveRange(Helper.DB.Screenshot.Where(x => x.FilmId == film.FilmId));
                Helper.DB.SaveChanges();
                screens.ForEach(x => x.FilmId = film.FilmId);
                Helper.DB.Screenshot.AddRange(screens);
            }
            
            Helper.DB.SaveChanges();

            MessageBox.Show("Фильм сохранен", Helper.formEvents + " фильма", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        List<Screenshot> screens = new List<Screenshot>();

        private void buttonScreens_Click(object sender, EventArgs e)
        {
            FormFilmScreenshots formFilmScreenshots = new FormFilmScreenshots();
            formFilmScreenshots.screens = new BindingList<Screenshot>(screens.ToList());
            formFilmScreenshots.ShowDialog();
            if (formFilmScreenshots.isSaved)
            {
                screens = formFilmScreenshots.screens.ToList();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int filmId = film.FilmId;
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
            Helper.DB.SaveChanges();
            Close();
        }

        private void checkBoxActive_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxActive.Text = checkBoxActive.Checked ? "Участвует" : "Не участвует";
        }
    }
}
