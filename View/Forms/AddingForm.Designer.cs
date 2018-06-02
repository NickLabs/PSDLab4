namespace View.Forms
{
    partial class AddingForm
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
            this.NameTxt = new System.Windows.Forms.TextBox();
            this.BalanceTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StatusTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DateTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AddChangeBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ФИО";
            // 
            // NameTxt
            // 
            this.NameTxt.Location = new System.Drawing.Point(84, 10);
            this.NameTxt.Name = "NameTxt";
            this.NameTxt.Size = new System.Drawing.Size(124, 20);
            this.NameTxt.TabIndex = 1;
            // 
            // BalanceTxt
            // 
            this.BalanceTxt.Location = new System.Drawing.Point(84, 45);
            this.BalanceTxt.Name = "BalanceTxt";
            this.BalanceTxt.Size = new System.Drawing.Size(124, 20);
            this.BalanceTxt.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Баланс";
            // 
            // StatusTxt
            // 
            this.StatusTxt.Location = new System.Drawing.Point(84, 80);
            this.StatusTxt.Name = "StatusTxt";
            this.StatusTxt.Size = new System.Drawing.Size(124, 20);
            this.StatusTxt.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Статус";
            // 
            // DateTxt
            // 
            this.DateTxt.Location = new System.Drawing.Point(119, 116);
            this.DateTxt.Name = "DateTxt";
            this.DateTxt.Size = new System.Drawing.Size(89, 20);
            this.DateTxt.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Дата оформления";
            // 
            // AddChangeBut
            // 
            this.AddChangeBut.Location = new System.Drawing.Point(133, 148);
            this.AddChangeBut.Name = "AddChangeBut";
            this.AddChangeBut.Size = new System.Drawing.Size(75, 23);
            this.AddChangeBut.TabIndex = 9;
            this.AddChangeBut.Text = "Изменить";
            this.AddChangeBut.UseVisualStyleBackColor = true;
            this.AddChangeBut.Click += new System.EventHandler(this.AddChangeBut_Click);
            // 
            // AddingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 182);
            this.ControlBox = false;
            this.Controls.Add(this.AddChangeBut);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DateTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StatusTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BalanceTxt);
            this.Controls.Add(this.NameTxt);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddingForm";
            this.Text = "Добавление";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NameTxt;
        private System.Windows.Forms.TextBox BalanceTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox StatusTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DateTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button AddChangeBut;
    }
}