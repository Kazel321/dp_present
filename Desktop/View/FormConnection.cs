using Diplom.Classes;
using Diplom.Classes.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class FormConnection : Form
    {
        public FormConnection()
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

        private void FormConnection_Load(object sender, EventArgs e)
        {
            Helper.ip = ConfigurationManager.ConnectionStrings["DB"].ConnectionString.Split(';')[0].Split('=')[1];
            textBoxIP.Text = Helper.ip;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxIP.Text))
            {
                MessageBox.Show("Вы не ввели данные", "Сохранение подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ip = textBoxIP.Text;

            // Получение конфигурации приложения
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Получение секции "connectionStrings"
            ConnectionStringsSection connectionStrings = (ConnectionStringsSection)config.GetSection("connectionStrings");

            // Получение строки подключения с именем "MyConnectionString"
            ConnectionStringSettings connectionString = connectionStrings.ConnectionStrings["DB"];

            // Изменение значения параметра "connectionString"
            connectionString.ConnectionString = "Host=" + ip + ";Database=Cinema;Username=postgres;Password=root;Persist Security Info=True";

            // Сохранение изменений
            config.Save(ConfigurationSaveMode.Modified);

            // Обновление секции "connectionStrings" в памяти
            ConfigurationManager.RefreshSection("connectionStrings");
            Helper.connection = connectionString.ConnectionString;
            Helper.DB = new DB(Helper.connection);
            Close();
        }
    }
}
