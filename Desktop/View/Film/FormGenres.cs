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
    public partial class FormGenres : Form
    {
        public BindingList<Genre> genres;
        public BindingList<Genre> sourceGenres;
        public bool isSaved;

        public FormGenres()
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

        private void FormGenres_Load(object sender, EventArgs e)
        {
            var genreIds = genres.Select(p => p.GenreId).ToArray();
            sourceGenres = new BindingList<Genre>(Helper.DB.Genre.Where(x => !genreIds.Contains(x.GenreId)).OrderBy(x => x.GenreName).ToList());

            listBoxSource.DataSource = sourceGenres;
            listBoxSource.ValueMember = "GenreId";
            listBoxSource.DisplayMember = "GenreName";

            listBoxMain.DataSource = genres;
            listBoxMain.ValueMember = "GenreId";
            listBoxMain.DisplayMember = "GenreName";
        }

        void updateList()
        {
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (listBoxSource.SelectedIndex >= 0)
            {
                Genre g = sourceGenres.Where(x => x.GenreId == (int)listBoxSource.SelectedValue).FirstOrDefault();
                genres.Add(g);
                sourceGenres.Remove(g);
            }
            else
            {
                MessageBox.Show("Не выбран жанр", "Жанры", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRem_Click(object sender, EventArgs e)
        {
            if (listBoxMain.SelectedIndex >= 0)
            {
                Genre g = genres.Where(x => x.GenreId == (int)listBoxMain.SelectedValue).FirstOrDefault();
                sourceGenres.Add(g);
                genres.Remove(g);
            }
            else
            {
                MessageBox.Show("Не выбран жанр", "Жанры", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            isSaved = true;
            Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            isSaved = false;
            Close();
        }
    }
}
