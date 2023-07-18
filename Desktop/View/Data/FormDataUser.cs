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
    public partial class FormDataUser : Form
    {
        public FormDataUser()
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
        private void FormDataUser_Load(object sender, EventArgs e)
        {
            update();
        }

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        private void update()
        {
            dataGridView.Rows.Clear();

            var data = Helper.DB.User.ToList();
            if (!String.IsNullOrEmpty(textBoxSearch.Text))
            {
                data = data.Where(x => x.UserLastName.Contains(textBoxSearch.Text)).ToList();
            }

            data = data.OrderBy(x => x.UserId).ToList();

            int ind = -1;
            foreach (var item in data)
            {
                ind = dataGridView.Rows.Add();
                dataGridView[0, ind].Value = item.UserId;
                dataGridView[1, ind].Value = item.UserLogin;
                dataGridView[2, ind].Value = item.UserLastName + " " + item.UserFirstName + " " + item.UserPatronymic;
                dataGridView[3, ind].Value = item.UserEmail;
                dataGridView[4, ind].Value = item.UserPhoneNumber;
                dataGridView[5, ind].Value = item.UserActive;
            }

            labelCount.Text = "Количество: " + (ind + 1) + " из " + Helper.DB.User.Count();
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
                form = new FormSingleUser();
                Helper.formEvents = FormEvents.Добавление;
            }
            else
            {
                form = new FormSingleUser((int)dataGridView[0, dataGridView.CurrentRow.Index].Value);
                Helper.formEvents = FormEvents.Редактирование;
            }

            Hide();
            form.ShowDialog();
            update();
            Show();
        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Вы не выбрали пользователя", "Изменение статуса пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userId = (int)(dataGridView[0, dataGridView.CurrentRow.Index].Value);
            if (userId == Helper.User.UserId)
            {
                if (MessageBox.Show("Вы меняете статус своей учетной записи. Вы уверены?", "Изменение статуса пользователя", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    return;
            }
            User user = Helper.DB.User.Where(x => x.UserId == userId).FirstOrDefault();
            user.UserActive = !user.UserActive;
            Helper.DB.SaveChanges();
            MessageBox.Show("Статус пользователя успешно изменен", "Изменение статуса пользователя", MessageBoxButtons.OK, MessageBoxIcon.Information);
            update();
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

        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow != null)
            {
                buttonAdd_Click(buttonEdit, e);
            }
        }
    }
}
