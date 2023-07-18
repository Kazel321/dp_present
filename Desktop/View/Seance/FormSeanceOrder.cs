using Diplom.Classes;
using Diplom.Classes.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using ZXing;
using Word = Microsoft.Office.Interop.Word;

namespace Diplom
{
    public partial class FormOrderSeance : Form
    {
        Seance seance;
        List<Ticket> tickets = new List<Ticket>();
        Random rand = new Random();
        DB DB = new DB(Helper.connection);
        List<Place> places;
        List<Place> busyPlaces;
        double sumTickets;

        public FormOrderSeance()
        {
            InitializeComponent();
            Helper.DB = new DB(Helper.connection);
        }

        public FormOrderSeance(int seanceId)
        {
            seance = DB.Seance.Where(x => x.SeanceId == seanceId).FirstOrDefault();
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
        private void FormOrderSeance_Load(object sender, EventArgs e)
        {
            Film film = seance.Film;
            this.Text = film.FilmName;
            labelTitle.Text = film.FilmName + ", " + seance.HallId + "-й зал, " + seance.SeanceDate.ToLongDateString() + ", " + seance.SeanceTime;

            places = DB.Place.Where(x => x.HallId == seance.HallId).ToList();
            busyPlaces = DB.Ticket.Where(x => x.SeanceId == seance.SeanceId).Select(x => x.Place).ToList();
            var rows = places.GroupBy(x => x.PlaceRow).ToArray();
            int maxNumber = places.Max(x => x.PlaceNumber);

            dataGridViewPlaces.RowCount = rows.Length;
            dataGridViewPlaces.ColumnCount = maxNumber;
            dataGridViewPlaces.DefaultCellStyle.Tag = null;
            dataGridViewPlaces.DefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewPlaces.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            dataGridViewPlaces.CellBorderStyle = DataGridViewCellBorderStyle.None;

            int maxPlaceRowNumber;
            int startPlace;
            int rowHeight = dataGridViewPlaces.Size.Height / dataGridViewPlaces.RowCount;

            for (int i = 0; i < rows.Length; i++)
            {
                dataGridViewPlaces.Rows[i].HeaderCell.Value = "Ряд " + (i + 1).ToString();
                dataGridViewPlaces.RowHeadersWidth = 75;
                dataGridViewPlaces.Rows[i].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                dataGridViewPlaces.Rows[i].Height = rowHeight;
                maxPlaceRowNumber = places.Where(x => x.PlaceRow == i + 1).Max(x => x.PlaceNumber);
                startPlace = (maxNumber - maxPlaceRowNumber) / 2;
                for (int j = 0; j < maxPlaceRowNumber; j++)
                {
                    Place place = places.Where(x => x.PlaceRow == (i + 1) && x.PlaceNumber == (j + 1)).FirstOrDefault();
                    if (place.PlaceTypeId != 3)
                    {
                        dataGridViewPlaces[j + startPlace, i].Tag = place.PlaceId;
                        dataGridViewPlaces[j + startPlace, i].ToolTipText = "Ряд " + place.PlaceRow + ", Место " + place.PlaceNumber + "\nЦена: " + (seance.SeanceCost + place.PlaceType.PlaceTypeCost);
                    }
                }
            }


            foreach (HScrollBar vBar in dataGridViewPlaces.Controls.OfType<HScrollBar>())
            {
                vBar.Enabled = false;
            }

            foreach (VScrollBar hBar in dataGridViewPlaces.Controls.OfType<VScrollBar>())
            {
                hBar.Enabled = false;
            }

            dataGridViewPlaces.CellPainting += dataGridViewPlaces_CellPainting;
            dataGridViewPlaces.SelectionChanged += dataGridViewPlaces_SelectionChanged;
            dataGridViewPlaces.Scroll += dataGridViewPlaces_Scroll;
            dataGridViewPlaces.ClearSelection();
        }

        private void dataGridViewPlaces_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);
                int rowHeight = dataGridViewPlaces.Size.Height / dataGridViewPlaces.RowCount;
                dataGridViewPlaces.Rows[e.RowIndex].Height = rowHeight;

                // Устанавливаем размеры места
                //int seatWidth = 50;
                int seatWidth = (int)(dataGridViewPlaces.Columns[e.ColumnIndex].Width / 1.35);
                //int seatHeight = 50;
                int seatHeight = (int)(dataGridViewPlaces.Rows[e.RowIndex].Height / 1.35);

                // Устанавливаем отступы между местами
                int w = dataGridViewPlaces.Columns[e.ColumnIndex].Width;
                int h = dataGridViewPlaces.Rows[e.RowIndex].Height;
                //int paddingX = 5;
                int paddingX = (w / 2) - seatWidth + (seatWidth / 2);
                int paddingY = (h / 2) - seatHeight + (seatHeight / 2);

                // Вычисляем координаты первого места
                int startX = e.CellBounds.Left + paddingX;
                int startY = e.CellBounds.Top + paddingY;

                int seatX = startX;
                int seatY = startY;

                Brush brush;

                Font font = new Font("Microsoft Sans Serif", 12);
                Brush textBrush = Brushes.Black;

                Place p = null;

                // Вычисляем номер текущего места
                if (dataGridViewPlaces[e.ColumnIndex, e.RowIndex].Tag == null)
                    return;
                else
                {
                    int currentSeat = (int)dataGridViewPlaces[e.ColumnIndex, e.RowIndex].Tag;

                    p = places.Where(x => x.PlaceId == currentSeat).FirstOrDefault();
                    if (p.PlaceTypeId == 3)
                        return;

                    //var isBusy = DB.Ticket.Where(x => x.PlaceId == currentSeat && x.SeanceId == seance.SeanceId).FirstOrDefault();
                    var isBusy = busyPlaces.Any(x => x.PlaceId == p.PlaceId);

                    //if (isBusy != null)
                    if (isBusy)
                    {
                        brush = Brushes.Red;
                    }
                    else
                    {
                        if (tickets.Any(x => x.Place.PlaceId == currentSeat))
                        {
                            brush = Brushes.White;
                            e.Graphics.DrawEllipse(new Pen(Color.Black, 2), seatX - 4, seatY - 4, seatWidth + 8, seatHeight + 8);
                        }
                        else
                        {

                            switch (p.PlaceTypeId)
                            {
                                case 1:
                                    brush = Brushes.LimeGreen;
                                    break;
                                case 2:
                                    brush = Brushes.Yellow;
                                    break;
                                default:
                                    brush = Brushes.Cyan;
                                    break;
                            }
                        }
                    }
                }

                // Рисуем место
                e.Graphics.FillEllipse(brush, seatX, seatY, seatWidth, seatHeight);
                e.Graphics.DrawEllipse(Pens.Black, seatX, seatY, seatWidth, seatHeight);
                if (p != null)
                {
                    if (p.PlaceTypeId != 3)
                    {
                        string text = p.PlaceNumber.ToString();
                        SizeF textSize = e.Graphics.MeasureString(text, font);
                        PointF textPosition = new PointF(seatX + (seatWidth - textSize.Width) / 2, seatY + (seatHeight - textSize.Height) / 2);
                        e.Graphics.DrawString(text, font, textBrush, textPosition);
                    }
                }

                // Останавливаем стандартную отрисовку ячейки
                e.Handled = true;
            }
        }

        private void dataGridViewPlaces_SelectionChanged(object sender, EventArgs e)
        {

            tickets.Clear();
            sumTickets = 0;

            for (int i = 0; i < dataGridViewPlaces.SelectedCells.Count; i++)
            {
                if (dataGridViewPlaces.SelectedCells[i].Tag != null)
                {
                    dataGridViewPlaces.InvalidateCell(dataGridViewPlaces.SelectedCells[i]);
                    int placeId = (int)dataGridViewPlaces.SelectedCells[i].Tag;
                    //var isBusy = DB.Ticket.Where(x => x.PlaceId == placeId && x.SeanceId == seance.SeanceId).FirstOrDefault();
                    var isBusy = busyPlaces.Any(x => x.PlaceId == placeId);

                    //if (isBusy != null)
                    if (!isBusy)
                    {
                        Ticket t = new Ticket();
                        Place p = places.Where(x => x.PlaceId == placeId).FirstOrDefault();
                        if (p.PlaceTypeId == 3)
                            break;
                        t.Place = p;
                        t.Seance = seance;
                        t.TicketCost = seance.SeanceCost + p.PlaceType.PlaceTypeCost;
                        sumTickets += t.TicketCost;
                        tickets.Add(t);
                        
                    }
                }
            }

            //if (tickets.Count > 0)
            //    dataGridViewPlaces.Invalidate();

            labelCount.Text = "Количество билетов: " + tickets.Count;
            labelCost.Text = "Стоимость: " + sumTickets.ToString("C2");
        }

        private void buttonBuy_Click(object sender, EventArgs e)
        {

            if (tickets.Count == 0)
            {
                MessageBox.Show("Вы не выбрали ни одного места", "Покупка билетов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var lst = DB.Ticket.Where(x => x.SeanceId == seance.SeanceId).ToList();
            foreach (var item in lst)
            {
                if (tickets.Any(x => x.Place.PlaceId == item.PlaceId))
                {
                    MessageBox.Show("Одно из мест уже купили", "Покупка билетов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tickets.Clear();
                    FormOrderSeance_Load(sender, e);
                    dataGridViewPlaces.Invalidate();
                    return;
                }
            }
            Thread thread = new Thread(buy);
            buttonBuy.Text = "Загрузка...";
            Enabled = false;
            buttonBuy.Enabled = false;
            buttonReturn.Enabled = false;
            dataGridViewPlaces.Enabled = false;
            thread.Start();
        }

        private void buy()
        {
            string code;

            for (int i = 0; i < tickets.Count; i++)
            {
                tickets[i].TicketDateTime = DateTime.Now;

                do
                {
                    code = rand.Next(100000, 1000000).ToString();
                }
                while (DB.Ticket.Where(x => x.TicketCode == code).FirstOrDefault() != null);
                tickets[i].TicketCode = code;
                tickets[i].TicketId = (DB.Ticket.Count() > 0 ? DB.Ticket.Max(x => x.TicketId) : 0) + i + 1;
                tickets[i].TicketActive = true;
                tickets[i].UserId = Helper.User.UserId;
                generateCheck(tickets[i]);
                DB.Ticket.Add(tickets[i]);
            }

            DB.SaveChanges();

            MessageBox.Show("Покупка проведена успешно", "Покупка билетов", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BeginInvoke(new MethodInvoker(() => Close()));
        }

        Color[] colors = new Color[] { Color.Green, Color.Yellow, Color.Blue };

        
        void generateCheck(Ticket ticket)
        {
            Word.Application wordApp = new Word.Application();
            Word.Document wordDoc = wordApp.Documents.Add();
            wordDoc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperCustom;
            wordDoc.PageSetup.LeftMargin = 10;
            wordDoc.PageSetup.BottomMargin = 5;
            wordDoc.PageSetup.RightMargin = 10;
            wordDoc.PageSetup.TopMargin = 5;

            wordDoc.PageSetup.SetAsTemplateDefault();
            wordDoc.PageSetup.PageWidth = 250;
            wordDoc.PageSetup.PageHeight = 305;

            Word.InlineShape wordInlineShape;
            Word.Shape wordShape;
            Word.Range wordRange;
            Word.Paragraph wordPar;

            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;
            generateBarcode(ticket.TicketDateTime.ToShortDateString().Replace(".", "") + ticket.TicketDateTime.ToString("HH mm").Replace(" ", "") + ticket.TicketCode).Save("temp");
            wordInlineShape = wordDoc.InlineShapes.AddPicture(Application.StartupPath + "\\temp", Type.Missing, Type.Missing, wordRange);
            File.Delete(Application.StartupPath + "\\temp");
            wordInlineShape.Width = 230;
            wordInlineShape.Height = 50;
            wordRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordRange.ParagraphFormat.SpaceAfter = 0;
            wordRange.InsertParagraphAfter();

            wordShape = wordDoc.Shapes.AddLine(0, 65, 250, 65);
            wordShape.Line.DashStyle = Microsoft.Office.Core.MsoLineDashStyle.msoLineLongDash;
            wordShape.Line.ForeColor.RGB = ColorTranslator.ToOle(Color.FromArgb(0, 0, 0));

            //film
            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;
            wordRange.Text = seance.Film.FilmName + " (" + seance.Film.MinAge.MinAgeValue + "+)";
            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordRange.Font.Size = 16;
            wordRange.Font.Bold = 1;
            wordRange.InsertParagraphAfter();

            //date
            wordPar = wordDoc.Paragraphs.Add();
            wordPar.Range.Text = "Дата: " + seance.SeanceDate.ToShortDateString();

            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordPar.Range.Font.Size = 14;
            wordPar.Range.Font.Bold = 0;
            wordRange = wordDoc.Range(wordPar.Range.Start + wordPar.Range.Text.IndexOf(":") + 1, wordPar.Range.End);
            wordRange.Font.Size = 16;
            wordRange.Font.Bold = 1;
            wordPar.Range.InsertParagraphAfter();

            //time
            wordPar = wordDoc.Paragraphs.Add();
            wordPar.Range.Text = "Время: " + seance.SeanceTime.ToString().Substring(0, 5);
            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordPar.Range.Font.Size = 14;
            wordPar.Range.Font.Bold = 0;
            wordRange = wordDoc.Range(wordPar.Range.Start + wordPar.Range.Text.IndexOf(":"), wordPar.Range.End);
            wordRange.Font.Size = 16;
            wordRange.Font.Bold = 1;
            wordPar.Range.InsertParagraphAfter();

            //Hall
            wordPar = wordDoc.Paragraphs.Add();
            wordPar.Range.Text = "Зал: " + seance.Hall.HallName;
            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordPar.Range.Font.Size = 14;
            wordPar.Range.Font.Bold = 0;
            wordRange = wordDoc.Range(wordPar.Range.Start + wordPar.Range.Text.IndexOf(":"), wordPar.Range.End);
            wordRange.Font.Size = 16;
            wordRange.Font.Bold = 1;
            wordPar.Range.InsertParagraphAfter();

            //Place
            wordPar = wordDoc.Paragraphs.Add();
            wordPar.Range.Text = "Ряд: " + ticket.Place.PlaceRow + " Место: " + ticket.Place.PlaceNumber;
            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordPar.Range.Font.Size = 14;
            wordPar.Range.Font.Bold = 0;
            wordRange = wordDoc.Range(wordPar.Range.Start + wordPar.Range.Text.IndexOf(":"), wordPar.Range.Start + wordPar.Range.Text.IndexOf("М"));
            wordRange.Font.Size = 16;
            wordRange.Font.Bold = 1;
            wordRange = wordDoc.Range(wordPar.Range.Start + wordPar.Range.Text.LastIndexOf(":"), wordPar.Range.End);
            wordRange.Font.Size = 16;
            wordRange.Font.Bold = 1;
            wordPar.SpaceAfter = 11;
            wordPar.Range.InsertParagraphAfter();

            //Operator
            wordPar = wordDoc.Paragraphs.Add();
            wordPar.Range.Text = "Оператор: " + Helper.User.UserLastName + " " + Helper.User.UserFirstName + " " + Helper.User.UserPatronymic;
            wordPar.Format.SpaceAfter = 0;
            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordPar.Range.Font.Size = 10;
            wordPar.Range.Font.Bold = 0;
            wordPar.Range.InsertParagraphAfter();

            //OrderDate
            wordPar = wordDoc.Paragraphs.Add();
            wordPar.Range.Text = "Продажа: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"); //DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            wordPar.Format.SpaceAfter = 0;
            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordPar.Range.Font.Size = 10;
            wordPar.Range.Font.Bold = 0;
            wordPar.Range.InsertParagraphAfter();

            //OrderNum
            wordPar = wordDoc.Paragraphs.Add();
            wordPar.Range.Text = "Заказ №: " + ticket.TicketId + " Цена: " + ticket.TicketCost.ToString("C2");
            wordPar.Format.SpaceAfter = 0;
            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordPar.Range.Font.Size = 10;
            wordPar.Range.Font.Bold = 0;
            wordPar.Range.InsertParagraphAfter();

            //wordDoc.Saved = true;
            //wordDoc.SaveAs2(Application.StartupPath + "\\tickets\\" + "Сеанс-" + seance.SeanceId + " Билет-" + ticket.TicketId + ".pdf", Word.WdExportFormat.wdExportFormatPDF);

            wordDoc.Saved = true;
            string seancetime = seance.SeanceTime.ToString().Substring(0, 5).Replace(":", "_");
            string savePath = Application.StartupPath + "\\tickets\\" + "Сеанс-" + seance.Film.FilmName + ", Время-" + seancetime + ", Билет-Ряд" + ticket.Place.PlaceRow + "Место" + ticket.Place.PlaceNumber + ".pdf";
            //wordDoc.SaveAs2(Application.StartupPath + "\\tickets\\" + "Сеанс-" + seance.SeanceId + " Билет-" + ticket.TicketId + ".pdf", Word.WdExportFormat.wdExportFormatPDF);
            wordDoc.SaveAs2(savePath, Word.WdExportFormat.wdExportFormatPDF);


            wordDoc.Close(true, null, null);
            wordApp.Quit();

            releaseObject(wordApp);
            releaseObject(wordDoc);

            Process.Start(savePath);
        }

        Bitmap generateBarcode(string s)
        {
            BarcodeWriter barcodeWriter = new BarcodeWriter()
            {
                Format = BarcodeFormat.CODE_128,
                Options = new ZXing.Common.EncodingOptions
                {
                    PureBarcode = true,
                }
            };

            Bitmap bmp = barcodeWriter.Write(s);
            return bmp;
        }

        void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void dataGridViewPlaces_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                // Блокируем прокрутку по вертикали
                e.NewValue = e.OldValue;
            }
        }
    }
}
