using System.ComponentModel;
using System.Windows.Forms;

namespace MyMoney.Windows
{
    partial class AddTransactionWindow
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
            this.descriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.amountTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            this.date = new System.Windows.Forms.DateTimePicker();
            this.toggleButton = new System.Windows.Forms.Button();
            this.outLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 77);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(260, 67);
            this.descriptionTextBox.TabIndex = 0;
            this.descriptionTextBox.Text = "";
            this.descriptionTextBox.MouseHover += new System.EventHandler(this.ShowDescriptionToolTip);
            // 
            // amountTextBox
            // 
            this.amountTextBox.Location = new System.Drawing.Point(157, 33);
            this.amountTextBox.Name = "amountTextBox";
            this.amountTextBox.Size = new System.Drawing.Size(115, 20);
            this.amountTextBox.TabIndex = 2;
            this.amountTextBox.MouseHover += new System.EventHandler(this.ShowAmountToolTip);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Amount (£):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Description:";
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(197, 150);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(75, 23);
            this.submit.TabIndex = 6;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // date
            // 
            this.date.Location = new System.Drawing.Point(79, 3);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(193, 20);
            this.date.TabIndex = 8;
            this.date.Value = new System.DateTime(2017, 6, 17, 9, 19, 8, 0);
            // 
            // toggleButton
            // 
            this.toggleButton.Font = new System.Drawing.Font("Stencil", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleButton.Location = new System.Drawing.Point(34, 32);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(20, 20);
            this.toggleButton.TabIndex = 9;
            this.toggleButton.Text = ">";
            this.toggleButton.UseVisualStyleBackColor = true;
            this.toggleButton.Click += new System.EventHandler(this.ToggleFlip);
            this.toggleButton.MouseHover += new System.EventHandler(this.ShowToggleToolTip);
            // 
            // outLabel
            // 
            this.outLabel.AutoSize = true;
            this.outLabel.Location = new System.Drawing.Point(60, 36);
            this.outLabel.Name = "outLabel";
            this.outLabel.Size = new System.Drawing.Size(24, 13);
            this.outLabel.TabIndex = 10;
            this.outLabel.Text = "Out";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "In";
            // 
            // AddTransactionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 190);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.outLabel);
            this.Controls.Add(this.toggleButton);
            this.Controls.Add(this.date);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.amountTextBox);
            this.Controls.Add(this.descriptionTextBox);
            this.MaximumSize = new System.Drawing.Size(300, 229);
            this.MinimumSize = new System.Drawing.Size(300, 229);
            this.Name = "AddTransactionWindow";
            this.Text = "Add Transaction";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddTransactionWindow_FormClosed);
            this.Load += new System.EventHandler(this.AddTransactionWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox descriptionTextBox;
        private TextBox amountTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button submit;
        private DateTimePicker date;
        private Button toggleButton;
        private Label outLabel;
        private Label label4;
    }
}