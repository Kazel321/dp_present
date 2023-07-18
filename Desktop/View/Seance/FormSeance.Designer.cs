namespace Diplom
{
    partial class FormSeance
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSeance));
            this.tableLayoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.buttonHist = new System.Windows.Forms.Button();
            this.tableLayoutPanelBottom = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelFilter = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxAge = new System.Windows.Forms.ComboBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelAge = new System.Windows.Forms.Label();
            this.labelGenre = new System.Windows.Forms.Label();
            this.dateTimePickerSeance = new System.Windows.Forms.DateTimePicker();
            this.comboBoxGenre = new System.Windows.Forms.ComboBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelYear = new System.Windows.Forms.Label();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelList = new System.Windows.Forms.TableLayoutPanel();
            this.timerTime = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelTop
            // 
            this.tableLayoutPanelTop.ColumnCount = 4;
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanelTop.Controls.Add(this.pictureBoxLogo, 0, 0);
            this.tableLayoutPanelTop.Controls.Add(this.labelTitle, 1, 0);
            this.tableLayoutPanelTop.Controls.Add(this.buttonReturn, 3, 0);
            this.tableLayoutPanelTop.Controls.Add(this.buttonHist, 2, 0);
            this.tableLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelTop.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            this.tableLayoutPanelTop.RowCount = 1;
            this.tableLayoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.Size = new System.Drawing.Size(1142, 43);
            this.tableLayoutPanelTop.TabIndex = 0;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(45, 43);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.Location = new System.Drawing.Point(145, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(100, 0, 0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(857, 43);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Сеансы";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonReturn
            // 
            this.buttonReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReturn.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReturn.Location = new System.Drawing.Point(1052, 0);
            this.buttonReturn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(90, 43);
            this.buttonReturn.TabIndex = 2;
            this.buttonReturn.Text = "Назад";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // buttonHist
            // 
            this.buttonHist.BackgroundImage = global::Diplom.Properties.Resources.history_btn;
            this.buttonHist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonHist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonHist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHist.Location = new System.Drawing.Point(1002, 0);
            this.buttonHist.Margin = new System.Windows.Forms.Padding(0);
            this.buttonHist.Name = "buttonHist";
            this.buttonHist.Size = new System.Drawing.Size(50, 43);
            this.buttonHist.TabIndex = 3;
            this.buttonHist.UseVisualStyleBackColor = true;
            this.buttonHist.Click += new System.EventHandler(this.buttonHist_Click);
            // 
            // tableLayoutPanelBottom
            // 
            this.tableLayoutPanelBottom.ColumnCount = 1;
            this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelBottom.Location = new System.Drawing.Point(0, 499);
            this.tableLayoutPanelBottom.Name = "tableLayoutPanelBottom";
            this.tableLayoutPanelBottom.RowCount = 1;
            this.tableLayoutPanelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBottom.Size = new System.Drawing.Size(1142, 27);
            this.tableLayoutPanelBottom.TabIndex = 1;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelFilter, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelList, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 43);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1142, 456);
            this.tableLayoutPanelMain.TabIndex = 2;
            // 
            // tableLayoutPanelFilter
            // 
            this.tableLayoutPanelFilter.ColumnCount = 10;
            this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelFilter.Controls.Add(this.comboBoxAge, 5, 0);
            this.tableLayoutPanelFilter.Controls.Add(this.labelDate, 0, 0);
            this.tableLayoutPanelFilter.Controls.Add(this.labelAge, 4, 0);
            this.tableLayoutPanelFilter.Controls.Add(this.labelGenre, 2, 0);
            this.tableLayoutPanelFilter.Controls.Add(this.dateTimePickerSeance, 1, 0);
            this.tableLayoutPanelFilter.Controls.Add(this.comboBoxGenre, 3, 0);
            this.tableLayoutPanelFilter.Controls.Add(this.textBoxSearch, 9, 0);
            this.tableLayoutPanelFilter.Controls.Add(this.labelName, 8, 0);
            this.tableLayoutPanelFilter.Controls.Add(this.labelYear, 6, 0);
            this.tableLayoutPanelFilter.Controls.Add(this.comboBoxYear, 7, 0);
            this.tableLayoutPanelFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelFilter.Location = new System.Drawing.Point(50, 8);
            this.tableLayoutPanelFilter.Margin = new System.Windows.Forms.Padding(50, 3, 50, 3);
            this.tableLayoutPanelFilter.Name = "tableLayoutPanelFilter";
            this.tableLayoutPanelFilter.RowCount = 1;
            this.tableLayoutPanelFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFilter.Size = new System.Drawing.Size(1042, 39);
            this.tableLayoutPanelFilter.TabIndex = 0;
            // 
            // comboBoxAge
            // 
            this.comboBoxAge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxAge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAge.FormattingEnabled = true;
            this.comboBoxAge.Location = new System.Drawing.Point(523, 8);
            this.comboBoxAge.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.comboBoxAge.Name = "comboBoxAge";
            this.comboBoxAge.Size = new System.Drawing.Size(98, 28);
            this.comboBoxAge.TabIndex = 13;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDate.Location = new System.Drawing.Point(3, 11);
            this.labelDate.Margin = new System.Windows.Forms.Padding(3, 11, 10, 0);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(91, 28);
            this.labelDate.TabIndex = 0;
            this.labelDate.Text = "Дата:";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelAge
            // 
            this.labelAge.AutoSize = true;
            this.labelAge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAge.Location = new System.Drawing.Point(419, 11);
            this.labelAge.Margin = new System.Windows.Forms.Padding(3, 11, 10, 0);
            this.labelAge.Name = "labelAge";
            this.labelAge.Size = new System.Drawing.Size(91, 28);
            this.labelAge.TabIndex = 11;
            this.labelAge.Text = "Возраст:";
            this.labelAge.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelGenre
            // 
            this.labelGenre.AutoSize = true;
            this.labelGenre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGenre.Location = new System.Drawing.Point(211, 11);
            this.labelGenre.Margin = new System.Windows.Forms.Padding(3, 11, 10, 0);
            this.labelGenre.Name = "labelGenre";
            this.labelGenre.Size = new System.Drawing.Size(91, 28);
            this.labelGenre.TabIndex = 10;
            this.labelGenre.Text = "Жанр:";
            this.labelGenre.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dateTimePickerSeance
            // 
            this.dateTimePickerSeance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dateTimePickerSeance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePickerSeance.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerSeance.Location = new System.Drawing.Point(107, 8);
            this.dateTimePickerSeance.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.dateTimePickerSeance.Name = "dateTimePickerSeance";
            this.dateTimePickerSeance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dateTimePickerSeance.Size = new System.Drawing.Size(98, 26);
            this.dateTimePickerSeance.TabIndex = 7;
            this.dateTimePickerSeance.Value = new System.DateTime(2022, 12, 2, 0, 0, 0, 0);
            // 
            // comboBoxGenre
            // 
            this.comboBoxGenre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxGenre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGenre.FormattingEnabled = true;
            this.comboBoxGenre.Location = new System.Drawing.Point(315, 8);
            this.comboBoxGenre.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.comboBoxGenre.Name = "comboBoxGenre";
            this.comboBoxGenre.Size = new System.Drawing.Size(98, 28);
            this.comboBoxGenre.TabIndex = 12;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSearch.Location = new System.Drawing.Point(939, 9);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(100, 26);
            this.textBoxSearch.TabIndex = 15;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.Filter);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.Location = new System.Drawing.Point(835, 11);
            this.labelName.Margin = new System.Windows.Forms.Padding(3, 11, 10, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(91, 28);
            this.labelName.TabIndex = 14;
            this.labelName.Text = "Название:";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelYear.Location = new System.Drawing.Point(627, 11);
            this.labelYear.Margin = new System.Windows.Forms.Padding(3, 11, 10, 0);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(91, 28);
            this.labelYear.TabIndex = 16;
            this.labelYear.Text = "Год:";
            this.labelYear.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Items.AddRange(new object[] {
            "без сортировки",
            "по убыванию",
            "по возрастанию"});
            this.comboBoxYear.Location = new System.Drawing.Point(731, 8);
            this.comboBoxYear.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(98, 28);
            this.comboBoxYear.TabIndex = 17;
            // 
            // tableLayoutPanelList
            // 
            this.tableLayoutPanelList.AutoScroll = true;
            this.tableLayoutPanelList.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelList.ColumnCount = 1;
            this.tableLayoutPanelList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1063F));
            this.tableLayoutPanelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelList.Location = new System.Drawing.Point(50, 53);
            this.tableLayoutPanelList.Margin = new System.Windows.Forms.Padding(50, 3, 50, 3);
            this.tableLayoutPanelList.Name = "tableLayoutPanelList";
            this.tableLayoutPanelList.RowCount = 1;
            this.tableLayoutPanelList.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelList.Size = new System.Drawing.Size(1042, 400);
            this.tableLayoutPanelList.TabIndex = 1;
            // 
            // timerTime
            // 
            this.timerTime.Enabled = true;
            this.timerTime.Interval = 1000;
            this.timerTime.Tick += new System.EventHandler(this.timerTime_Tick);
            // 
            // FormSeance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 526);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.tableLayoutPanelBottom);
            this.Controls.Add(this.tableLayoutPanelTop);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormSeance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сеансы";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormSeance_Load);
            this.tableLayoutPanelTop.ResumeLayout(false);
            this.tableLayoutPanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelFilter.ResumeLayout(false);
            this.tableLayoutPanelFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFilter;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerSeance;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelList;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ComboBox comboBoxAge;
        private System.Windows.Forms.Label labelAge;
        private System.Windows.Forms.Label labelGenre;
        private System.Windows.Forms.ComboBox comboBoxGenre;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.Timer timerTime;
        private System.Windows.Forms.Button buttonHist;
    }
}

