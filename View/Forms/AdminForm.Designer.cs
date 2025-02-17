﻿namespace View.Forms
{
    partial class AdminForm
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
            this.tablesList = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.сменитьПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TableView = new System.Windows.Forms.DataGridView();
            this.Modify = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.DBRow = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.addChangeStatus = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).BeginInit();
            this.SuspendLayout();
            // 
            // tablesList
            // 
            this.tablesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tablesList.FormattingEnabled = true;
            this.tablesList.Location = new System.Drawing.Point(6, 36);
            this.tablesList.Name = "tablesList";
            this.tablesList.Size = new System.Drawing.Size(121, 21);
            this.tablesList.TabIndex = 0;
            this.tablesList.SelectedValueChanged += new System.EventHandler(this.TableChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сменитьПользователяToolStripMenuItem,
            this.помощьToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // сменитьПользователяToolStripMenuItem
            // 
            this.сменитьПользователяToolStripMenuItem.Name = "сменитьПользователяToolStripMenuItem";
            this.сменитьПользователяToolStripMenuItem.Size = new System.Drawing.Size(145, 20);
            this.сменитьПользователяToolStripMenuItem.Text = "Сменить пользователя";
            this.сменитьПользователяToolStripMenuItem.Click += new System.EventHandler(this.сменитьПользователяToolStripMenuItem_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.помощьToolStripMenuItem.Text = "Помощь";
            this.помощьToolStripMenuItem.Click += new System.EventHandler(this.помощьToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Таблица";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tablesList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 63);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выбор таблицы";
            // 
            // TableView
            // 
            this.TableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableView.Location = new System.Drawing.Point(12, 96);
            this.TableView.Name = "TableView";
            this.TableView.ReadOnly = true;
            this.TableView.RowHeadersWidth = 51;
            this.TableView.Size = new System.Drawing.Size(776, 223);
            this.TableView.TabIndex = 4;
            // 
            // Modify
            // 
            this.Modify.Location = new System.Drawing.Point(583, 325);
            this.Modify.Name = "Modify";
            this.Modify.Size = new System.Drawing.Size(124, 23);
            this.Modify.TabIndex = 6;
            this.Modify.Text = "Изменить/Добавить";
            this.Modify.UseVisualStyleBackColor = true;
            this.Modify.Click += new System.EventHandler(this.ModifyClick);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(713, 325);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 7;
            this.Delete.Text = "Удалить";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.DeleteClick);
            // 
            // DBRow
            // 
            this.DBRow.Location = new System.Drawing.Point(13, 355);
            this.DBRow.Name = "DBRow";
            this.DBRow.Size = new System.Drawing.Size(775, 83);
            this.DBRow.TabIndex = 8;
            this.DBRow.TabStop = false;
            this.DBRow.Text = "Выбранная строка";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 336);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Текущий режим работы: ";
            // 
            // addChangeStatus
            // 
            this.addChangeStatus.AutoSize = true;
            this.addChangeStatus.Location = new System.Drawing.Point(154, 336);
            this.addChangeStatus.Name = "addChangeStatus";
            this.addChangeStatus.Size = new System.Drawing.Size(0, 13);
            this.addChangeStatus.TabIndex = 10;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.addChangeStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DBRow);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.Modify);
            this.Controls.Add(this.TableView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminForm_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox tablesList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView TableView;
        private System.Windows.Forms.Button Modify;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.GroupBox DBRow;
        private System.Windows.Forms.ToolStripMenuItem сменитьПользователяToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label addChangeStatus;
    }
}