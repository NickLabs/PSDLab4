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
        public string FullName { get { return NameTxt.Text; } }
        public string Balance { get { return BalanceTxt.Text; } }
        public string Status { get { return StatusTxt.Text; } }
        public string RegistrationDate { get { return DateTxt.Text; } }
        public event EventHandler Add;
        public event EventHandler Change;

        public AddingForm()
        {
            InitializeComponent();
        }

        public void WrongInput()
        {

        }

        private void AddBut_Click(object sender, EventArgs e)
        {
            Add?.Invoke(this, null);
        }

        private void ChangeBut_Click(object sender, EventArgs e)
        {
            Change?.Invoke(this, null);
        }
    }
}
