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
    public partial class AddingForm : Form, IInputView
    {
        public string FullName
        {
            get
            {
                return NameTxt.Text;
            }
            set
            {
                this.NameTxt.Text = value;
            }
        }
        public string Balance
        {
            get
            {
                return BalanceTxt.Text;
            }
            set
            {
                BalanceTxt.Text = value;
            }
        }
        public string Status
        {
            get
            {
                return StatusTxt.Text;
            }
            set
            {
                this.StatusTxt.Text = value;

            }
        }
        public string RegistrationDate
        {
            get
            {
                return DateTxt.Text;
            }
            set
            {
                this.DateTxt.Text = value;
            }
        }

        public event EventHandler Add;
        public event EventHandler Change;

        public AddingForm()
        {
            InitializeComponent();
        }

        public void WrongInput()
        {
            string caption = "Ошибка при вводе данных";
            string message = "Данные, либо не введены, либо выбран неправильный их формат - попробуйте ещё раз";
            MessageBox.Show(message, caption, MessageBoxButtons.OK);
        }

        public void ChangeToAddButton()
        {
            this.AddChangeBut.Text = "Добавить";
        }

        public void AddToChangeButton()
        {
            this.AddChangeBut.Text = "Изменить";
        }

        private void AddChangeBut_Click(object sender, EventArgs e)
        {
            if(this.AddChangeBut.Text == "Изменить")
            {
                Change?.Invoke(this, null);
            }
            else
            {
                Add?.Invoke(this, null);
            }
        }
    }
}
