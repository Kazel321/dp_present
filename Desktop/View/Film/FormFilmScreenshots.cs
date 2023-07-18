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
    public partial class FormFilmScreenshots : Form
    {
        public BindingList<Screenshot> screens;
        public bool isSaved;

        OpenFileDialog openFileDialog = new OpenFileDialog();

        public FormFilmScreenshots()
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

        private void FormFilmScreenshots_Load(object sender, EventArgs e)
        {
            openFileDialog.Filter = Helper.Filter;

            listBoxSource.DataSource = screens;
            listBoxSource.DisplayMember = "ScreenshotId";
            listBoxSource.ValueMember = "ScreenshotId";

            listBoxSource.SelectedValueChanged += updateImage;
            updateImage(sender, e);
        }

        void updateImage(object sender, EventArgs e)
        {
            if (listBoxSource.SelectedIndex >= 0)
            {
                Screenshot s = screens.Where(x => x.ScreenshotId == (int)listBoxSource.SelectedValue).FirstOrDefault();
                pictureBoxImage.Image = Image.FromStream(new MemoryStream(s.ScreenshotImage));
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (listBoxSource.SelectedIndex != 0)
            {
                listBoxSource.SelectedIndex--;
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (listBoxSource.SelectedIndex != (listBoxSource.Items.Count - 1))
            {
                listBoxSource.SelectedIndex++;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Screenshot s = new Screenshot();
                s.ScreenshotId = screens.Count + 1;

                Image img = Image.FromFile(openFileDialog.FileName);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                s.ScreenshotImage= ms.ToArray();
                screens.Add(s);
                listBoxSource.SelectedValue = s.ScreenshotId;
                updateImage(sender, e);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxSource.SelectedIndex >= 0)
            {
                Screenshot s = screens.Where(x => x.ScreenshotId == (int)listBoxSource.SelectedValue).FirstOrDefault();
                screens.Remove(s);
                if (screens.Count != 0)
                    listBoxSource.SelectedIndex = 0;
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            if (listBoxSource.SelectedIndex >= 0)
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Screenshot s = screens.Where(x => x.ScreenshotId == (int)listBoxSource.SelectedValue).FirstOrDefault();
                    Image img = Image.FromFile(openFileDialog.FileName);
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, ImageFormat.Jpeg);
                    s.ScreenshotImage= ms.ToArray();
                    updateImage(sender, e);
                }
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
