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
    public partial class FormScreenshot : Form
    {
        List<Image> screenshots = new List<Image>();
        int selectedInd;

        public FormScreenshot()
        {
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
        }

        public FormScreenshot(int filmId)
        {
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
            labelTitle.Text = "Скриншоты из " + "\"" + Helper.DB.Film.Where(x => x.FilmId == filmId).FirstOrDefault().FilmName + "\""; 
            var binaryScreenshots = Helper.DB.Screenshot.Where(x => x.FilmId == filmId).ToList();
            screenshots.Clear();
            foreach (var item in binaryScreenshots)
            {
                Image image = Image.FromStream(new MemoryStream(item.ScreenshotImage));
                screenshots.Add(image);
            }
            pictureBoxScreenshot.Image = screenshots[0];
            selectedInd = 0;
            labelScreenshot.Text = "Скриншот 1";
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
        /// Следующий скриншот
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (selectedInd == screenshots.Count - 1)
            {
                MessageBox.Show("Это последний скриншот", "Следующий скриншот", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            selectedInd++;
            pictureBoxScreenshot.Image = screenshots[selectedInd];
            labelScreenshot.Text = "Скриншот " + (selectedInd + 1);
        }

        /// <summary>
        /// Предыдущий скриншот
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (selectedInd == 0)
            {
                MessageBox.Show("Это первый скриншот", "Предыдущий скриншот", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            selectedInd--;
            pictureBoxScreenshot.Image = screenshots[selectedInd];
            labelScreenshot.Text = "Скриншот " + (selectedInd + 1);
        }

    }
}
