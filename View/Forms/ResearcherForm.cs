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
        public string ChosenMaterial{ get {return this.materialType.Text;}}
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
                double[] temporary = { Convert.ToDouble(width.Text), Convert.ToDouble(length.Text), Convert.ToDouble(depth.Text) };
                return temporary;
            }
        }

        public double[] VariableParams
        {
            get
            {
                double[] temporary = { Convert.ToDouble(capSpeed.Text), Convert.ToDouble(temperature.Text), Convert.ToDouble(step.Text) };
                return temporary;
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

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }      
        }

        private void TextChangedHandle(object sender, EventArgs e)
        {
            if((sender as TextBox).Text.Split('.')[0].Contains("00"))
            {
                (sender as TextBox).Text = "0";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Проверяем, есть ли нули в текстбооксах
            MessageBox.Show(Convert.ToDouble(temperature.Text).ToString());
            this.calculate?.Invoke(this, null);
        }

        private void MaterialTypeSelected(object sender, EventArgs e)
        {
            if((sender as ComboBox).Text.Equals(""))
            {
                MessageBox.Show("Элемент не был выбран", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                materialChanged?.Invoke(this, null);
            }
        }
    }
}
