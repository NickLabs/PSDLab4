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
    public partial class RegistrationForm : Form, IRegistrationForm
    {

        public RegistrationForm()
        {
            InitializeComponent();
        }

        public string login => throw new NotImplementedException();

        public string password => throw new NotImplementedException();

        public event EventHandler authentificationAttempt;

        public void AuthentificationFail(int tries_left)
        {
            throw new NotImplementedException();
        }

        public void DeactivateLoginFunctionality(string adminLogin, string adminPass)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            authentificationAttempt?.Invoke(this, null);
        }

    }
}
