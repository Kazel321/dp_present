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
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using ZXing;
using System.Diagnostics;

namespace Diplom
{
    public partial class FormReport : Form
    {
        string report;
        string title;
        string chartTitle;
        List<KeyValuePair<string, int>> chartValues;
        Random rand = new Random();

        public dynamic DataSource
        {
            set { dataGridView.DataSource = value; }
        }


        public FormReport()
        {
            InitializeComponent(); Helper.DB = new DB(Helper.connection);
        }

        public FormReport(string title, List<KeyValuePair<string, int>> chartValues, string chartTitle)
        {
            this.title = title;
            this.chartValues = chartValues;
            this.chartTitle = chartTitle;
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

        Chart chart1;

        private void FormReport_Load(object sender, EventArgs e)
        {
            labelTitle.Text = title;
            Text = title;

            chart1 = new Chart();

            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();
            Series series1 = new Series();

            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new System.Drawing.Point(3, 53);
            chart1.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new System.Drawing.Size(573, 231);
            chart1.TabIndex = 1;
            chart1.Text = "chart1";

            chart1.Series[0].ChartType = SeriesChartType.Column;
            chart1.ChartAreas[0].AxisX.Interval = 1;

            string seriesName = "Отчет";
            string legendName = chartTitle;

            Series series = new Series(seriesName);

            for (int i = 0; i < chartValues.Count; i++)
            {
                series.Points.AddXY(chartValues[i].Key, chartValues[i].Value);
                series.Points[series.Points.Count - 1].Color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            }

            chart1.Series.Add(series);

            chart1.Series[seriesName].CustomProperties = "DrawSideBySide=False"; // отключение отображения столбцов рядом
            chart1.Series[seriesName].IsValueShownAsLabel = true;

            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series[1].IsVisibleInLegend = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            Title titleChart = new Title();
            titleChart.Text = legendName;
            titleChart.Font = new Font("Microsoft Sans Serif", 12.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            chart1.Titles.Add(titleChart);

            tableLayoutPanelMain.Controls.Add(chart1);

            //datagrid

            foreach (DataGridViewColumn c in dataGridView.Columns)
            {
                c.HeaderText = c.HeaderText.Replace("_", " ");
            }

        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            buttonReport.Enabled = false;
            ExportToWord(dataGridView, chart1, title, Application.StartupPath + @"\reports\" + title + ".docx");
        }

        void ExportToWord(DataGridView dataGridView, Chart chart, string label, string fileName)
        {
            var word = new Word.Application();
            var doc = word.Documents.Add();

            // Настраиваем параметры страницы и шрифта
            doc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;
            doc.PageSetup.LeftMargin = 72;
            doc.PageSetup.RightMargin = 72;
            doc.PageSetup.TopMargin = 72;
            doc.PageSetup.BottomMargin = 72;
            doc.Content.Font.Name = "Arial";
            doc.Content.Font.Size = 11;

            // Добавляем заголовок "Cinema"
            var titleParagraph = doc.Paragraphs.Add();
            var titleRange = titleParagraph.Range;
            titleRange.Text = "«Синема»";
            titleRange.Font.Size = 11;
            titleRange.Font.ColorIndex = Word.WdColorIndex.wdBlack;
            titleRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            titleRange.InsertParagraphAfter();

            // Добавляем текущую дату
            var dateParagraph = doc.Paragraphs.Add();
            var dateRange = dateParagraph.Range;
            dateRange.Text = "Дата: " + DateTime.Now.ToString("dd.MM.yyyy");
            dateRange.Font.Size = 11;
            dateRange.Font.Bold = 0;
            dateRange.Font.ColorIndex = Word.WdColorIndex.wdBlack;
            dateRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            dateRange.InsertParagraphAfter();

            // Добавляем жирный текст по центру
            var boldParagraph = doc.Paragraphs.Add();
            var boldRange = boldParagraph.Range;
            boldRange.Text = label;
            boldRange.Font.Bold = 1;
            boldRange.Font.Size = 16;
            boldRange.Font.ColorIndex = Word.WdColorIndex.wdBlack;
            boldRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            boldRange.InsertParagraphAfter();

            // Сохраняем изображение компонента Chart
            string chartFilePath = Path.Combine(Application.StartupPath, "chart.png");
            chart.SaveImage(chartFilePath, ChartImageFormat.Png);

            // Добавляем изображение в документ Word
            var chartParagraph = doc.Paragraphs.Add();
            var chartRange = chartParagraph.Range;
            chartRange.InlineShapes.AddPicture(chartFilePath, LinkToFile: false, SaveWithDocument: true);
            chartRange.Borders.Enable = 1;
            chartRange.InsertParagraphAfter();

            // Надпись до таблицы
            var tableNameParagraph = doc.Paragraphs.Add();
            var tableNameRange = tableNameParagraph.Range;
            tableNameRange.Text = "Таблица с данными";
            tableNameRange.Font.Size = 11;
            tableNameRange.Font.Bold = 1;
            tableNameRange.Font.ColorIndex = Word.WdColorIndex.wdBlack;
            tableNameRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            tableNameRange.InsertParagraphAfter();

            // Добавляем таблицу
            var tableParagraph = doc.Paragraphs.Add();
            var tableRange = tableParagraph.Range;
            var table = doc.Tables.Add(tableRange, dataGridView.RowCount + 1, dataGridView.Columns.Count);

            // Добавляем заголовки столбцов
            for (int j = 0; j < dataGridView.Columns.Count; j++)
            {
                var header = dataGridView.Columns[j].HeaderText;
                table.Cell(1, j + 1).Range.Text = header;
                table.Cell(1, j + 1).Range.Font.Bold = 1;
                table.Cell(1, j + 1).Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
                table.Cell(1, j + 1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            }

            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    table.Cell(i + 2, j + 1).Range.Text = dataGridView.Rows[i].Cells[j].Value.ToString();
                }
            }

            // Выравниваем таблицу по центру страницы
            table.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

            table.Range.InsertParagraphAfter();

            try
            {
                doc.SaveAs2(fileName);
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить файл", "Печать отчета", MessageBoxButtons.OK, MessageBoxIcon.Error);
                releaseObject(word);
                releaseObject(doc);
                return;
            }
            // Закрываем документ и приложение Word
            doc.Close(true, null, null);
            word.Quit();

            releaseObject(word);
            releaseObject(doc);

            Process.Start(fileName);
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
    }
}
