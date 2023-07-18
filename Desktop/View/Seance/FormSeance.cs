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
using static System.Net.WebRequestMethods;

namespace Diplom
{
    public partial class FormSeance : Form
    {
        DateTime date;
        static FormSeance context;

        public FormSeance()
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
        private void FormSeance_Load(object sender, EventArgs e)
        {
            context = this;
            labelTitle.Text = DateTime.Now.ToLongTimeString();
            dateTimePickerSeance.MinDate = DateTime.Now.Date;
            //comboBoxGenre.SelectedIndexChanged -= Filter;
            //comboBoxAge.SelectedIndexChanged -= Filter;
            //comboBoxYear.SelectedIndexChanged -= Filter;
            //dateTimePickerSeance.ValueChanged -= Filter;
            DateTime now = DateTime.Now.Date;
            TimeSpan timeNow = DateTime.Now.TimeOfDay;
            //date = Helper.DB.Seance.Where(x => x.SeanceDate >= now).GroupBy(x => x.SeanceDate).FirstOrDefault().Key;
            //date = Helper.DB.Seance.Where(x => x.SeanceDate >= now).OrderBy(x => x.SeanceDate).FirstOrDefault().SeanceDate;
            date = Helper.DB.Seance.Where(x => x.SeanceDate > now || (x.SeanceDate == now && x.SeanceTime > timeNow)).Min(x => x.SeanceDate);
            //minDate = Helper.DB.Seance.GroupBy(x => x.SeanceDate).FirstOrDefault();
            dateTimePickerSeance.Value = date;
            var genres = Helper.DB.Genre.Select(x => x.GenreName).ToList();
            genres.Insert(0, "все жанры");
            comboBoxGenre.DataSource = genres;
            comboBoxGenre.SelectedIndex = 0;
            var minAges = Helper.DB.MinAge.Select(x => x.MinAgeValue).ToList();
            comboBoxAge.Items.Add("всех возрастов");
            comboBoxAge.SelectedIndex = 0;
            foreach (var item in minAges)
            {
                comboBoxAge.Items.Add(item.ToString() + "+");
            }
            comboBoxYear.SelectedIndex = 1;
            ShowSeance();
            int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;

            tableLayoutPanelList.Padding = new Padding(0, 0, vertScrollWidth, 0);

            comboBoxGenre.SelectedIndexChanged += Filter;
            comboBoxAge.SelectedIndexChanged += Filter;
            comboBoxYear.SelectedIndexChanged += Filter;
            dateTimePickerSeance.ValueChanged += Filter;
        }

        class FilmSeance : TableLayoutPanel
        {
            //TableLayoutPanel tableData;
            PictureBox pictureBoxCover;
            Panel panelButtons;
            Label filmName;
            Label filmAge;
            Label filmDesc;
            //List<Button> buttons;
            Button[] buttons;


            public FilmSeance()
            {
                this.Size = new Size(200, 250);
                this.Dock = DockStyle.Top;
                this.RowCount = 3;
                this.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
                this.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
                this.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
                this.ColumnCount = 3;
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

                pictureBoxCover = new PictureBox();
                pictureBoxCover.Dock = DockStyle.Fill;
                pictureBoxCover.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxCover.Cursor = Cursors.Hand;
                pictureBoxCover.MouseEnter += pictureBoxCover_MouseEnter;
                pictureBoxCover.MouseLeave += pictureBoxCover_MouseLeave;
                pictureBoxCover.Click += film_Click;
                this.SetRowSpan(pictureBoxCover, 3);

                filmName = new Label();
                filmName.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
                filmName.MouseEnter += filmName_MouseEnter;
                filmName.MouseLeave += filmName_MouseLeave;
                filmName.Click += film_Click;
                filmName.Dock = DockStyle.Fill;
                filmName.Cursor = Cursors.Hand;
                filmName.TextAlign = ContentAlignment.MiddleLeft;

                filmDesc = new Label();
                filmDesc.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                filmDesc.Dock = DockStyle.Fill;
                filmDesc.TextAlign = ContentAlignment.TopLeft;
                filmDesc.ForeColor = Color.Gray;
                this.SetColumnSpan(filmDesc, 2);

                filmAge = new Label();
                filmAge.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                filmAge.Dock = DockStyle.Fill;
                filmAge.TextAlign = ContentAlignment.MiddleRight;
                filmAge.ForeColor = Color.DarkRed;

                panelButtons = new Panel();
                panelButtons.Dock = DockStyle.Fill;
                this.SetColumnSpan(panelButtons, 2);

                this.Controls.Add(this.pictureBoxCover, 0, 0);
                this.Controls.Add(this.filmName, 1, 0);
                this.Controls.Add(this.filmAge, 2, 0);
                this.Controls.Add(this.filmDesc, 1, 1);
                this.Controls.Add(this.panelButtons, 1, 2);
            }

            public FilmSeance(String filmName, int filmYear, String filmAge, String filmDesc, List<Seance> seances, Image filmImage) : this()
            {
                this.filmName.Text = filmName;
                this.filmName.Text += $" ({filmYear})";
                this.filmAge.Text = filmAge + "+";
                this.filmDesc.Text = filmDesc;
                this.pictureBoxCover.Image = filmImage;

                buttons = new Button[seances.Count];
                for (int i = 0; i < seances.Count; i++)
                {
                    buttons[i] = new Button();
                    buttons[i].Location = new Point(50 * i, 0);
                    buttons[i].Width = 75;
                    buttons[i].Height = 25;
                    buttons[i].Tag = seances[i].SeanceId;
                    buttons[i].Text = seances[i].SeanceTime.ToString().Substring(0, seances[i].SeanceTime.ToString().LastIndexOf(':'));
                    buttons[i].Click += buttonSeance_Click;
                }

                panelButtons.Controls.AddRange(buttons);
            }

            public FilmSeance(Film film, List<Seance> seances) : this()
            {
                this.filmName.Text = film.FilmName;
                this.filmName.Text += $" ({film.FilmYear})";
                this.filmAge.Text = film.MinAge.MinAgeValue.ToString() + "+";
                List<Genre> genres = film.Genre.ToList();
                string desc = "Жанры: " + genres[0].GenreName;
                for (int i = 1; i < genres.Count; i++)
                {
                    desc += ", " + genres[i].GenreName;
                }
                desc += Environment.NewLine;
                desc += Environment.NewLine;
                desc += film.FilmDescription;
                this.filmDesc.Text = desc;
                this.pictureBoxCover.Image = Image.FromStream(new MemoryStream(film.FilmCover));
                this.pictureBoxCover.Tag = film.FilmId;
                this.filmName.Tag = film.FilmId;

                buttons = new Button[seances.Count];
                for (int i = 0; i < seances.Count; i++)
                {
                    buttons[i] = new Button();
                    buttons[i].Location = new Point(85 * i, 0);
                    buttons[i].Width = 75;
                    buttons[i].Height = 30;
                    buttons[i].Tag = seances[i].SeanceId;
                    buttons[i].Text = seances[i].SeanceTime.ToString().Substring(0, seances[i].SeanceTime.ToString().LastIndexOf(':'));
                    buttons[i].Cursor = Cursors.Hand;
                    buttons[i].Click += buttonSeance_Click;
                }

                panelButtons.Controls.AddRange(buttons);
            }

            private void filmName_MouseEnter(object sender, EventArgs e)
            {
                filmName.Font = new Font("Microsoft Sans Serif", 14F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point, ((byte)(204)));
            }

            private void filmName_MouseLeave(object sender, EventArgs e)
            {
                filmName.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            }

            private void pictureBoxCover_MouseEnter(object sender, EventArgs e)
            {
                pictureBoxCover.BorderStyle = BorderStyle.FixedSingle;
            }

            private void pictureBoxCover_MouseLeave(object sender, EventArgs e)
            {
                pictureBoxCover.BorderStyle = BorderStyle.None;
            }
        }


        static void buttonSeance_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int seanceId = (int)button.Tag;
            if (context.date == DateTime.Now.Date)
            { 
                Seance s = Helper.DB.Seance.Where(x => x.SeanceId == seanceId).FirstOrDefault();
                if (s.SeanceTime < DateTime.Now.TimeOfDay)
                {
                    MessageBox.Show("Данный сеанс больше не доступен", "Выбор сеанса", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    context.ShowSeance();
                    return;
                }
            }
            FormOrderSeance formOrderSeance = new FormOrderSeance((int)button.Tag);
            formOrderSeance.ShowDialog();
            //MessageBox.Show(button.Tag.ToString());
        }

        static void film_Click(object sender, EventArgs e)
        {
            Control film = (Control)sender;
            FormFilm formFilm = new FormFilm((int)film.Tag);
            context.Hide();
            formFilm.ShowDialog();
            context.Show();
        }

        /// <summary>
        /// Показать сеансы
        /// </summary>
        void ShowSeance()
        {
            tableLayoutPanelList.Controls.Clear();
            tableLayoutPanelList.RowCount = 0;
            tableLayoutPanelList.RowStyles.Clear();

            date = dateTimePickerSeance.Value;
            string searchName = textBoxSearch.Text;

            using (DB DB = new DB(Helper.connection))
            {
                var filmSeances = DB.Seance.Where(x => x.SeanceDate == date).ToList();
                if (comboBoxAge.SelectedIndex > 0) filmSeances = filmSeances.Where(x => x.Film.MinAgeId <= comboBoxAge.SelectedIndex).ToList();
                //if (comboBoxYear.SelectedIndex != 0) filmSeances = filmSeances.Where(x => x.Film.FilmYear == (int)comboBoxYear.SelectedItem).ToList();

                if (comboBoxGenre.SelectedIndex > 0) filmSeances = filmSeances.Where(x => x.Film.Genre.Any(g => g.GenreId == comboBoxGenre.SelectedIndex)).ToList();

                var films = filmSeances.GroupBy(x => x.Film).ToList();

                //if (comboBoxGenre.SelectedIndex > 0)
                //{
                //    Genre selectedGenre = DB.Genre.Where(x => x.GenreId == comboBoxGenre.SelectedIndex).FirstOrDefault();
                //    for (int i = 0; i < films.Count; i++)
                //    {
                //        Film film = films[i].Key;
                //        var find = film.Genre.Where(x => x.GenreId == selectedGenre.GenreId).FirstOrDefault();
                //        if (find == null)
                //        {
                //            films.Remove(films[i]);
                //            i--;
                //        }
                //    }
                //}

                

                //var filmSeances = DB.Seance.Where(x => x.SeanceDate == date).GroupBy(x => x.FilmId).ToList();

                if (comboBoxYear.SelectedIndex > 0)
                {
                    if (comboBoxYear.SelectedIndex == 1) films = films.OrderByDescending(x => x.Key.FilmYear).ToList();
                    else films = films.OrderBy(x => x.Key.FilmYear).ToList();
                }
                else 
                    films = films.OrderBy(x => x.Key.FilmName).ToList();

                List<Seance> seances;
                int ind = 0;
                foreach (var item in films)
                {
                    Film film = item.Key;
                    if (String.IsNullOrEmpty(searchName) || film.FilmName.Contains(searchName))
                    {
                        if (date == DateTime.Now.Date)
                        {
                            TimeSpan timeNow = DateTime.Now.TimeOfDay;
                            seances = DB.Seance.Where(x => x.SeanceDate == date && x.FilmId == film.FilmId && x.SeanceTime >= timeNow).ToList();
                        }
                        else
                            seances = DB.Seance.Where(x => x.SeanceDate == date && x.FilmId == film.FilmId).ToList();

                        if (seances.Count == 0)
                        {
                            continue;
                        }

                        seances = seances.OrderBy(x => x.SeanceTime).ToList();
                        //Image filmImage = byteArrayToImage(film.FilmCover);
                        //memoryStream.Close();
                        //FilmSeance filmSeance = new FilmSeance(film.FilmName, film.FilmYear, film.MinAge.MinAgeValue.ToString(), film.FilmDescription, seances, filmImage);
                        FilmSeance filmSeance = new FilmSeance(film, seances);
                        tableLayoutPanelList.RowCount++;
                        tableLayoutPanelList.Controls.Add(filmSeance, 0, ind);
                        tableLayoutPanelList.RowStyles.Add(new RowStyle(SizeType.Absolute, 250F));
                        ind++;
                    }
                }
            }

            ////List<Seance> seances = Helper.DB.Seance.ToList();
            //FilmSeance abc = new FilmSeance("Абоб абоб", 1999, "325", "фывыфыфвыфвфывфывфывыфвыфвффыв", seances);
            //tableLayoutPanelList.RowCount++;
            //tableLayoutPanelList.Controls.Add(abc, 1, 0);
            //tableLayoutPanelList.RowStyles.Add(new RowStyle(SizeType.Absolute, 250F));
            //tableLayoutPanelList.RowCount++;
            //tableLayoutPanelList.Controls.Add(new FilmSeance("321", 5555, " 3123", "фывыфыфвыфвфывфывфывыфвыфвффыв", seances), 1, 1);
            //tableLayoutPanelList.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
        }

        private void Filter(object sender, EventArgs e)
        {
            //if (sender == dateTimePickerSeance)
            //{
            //    if (dateTimePickerSeance.Value < DateTime.Now.Date)
            //    {
            //        MessageBox.Show("Вы выбрали прошедшую дату", "Выбор даты", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        dateTimePickerSeance.Value = date;
            //        return;
            //    }
            //}
            ShowSeance();
        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            labelTitle.Text = DateTime.Now.ToLongTimeString();
        }

        private void buttonHist_Click(object sender, EventArgs e)
        {
            FormSeanceHistory formSeanceHistory = new FormSeanceHistory();
            formSeanceHistory.ShowDialog();
        }
    }
}
