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
    public partial class FormSinglePlaceType : Form
    {
        PlaceType pt;
        int id;
        string title;
        public FormSinglePlaceType()
        {
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
        }

        public FormSinglePlaceType(int id)
        {
            this.id = id;
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

        private void FormSinglePlaceType_Load(object sender, EventArgs e)
        {
            title = Helper.formEvents + " типа места";
            Text = title;
            labelTitle.Text = title;
            if (Helper.formEvents == FormEvents.Добавление)
            {
                pt = new PlaceType();
                pt.PlaceTypeName = "Пусто";
                pt.PlaceTypeCost = 0;
                Helper.DB.PlaceType.Add(pt);
                buttonDelete.Enabled = false;
            }
            else
            {
                pt = Helper.DB.PlaceType.Where(x => x.PlaceTypeId == id).FirstOrDefault();
            }
            textBoxName.Text = pt.PlaceTypeName;
            textBoxCost.Text = pt.PlaceTypeCost.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string stringCost = textBoxCost.Text;
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(stringCost))
            {
                MessageBox.Show("Вы ввели не все даныне", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float cost;
            try
            {
                cost = float.Parse(stringCost);
            }
            catch
            {
                MessageBox.Show("Неправильный формат данных цены", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cost < 0)
            {
                MessageBox.Show("Цена не может быть меньше нуля", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pt.PlaceTypeName = name;
            pt.PlaceTypeCost = cost;
            Helper.DB.SaveChanges();
            MessageBox.Show("Данные сохранены", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (id < 4)
            {
                MessageBox.Show("Данный тип неоходим для корректной работы системы, вы не можете его удалить", "Удаление типа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Helper.DB.Place.Where(x => x.PlaceTypeId == id).Count() > 0)
            {
                MessageBox.Show("Данный тип используется в местах", "Удаление типа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Вы уверены", "Удаление типа", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            Helper.DB.PlaceType.Remove(pt);
            Helper.DB.SaveChanges();
            Close();
        }
    }
}
