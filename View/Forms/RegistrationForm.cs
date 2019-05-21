using System;
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
            return login.Text;
        }

        public string GetPassword()
        {
            return password.Text;
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
            button1.Enabled=false;
            login.Leave += Login_Leave;
            password.Leave += Password_Leave;
        }

        private void Password_Leave(object sender, EventArgs e)
        {
            if (password.Text.Equals(pass) && adminLogIsCorrect)
            {
                authentificationAttempt?.Invoke(this, null);
            }
            else if(password.Text.Equals(pass))
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
            if (login.Text.Equals(log) && adminPassIsCorrect)
            {
                authentificationAttempt?.Invoke(this, null);
            }
            else if(login.Text.Equals(log))
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
            catch (InvalidOperationException e)
            {
                Visible = true;
            }
        }

        public void Stop()
        {
            Visible=false;
            login.Text = "";
            password.Text = "";
        }
    }
}
