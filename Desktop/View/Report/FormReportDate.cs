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
    public partial class FormReportDate : Form
    {
        public FormReportDate()
        {
            InitializeComponent(); 
            Helper.DB = new DB(Helper.connection);
            maskedTextBoxDateTo.Text = DateTime.Now.ToString();
            maskedTextBoxDateFrom.Text = DateTime.Now.AddMonths(-1).ToString();
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

        public DateTime minDate;
        public DateTime maxDate;

        public bool isSuccess = false;

        private void buttonNext_Click(object sender, EventArgs e)
        {
            try
            {
                minDate = DateTime.Parse(maskedTextBoxDateFrom.Text);
                maxDate = DateTime.Parse(maskedTextBoxDateTo.Text);
            }
            catch
            {
                MessageBox.Show("Неверный формат даты", "Выбор даты", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (maxDate < minDate)
            {
                MessageBox.Show("Вторая дата не может быть меньше первой", "Выбор даты", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            isSuccess = true;
            Close();
        }
    }
}
