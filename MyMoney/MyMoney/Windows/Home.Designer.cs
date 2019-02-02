using MyMoney.Windows.Components;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MyMoney.Windows
{
   sealed partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.viewer = new MyMoney.Windows.Components.TransactionViewGroup();
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
            this.transactionsBox.Controls.Add(this.viewer);
            this.transactionsBox.Controls.Add(this.label3);
            this.transactionsBox.Controls.Add(this.label2);
            this.transactionsBox.Controls.Add(this.label1);
            this.transactionsBox.Location = new System.Drawing.Point(12, 284);
            this.transactionsBox.Name = "transactionsBox";
            this.transactionsBox.Size = new System.Drawing.Size(441, 132);
            this.transactionsBox.TabIndex = 14;
            this.transactionsBox.TabStop = false;
            // 
            // viewer
            // 
            this.viewer.Location = new System.Drawing.Point(7, 33);
            this.viewer.Name = "viewer";
            this.viewer.Size = new System.Drawing.Size(428, 92);
            this.viewer.TabIndex = 21;
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
            this.FileOptionMenu.Size = new System.Drawing.Size(463, 24);
            this.FileOptionMenu.TabIndex = 20;
            // 
            // FileOptionsBox
            // 
            this.FileOptionsBox.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importButton,
            this.createButton,
            this.aboutToolStripMenuItem});
            this.FileOptionsBox.Name = "FileOptionsBox";
            this.FileOptionsBox.Size = new System.Drawing.Size(37, 20);
            this.FileOptionsBox.Text = "File";
            // 
            // importButton
            // 
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(180, 22);
            this.importButton.Text = "Open";
            this.importButton.Click += new System.EventHandler(this.ImportDBFile);
            // 
            // createButton
            // 
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(180, 22);
            this.createButton.Text = "New";
            this.createButton.Click += new System.EventHandler(this.CreateDBFile);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "Info";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OpenAboutWindow);
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
            // Home
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(463, 427);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.addTransactionButton);
            this.Controls.Add(this.changeMonthlyAllowanceButtton);
            this.Controls.Add(this.monthPlot);
            this.Controls.Add(this.transactionsBox);
            this.Controls.Add(this.errorOutput);
            this.Controls.Add(this.FileOptionMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(479, 466);
            this.MinimumSize = new System.Drawing.Size(479, 466);
            this.Name = "Home";
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

        private Label errorOutput;
        private GroupBox transactionsBox;
        private Chart monthPlot;
        private Label label1;
        private Label label3;
        private Label label2;
        private Button changeMonthlyAllowanceButtton;
        private Button addTransactionButton;
        private MenuStrip FileOptionMenu;
        private ToolStripMenuItem FileOptionsBox;
        private ToolStripMenuItem importButton;
        private ToolStripMenuItem createButton;
        private Button rightButton;
        private Button leftButton;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private TransactionViewGroup viewer;
    }
}

