using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View.ViewInterfaces;

namespace View.Forms
{
    public partial class ResearcherForm : Form, IResearcherForm
    {
        public string ChosenMaterial{ get {return this.materialType.SelectedItem.ToString();}}
        public double[] Properties
        {
            set
            {
                Label[] temporary = { density, heatCapacity, meltingTemperature };
                Properties = value;
                for(int i = 0; i < temporary.Length; i++)
                {
                    temporary[i].Text = Properties[i].ToString();
                }
            }
            get
            {
                return Properties;
            }
        }

        public double[] Coefficients
        {
            set
            {
                Label[] temporary = { consistencyRatio, viscosityCoefficient, reductionTemperature, flowIndex, heatTransferCoefficient };
                Coefficients = value;
                for (int i = 0; i < temporary.Length; i++)
                {
                    temporary[i].Text = Coefficients[i].ToString();
                }
            }
            get
            {
                return Coefficients;
            }
        }

        public double[] CanalGeometry
        {
            get
            {
                double[] temporary = { Convert.ToDouble(length.Text), Convert.ToDouble(width.Text), Convert.ToDouble(depth.Text) };
                return temporary;
            }
            set
            {
                CanalGeometry = value;
            }
        }

        public double[] VariableParams
        {
            get
            {
                double[] temporary = { Convert.ToDouble(capSpeed.Text), Convert.ToDouble(temperature.Text) };
                return temporary;
            }
            set
            {
                VariableParams = value;
            }
        }
        public int NumberOfSteps
        {
            get
            {
                return Convert.ToInt32(this.stepCanal.Text);
            }
        }

        public event EventHandler calculate;
        public event EventHandler materialChanged;

        public ResearcherForm()
        {
            InitializeComponent();
        }

        public void Start(string name, string[] materialNames)
        {
            this.nameSurname.Text = name;
            this.materialType.Items.AddRange(materialNames);
            this.Show();
        }

        private void KeyPressHandle(object sender, KeyPressEventArgs e)
        {      
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }      
        }

        private void LengthLeave(object sender, EventArgs e)
        {
            if (!length.Text.Equals("") && !stepCanal.Text.Equals(""))
            {
                stepValue.Text = Math.Round(Convert.ToDouble(length.Text) / Convert.ToDouble(stepCanal.Text),2).ToString();
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
                else
                {
                    stepValue.Text = Math.Round(Convert.ToDouble(length.Text) / tmp , 2).ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Шаг должен быть целым числом", "Неправильное количество шагов", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextChangedHandle(object sender, EventArgs e)
        {
            if((sender as TextBox).Text.Split('.')[0].StartsWith("00"))
            {
                (sender as TextBox).Text = "0";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if((!length.Text.Equals("") && !width.Text.Equals("") && !depth.Text.Equals("") && materialType.Text.Equals("") &&
                !capSpeed.Text.Equals("") && !temperature.Text.Equals("") && !stepCanal.Text.Equals("")))
            {
                double[] temporaryGeometry = {Convert.ToDouble(this.length.Text), Convert.ToDouble(this.width.Text),
                    Convert.ToDouble(this.depth.Text) };
                double[] temporaryVar = {Convert.ToDouble(this.capSpeed.Text), Convert.ToDouble(this.temperature.Text),
                    Convert.ToDouble(this.stepCanal.Text) };
                VariableParams = temporaryVar;
                CanalGeometry = temporaryGeometry;
                this.calculate?.Invoke(this, null);
            }
        }

        private void MaterialTypeSelected(object sender, EventArgs e)
        {
            if(materialType.SelectedValue.ToString().Equals("") || materialType.SelectedText.Equals(""))
            {
                MessageBox.Show("Не выбран материал", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                materialChanged?.Invoke(this, null);
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
            foreach(int variable in variablesWithErrors)
            {
                switch (variable)
                {
                    case 0:
                        errorMessage += string.Format("Длина не должна быть больше {0} или меньше {1}\n", minLimits[0], maxLimits[0]);
                        break;
                    case 1:
                        errorMessage += string.Format("Ширина не должна быть больше {0} или меньше {1}\n", minLimits[1], maxLimits[1]);
                        break;
                    case 2:
                        errorMessage += string.Format("Глубина не должна быть больше {0} или меньше {1}\n", minLimits[2], maxLimits[2]);
                        break;
                    case 3:
                        errorMessage += string.Format("Скорость крышки не должна быть больше {0} или меньше {1}\n", minLimits[3], maxLimits[3]);
                        break;
                    case 4:
                        errorMessage += string.Format("Температура не должна быть больше {0} или меньше {1}\n", minLimits[4], maxLimits[4]);
                        break;
                }
            }
            errorMessage += "Пожалуйста, скорректируйте значения";
            MessageBox.Show(errorMessage, "Недопустимые значения параметров", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void DivideByZeroError()
        {
            MessageBox.Show("В процессе вычислений с данными параметрами возникла ситуация деления на 0", "Деление на ноль", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
