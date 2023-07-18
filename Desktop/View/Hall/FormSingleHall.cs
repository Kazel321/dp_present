using Diplom.Classes;
using Diplom.Classes.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class FormSingleHall : Form
    {
        int hallid;
        Hall hall;

        public FormSingleHall()
        {
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
        }

        public FormSingleHall(int hallId)
        {
            this.hallid = hallId;
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
        }

        public void RollBack()
        {
            var changedEntries = Helper.DB.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        /// <summary>
        /// Вернутся
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReturn_Click(object sender, EventArgs e)
        {
            //RollBack();
            Close();
        }

        private void FormSingleHall_Load(object sender, EventArgs e)
        {
            Helper.DB = new DB(Helper.connection);
            if (Helper.formEvents == FormEvents.Редактирование)
            {
                hall = Helper.DB.Hall.Where(h => h.HallId == hallid).FirstOrDefault();
                textBoxName.Text = hall.HallName;
                update();
            }
            else
            {
                hall = new Hall();
                hall.HallId = Helper.id;
                textBoxName.Text = "Пусто";
                hall.HallName = "Пусто";
                Helper.DB.Hall.Add(hall);

                buttonDel.Enabled = false;
            }

            Text = Helper.formEvents + " зала";
            labelTitle.Text = Helper.formEvents + " зала";
        }

        void update()
        {
            dataGridViewPlaces.Rows.Clear();
            dataGridViewPlaces.Columns.Clear();
            if (hall.Place.Count() == 0)
                return;

            List<Place> places = hall.Place.ToList();
            var rows = places.GroupBy(x => x.PlaceRow).ToArray();
            int maxNumber = places.Max(x => x.PlaceNumber);

            dataGridViewPlaces.RowCount = rows.Length;
            dataGridViewPlaces.ColumnCount = maxNumber;
            dataGridViewPlaces.DefaultCellStyle.Tag = null;
            dataGridViewPlaces.DefaultCellStyle.BackColor = Color.DarkGray;
            dataGridViewPlaces.DefaultCellStyle.SelectionBackColor = Color.DarkGray;

            int maxPlaceRowNumber;
            int startPlace;
            int rowHeight = (dataGridViewPlaces.Size.Height - dataGridViewPlaces.ColumnHeadersHeight) / dataGridViewPlaces.RowCount;

            for (int i = 0; i < rows.Length; i++)
            {
                dataGridViewPlaces.Rows[i].HeaderCell.Value = "Ряд " + (i + 1).ToString();
                dataGridViewPlaces.RowHeadersWidth = 100;
                dataGridViewPlaces.Rows[i].Height = rowHeight;
                maxPlaceRowNumber = places.Where(x => x.PlaceRow == i + 1).Max(x => x.PlaceNumber);
                startPlace = (maxNumber - maxPlaceRowNumber) / 2;
                for (int j = 0; j < maxPlaceRowNumber; j++)
                {
                    Place place = places.Where(x => x.PlaceRow == (i + 1) && x.PlaceNumber == (j + 1)).FirstOrDefault();
                    dataGridViewPlaces[j + startPlace, i].Tag = place;
                    dataGridViewPlaces[j + startPlace, i].ToolTipText = "Ряд " + place.PlaceRow + ", Место " + place.PlaceNumber;

                    switch (place.PlaceTypeId)
                    {
                        case 1: 
                            dataGridViewPlaces[j + startPlace, i].Style.BackColor = Color.Green;
                            break;
                        case 2:
                            dataGridViewPlaces[j + startPlace, i].Style.BackColor = Color.Yellow;
                            break;
                        case 3:
                            dataGridViewPlaces[j + startPlace, i].Style.BackColor = Color.Red;
                            break;
                        default:
                            dataGridViewPlaces[j + startPlace, i].Style.BackColor = Color.Cyan;
                            break;
                    }

                    dataGridViewPlaces[j + startPlace, i].Style.SelectionBackColor = SystemColors.Highlight;
                }
            }

            for (int i = 0; i < dataGridViewPlaces.Columns.Count; i++)
            {
                dataGridViewPlaces.Columns[i].HeaderText = (i + 1).ToString();
            }

            dataGridViewPlaces.ClearSelection();
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string tag = btn.Tag.ToString();

            int rowId = -1;
            switch (tag)
            {
                case "edit":
                    rowId = (int)dataGridViewPlaces.CurrentRow.Index;
                    break;
            }
            Form nextForm = new FormHallPlaces(hall, rowId);
            nextForm.ShowDialog();
            update();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Не задано название зала", Helper.formEvents + " зала", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            hall.HallName = textBoxName.Text;

            if (Helper.formEvents == FormEvents.Редактирование)
            {
                try
                {
                    Helper.DB.SaveChanges();
                }
                catch
                {
                    var t = Helper.DB.Ticket.Where(x => x.Seance.HallId == hall.HallId).ToList();
                    List<Place> pl = hall.Place.ToList();
                    if (t.Any(x => x.Seance.HallId == hall.HallId))
                    {
                        MessageBox.Show("Места используются в билетах", Helper.formEvents + " зала", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Helper.DB.Place.RemoveRange(Helper.DB.Place.Where(x => x.HallId == hallid));
                    Helper.DB.SaveChanges();
                    hall.Place = pl;
                }
            }

            Helper.DB.SaveChanges();

            
            MessageBox.Show("Зал сохранен", Helper.formEvents + " зала", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        
        Place p;

        private void dataGridViewPlaces_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int row = dataGridViewPlaces.HitTest(e.X, e.Y).RowIndex;
                int col = dataGridViewPlaces.HitTest(e.X, e.Y).ColumnIndex;

                if (row < 0 || col < 0)
                {
                    return;
                }

                if (dataGridViewPlaces[col, row].Tag != null)
                {
                    p = (Place)dataGridViewPlaces[col, row].Tag;
                    ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                    List<PlaceType> placeTypes = Helper.DB.PlaceType.ToList();
                    placeTypes = placeTypes.OrderBy(x => x.PlaceTypeId).ToList();

                    for (int i = 0; i < placeTypes.Count; i++)
                    {
                        PlaceType item = placeTypes[i];
                        Image img;
                        switch (item.PlaceTypeId)
                        {
                            case 1:
                                img = Properties.Resources.stand_place_type;
                                break;
                            case 2:
                                img = Properties.Resources.vip_place_type;
                                break;
                            case 3:
                                img = Properties.Resources.dontWork_place_type;
                                break;
                            default:
                                img = Properties.Resources.picture;
                                break;
                        }
                        string s = item.PlaceTypeName;
                        EventHandler onClick = setPlaceType;
                        contextMenuStrip.Items.Add(s, img, onClick);
                        contextMenuStrip.Items[contextMenuStrip.Items.Count - 1].Tag = item.PlaceTypeId;
                    }
                    delRow = row;
                    contextMenuStrip.Items.Add("Удалить ряд", Properties.Resources.remove, removeRow);
                    contextMenuStrip.Show(dataGridViewPlaces, new Point(e.X, e.Y));
                }
            }
        }

        int delRow = -1;

        void removeRow(object sender, EventArgs e)
        {
            if (delRow >= 0)
            {
                int row = delRow;

                int count = dataGridViewPlaces.Rows.Count;
                List<Place> pl = hall.Place.Where(x => x.PlaceRow == row + 1).ToList();
                foreach (Place item in pl)
                {
                    hall.Place.Remove(item);
                }

                if (row != (count - 1))
                {
                    hall.Place.Where(x => x.PlaceRow > (row + 1)).ToList().ForEach(x => x.PlaceRow--);
                }
                update();
            }
        }

        private void buttonRemoveRow_Click(object sender, EventArgs e)
        {
            if (dataGridViewPlaces.CurrentRow.Index == null || dataGridViewPlaces.CurrentRow.Index < 0)
            {
                MessageBox.Show("Не выбран ряд", "Удаление ряда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int row = dataGridViewPlaces.CurrentRow.Index;

            int count = dataGridViewPlaces.Rows.Count;
            List<Place> pl = hall.Place.Where(x => x.PlaceRow == row + 1).ToList();
            foreach (Place item in pl)
            {
                hall.Place.Remove(item);
            }

            if (row != (count - 1))
            {
                hall.Place.Where(x => x.PlaceRow > (row + 1)).ToList().ForEach(x => x.PlaceRow--);
            }
            update();
        }

        private void setPlaceType(object sender, EventArgs e)
        {
            ToolStripItem itm = (ToolStripItem)sender;
            int placeTypeId = (int)itm.Tag;
            for (int i = 0; i < dataGridViewPlaces.SelectedCells.Count; i++)
            {
                Place p = (Place)dataGridViewPlaces.SelectedCells[i].Tag;
                if (p == null)
                    continue;
                hall.Place.Where(x => x == p).FirstOrDefault().PlaceTypeId = placeTypeId;
            }
            update();
            //if (p != null)
            //{
            //    hall.Place.Where(x => x == p).FirstOrDefault().PlaceTypeId = placeTypeId;
            //    update();
            //    //Helper.DB.Place.Where(x => x == placeId).FirstOrDefault().PlaceTypeId = placeTypeId;
            //}
        }

        private void dataGridViewPlaces_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonAddRow_Click(buttonEditRow, e);
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            int hallId = hall.HallId;
            if (Helper.DB.Seance.Where(x => x.HallId == hallId).Count() == 0 && Helper.DB.Ticket.Where(x => x.Place.HallId == hallId).Count() == 0)
            {
                Helper.DB.Place.RemoveRange(Helper.DB.Place.Where(x => x.HallId == hallId));
                Helper.DB.Hall.Remove(Helper.DB.Hall.Where(x => x.HallId == hallId).FirstOrDefault());
                Helper.DB.SaveChanges();
                MessageBox.Show("Зал удален", "Удаление зала", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Зал используется в сеансах или его места используются в билетах", "Удаление зала", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
