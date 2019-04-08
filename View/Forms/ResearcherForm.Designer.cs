namespace View.Forms
{
    partial class ResearcherForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.depth = new System.Windows.Forms.TextBox();
            this.width = new System.Windows.Forms.TextBox();
            this.length = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.meltingTemperature = new System.Windows.Forms.Label();
            this.heatCapacity = new System.Windows.Forms.Label();
            this.density = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.step = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.temperature = new System.Windows.Forms.TextBox();
            this.capSpeed = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.heatTransferCoefficient = new System.Windows.Forms.Label();
            this.flowIndex = new System.Windows.Forms.Label();
            this.reductionTemperature = new System.Windows.Forms.Label();
            this.viscosityCoefficient = new System.Windows.Forms.Label();
            this.consistencyRatio = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.materialType = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label17 = new System.Windows.Forms.Label();
            this.nameSurname = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выбор материала";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.depth);
            this.groupBox1.Controls.Add(this.width);
            this.groupBox1.Controls.Add(this.length);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 116);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Геометрические параметры канала";
            // 
            // depth
            // 
            this.depth.Location = new System.Drawing.Point(217, 84);
            this.depth.Name = "depth";
            this.depth.Size = new System.Drawing.Size(100, 20);
            this.depth.TabIndex = 5;
            this.depth.TextChanged += new System.EventHandler(this.TextChangedHandle);
            this.depth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressHandle);
            // 
            // width
            // 
            this.width.Location = new System.Drawing.Point(217, 55);
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(100, 20);
            this.width.TabIndex = 4;
            this.width.TextChanged += new System.EventHandler(this.TextChangedHandle);
            this.width.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressHandle);
            // 
            // length
            // 
            this.length.Location = new System.Drawing.Point(217, 26);
            this.length.Name = "length";
            this.length.Size = new System.Drawing.Size(100, 20);
            this.length.TabIndex = 3;
            this.length.TextChanged += new System.EventHandler(this.TextChangedHandle);
            this.length.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressHandle);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Глубина, м";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Ширина, м";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Длина, м";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.meltingTemperature);
            this.groupBox2.Controls.Add(this.heatCapacity);
            this.groupBox2.Controls.Add(this.density);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(345, 116);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры свойств материала";
            // 
            // meltingTemperature
            // 
            this.meltingTemperature.AutoSize = true;
            this.meltingTemperature.Location = new System.Drawing.Point(326, 73);
            this.meltingTemperature.Name = "meltingTemperature";
            this.meltingTemperature.Size = new System.Drawing.Size(13, 13);
            this.meltingTemperature.TabIndex = 5;
            this.meltingTemperature.Text = "0";
            // 
            // heatCapacity
            // 
            this.heatCapacity.AutoSize = true;
            this.heatCapacity.Location = new System.Drawing.Point(326, 51);
            this.heatCapacity.Name = "heatCapacity";
            this.heatCapacity.Size = new System.Drawing.Size(13, 13);
            this.heatCapacity.TabIndex = 4;
            this.heatCapacity.Text = "0";
            // 
            // density
            // 
            this.density.AutoSize = true;
            this.density.Location = new System.Drawing.Point(326, 29);
            this.density.Name = "density";
            this.density.Size = new System.Drawing.Size(13, 13);
            this.density.TabIndex = 3;
            this.density.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(151, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Температура плавления, °C ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(198, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Удельная теплоёмкость, Дж/( кг*°C )";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Плотность кг/м³";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.step);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.trackBar1);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Location = new System.Drawing.Point(9, 72);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(355, 301);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Варьируемые параметры модели";
            // 
            // step
            // 
            this.step.AutoSize = true;
            this.step.Location = new System.Drawing.Point(316, 258);
            this.step.Name = "step";
            this.step.Size = new System.Drawing.Size(13, 13);
            this.step.TabIndex = 6;
            this.step.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 246);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 26);
            this.label15.TabIndex = 4;
            this.label15.Text = "Шаг по \r\nдлине канала";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(101, 246);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(198, 45);
            this.trackBar1.TabIndex = 3;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.temperature);
            this.groupBox5.Controls.Add(this.capSpeed);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Location = new System.Drawing.Point(6, 141);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(323, 99);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Режимные параметры";
            // 
            // temperature
            // 
            this.temperature.Location = new System.Drawing.Point(217, 59);
            this.temperature.Name = "temperature";
            this.temperature.Size = new System.Drawing.Size(100, 20);
            this.temperature.TabIndex = 3;
            this.temperature.TextChanged += new System.EventHandler(this.TextChangedHandle);
            this.temperature.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressHandle);
            // 
            // capSpeed
            // 
            this.capSpeed.Location = new System.Drawing.Point(217, 26);
            this.capSpeed.Name = "capSpeed";
            this.capSpeed.Size = new System.Drawing.Size(100, 20);
            this.capSpeed.TabIndex = 2;
            this.capSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressHandle);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 61);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Темература, °C";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(176, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Скорость движения крышки, м/с";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.heatTransferCoefficient);
            this.groupBox4.Controls.Add(this.flowIndex);
            this.groupBox4.Controls.Add(this.reductionTemperature);
            this.groupBox4.Controls.Add(this.viscosityCoefficient);
            this.groupBox4.Controls.Add(this.consistencyRatio);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(6, 141);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(345, 158);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Эмпирические коэффициенты математической модели";
            // 
            // heatTransferCoefficient
            // 
            this.heatTransferCoefficient.AutoSize = true;
            this.heatTransferCoefficient.Location = new System.Drawing.Point(326, 117);
            this.heatTransferCoefficient.Name = "heatTransferCoefficient";
            this.heatTransferCoefficient.Size = new System.Drawing.Size(13, 13);
            this.heatTransferCoefficient.TabIndex = 10;
            this.heatTransferCoefficient.Text = "0";
            // 
            // flowIndex
            // 
            this.flowIndex.AutoSize = true;
            this.flowIndex.Location = new System.Drawing.Point(326, 95);
            this.flowIndex.Name = "flowIndex";
            this.flowIndex.Size = new System.Drawing.Size(13, 13);
            this.flowIndex.TabIndex = 9;
            this.flowIndex.Text = "0";
            // 
            // reductionTemperature
            // 
            this.reductionTemperature.AutoSize = true;
            this.reductionTemperature.Location = new System.Drawing.Point(326, 73);
            this.reductionTemperature.Name = "reductionTemperature";
            this.reductionTemperature.Size = new System.Drawing.Size(13, 13);
            this.reductionTemperature.TabIndex = 8;
            this.reductionTemperature.Text = "0";
            // 
            // viscosityCoefficient
            // 
            this.viscosityCoefficient.AutoSize = true;
            this.viscosityCoefficient.Location = new System.Drawing.Point(326, 51);
            this.viscosityCoefficient.Name = "viscosityCoefficient";
            this.viscosityCoefficient.Size = new System.Drawing.Size(13, 13);
            this.viscosityCoefficient.TabIndex = 7;
            this.viscosityCoefficient.Text = "0";
            // 
            // consistencyRatio
            // 
            this.consistencyRatio.AutoSize = true;
            this.consistencyRatio.Location = new System.Drawing.Point(326, 29);
            this.consistencyRatio.Name = "consistencyRatio";
            this.consistencyRatio.Size = new System.Drawing.Size(13, 13);
            this.consistencyRatio.TabIndex = 5;
            this.consistencyRatio.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(250, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Коэффициент теплоотдачи от крышки, Вт/(м²*с)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(146, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Индекс течения материала";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Температура привидения,  °C";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(238, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Температурный коэффициент вязкости, 1/°C";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Коэффициент консистенции, Па*сⁿ";
            // 
            // materialType
            // 
            this.materialType.FormattingEnabled = true;
            this.materialType.Location = new System.Drawing.Point(110, 35);
            this.materialType.Name = "materialType";
            this.materialType.Size = new System.Drawing.Size(121, 21);
            this.materialType.TabIndex = 5;
            this.materialType.SelectedValueChanged += new System.EventHandler(this.MaterialTypeSelected);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox2);
            this.groupBox6.Controls.Add(this.groupBox4);
            this.groupBox6.Location = new System.Drawing.Point(402, 72);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(364, 301);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Неизменямые параметры модели";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(344, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Расчёт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.справкаToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(766, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(402, 37);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Вы вошли как: ";
            // 
            // nameSurname
            // 
            this.nameSurname.AutoSize = true;
            this.nameSurname.Location = new System.Drawing.Point(492, 38);
            this.nameSurname.Name = "nameSurname";
            this.nameSurname.Size = new System.Drawing.Size(34, 13);
            this.nameSurname.TabIndex = 10;
            this.nameSurname.Text = "ФИО";
            // 
            // ResearcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 450);
            this.Controls.Add(this.nameSurname);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.materialType);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ResearcherForm";
            this.Text = "Flow model";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox depth;
        private System.Windows.Forms.TextBox width;
        private System.Windows.Forms.TextBox length;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label meltingTemperature;
        private System.Windows.Forms.Label heatCapacity;
        private System.Windows.Forms.Label density;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox capSpeed;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox materialType;
        private System.Windows.Forms.Label heatTransferCoefficient;
        private System.Windows.Forms.Label flowIndex;
        private System.Windows.Forms.Label reductionTemperature;
        private System.Windows.Forms.Label viscosityCoefficient;
        private System.Windows.Forms.Label consistencyRatio;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox temperature;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.Label step;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label nameSurname;
    }
}