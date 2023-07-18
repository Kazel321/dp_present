namespace Diplom
{
    partial class FormSingleFilm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSingleFilm));
            this.tableLayoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.tableLayoutPanelBottom = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.labelId = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelContry = new System.Windows.Forms.Label();
            this.labelMinAge = new System.Windows.Forms.Label();
            this.labelDuration = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.comboBoxCountry = new System.Windows.Forms.ComboBox();
            this.comboBoxMinAge = new System.Windows.Forms.ComboBox();
            this.maskedTextBoxDuration = new System.Windows.Forms.MaskedTextBox();
            this.labelCover = new System.Windows.Forms.Label();
            this.pictureBoxCover = new System.Windows.Forms.PictureBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonScreens = new System.Windows.Forms.Button();
            this.buttonGenre = new System.Windows.Forms.Button();
            this.labelDesc = new System.Windows.Forms.Label();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.labelYear = new System.Windows.Forms.Label();
            this.labelTrailer = new System.Windows.Forms.Label();
            this.textBoxTrailer = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.tableLayoutPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelTop
            // 
            this.tableLayoutPanelTop.ColumnCount = 3;
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanelTop.Controls.Add(this.pictureBoxLogo, 0, 0);
            this.tableLayoutPanelTop.Controls.Add(this.labelTitle, 1, 0);
            this.tableLayoutPanelTop.Controls.Add(this.buttonReturn, 2, 0);
            this.tableLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelTop.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            this.tableLayoutPanelTop.RowCount = 1;
            this.tableLayoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.Size = new System.Drawing.Size(1193, 43);
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
            this.labelTitle.Location = new System.Drawing.Point(93, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(48, 0, 3, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(1007, 43);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Фильм";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonReturn
            // 
            this.buttonReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReturn.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReturn.Location = new System.Drawing.Point(1103, 0);
            this.buttonReturn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(90, 43);
            this.buttonReturn.TabIndex = 2;
            this.buttonReturn.Text = "Назад";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // tableLayoutPanelBottom
            // 
            this.tableLayoutPanelBottom.ColumnCount = 1;
            this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelBottom.Location = new System.Drawing.Point(0, 766);
            this.tableLayoutPanelBottom.Name = "tableLayoutPanelBottom";
            this.tableLayoutPanelBottom.RowCount = 1;
            this.tableLayoutPanelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBottom.Size = new System.Drawing.Size(1193, 27);
            this.tableLayoutPanelBottom.TabIndex = 1;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 4;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelMain.Controls.Add(this.textBoxYear, 1, 5);
            this.tableLayoutPanelMain.Controls.Add(this.labelId, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelName, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelContry, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelMinAge, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelDuration, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxId, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxName, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.comboBoxCountry, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.comboBoxMinAge, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.maskedTextBoxDuration, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.labelCover, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxCover, 2, 1);
            this.tableLayoutPanelMain.Controls.Add(this.buttonSave, 0, 11);
            this.tableLayoutPanelMain.Controls.Add(this.buttonRemove, 2, 11);
            this.tableLayoutPanelMain.Controls.Add(this.buttonScreens, 1, 10);
            this.tableLayoutPanelMain.Controls.Add(this.buttonGenre, 0, 10);
            this.tableLayoutPanelMain.Controls.Add(this.labelDesc, 0, 7);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxDesc, 1, 7);
            this.tableLayoutPanelMain.Controls.Add(this.labelYear, 0, 5);
            this.tableLayoutPanelMain.Controls.Add(this.labelTrailer, 0, 6);
            this.tableLayoutPanelMain.Controls.Add(this.textBoxTrailer, 1, 6);
            this.tableLayoutPanelMain.Controls.Add(this.labelStatus, 0, 9);
            this.tableLayoutPanelMain.Controls.Add(this.checkBoxActive, 1, 9);
            this.tableLayoutPanelMain.Controls.Add(this.buttonOpen, 2, 10);
            this.tableLayoutPanelMain.Controls.Add(this.buttonClear, 3, 10);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 43);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 12;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1193, 723);
            this.tableLayoutPanelMain.TabIndex = 2;
            // 
            // textBoxYear
            // 
            this.textBoxYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYear.Location = new System.Drawing.Point(301, 317);
            this.textBoxYear.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(265, 26);
            this.textBoxYear.TabIndex = 21;
            // 
            // labelId
            // 
            this.labelId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(30, 20);
            this.labelId.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(265, 20);
            this.labelId.TabIndex = 0;
            this.labelId.Text = "ID:";
            // 
            // labelName
            // 
            this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(30, 80);
            this.labelName.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(265, 20);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Название:";
            // 
            // labelContry
            // 
            this.labelContry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelContry.AutoSize = true;
            this.labelContry.Location = new System.Drawing.Point(30, 140);
            this.labelContry.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelContry.Name = "labelContry";
            this.labelContry.Size = new System.Drawing.Size(265, 20);
            this.labelContry.TabIndex = 2;
            this.labelContry.Text = "Страна:";
            // 
            // labelMinAge
            // 
            this.labelMinAge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMinAge.AutoSize = true;
            this.labelMinAge.Location = new System.Drawing.Point(30, 200);
            this.labelMinAge.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelMinAge.Name = "labelMinAge";
            this.labelMinAge.Size = new System.Drawing.Size(265, 20);
            this.labelMinAge.TabIndex = 3;
            this.labelMinAge.Text = "Мин. возраст:";
            // 
            // labelDuration
            // 
            this.labelDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDuration.AutoSize = true;
            this.labelDuration.Location = new System.Drawing.Point(30, 260);
            this.labelDuration.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(265, 20);
            this.labelDuration.TabIndex = 4;
            this.labelDuration.Text = "Длительность:";
            // 
            // textBoxId
            // 
            this.textBoxId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxId.Location = new System.Drawing.Point(301, 17);
            this.textBoxId.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(265, 26);
            this.textBoxId.TabIndex = 15;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(301, 77);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(265, 26);
            this.textBoxName.TabIndex = 16;
            // 
            // comboBoxCountry
            // 
            this.comboBoxCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCountry.FormattingEnabled = true;
            this.comboBoxCountry.Location = new System.Drawing.Point(301, 139);
            this.comboBoxCountry.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.comboBoxCountry.Name = "comboBoxCountry";
            this.comboBoxCountry.Size = new System.Drawing.Size(265, 28);
            this.comboBoxCountry.TabIndex = 17;
            // 
            // comboBoxMinAge
            // 
            this.comboBoxMinAge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMinAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMinAge.FormattingEnabled = true;
            this.comboBoxMinAge.Location = new System.Drawing.Point(301, 199);
            this.comboBoxMinAge.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.comboBoxMinAge.Name = "comboBoxMinAge";
            this.comboBoxMinAge.Size = new System.Drawing.Size(265, 28);
            this.comboBoxMinAge.TabIndex = 18;
            // 
            // maskedTextBoxDuration
            // 
            this.maskedTextBoxDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBoxDuration.Location = new System.Drawing.Point(301, 257);
            this.maskedTextBoxDuration.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.maskedTextBoxDuration.Mask = "90:00";
            this.maskedTextBoxDuration.Name = "maskedTextBoxDuration";
            this.maskedTextBoxDuration.Size = new System.Drawing.Size(265, 26);
            this.maskedTextBoxDuration.TabIndex = 19;
            this.maskedTextBoxDuration.ValidatingType = typeof(System.DateTime);
            // 
            // labelCover
            // 
            this.labelCover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCover.AutoSize = true;
            this.tableLayoutPanelMain.SetColumnSpan(this.labelCover, 2);
            this.labelCover.Location = new System.Drawing.Point(626, 20);
            this.labelCover.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.labelCover.Name = "labelCover";
            this.labelCover.Size = new System.Drawing.Size(537, 20);
            this.labelCover.TabIndex = 6;
            this.labelCover.Text = "Обложка";
            this.labelCover.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxCover
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.pictureBoxCover, 2);
            this.pictureBoxCover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxCover.Location = new System.Drawing.Point(626, 60);
            this.pictureBoxCover.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.pictureBoxCover.Name = "pictureBoxCover";
            this.tableLayoutPanelMain.SetRowSpan(this.pictureBoxCover, 9);
            this.pictureBoxCover.Size = new System.Drawing.Size(537, 540);
            this.pictureBoxCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCover.TabIndex = 9;
            this.pictureBoxCover.TabStop = false;
            // 
            // buttonSave
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.buttonSave, 2);
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(50, 675);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(50, 15, 50, 15);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(496, 33);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonRemove
            // 
            this.tableLayoutPanelMain.SetColumnSpan(this.buttonRemove, 2);
            this.buttonRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRemove.Location = new System.Drawing.Point(646, 675);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(50, 15, 50, 15);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(497, 33);
            this.buttonRemove.TabIndex = 13;
            this.buttonRemove.Text = "Удалить";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonScreens
            // 
            this.buttonScreens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonScreens.Location = new System.Drawing.Point(328, 615);
            this.buttonScreens.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.buttonScreens.Name = "buttonScreens";
            this.buttonScreens.Size = new System.Drawing.Size(238, 30);
            this.buttonScreens.TabIndex = 8;
            this.buttonScreens.Text = "Скриншоты";
            this.buttonScreens.UseVisualStyleBackColor = true;
            this.buttonScreens.Click += new System.EventHandler(this.buttonScreens_Click);
            // 
            // buttonGenre
            // 
            this.buttonGenre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGenre.Location = new System.Drawing.Point(30, 615);
            this.buttonGenre.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.buttonGenre.Name = "buttonGenre";
            this.buttonGenre.Size = new System.Drawing.Size(238, 30);
            this.buttonGenre.TabIndex = 7;
            this.buttonGenre.Text = "Жанры";
            this.buttonGenre.UseVisualStyleBackColor = true;
            this.buttonGenre.Click += new System.EventHandler(this.buttonGenre_Click);
            // 
            // labelDesc
            // 
            this.labelDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDesc.AutoSize = true;
            this.labelDesc.Location = new System.Drawing.Point(30, 470);
            this.labelDesc.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelDesc.Name = "labelDesc";
            this.tableLayoutPanelMain.SetRowSpan(this.labelDesc, 2);
            this.labelDesc.Size = new System.Drawing.Size(265, 20);
            this.labelDesc.TabIndex = 5;
            this.labelDesc.Text = "Описание:";
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDesc.Location = new System.Drawing.Point(301, 435);
            this.textBoxDesc.Margin = new System.Windows.Forms.Padding(3, 15, 30, 15);
            this.textBoxDesc.Multiline = true;
            this.textBoxDesc.Name = "textBoxDesc";
            this.tableLayoutPanelMain.SetRowSpan(this.textBoxDesc, 2);
            this.textBoxDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDesc.Size = new System.Drawing.Size(265, 90);
            this.textBoxDesc.TabIndex = 14;
            // 
            // labelYear
            // 
            this.labelYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelYear.AutoSize = true;
            this.labelYear.Location = new System.Drawing.Point(30, 320);
            this.labelYear.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(265, 20);
            this.labelYear.TabIndex = 20;
            this.labelYear.Text = "Год производства:";
            // 
            // labelTrailer
            // 
            this.labelTrailer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTrailer.AutoSize = true;
            this.labelTrailer.Location = new System.Drawing.Point(30, 380);
            this.labelTrailer.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelTrailer.Name = "labelTrailer";
            this.labelTrailer.Size = new System.Drawing.Size(265, 20);
            this.labelTrailer.TabIndex = 22;
            this.labelTrailer.Text = "Ссылка на трейлер:";
            // 
            // textBoxTrailer
            // 
            this.textBoxTrailer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTrailer.Location = new System.Drawing.Point(301, 377);
            this.textBoxTrailer.Margin = new System.Windows.Forms.Padding(3, 3, 30, 3);
            this.textBoxTrailer.Name = "textBoxTrailer";
            this.textBoxTrailer.Size = new System.Drawing.Size(265, 26);
            this.textBoxTrailer.TabIndex = 23;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(30, 560);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(265, 20);
            this.labelStatus.TabIndex = 24;
            this.labelStatus.Text = "Участие в сеансах:";
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Checked = true;
            this.checkBoxActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxActive.Location = new System.Drawing.Point(301, 558);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(292, 24);
            this.checkBoxActive.TabIndex = 25;
            this.checkBoxActive.Text = "Участвует";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            this.checkBoxActive.CheckedChanged += new System.EventHandler(this.checkBoxActive_CheckedChanged);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(626, 615);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(238, 30);
            this.buttonOpen.TabIndex = 10;
            this.buttonOpen.Text = "Открыть";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(924, 615);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(239, 30);
            this.buttonClear.TabIndex = 11;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // FormSingleFilm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 793);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.tableLayoutPanelBottom);
            this.Controls.Add(this.tableLayoutPanelTop);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1209, 832);
            this.Name = "FormSingleFilm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Фильм";
            this.Load += new System.EventHandler(this.FormSingleFilm_Load);
            this.tableLayoutPanelTop.ResumeLayout(false);
            this.tableLayoutPanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelContry;
        private System.Windows.Forms.Label labelMinAge;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.Label labelCover;
        private System.Windows.Forms.Button buttonGenre;
        private System.Windows.Forms.Button buttonScreens;
        private System.Windows.Forms.PictureBox pictureBoxCover;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ComboBox comboBoxCountry;
        private System.Windows.Forms.ComboBox comboBoxMinAge;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxDuration;
        private System.Windows.Forms.TextBox textBoxDesc;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.Label labelTrailer;
        private System.Windows.Forms.TextBox textBoxTrailer;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckBox checkBoxActive;
    }
}

