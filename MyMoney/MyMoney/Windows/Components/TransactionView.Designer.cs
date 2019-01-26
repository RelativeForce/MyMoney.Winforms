using System;

namespace MyMoney.Windows.Components
{
    partial class TransactionView
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
            this.delete = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.date = new MyMoney.Windows.Components.TransactionTextBox();
            this.description = new MyMoney.Windows.Components.TransactionTextBox();
            this.amount = new MyMoney.Windows.Components.TransactionTextBox();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // delete
            // 
            this.delete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.delete.ForeColor = System.Drawing.Color.Red;
            this.delete.Location = new System.Drawing.Point(367, 0);
            this.delete.Margin = new System.Windows.Forms.Padding(0);
            this.delete.MaximumSize = new System.Drawing.Size(38, 30);
            this.delete.MinimumSize = new System.Drawing.Size(38, 30);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(38, 30);
            this.delete.TabIndex = 28;
            this.delete.Text = "X";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.Delete_Btn_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.Controls.Add(this.date, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.description, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.amount, 2, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(367, 30);
            this.tableLayoutPanel.TabIndex = 32;
            // 
            // date
            // 
            this.date.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.date.HasChanged = false;
            this.date.Location = new System.Drawing.Point(0, 0);
            this.date.Margin = new System.Windows.Forms.Padding(0);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(146, 30);
            this.date.TabIndex = 29;
            // 
            // description
            // 
            this.description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.description.HasChanged = false;
            this.description.Location = new System.Drawing.Point(146, 0);
            this.description.Margin = new System.Windows.Forms.Padding(0);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(146, 30);
            this.description.TabIndex = 30;
            this.description.Load += new System.EventHandler(this.description_Load);
            // 
            // amount
            // 
            this.amount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.amount.HasChanged = false;
            this.amount.Location = new System.Drawing.Point(292, 0);
            this.amount.Margin = new System.Windows.Forms.Padding(0);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(75, 30);
            this.amount.TabIndex = 31;
            // 
            // TransactionView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.delete);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TransactionView";
            this.Size = new System.Drawing.Size(405, 31);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TransactionTextBox amount;
        private TransactionTextBox description;
        private TransactionTextBox date;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    }
}
