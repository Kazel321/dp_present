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
    public partial class FormListPlaceType : Form
    {
        public FormListPlaceType()
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
        private void FormListPlaceType_Load(object sender, EventArgs e)
        {
            update();
        }

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        private void update()
        {
            dataGridView.Rows.Clear();
            var tp = Helper.DB.PlaceType.ToList();

            if (!String.IsNullOrEmpty(textBoxSearch.Text)) tp = tp.Where(x => x.PlaceTypeName.Contains(textBoxSearch.Text)).ToList();

            tp = tp.OrderBy(x => x.PlaceTypeId).ToList();

            int ind = -1;

            foreach (var item in tp)
            {
                ind = dataGridView.Rows.Add();
                dataGridView[0, ind].Value = item.PlaceTypeId;
                dataGridView[1, ind].Value = item.PlaceTypeName;
                dataGridView[2, ind].Value = item.PlaceTypeCost;
            }

            labelCount.Text = "Всего: " + (ind + 1) + " из " + Helper.DB.PlaceType.Count();
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
                form = new FormSinglePlaceType();
                Helper.formEvents = FormEvents.Добавление;
            }
            else
            {
                if (dataGridView[0, dataGridView.CurrentRow.Index].Value == null || (int)dataGridView[0, dataGridView.CurrentRow.Index].Value == 0)
                {
                    MessageBox.Show("Вы не выбрали тип", "Изменение типа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                form = new FormSinglePlaceType((int)dataGridView[0, dataGridView.CurrentRow.Index].Value);
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
            if (dataGridView[0, dataGridView.CurrentRow.Index].Value == null || (int)dataGridView[0, dataGridView.CurrentRow.Index].Value == 0)
            {
                MessageBox.Show("Вы не выбрали тип", "Удаление типа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int id = (int)dataGridView[0, dataGridView.CurrentRow.Index].Value;
            if (id < 4)
            {
                MessageBox.Show("Данный тип необходим для корректной работы системы, вы не можете его удалить", "Удаление типа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Helper.DB.Place.Where(x => x.PlaceTypeId == id).Count() > 0)
            {
                MessageBox.Show("Данный тип используется в местах", "Удаление типа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PlaceType pt = Helper.DB.PlaceType.Where(x => x.PlaceTypeId == id).FirstOrDefault();
            Helper.DB.PlaceType.Remove(pt);
            Helper.DB.SaveChanges();
            update();
        }

        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            buttonAdd_Click(buttonEdit, e);
        }
    }
}
