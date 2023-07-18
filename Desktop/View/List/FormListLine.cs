using Diplom.Classes;
using Diplom.Classes.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.Entity.Migrations;

namespace Diplom
{
    public partial class FormListLine : Form
    {
        BindingList<Country> countries;
        BindingList<Genre> genres;
        BindingList<Role> roles;
        BindingList<MinAge> minAges;

        public FormListLine()
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

        private void FormListLine_Load(object sender, EventArgs e)
        {
            //Helper.DB.Configuration.AutoDetectChangesEnabled = false;

            countries = new BindingList<Country>(Helper.DB.Country.OrderBy(x => x.CountryName).ToList());
            genres = new BindingList<Genre>(Helper.DB.Genre.OrderBy(x => x.GenreName).ToList());
            roles = new BindingList<Role>(Helper.DB.Role.OrderBy(x => x.RoleName).ToList());
            minAges = new BindingList<MinAge>(Helper.DB.MinAge.OrderBy(x => x.MinAgeValue).ToList());

            listBoxList.SelectedIndexChanged += updateData;
            listBoxList.SelectedIndex = 0;
        }

        private void updateData(object sender, EventArgs e)
        {
            if (listBoxList.SelectedIndex >= 0)
            {
                int ind = listBoxList.SelectedIndex;
                switch (ind)
                {
                    case 0: 
                        listBoxData.DataSource = countries;
                        listBoxData.DisplayMember = "CountryName";
                        listBoxData.ValueMember = "CountryId";
                        break;
                    case 1:
                        listBoxData.DataSource = genres;
                        listBoxData.DisplayMember = "GenreName";
                        listBoxData.ValueMember = "GenreId";
                        break;
                    case 2:
                        listBoxData.DataSource = minAges;
                        listBoxData.DisplayMember = "MinAgeValue";
                        listBoxData.ValueMember = "MinAgeId";
                        break;
                    case 3:
                        listBoxData.DataSource = roles;
                        listBoxData.DisplayMember = "RoleName";
                        listBoxData.ValueMember = "RoleId";
                        break;
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string s = Interaction.InputBox("Введите значение справочника", "Добавление значения");
            if (String.IsNullOrEmpty(s))
            {
                MessageBox.Show("Вы ввели пустое значение", "Добавление мин. возраста", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dynamic n;
            Type type = listBoxData.DataSource.GetType();
            Type listType = typeof(List<>).MakeGenericType(new[] { type });
            IList lst = (IList)Activator.CreateInstance(listType);
            int ind = listBoxList.SelectedIndex;
            switch (ind)
            {
                case 0:
                    lst = countries;
                    n = new Country();
                    n.CountryId = countries.Max(x => x.CountryId) + 1;
                    n.CountryName = s;
                    Helper.DB.Country.Add(n);
                    //var c = Helper.DB.Set<Country>().ToList();
                    //n = Helper.DB.Country.Where(x => x == n).FirstOrDefault();
                    break;
                case 1:
                    lst = genres;
                    n = new Genre();
                    n.GenreId = genres.Max(x => x.GenreId) + 1;
                    n.GenreName = s;
                    Helper.DB.Genre.Add(n);
                    break;
                case 2:
                    lst = minAges;
                    n = new MinAge();
                    n.MinAgeId = minAges.Max(x => x.MinAgeId) + 1;
                    try
                    {
                        n.MinAgeValue = int.Parse(s);
                    }
                    catch
                    {
                        MessageBox.Show("Неверный формат данных", "Добавление мин. возраста", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Helper.DB.MinAge.Add(n);
                    break;
                case 3:
                    lst = roles;
                    n = new Role();
                    n.RoleId = roles.Max(x => x.RoleId) + 1;
                    n.RoleName = s;
                    Helper.DB.Role.Add(n);
                    break;
                default:
                    return;
            }
            lst.Add(n);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBoxData.SelectedIndex >= 0)
            {
                string s = Interaction.InputBox("Введите значение справочника", "Добавление значения");
                if (String.IsNullOrEmpty(s))
                {
                    MessageBox.Show("Вы ввели пустое значение", "Добавление значения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dynamic n;
                Type type = listBoxData.DataSource.GetType();
                Type listType = typeof(List<>).MakeGenericType(new[] { type });
                IList lst = (IList)Activator.CreateInstance(listType);
                int ind = listBoxList.SelectedIndex;
                int sel = listBoxData.SelectedIndex;
                switch (ind)
                {
                    case 0:
                        countries[sel].CountryName = s;
                        lst = countries;
                        n = lst[sel];
                        n.CountryName = s;
                        //Helper.DB.Country.Where(x => x.CountryId == (int)listBoxData.SelectedValue).FirstOrDefault().CountryName = s;
                        break;
                    case 1:
                        lst = genres;
                        n = lst[sel];
                        n.GenreName = s;
                        //Helper.DB.Genre.Where(x => x.GenreId == (int)listBoxData.SelectedValue).FirstOrDefault().GenreName = s;
                        break;
                    case 2:
                        lst = minAges;
                        n = lst[sel];
                        try
                        {
                            n.MinAgeValue = int.Parse(s);
                            //Helper.DB.MinAge.Where(x => x.MinAgeId == (int)listBoxData.SelectedValue).FirstOrDefault().MinAgeValue = int.Parse(s);
                        }
                        catch
                        {
                            MessageBox.Show("Неверный формат данных", "Добавление мин. возраста", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    case 3:
                        lst = roles;
                        n = lst[sel];
                        n.RoleName = s;
                        //Helper.DB.Role.Where(x => x.RoleId == (int)listBoxData.SelectedValue).FirstOrDefault().RoleName = s;
                        break;
                    default:
                        return;
                }
                lst[sel] = n;
                //updateData(sender, e);
            }
            else
            {
                MessageBox.Show("Вы не выбрали значение", "Изменение значения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxData.SelectedIndex >= 0)
            {
                int ind = listBoxList.SelectedIndex;
                int sel = listBoxData.SelectedIndex;
                int data = (int)listBoxData.SelectedValue;
                switch (ind)
                {
                    case 0:

                        if (Helper.DB.Film.Where(x => x.CountryId == data).Count() > 0)
                        {
                            MessageBox.Show("Значение участвует в основных таблицах", "Удаление значения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //Helper.DB.Country.Remove(Helper.DB.Country.Where(x => x.CountryId == (int)listBoxData.SelectedValue).FirstOrDefault());
                        //countries.Remove(countries.Where(x => x.CountryId == (int)listBoxData.SelectedValue).FirstOrDefault());
                        try
                        {
                            Helper.DB.Country.Remove(countries[sel]);
                        }
                        catch
                        {

                        }
                        countries.Remove(countries[sel]);
                        break;
                    case 1:
                        if (Helper.DB.Film.Where(x => x.Genre.Any(g => g.GenreId == data)).Count() > 0)
                        {
                            MessageBox.Show("Значение участвует в основных таблицах", "Удаление значения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //Helper.DB.Genre.Remove(Helper.DB.Genre.Where(x => x.GenreId == (int)listBoxData.SelectedValue).FirstOrDefault());
                        try
                        {
                            Helper.DB.Genre.Remove(genres[sel]);
                        }
                        catch
                        {

                        }
                        genres.Remove(genres.Where(x => x.GenreId == (int)listBoxData.SelectedValue).FirstOrDefault());
                        break;
                    case 2:
                        if (Helper.DB.Film.Where(x => x.MinAgeId == data).Count() > 0)
                        {
                            MessageBox.Show("Значение участвует в основных таблицах", "Удаление значения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //Helper.DB.MinAge.Remove(Helper.DB.MinAge.Where(x => x.MinAgeId == (int)listBoxData.SelectedValue).FirstOrDefault());
                        try
                        {
                            Helper.DB.MinAge.Remove(minAges[sel]);
                        }
                        catch
                        {

                        }
                        minAges.Remove(minAges.Where(x => x.MinAgeId == (int)listBoxData.SelectedValue).FirstOrDefault());
                        break;
                    case 3:
                        if (Helper.DB.User.Where(x => x.RoleId == data).Count() > 0)
                        {
                            MessageBox.Show("Значение участвует в основных таблицах", "Удаление значения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //a\Helper.DB.Role.Remove(Helper.DB.Role.Where(x => x.RoleId == (int)listBoxData.SelectedValue).FirstOrDefault());
                        try
                        {
                            Helper.DB.Role.Remove(roles[sel]);
                        }
                        catch
                        {

                        }
                        roles.Remove(roles.Where(x => x.RoleId == (int)listBoxData.SelectedValue).FirstOrDefault());
                        break;
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали значение", "Удаление значения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (listBoxList.SelectedIndex != 0)
            {
                listBoxList.SelectedIndex--;
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (listBoxList.SelectedIndex != (listBoxList.Items.Count - 1))
            {
                listBoxList.SelectedIndex++;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Helper.DB.SaveChanges();
            MessageBox.Show("Данные сохранены", "Управление справочниками", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBoxData_DoubleClick(object sender, EventArgs e)
        {
            buttonEdit_Click(buttonEdit, e);
        }
    }
}
