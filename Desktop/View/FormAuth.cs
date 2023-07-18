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
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using ZXing;

namespace Diplom.View
{
    public partial class FormAuth : Form
    {
        public FormAuth()
        {
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
        }

        /// <summary>
        /// Изменение флажка отображения пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked == true)
                textBoxPassword.PasswordChar = '\0';

            else
                textBoxPassword.PasswordChar = '*';
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string passwd = textBoxPassword.Text;
            User user = null;
            try
            {
                user = Helper.DB.User.Where(x => (x.UserLogin == login && x.UserPassword == passwd) || (x.UserEmail == login && x.UserPassword == passwd)).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при соединении с базой данных", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (user != null)
            {
                if (user.UserActive)
                {
                    Helper.User = user;
                    Login newLogin = new Login();
                    newLogin.LoginId = Helper.DB.Login.Count() + 1;
                    newLogin.User = user;
                    newLogin.LoginDateTime = DateTime.Now;
                    Helper.DB.Login.Add(newLogin);
                    Helper.DB.SaveChanges();
                    Hide();
                    MessageBox.Show("Вы вошли с ролью: " + user.Role.RoleName, "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form form = null;
                    switch (user.RoleId)
                    {
                        case 1: 
                            form = new FormSeance();
                            break;
                        case 2: 
                            form = new FormManagerMenu();
                            break;
                        case 3: 
                            form = new FormAdminMenu();
                            break;
                    }
                    form.ShowDialog();
                    Helper.User = null;
                    Show();
                }
                else
                {
                    MessageBox.Show("Вы заблокированы в системе", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Вернуться
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Нажатие клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormAuth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                buttonEnter_Click(null, null);
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.FlatStyle = FlatStyle.Popup;
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.FlatStyle = FlatStyle.Flat;
        }


        private void pictureBoxIP_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FormConnection f = new FormConnection();
            Hide();
            f.ShowDialog();
            Show();
        }

        private void textBoxLogin_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.SelectAll();
        }

        bool isSelected = false;

        private void textBoxLogin_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!isSelected)
            {
                tb.SelectAll();
                isSelected = true;
            }
        }

        private void textBoxLogin_Leave(object sender, EventArgs e)
        {
            isSelected = false;
        }

    }
}
