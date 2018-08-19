namespace MyMoney.Windows
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.errorOutput = new System.Windows.Forms.Label();
            this.transactionsBox = new System.Windows.Forms.GroupBox();
            this.delete3 = new System.Windows.Forms.Button();
            this.amount3 = new System.Windows.Forms.RichTextBox();
            this.description3 = new System.Windows.Forms.RichTextBox();
            this.date3 = new System.Windows.Forms.RichTextBox();
            this.scrollBar = new System.Windows.Forms.VScrollBar();
            this.amount2 = new System.Windows.Forms.RichTextBox();
            this.amount1 = new System.Windows.Forms.RichTextBox();
            this.description2 = new System.Windows.Forms.RichTextBox();
            this.description1 = new System.Windows.Forms.RichTextBox();
            this.date2 = new System.Windows.Forms.RichTextBox();
            this.date1 = new System.Windows.Forms.RichTextBox();
            this.delete2 = new System.Windows.Forms.Button();
            this.delete1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.monthPlot = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.changeMonthlyAllowanceButtton = new System.Windows.Forms.Button();
            this.addTransactionButton = new System.Windows.Forms.Button();
            this.FileOptionMenu = new System.Windows.Forms.MenuStrip();
            this.FileOptionsBox = new System.Windows.Forms.ToolStripMenuItem();
            this.importButton = new System.Windows.Forms.ToolStripMenuItem();
            this.createButton = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateDBSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.transactionsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthPlot)).BeginInit();
            this.FileOptionMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorOutput
            // 
            this.errorOutput.AutoSize = true;
            this.errorOutput.Location = new System.Drawing.Point(12, 239);
            this.errorOutput.Name = "errorOutput";
            this.errorOutput.Size = new System.Drawing.Size(0, 13);
            this.errorOutput.TabIndex = 1;
            // 
            // transactionsBox
            // 
            this.transactionsBox.Controls.Add(this.delete3);
            this.transactionsBox.Controls.Add(this.amount3);
            this.transactionsBox.Controls.Add(this.description3);
            this.transactionsBox.Controls.Add(this.date3);
            this.transactionsBox.Controls.Add(this.scrollBar);
            this.transactionsBox.Controls.Add(this.amount2);
            this.transactionsBox.Controls.Add(this.amount1);
            this.transactionsBox.Controls.Add(this.description2);
            this.transactionsBox.Controls.Add(this.description1);
            this.transactionsBox.Controls.Add(this.date2);
            this.transactionsBox.Controls.Add(this.date1);
            this.transactionsBox.Controls.Add(this.delete2);
            this.transactionsBox.Controls.Add(this.delete1);
            this.transactionsBox.Controls.Add(this.label3);
            this.transactionsBox.Controls.Add(this.label2);
            this.transactionsBox.Controls.Add(this.label1);
            this.transactionsBox.Location = new System.Drawing.Point(12, 284);
            this.transactionsBox.Name = "transactionsBox";
            this.transactionsBox.Size = new System.Drawing.Size(441, 145);
            this.transactionsBox.TabIndex = 14;
            this.transactionsBox.TabStop = false;
            // 
            // delete3
            // 
            this.delete3.ForeColor = System.Drawing.Color.Red;
            this.delete3.Location = new System.Drawing.Point(371, 104);
            this.delete3.Name = "delete3";
            this.delete3.Size = new System.Drawing.Size(37, 30);
            this.delete3.TabIndex = 33;
            this.delete3.Text = "X";
            this.delete3.UseVisualStyleBackColor = true;
            // 
            // amount3
            // 
            this.amount3.Location = new System.Drawing.Point(294, 104);
            this.amount3.Name = "amount3";
            this.amount3.Size = new System.Drawing.Size(71, 30);
            this.amount3.TabIndex = 32;
            this.amount3.Text = "";
            this.amount3.Enter += new System.EventHandler(this.BeginUpdate);
            this.amount3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdateField);
            this.amount3.Leave += new System.EventHandler(this.EndUpdate);
            this.amount3.MouseHover += new System.EventHandler(this.DisplayUpdateTransactionToolTip);
            // 
            // description3
            // 
            this.description3.Location = new System.Drawing.Point(137, 104);
            this.description3.Name = "description3";
            this.description3.Size = new System.Drawing.Size(151, 30);
            this.description3.TabIndex = 31;
            this.description3.Text = "";
            this.description3.Enter += new System.EventHandler(this.BeginUpdate);
            this.description3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdateField);
            this.description3.Leave += new System.EventHandler(this.EndUpdate);
            this.description3.MouseHover += new System.EventHandler(this.DisplayUpdateTransactionToolTip);
            // 
            // date3
            // 
            this.date3.Location = new System.Drawing.Point(10, 104);
            this.date3.Name = "date3";
            this.date3.Size = new System.Drawing.Size(121, 30);
            this.date3.TabIndex = 30;
            this.date3.Text = "";
            this.date3.Enter += new System.EventHandler(this.BeginUpdate);
            this.date3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdateField);
            this.date3.Leave += new System.EventHandler(this.EndUpdate);
            this.date3.MouseHover += new System.EventHandler(this.DisplayUpdateTransactionToolTip);
            // 
            // scrollBar
            // 
            this.scrollBar.LargeChange = 1;
            this.scrollBar.Location = new System.Drawing.Point(411, 32);
            this.scrollBar.Name = "scrollBar";
            this.scrollBar.Size = new System.Drawing.Size(17, 100);
            this.scrollBar.TabIndex = 29;
            this.scrollBar.ValueChanged += new System.EventHandler(this.ScrollTransactions);
            // 
            // amount2
            // 
            this.amount2.Location = new System.Drawing.Point(294, 68);
            this.amount2.Name = "amount2";
            this.amount2.Size = new System.Drawing.Size(71, 30);
            this.amount2.TabIndex = 28;
            this.amount2.Text = "";
            this.amount2.Enter += new System.EventHandler(this.BeginUpdate);
            this.amount2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdateField);
            this.amount2.Leave += new System.EventHandler(this.EndUpdate);
            this.amount2.MouseHover += new System.EventHandler(this.DisplayUpdateTransactionToolTip);
            // 
            // amount1
            // 
            this.amount1.Location = new System.Drawing.Point(294, 32);
            this.amount1.Name = "amount1";
            this.amount1.Size = new System.Drawing.Size(71, 30);
            this.amount1.TabIndex = 27;
            this.amount1.Text = "";
            this.amount1.Enter += new System.EventHandler(this.BeginUpdate);
            this.amount1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdateField);
            this.amount1.Leave += new System.EventHandler(this.EndUpdate);
            this.amount1.MouseHover += new System.EventHandler(this.DisplayUpdateTransactionToolTip);
            // 
            // description2
            // 
            this.description2.Location = new System.Drawing.Point(137, 68);
            this.description2.Name = "description2";
            this.description2.Size = new System.Drawing.Size(151, 30);
            this.description2.TabIndex = 26;
            this.description2.Text = "";
            this.description2.Enter += new System.EventHandler(this.BeginUpdate);
            this.description2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdateField);
            this.description2.Leave += new System.EventHandler(this.EndUpdate);
            this.description2.MouseHover += new System.EventHandler(this.DisplayUpdateTransactionToolTip);
            // 
            // description1
            // 
            this.description1.Location = new System.Drawing.Point(137, 32);
            this.description1.Name = "description1";
            this.description1.Size = new System.Drawing.Size(151, 30);
            this.description1.TabIndex = 25;
            this.description1.Text = "";
            this.description1.Enter += new System.EventHandler(this.BeginUpdate);
            this.description1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdateField);
            this.description1.Leave += new System.EventHandler(this.EndUpdate);
            this.description1.MouseHover += new System.EventHandler(this.DisplayUpdateTransactionToolTip);
            // 
            // date2
            // 
            this.date2.Location = new System.Drawing.Point(10, 68);
            this.date2.Name = "date2";
            this.date2.Size = new System.Drawing.Size(121, 30);
            this.date2.TabIndex = 24;
            this.date2.Text = "";
            this.date2.Enter += new System.EventHandler(this.BeginUpdate);
            this.date2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdateField);
            this.date2.Leave += new System.EventHandler(this.EndUpdate);
            this.date2.MouseHover += new System.EventHandler(this.DisplayUpdateTransactionToolTip);
            // 
            // date1
            // 
            this.date1.Location = new System.Drawing.Point(10, 32);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(121, 30);
            this.date1.TabIndex = 23;
            this.date1.Text = "";
            this.date1.Enter += new System.EventHandler(this.BeginUpdate);
            this.date1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UpdateField);
            this.date1.Leave += new System.EventHandler(this.EndUpdate);
            this.date1.MouseHover += new System.EventHandler(this.DisplayUpdateTransactionToolTip);
            // 
            // delete2
            // 
            this.delete2.ForeColor = System.Drawing.Color.Red;
            this.delete2.Location = new System.Drawing.Point(371, 68);
            this.delete2.Name = "delete2";
            this.delete2.Size = new System.Drawing.Size(37, 30);
            this.delete2.TabIndex = 22;
            this.delete2.Text = "X";
            this.delete2.UseVisualStyleBackColor = true;
            this.delete2.Click += new System.EventHandler(this.DeleteTransaction);
            // 
            // delete1
            // 
            this.delete1.ForeColor = System.Drawing.Color.Red;
            this.delete1.Location = new System.Drawing.Point(371, 32);
            this.delete1.Name = "delete1";
            this.delete1.Size = new System.Drawing.Size(37, 30);
            this.delete1.TabIndex = 21;
            this.delete1.Text = "X";
            this.delete1.UseVisualStyleBackColor = true;
            this.delete1.Click += new System.EventHandler(this.DeleteTransaction);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(291, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Amount (£)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Date";
            // 
            // monthPlot
            // 
            chartArea1.Name = "ChartArea1";
            this.monthPlot.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.monthPlot.Legends.Add(legend1);
            this.monthPlot.Location = new System.Drawing.Point(12, 42);
            this.monthPlot.Name = "monthPlot";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.monthPlot.Series.Add(series1);
            this.monthPlot.Size = new System.Drawing.Size(441, 207);
            this.monthPlot.TabIndex = 18;
            this.monthPlot.Text = "chart1";
            // 
            // changeMonthlyAllowanceButtton
            // 
            this.changeMonthlyAllowanceButtton.Location = new System.Drawing.Point(22, 255);
            this.changeMonthlyAllowanceButtton.Name = "changeMonthlyAllowanceButtton";
            this.changeMonthlyAllowanceButtton.Size = new System.Drawing.Size(108, 23);
            this.changeMonthlyAllowanceButtton.TabIndex = 1;
            this.changeMonthlyAllowanceButtton.Text = "Change Allowance";
            this.changeMonthlyAllowanceButtton.UseVisualStyleBackColor = true;
            this.changeMonthlyAllowanceButtton.Click += new System.EventHandler(this.ChangeMonthlyAllowance);
            // 
            // addTransactionButton
            // 
            this.addTransactionButton.Location = new System.Drawing.Point(136, 255);
            this.addTransactionButton.Name = "addTransactionButton";
            this.addTransactionButton.Size = new System.Drawing.Size(108, 23);
            this.addTransactionButton.TabIndex = 0;
            this.addTransactionButton.Text = "Add Transaction";
            this.addTransactionButton.UseVisualStyleBackColor = true;
            this.addTransactionButton.Click += new System.EventHandler(this.AddTransaction);
            // 
            // FileOptionMenu
            // 
            this.FileOptionMenu.BackColor = System.Drawing.SystemColors.ControlLight;
            this.FileOptionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileOptionsBox});
            this.FileOptionMenu.Location = new System.Drawing.Point(0, 0);
            this.FileOptionMenu.Name = "FileOptionMenu";
            this.FileOptionMenu.Size = new System.Drawing.Size(465, 24);
            this.FileOptionMenu.TabIndex = 20;
            // 
            // FileOptionsBox
            // 
            this.FileOptionsBox.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importButton,
            this.createButton,
            this.aboutToolStripMenuItem,
            this.updateDBSchemaToolStripMenuItem});
            this.FileOptionsBox.Name = "FileOptionsBox";
            this.FileOptionsBox.Size = new System.Drawing.Size(37, 20);
            this.FileOptionsBox.Text = "File";
            // 
            // importButton
            // 
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(157, 22);
            this.importButton.Text = "Import";
            this.importButton.Click += new System.EventHandler(this.ImportDBFile);
            // 
            // createButton
            // 
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(157, 22);
            this.createButton.Text = "Create";
            this.createButton.Click += new System.EventHandler(this.CreateDBFile);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OpenAboutWindow);
            // 
            // updateDBSchemaToolStripMenuItem
            // 
            this.updateDBSchemaToolStripMenuItem.Name = "updateDBSchemaToolStripMenuItem";
            this.updateDBSchemaToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.updateDBSchemaToolStripMenuItem.Text = "Update Schema";
            this.updateDBSchemaToolStripMenuItem.Click += new System.EventHandler(this.UpdateDatabaseFile);
            // 
            // rightButton
            // 
            this.rightButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.rightButton.FlatAppearance.BorderSize = 0;
            this.rightButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.rightButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.rightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rightButton.Image = ((System.Drawing.Image)(resources.GetObject("rightButton.Image")));
            this.rightButton.Location = new System.Drawing.Point(410, 27);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(43, 36);
            this.rightButton.TabIndex = 21;
            this.rightButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.NextMonth);
            // 
            // leftButton
            // 
            this.leftButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.leftButton.FlatAppearance.BorderSize = 0;
            this.leftButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.leftButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
            this.leftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leftButton.Image = ((System.Drawing.Image)(resources.GetObject("leftButton.Image")));
            this.leftButton.Location = new System.Drawing.Point(12, 27);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(43, 36);
            this.leftButton.TabIndex = 22;
            this.leftButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.PreviousMonth);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(465, 441);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.addTransactionButton);
            this.Controls.Add(this.changeMonthlyAllowanceButtton);
            this.Controls.Add(this.monthPlot);
            this.Controls.Add(this.transactionsBox);
            this.Controls.Add(this.errorOutput);
            this.Controls.Add(this.FileOptionMenu);
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(481, 480);
            this.MinimumSize = new System.Drawing.Size(481, 480);
            this.Name = "Main_Form";
            this.Text = "MyMoney";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainClosed);
            this.Load += new System.EventHandler(this.LoadForm);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyPressed);
            this.transactionsBox.ResumeLayout(false);
            this.transactionsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthPlot)).EndInit();
            this.FileOptionMenu.ResumeLayout(false);
            this.FileOptionMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label errorOutput;
        private System.Windows.Forms.GroupBox transactionsBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart monthPlot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button changeMonthlyAllowanceButtton;
        private System.Windows.Forms.Button addTransactionButton;
        private System.Windows.Forms.Button delete1;
        private System.Windows.Forms.Button delete2;
        private System.Windows.Forms.RichTextBox amount2;
        private System.Windows.Forms.RichTextBox amount1;
        private System.Windows.Forms.RichTextBox description2;
        private System.Windows.Forms.RichTextBox description1;
        private System.Windows.Forms.RichTextBox date2;
        private System.Windows.Forms.RichTextBox date1;
        private System.Windows.Forms.MenuStrip FileOptionMenu;
        private System.Windows.Forms.ToolStripMenuItem FileOptionsBox;
        private System.Windows.Forms.ToolStripMenuItem importButton;
        private System.Windows.Forms.ToolStripMenuItem createButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button delete3;
        private System.Windows.Forms.RichTextBox amount3;
        private System.Windows.Forms.RichTextBox description3;
        private System.Windows.Forms.RichTextBox date3;
        private System.Windows.Forms.VScrollBar scrollBar;
        private System.Windows.Forms.ToolStripMenuItem updateDBSchemaToolStripMenuItem;
    }
}

