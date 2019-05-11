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
        private string log;
        private string pass;
        private bool adminPassIsCorrect = false;
        private bool adminLogIsCorrect = false;
        public RegistrationForm()
        {
            InitializeComponent();
        }

        public string GetLogin()
        {
            return this.login.Text;
        }

        public string GetPassword()
        {
            return this.password.Text;
        }

        public event EventHandler authentificationAttempt;

        public void AuthentificationFail(int tries_left)
        {
            string msg = "Неправильно введённая связка логин-пароль\nОсталось попыток: " + tries_left.ToString();
            string capt = "Ошибка при входе";
            MessageBox.Show(msg, capt, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void DeactivateLoginFunctionality(string adminLogin, string adminPass)
        {
            log = adminLogin;
            pass = adminPass;
            this.button1.Enabled=false;
            login.Leave += Login_Leave;
            password.Leave += Password_Leave;
        }

        private void Password_Leave(object sender, EventArgs e)
        {
            if (password.Text.Equals(this.pass) && adminLogIsCorrect)
            {
                authentificationAttempt?.Invoke(this, null);
            }
            else if(password.Text.Equals(this.pass))
            {
                adminPassIsCorrect = true;
            }
            else
            {
                adminPassIsCorrect = false;
            }
        }

        private void Login_Leave(object sender, EventArgs e)
        {
            if (login.Text.Equals(this.log) && adminPassIsCorrect)
            {
                authentificationAttempt?.Invoke(this, null);
            }
            else if(login.Text.Equals(this.log))
            {
                adminLogIsCorrect = true;
            }
            else
            {
                adminLogIsCorrect = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            authentificationAttempt?.Invoke(this, null);
        }

        public void Start()
        {
            try
            {
                Application.Run(this);
                
            }
            catch (Exception)
            {
                this.Visible = true;
            }
        }

        public void Stop()
        {
            this.Visible=false;
            this.login.Text = "";
            this.password.Text = "";
        }
    }
}
