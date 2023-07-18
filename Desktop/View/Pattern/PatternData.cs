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
    public partial class PatternData : Form
    {
        public PatternData()
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
        private void PatternData_Load(object sender, EventArgs e)
        {
            update();
        }

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        private void update()
        {

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
                form = null;
                Helper.formEvents = FormEvents.Добавление;
            }
            else
            {
                form = null;
                Helper.formEvents = FormEvents.Редактирование;
            }

            Hide();
            form.ShowDialog();
            Show();
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
    }
}
