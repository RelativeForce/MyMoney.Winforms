namespace MyMoney.Windows
{
    partial class MonthlyAllowanceChanger
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
            this.label1 = new System.Windows.Forms.Label();
            this.newAllowanceBox = new System.Windows.Forms.TextBox();
            this.recommendButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.oldAllowanceBox = new System.Windows.Forms.TextBox();
            this.submit = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.month = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Monthly Allowance (£):";
            // 
            // newAllowanceBox
            // 
            this.newAllowanceBox.Location = new System.Drawing.Point(154, 36);
            this.newAllowanceBox.Name = "newAllowanceBox";
            this.newAllowanceBox.Size = new System.Drawing.Size(82, 20);
            this.newAllowanceBox.TabIndex = 1;
            this.newAllowanceBox.MouseHover += new System.EventHandler(this.showAmountToolTip);
            // 
            // recommendButton
            // 
            this.recommendButton.Location = new System.Drawing.Point(242, 36);
            this.recommendButton.Name = "recommendButton";
            this.recommendButton.Size = new System.Drawing.Size(86, 20);
            this.recommendButton.TabIndex = 2;
            this.recommendButton.Text = "Recommend";
            this.recommendButton.UseVisualStyleBackColor = true;
            this.recommendButton.Click += new System.EventHandler(this.recommendButton_Click);
            this.recommendButton.MouseHover += new System.EventHandler(this.showRecommendToolTip);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "New Monthly Allowance (£):";
            // 
            // oldAllowanceBox
            // 
            this.oldAllowanceBox.Enabled = false;
            this.oldAllowanceBox.Location = new System.Drawing.Point(154, 10);
            this.oldAllowanceBox.Name = "oldAllowanceBox";
            this.oldAllowanceBox.Size = new System.Drawing.Size(82, 20);
            this.oldAllowanceBox.TabIndex = 4;
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(242, 62);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(86, 20);
            this.submit.TabIndex = 5;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(154, 62);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(82, 20);
            this.cancel.TabIndex = 6;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // month
            // 
            this.month.CustomFormat = "MM - yyyy";
            this.month.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.month.Location = new System.Drawing.Point(242, 10);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(86, 20);
            this.month.TabIndex = 7;
            this.month.Value = new System.DateTime(2017, 8, 15, 12, 59, 2, 0);
            this.month.ValueChanged += new System.EventHandler(this.month_ValueChanged);
            // 
            // MonthlyAllowanceChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 95);
            this.Controls.Add(this.month);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.oldAllowanceBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.recommendButton);
            this.Controls.Add(this.newAllowanceBox);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(359, 134);
            this.MinimumSize = new System.Drawing.Size(359, 134);
            this.Name = "MonthlyAllowanceChanger";
            this.Text = "Change Monthly Allowance";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MonthlyAllowanceChanger_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newAllowanceBox;
        private System.Windows.Forms.Button recommendButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox oldAllowanceBox;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.DateTimePicker month;
    }
}