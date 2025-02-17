﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using View.ViewInterfaces;

namespace View.Forms
{
    public partial class ResearcherForm : Form, IResearcherForm
    {
        public string ChosenMaterial { get { return materialType.SelectedItem.ToString(); } }
        private string Filename = "";
        public double[] GetCanalGeometry()
        {
            double[] results = { Convert.ToDouble(length.Text), Convert.ToDouble(width.Text), Convert.ToDouble(depth.Text) };
            return results;
        }

        public double[] GetVariableParams()
        {
            double[] results = {Convert.ToDouble(capSpeed.Text), Convert.ToDouble(temperature.Text),
                    Convert.ToDouble(stepCanal.Text) };
            return results;
        }

        public double[] GetParams()
        {
            double[] results = { Convert.ToDouble(density.Text), Convert.ToDouble(heatCapacity.Text), Convert.ToDouble(meltingTemperature.Text) };
            return results;
        }

        public double[] GetCoefs()
        {
            double[] results = { Convert.ToDouble(consistencyRatio.Text), Convert.ToDouble(viscosityCoefficient.Text), Convert.ToDouble(reductionTemperature.Text),
            Convert.ToDouble(flowIndex.Text), Convert.ToDouble(heatTransferCoefficient.Text)};
            return results;
        }

        public int NumberOfSteps
        {
            get
            {
                return Convert.ToInt32(stepCanal.Text);
            }
        }

        public event EventHandler generateReport;
        public event EventHandler calculate;
        public event EventHandler materialChanged;
        public event EventHandler changeUser;

        public ResearcherForm()
        {
            InitializeComponent();
        }

        public void Start(string name, string[] materialNames)
        {
            nameSurname.Text = name;
            materialType.Items.Clear();
            materialType.Items.AddRange(materialNames);
            materialType.Text = "Поливинилхлорид";
            materialChanged?.Invoke(this, null);
            length.Text = "9,5";
            width.Text = "2,5 ";
            depth.Text = "0,001";
            capSpeed.Text = "2";
            temperature.Text = "160";
            stepCanal.Text = "100";

            Visible = true;
        }
        
        public void Stop()
        {
            resultSet.Rows.Clear();
            Visible = false;
            materialType.Text = "";
            length.Text = "";
            width.Text = "";
            depth.Text = "";
            capSpeed.Text = "";
            temperature.Text = "";
            stepCanal.Text = "";
            density.Text = "0";
            heatCapacity.Text = "0";
            meltingTemperature.Text = "0";
            consistencyRatio.Text = "0";
            viscosityCoefficient.Text = "0";
            reductionTemperature.Text = "0";
            flowIndex.Text = "0";
            heatTransferCoefficient.Text = "0";
        }

        private void KeyPressHandle(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void StepCanalLeave(object sender, EventArgs e)
        {
            int tmp = 0;
            try
            {
                tmp = Convert.ToInt32(stepCanal.Text);
                if (length.Text.Equals(""))
                {

                }
                else if (tmp < 10 || tmp > 10000)
                {
                    MessageBox.Show("Шаг должен находится в диапозоне от 10 до 10000", "Неправильное количество шагов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Шаг должен быть целым числом", "Некорректные данные", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextChangedHandle(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Split('.')[0].StartsWith("00"))
            {
                (sender as TextBox).Text = "0";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!length.Text.Equals("") && !width.Text.Equals("") && !depth.Text.Equals("") && !materialType.Text.Equals("") &&
                !capSpeed.Text.Equals("") && !temperature.Text.Equals("") && !stepCanal.Text.Equals(""))
            {
                double[] temporaryGeometry = {Convert.ToDouble(length.Text), Convert.ToDouble(width.Text),
                    Convert.ToDouble(depth.Text) };
                double[] temporaryVar = {Convert.ToDouble(capSpeed.Text), Convert.ToDouble(temperature.Text),
                    Convert.ToDouble(stepCanal.Text) };

                calculate?.Invoke(this, null);
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля перед началом расчёта", "Заполнены не все поля", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }          
        }

        private void MaterialTypeSelected(object sender, EventArgs e)
        {
            if (materialType.Text.ToString().Equals("") || materialType.Text.Equals(""))
            {
                MessageBox.Show("Не выбран материал", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                materialChanged?.Invoke(this, null);
                length.Text = "9,5";
                width.Text = "2,5 ";
                depth.Text = "0,001";
                capSpeed.Text = "2";
                temperature.Text = "160";
                stepCanal.Text = "100";          
            }
        }

        private void MaterialTypeLeave(object sender, EventArgs e)
        {
            if (!materialType.Text.Equals(""))
            {
                materialChanged?.Invoke(this, null);
            }
            else
            {
                MessageBox.Show("Не выбран материал", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void VariableOutOfBounds(List<int> variablesWithErrors, double[] minLimits, double[] maxLimits)
        {
            string errorMessage = "";
            foreach (int variable in variablesWithErrors)
            {
                switch (variable)
                {
                    case -1:
                        Control[] tmp = { length, width, depth };
                        foreach(Control c in tmp)
                        {
                            if (c.Text.Equals("0"))
                            {
                                c.BackColor = Color.Red;
                            }
                        }
                        break;
                    case 0:
                        capSpeed.BackColor = Color.Red;
                        break;
                    case 1:
                        temperature.BackColor = Color.Red;
                        break;
                }
            }
            errorMessage += "Пожалуйста, скорректируйте значения, подсвеченные красным";
            MessageBox.Show(errorMessage, "Недопустимые значения параметров", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void DivideByZeroError()
        {
            MessageBox.Show("В процессе вычислений с данными параметрами возникла ситуация деления на 0", "Деление на ноль", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void SetData(double[] coefficients, double[] properties)
        {

            
            TextBox[] temporary = { consistencyRatio, viscosityCoefficient, reductionTemperature, flowIndex, heatTransferCoefficient };

            for (int i = 0; i < 5/*coefficients.Length*/; i++)
            {
                temporary[i].Text = coefficients[i].ToString();
            }

            TextBox[] temporary2 = { density, heatCapacity, meltingTemperature };
            for (int i = 0; i < 3/*properties.Length*/; i++)
            {
                temporary2[i].Text = properties[i].ToString();
            }
        }

        public void SetResults(double[] temperature, double[] viscosity, double length, double output, string time)
        {
            Control[] tmp = { this.length, width, depth };
            foreach (Control c in tmp)
            {
                c.BackColor = SystemColors.Window;
            }
            capSpeed.BackColor = SystemColors.Window;
            this.temperature.BackColor = SystemColors.Window;

            Stopwatch t = new Stopwatch();
            t.Start();

            double currentLength = 0;
            resultSet.Rows.Clear();
            double stepIncrease = length / Convert.ToInt32(stepCanal.Text);
            double[] steps = new double[temperature.Length];
            for(int i = 0; i < temperature.Length; i++)
            {
                if ((currentLength-0.001) > length)
                {
                    break;
                }
                resultSet.Rows.Add(Math.Round(currentLength,2), Math.Round(temperature[i],1), Math.Round(viscosity[i],0));
                steps[i] = currentLength;
                currentLength += stepIncrease;
            }
            resOutput.Text = Math.Round(output, 2).ToString();
            resTemperature.Text = Math.Round(temperature[temperature.Length-1], 1).ToString();
            resViscosity.Text = Math.Round(viscosity[viscosity.Length-1], 0).ToString();
            timeElapsed.Text = time;
            tabControl1.SelectTab(1);

            temperature=temperature.Select(x => Math.Round(x, 1)).ToArray();
            viscosity = viscosity.Select(x => Math.Round(x, 0)).ToArray();

            chartFromViscosity.ChartAreas[0].AxisY.Minimum = temperature.Min();
            chartFromViscosity.ChartAreas[0].AxisY.Maximum = temperature.Max();
            chartFromViscosity.ChartAreas[0].AxisX.Minimum = steps.Min();
            chartFromViscosity.Series["Температура, °C"].Points.DataBindXY(steps, temperature);
            chartFromLength.ChartAreas[0].AxisX.Minimum = steps.Min();
            chartFromLength.ChartAreas[0].AxisY.Minimum = viscosity.Min();
            chartFromLength.ChartAreas[0].AxisY.Maximum = viscosity.Max();
            chartFromLength.Series["Вязкость, Па * с"].Points.DataBindXY(steps, viscosity);
            t.Stop();
            visTime.Text = t.Elapsed.ToString().Split(':')[2];
        }

        private void GenerateReportClick(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "docx files (*.docx)|*.docx";
            save.CreatePrompt = true;
            save.OverwritePrompt = true;
            save.AddExtension = true;
            if (save.ShowDialog() == DialogResult.Cancel)
                return;
            Filename = save.FileName;

            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/tmp");
            chartFromLength.SaveImage(Directory.GetCurrentDirectory() + "/tmp/" + "1.png", ChartImageFormat.Png);
            chartFromViscosity.SaveImage(Directory.GetCurrentDirectory() + "/tmp/" + "2.png", ChartImageFormat.Png);

            generateReport?.Invoke(this, null);
        }

        public string GetFileName()
        {
            return Filename;
        }

        public DataTable GetDataForReport()
        {
            DataTable tmp = new DataTable();
            tmp.Columns.Add();
            tmp.Columns.Add();
            tmp.Columns.Add();
            for (int i = 0; i < resultSet.Rows.Count; i++)
            {
                object[] ttt = new object[resultSet.Rows[i].Cells.Count];
                for(int j = 0; j < resultSet.Rows[i].Cells.Count; j++)
                {
                    ttt[j] = resultSet.Rows[i].Cells[j].Value;
                }
                
                tmp.Rows.Add(ttt);
            }
            return tmp;
        }

        private void сменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Вы точно хотите выйти из данного меню?\nВсе несохранённые данные будут утрачены", "Подтвердите выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                changeUser?.Invoke(this, null);
            }
        }

        public double[] GetResults()
        {
            double[] res = { Convert.ToDouble(resOutput.Text), Convert.ToDouble(resTemperature.Text), Convert.ToDouble(resViscosity.Text) };
            return res;
        }

        private void СправкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Программу разработали студенты СПбГТИ(ТУ) 465 группы:\n" +
                "\tВинокуров Никита Александрович\n" +
                "\tТатаринцев Вадим Павлович\n" +
                "Под руководством:\n" +
                "\tПолосина Андрея Николаевича",
                "Справка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ПомощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Для корректной работы программы необходимо:\n" +
                "\t1)Выбрать материал\n" +
                "\t2)Ввести требуемые значения в поля ввода\n" +
                "\t3)Нажать кнопку \"Рассчитать\"",
                "Помощь",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void ResearcherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
