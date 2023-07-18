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
    public partial class FormSingleUser : Form
    {
        User user;

        public FormSingleUser()
        {
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
        }

        public FormSingleUser(int userId)
        {
            Helper.DB = new DB(Helper.connection);
            user = Helper.DB.User.Where(x => x.UserId == userId).FirstOrDefault();
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

        private void FormSingleUser_Load(object sender, EventArgs e)
        {
            comboBoxRole.DataSource = Helper.DB.Role.ToList();
            comboBoxRole.DisplayMember = "RoleName";
            comboBoxRole.ValueMember = "RoleId";
            comboBoxRole.SelectedIndex = 0;

            if (Helper.formEvents == FormEvents.Редактирование)
            {
                Text = "Пользователь (Редактирование)";
                labelTitle.Text = user.UserLastName + " " + user.UserFirstName + " " + user.UserPatronymic;

                textBoxLastName.Text = user.UserLastName;
                textBoxFirstName.Text = user.UserFirstName;
                textBoxPatronymic.Text = user.UserPatronymic;
                textBoxLogin.Text = user.UserLogin;
                textBoxPass.Text = user.UserPassword;
                textBoxEmail.Text = user.UserEmail;

                maskedTextBoxBirthDate.Text = user.UserBirthDate.ToShortDateString();
                maskedTextBoxPhone.Text = user.UserPhoneNumber;

                comboBoxRole.SelectedValue = user.RoleId;
                checkBoxActive.Checked = user.UserActive;
                if (user.UserActive) checkBoxActive.Text = "Активен";
                else checkBoxActive.Text = "Заблокирован";
            }
            else
            {
                Text = "Пользователь (Добавление)";
                labelTitle.Text = "Добавление пользователя";
                buttonDelete.Enabled = false;
                checkBoxActive.Checked = true;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxLastName.Text) || String.IsNullOrEmpty(textBoxFirstName.Text) || String.IsNullOrEmpty(textBoxPatronymic.Text) || String.IsNullOrEmpty(textBoxLogin.Text) || String.IsNullOrEmpty(textBoxPass.Text) || String.IsNullOrEmpty(textBoxEmail.Text) || String.IsNullOrEmpty(maskedTextBoxPhone.Text))
            {
                MessageBox.Show("Вы заполнили не все данные!", "Сохранение пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string lastName = textBoxLastName.Text;
            string firstName = textBoxFirstName.Text;
            string patronymic = textBoxPatronymic.Text;

            //login
            string login = textBoxLogin.Text;
            if (Helper.formEvents != FormEvents.Редактирование)
            if (Helper.DB.User.Where(x => x.UserLogin == login).FirstOrDefault() != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует", Helper.formEvents + " пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //pass
            string pass = textBoxPass.Text;
            if (pass.Length < 8)
            {
                MessageBox.Show("В пароле должно быть минимум 8 символов", Helper.formEvents + " пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!pass.Any(x => char.IsUpper(x)))
            {
                MessageBox.Show("В пароле должна быть как минимум одна большая буква", Helper.formEvents + " пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!pass.Any(x => char.IsDigit(x)))
            {
                MessageBox.Show("В пароле должна быть как минимум одна цифра", Helper.formEvents + " пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }    

            //email
            string email = textBoxEmail.Text;
            if (Helper.formEvents != FormEvents.Редактирование)
            if (Helper.DB.User.Where(x => x.UserEmail == email).FirstOrDefault() != null)
            {
                MessageBox.Show("Пользователь с таким E-mail уже существует", Helper.formEvents + " пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!email.Any(x => x == '@'))
            {
                MessageBox.Show("У E-mail должна быть \"@\"", Helper.formEvents + " пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!email.Split('@')[1].Any(x => x == '.'))
            {
                MessageBox.Show("Неправльный формат E-mail после \"@\"", Helper.formEvents + " пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime birthDate;
            try
            {
                int day = Convert.ToInt32(maskedTextBoxBirthDate.Text.Split('.')[0]);
                int month = Convert.ToInt32(maskedTextBoxBirthDate.Text.Split('.')[1]);
                int year = Convert.ToInt32(maskedTextBoxBirthDate.Text.Split('.')[2]);
                birthDate = new DateTime(year, month, day);
            }
            catch
            {
                MessageBox.Show("Неправильный формат даты", Helper.formEvents + " пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string phone = maskedTextBoxPhone.Text.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");

            int roleId = (int)comboBoxRole.SelectedValue;
            bool active = checkBoxActive.Checked;

            User newUser;
            if (Helper.formEvents == FormEvents.Добавление)
            {
                newUser = new User();
                newUser.UserId = Helper.DB.User.Count() + 1;
            }
            else
            {
                newUser = user;
            }
            newUser.Role = Helper.DB.Role.Where(x => x.RoleId == roleId).FirstOrDefault();
            newUser.UserLogin = login;
            newUser.UserEmail = email;
            newUser.UserPassword = pass;
            newUser.UserLastName = lastName;
            newUser.UserFirstName = firstName;
            newUser.UserPatronymic = patronymic;
            newUser.UserPhoneNumber = phone;
            newUser.UserBirthDate = birthDate;
            newUser.UserActive = active;

            if (Helper.formEvents == FormEvents.Добавление) Helper.DB.User.Add(newUser);

            Helper.DB.SaveChanges();

            MessageBox.Show("Данные успешно сохранены", Helper.formEvents + " пользователя", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void checkBoxActive_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxActive.Checked) checkBoxActive.Text = "Активен";
            else checkBoxActive.Text = "Заблокирован";
        }

        private void maskedTextBoxBirthDate_Enter(object sender, EventArgs e)
        {
            TextBoxBase textBox = (TextBoxBase)sender;
            string tag = textBox.Tag.ToString();

            ToolTip toolTip = new ToolTip();
            toolTip.IsBalloon = true;
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.ToolTipTitle = "Подсказка";
            toolTip.UseAnimation = true;
            toolTip.UseFading = true;
            string textTip = "";

            switch (tag)
            {
                case "BirthDate":
                    textTip = "Дата рождения в формате ДД.ММ.ГГГГ";
                    break;
                case "Phone":
                    textTip = "Номер телефона в стандартном формате";
                    break;
                case "Login":
                    textTip = "Уникальный логин";
                    break;
                case "Pass":
                    textTip = "Пароль не менее 8 символов и должен содержать как минимум одну заглавную букву и одну цифру";
                    break;
                case "Email":
                    textTip = "Уникальный E-mail в формате *@*.*";
                    break;
            }

            toolTip.Show(textTip, textBox, 0, -75, 3000);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int userId = user.UserId;

            if (Helper.DB.Login.Where(x => x.UserId == userId).FirstOrDefault() != null)
            {
                if (MessageBox.Show("У данного пользователя присутствуют записи в истории входа, удалить его вместе с ними?", "Удаление пользователя", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            Helper.DB.Login.RemoveRange(Helper.DB.Login.Where(x => x.UserId == userId));
            Helper.DB.User.Remove(Helper.DB.User.Where(x => x.UserId == userId).FirstOrDefault());
            Helper.DB.SaveChanges();
            MessageBox.Show("Пользователь удален", "Удаление пользователя", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
