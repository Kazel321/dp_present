namespace Diplom
{
    partial class FormFilm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFilm));
            this.tableLayoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.tableLayoutPanelBottom = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelFilm = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxCover = new System.Windows.Forms.PictureBox();
            this.buttonScreenshots = new System.Windows.Forms.Button();
            this.labelYearName = new System.Windows.Forms.Label();
            this.labelCountryName = new System.Windows.Forms.Label();
            this.labelGenresName = new System.Windows.Forms.Label();
            this.labelAgeName = new System.Windows.Forms.Label();
            this.labelTimeName = new System.Windows.Forms.Label();
            this.labelDesc = new System.Windows.Forms.Label();
            this.labelYear = new System.Windows.Forms.Label();
            this.labelCountry = new System.Windows.Forms.Label();
            this.labelGenres = new System.Windows.Forms.Label();
            this.labelAge = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.tableLayoutPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelFilm.SuspendLayout();
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
            this.tableLayoutPanelTop.Size = new System.Drawing.Size(959, 43);
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
            this.labelTitle.Size = new System.Drawing.Size(773, 43);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Название фильма";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonReturn
            // 
            this.buttonReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReturn.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReturn.Location = new System.Drawing.Point(869, 0);
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
            this.tableLayoutPanelBottom.Location = new System.Drawing.Point(0, 745);
            this.tableLayoutPanelBottom.Name = "tableLayoutPanelBottom";
            this.tableLayoutPanelBottom.RowCount = 1;
            this.tableLayoutPanelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBottom.Size = new System.Drawing.Size(959, 27);
            this.tableLayoutPanelBottom.TabIndex = 1;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelFilm, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 43);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(959, 702);
            this.tableLayoutPanelMain.TabIndex = 2;
            // 
            // tableLayoutPanelFilm
            // 
            this.tableLayoutPanelFilm.ColumnCount = 3;
            this.tableLayoutPanelFilm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanelFilm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelFilm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelFilm.Controls.Add(this.pictureBoxCover, 0, 0);
            this.tableLayoutPanelFilm.Controls.Add(this.buttonScreenshots, 0, 5);
            this.tableLayoutPanelFilm.Controls.Add(this.labelYearName, 1, 0);
            this.tableLayoutPanelFilm.Controls.Add(this.labelCountryName, 1, 1);
            this.tableLayoutPanelFilm.Controls.Add(this.labelGenresName, 1, 2);
            this.tableLayoutPanelFilm.Controls.Add(this.labelAgeName, 1, 3);
            this.tableLayoutPanelFilm.Controls.Add(this.labelTimeName, 1, 4);
            this.tableLayoutPanelFilm.Controls.Add(this.labelDesc, 1, 5);
            this.tableLayoutPanelFilm.Controls.Add(this.labelYear, 2, 0);
            this.tableLayoutPanelFilm.Controls.Add(this.labelCountry, 2, 1);
            this.tableLayoutPanelFilm.Controls.Add(this.labelGenres, 2, 2);
            this.tableLayoutPanelFilm.Controls.Add(this.labelAge, 2, 3);
            this.tableLayoutPanelFilm.Controls.Add(this.labelTime, 2, 4);
            this.tableLayoutPanelFilm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFilm.Location = new System.Drawing.Point(50, 3);
            this.tableLayoutPanelFilm.Margin = new System.Windows.Forms.Padding(50, 3, 50, 3);
            this.tableLayoutPanelFilm.Name = "tableLayoutPanelFilm";
            this.tableLayoutPanelFilm.RowCount = 6;
            this.tableLayoutPanelFilm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanelFilm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanelFilm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanelFilm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanelFilm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanelFilm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelFilm.Size = new System.Drawing.Size(859, 696);
            this.tableLayoutPanelFilm.TabIndex = 3;
            // 
            // pictureBoxCover
            // 
            this.pictureBoxCover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCover.Location = new System.Drawing.Point(0, 17);
            this.pictureBoxCover.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxCover.Name = "pictureBoxCover";
            this.tableLayoutPanelFilm.SetRowSpan(this.pictureBoxCover, 5);
            this.pictureBoxCover.Size = new System.Drawing.Size(300, 450);
            this.pictureBoxCover.TabIndex = 0;
            this.pictureBoxCover.TabStop = false;
            // 
            // buttonScreenshots
            // 
            this.buttonScreenshots.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonScreenshots.Location = new System.Drawing.Point(30, 485);
            this.buttonScreenshots.Margin = new System.Windows.Forms.Padding(30, 0, 30, 150);
            this.buttonScreenshots.Name = "buttonScreenshots";
            this.buttonScreenshots.Size = new System.Drawing.Size(240, 61);
            this.buttonScreenshots.TabIndex = 1;
            this.buttonScreenshots.Text = "Скриншоты";
            this.buttonScreenshots.UseVisualStyleBackColor = true;
            this.buttonScreenshots.Click += new System.EventHandler(this.buttonScreenshots_Click);
            // 
            // labelYearName
            // 
            this.labelYearName.AutoSize = true;
            this.labelYearName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelYearName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelYearName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelYearName.Location = new System.Drawing.Point(330, 0);
            this.labelYearName.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelYearName.Name = "labelYearName";
            this.labelYearName.Size = new System.Drawing.Size(246, 97);
            this.labelYearName.TabIndex = 2;
            this.labelYearName.Text = "Год производства:";
            this.labelYearName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCountryName
            // 
            this.labelCountryName.AutoSize = true;
            this.labelCountryName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCountryName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCountryName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelCountryName.Location = new System.Drawing.Point(330, 97);
            this.labelCountryName.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelCountryName.Name = "labelCountryName";
            this.labelCountryName.Size = new System.Drawing.Size(246, 97);
            this.labelCountryName.TabIndex = 3;
            this.labelCountryName.Text = "Страна:";
            this.labelCountryName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelGenresName
            // 
            this.labelGenresName.AutoSize = true;
            this.labelGenresName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGenresName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGenresName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelGenresName.Location = new System.Drawing.Point(330, 194);
            this.labelGenresName.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelGenresName.Name = "labelGenresName";
            this.labelGenresName.Size = new System.Drawing.Size(246, 97);
            this.labelGenresName.TabIndex = 4;
            this.labelGenresName.Text = "Жанры:";
            this.labelGenresName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelAgeName
            // 
            this.labelAgeName.AutoSize = true;
            this.labelAgeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAgeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAgeName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelAgeName.Location = new System.Drawing.Point(330, 291);
            this.labelAgeName.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelAgeName.Name = "labelAgeName";
            this.labelAgeName.Size = new System.Drawing.Size(246, 97);
            this.labelAgeName.TabIndex = 5;
            this.labelAgeName.Text = "Возраст:";
            this.labelAgeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTimeName
            // 
            this.labelTimeName.AutoSize = true;
            this.labelTimeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTimeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTimeName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelTimeName.Location = new System.Drawing.Point(330, 388);
            this.labelTimeName.Margin = new System.Windows.Forms.Padding(30, 0, 3, 0);
            this.labelTimeName.Name = "labelTimeName";
            this.labelTimeName.Size = new System.Drawing.Size(246, 97);
            this.labelTimeName.TabIndex = 6;
            this.labelTimeName.Text = "Продолжительность:";
            this.labelTimeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDesc
            // 
            this.labelDesc.AutoSize = true;
            this.tableLayoutPanelFilm.SetColumnSpan(this.labelDesc, 2);
            this.labelDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelDesc.Location = new System.Drawing.Point(330, 515);
            this.labelDesc.Margin = new System.Windows.Forms.Padding(30, 30, 3, 0);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(526, 181);
            this.labelDesc.TabIndex = 7;
            this.labelDesc.Text = "Описание";
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelYear.Location = new System.Drawing.Point(582, 0);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(274, 97);
            this.labelYear.TabIndex = 8;
            this.labelYear.Text = "label";
            this.labelYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCountry
            // 
            this.labelCountry.AutoSize = true;
            this.labelCountry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCountry.Location = new System.Drawing.Point(582, 97);
            this.labelCountry.Name = "labelCountry";
            this.labelCountry.Size = new System.Drawing.Size(274, 97);
            this.labelCountry.TabIndex = 9;
            this.labelCountry.Text = "label";
            this.labelCountry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelGenres
            // 
            this.labelGenres.AutoSize = true;
            this.labelGenres.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGenres.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGenres.Location = new System.Drawing.Point(582, 194);
            this.labelGenres.Name = "labelGenres";
            this.labelGenres.Size = new System.Drawing.Size(274, 97);
            this.labelGenres.TabIndex = 10;
            this.labelGenres.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelAge
            // 
            this.labelAge.AutoSize = true;
            this.labelAge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAge.Location = new System.Drawing.Point(582, 291);
            this.labelAge.Name = "labelAge";
            this.labelAge.Size = new System.Drawing.Size(274, 97);
            this.labelAge.TabIndex = 11;
            this.labelAge.Text = "label";
            this.labelAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTime.Location = new System.Drawing.Point(582, 388);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(274, 97);
            this.labelTime.TabIndex = 12;
            this.labelTime.Text = "label";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormFilm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 772);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.tableLayoutPanelBottom);
            this.Controls.Add(this.tableLayoutPanelTop);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(975, 811);
            this.Name = "FormFilm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Название фильма";
            this.Load += new System.EventHandler(this.FormFilm_Load);
            this.tableLayoutPanelTop.ResumeLayout(false);
            this.tableLayoutPanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelFilm.ResumeLayout(false);
            this.tableLayoutPanelFilm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottom;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFilm;
        private System.Windows.Forms.PictureBox pictureBoxCover;
        private System.Windows.Forms.Button buttonScreenshots;
        private System.Windows.Forms.Label labelYearName;
        private System.Windows.Forms.Label labelCountryName;
        private System.Windows.Forms.Label labelGenresName;
        private System.Windows.Forms.Label labelAgeName;
        private System.Windows.Forms.Label labelTimeName;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.Label labelCountry;
        private System.Windows.Forms.Label labelGenres;
        private System.Windows.Forms.Label labelAge;
        private System.Windows.Forms.Label labelTime;
    }
}

