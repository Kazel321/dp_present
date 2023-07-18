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
    public partial class FormLoginHistory : Form
    {
        public FormLoginHistory()
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
        private void FormLoginHistory_Load(object sender, EventArgs e)
        {
            update();
        }

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        private void update()
        {
            dataGridView.Rows.Clear();

            var data = Helper.DB.Login.OrderBy(x => x.LoginDateTime).ToList();
            if (!String.IsNullOrEmpty(textBoxSearch.Text))
            {
                data = data.Where(x => x.User.UserLastName.Contains(textBoxSearch.Text)).ToList();
            }

            data = data.OrderBy(x => x.UserId).ToList();

            int ind = -1;
            foreach (var item in data)
            {
                ind = dataGridView.Rows.Add();
                dataGridView[0, ind].Value = item.LoginId;
                dataGridView[1, ind].Value = item.UserId + ": " + item.User.UserLastName + " " + item.User.UserFirstName + " " + item.User.UserPatronymic;
                dataGridView[2, ind].Value = item.LoginDateTime.ToShortDateString();
                dataGridView[3, ind].Value = item.LoginDateTime.ToLongTimeString();
            }

            labelCount.Text = "Количество: " + (ind + 1) + " из " + Helper.DB.Login.Count();
        }


        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Helper.DB.SaveChanges();
        }

        /// <summary>
        /// Изменение строки поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            update();
        }

        /// <summary>
        /// Очистить историю входа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            Helper.DB.Login.RemoveRange(Helper.DB.Login);
            Helper.DB.SaveChanges();
            update();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView[0, dataGridView.CurrentRow.Index].Value == null)
            {
                MessageBox.Show("Вы не выбрали запись", "Удаление записи входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int id = (int)dataGridView[0, dataGridView.CurrentRow.Index].Value;
            Helper.DB.Login.Remove(Helper.DB.Login.Where(x => x.LoginId == id).FirstOrDefault());
            Helper.DB.SaveChanges();
            update();
        }
    }
}
