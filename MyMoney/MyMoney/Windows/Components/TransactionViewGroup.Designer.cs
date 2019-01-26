namespace MyMoney.Windows.Components
{
    partial class TransactionViewGroup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.scrollBar = new System.Windows.Forms.VScrollBar();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.transactionView1 = new MyMoney.Windows.Components.TransactionView();
            this.transactionView2 = new MyMoney.Windows.Components.TransactionView();
            this.transactionView3 = new MyMoney.Windows.Components.TransactionView();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollBar
            // 
            this.scrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollBar.LargeChange = 1;
            this.scrollBar.Location = new System.Drawing.Point(408, 0);
            this.scrollBar.Margin = new System.Windows.Forms.Padding(3);
            this.scrollBar.Name = "scrollBar";
            this.scrollBar.Size = new System.Drawing.Size(18, 88);
            this.scrollBar.TabIndex = 30;
            this.scrollBar.ValueChanged += new System.EventHandler(this.ScrollBarValueChanged);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Controls.Add(this.transactionView1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.transactionView2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.transactionView3, 0, 2);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(405, 88);
            this.tableLayoutPanel.TabIndex = 34;
            // 
            // transactionView1
            // 
            this.transactionView1.Location = new System.Drawing.Point(0, 0);
            this.transactionView1.Margin = new System.Windows.Forms.Padding(0);
            this.transactionView1.MinimumSize = new System.Drawing.Size(398, 30);
            this.transactionView1.Name = "transactionView1";
            this.transactionView1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.transactionView1.Size = new System.Drawing.Size(405, 30);
            this.transactionView1.TabIndex = 31;
            // 
            // transactionView2
            // 
            this.transactionView2.Location = new System.Drawing.Point(0, 29);
            this.transactionView2.Margin = new System.Windows.Forms.Padding(0);
            this.transactionView2.MinimumSize = new System.Drawing.Size(398, 30);
            this.transactionView2.Name = "transactionView2";
            this.transactionView2.Size = new System.Drawing.Size(405, 30);
            this.transactionView2.TabIndex = 32;
            // 
            // transactionView3
            // 
            this.transactionView3.Location = new System.Drawing.Point(0, 58);
            this.transactionView3.Margin = new System.Windows.Forms.Padding(0);
            this.transactionView3.MinimumSize = new System.Drawing.Size(398, 30);
            this.transactionView3.Name = "transactionView3";
            this.transactionView3.Size = new System.Drawing.Size(405, 30);
            this.transactionView3.TabIndex = 33;
            // 
            // TransactionViewGroup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.scrollBar);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "TransactionViewGroup";
            this.Size = new System.Drawing.Size(428, 88);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar scrollBar;
        private TransactionView transactionView1;
        private TransactionView transactionView2;
        private TransactionView transactionView3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    }
}
