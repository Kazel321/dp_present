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
    public partial class FormHallPlaces : Form
    {
        Hall hall;
        int rowId;

        public FormHallPlaces()
        {
            InitializeComponent();
        }

        public FormHallPlaces(Hall hall, int rowId)
        {
            this.hall = hall;
            this.rowId = rowId;
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

        private void FormHallPlaces_Load(object sender, EventArgs e)
        {
            BindingList<int> rows = new BindingList<int>(hall.Place.Select(x => x.PlaceRow).Distinct().ToList());
            comboBoxRow.DataSource = rows;

            if (rowId != -1)
            {
                comboBoxRow.SelectedItem = rowId + 1;
            }
            else
            {
                rows.Insert(rows.Count, rows.Count + 1);
                comboBoxRow.SelectedItem = rows.Count;
                comboBoxRow.Enabled = false;
                buttonRemoveRow.Enabled = false;
            }

            updateValue(sender, e);
            comboBoxRow.SelectedIndexChanged += updateValue;

            textBoxCount.Select();
        }

        void updateValue(object sender, EventArgs e)
        {
            int count = hall.Place.Where(x => x.PlaceRow == (int)comboBoxRow.SelectedItem).Count();
            textBoxCount.Text = count.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Вы не заполнили поле количества мест", "Сохранение ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int count;
            try
            {
                count = int.Parse(textBoxCount.Text);
            }
            catch
            {
                MessageBox.Show("Неправильный формат данных", "Сохранение ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (count <= 0 || count > 100)
            {
                MessageBox.Show("Задано неверное количество мест: менее 1 или более 100", "Сохранение ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rowId < 0)
            {
                for (int i = 1; i <= count; i++)
                {
                    Place p = new Place();
                    p.Hall = hall;
                    p.HallId = hall.HallId;
                    p.PlaceRow = (int)comboBoxRow.SelectedItem;
                    //p.PlaceType = Helper.DB.PlaceType.Where(x => x.PlaceTypeId == 1).FirstOrDefault();
                    p.PlaceTypeId = 1;
                    p.PlaceNumber = i;
                    hall.Place.Add(p);
                }
            }
            else
            {
                rowId = (int)comboBoxRow.SelectedItem;
                List<Place> pl = hall.Place.Where(x => x.PlaceRow == rowId).ToList();
                //foreach (Place item in pl)
                //{
                //    hall.Place.Remove(item);
                //}

                int c = pl.Count;
                int dif = Math.Abs(c - count);

                if (c < count)
                {
                    for (int i = 1; i <= dif; i++)
                    {
                        Place p = new Place();
                        p.Hall = hall;
                        p.HallId = hall.HallId;
                        p.PlaceRow = (int)comboBoxRow.SelectedItem;
                        p.PlaceType = Helper.DB.PlaceType.Where(x => x.PlaceTypeId == 1).FirstOrDefault();
                        p.PlaceTypeId = 1;
                        p.PlaceNumber = i + c;
                        hall.Place.Add(p);
                    }
                }
                else if (c > count)
                {
                    for (int i = 0; i < dif; i++)
                    {
                        int max = hall.Place.Where(x => x.PlaceRow == (int)comboBoxRow.SelectedItem).Max(x => x.PlaceNumber);
                        Place p = hall.Place.Where(x => x.PlaceNumber == max && x.PlaceRow == (int)comboBoxRow.SelectedItem).FirstOrDefault();
                        hall.Place.Remove(p);
                    }
                }

                //for (int i = 1; i <= count; i++)
                //{
                //    Place p = new Place();
                //    p.Hall = hall;
                //    p.HallId = hall.HallId;
                //    p.PlaceRow = (int)comboBoxRow.SelectedItem;
                //    p.PlaceType = Helper.DB.PlaceType.Where(x => x.PlaceTypeId == 1).FirstOrDefault();
                //    p.PlaceTypeId = 1;
                //    p.PlaceNumber = i;
                //    hall.Place.Add(p);
                //}
            }

            Close();
        }

        private void buttonRemoveRow_Click(object sender, EventArgs e)
        {
            int row = (int)comboBoxRow.SelectedItem;
            if (row < 0)
            {
                MessageBox.Show("Не выбран ряд", "Удаление ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int count = hall.Place.Where(x => x.PlaceRow == (int)comboBoxRow.SelectedItem).Count();
            List<Place> pl = hall.Place.Where(x => x.PlaceRow == row + 1).ToList();
            foreach (Place item in pl)
            {
                hall.Place.Remove(item);
            }

            if (row != (count - 1))
            {
                hall.Place.Where(x => x.PlaceRow > (row + 1)).ToList().ForEach(x => x.PlaceRow--);
            }

            MessageBox.Show("Ряд удален", "Удаление ряда", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void textBoxCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Space)
                buttonSave_Click(buttonSave, e);
        }
    }
}
