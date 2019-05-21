using DomainModel.Infrastructure;
using System;
using View.ViewInterfaces;

namespace PSDLab4.Presenters
{
    public class RegistrationPresenter
    {
        private readonly IRegistrationForm registration;
        private readonly IDataBaseModel dataBase;
        private int numberOfTries = 5;
        public event EventHandler adminPass;
        public event EventHandler researcehrPass;
        private string userName;


        public RegistrationPresenter(IRegistrationForm registration, IDataBaseModel dataBase)
        {
            this.registration = registration;
            this.dataBase = dataBase;
            this.registration.authentificationAttempt += AuthentificationAttempt;
        }

        public string GetUserName()
        {
            return userName;
        }

        public void Start()
        {
            registration.Start();
        }

        public void Close()
        {
            registration.Stop();
        }

        private void AuthentificationAttempt(object sender, EventArgs e)
        {
            bool isSuccesful = true;
            bool isAdmin = false;
            bool[] temporary = dataBase.DoesUserExist(registration.GetLogin().ToLower(), registration.GetPassword());
            isSuccesful = temporary[0];
            isAdmin = temporary[1];       
            
            //Какой-то код с вызовом базы
            if (isSuccesful)
            {
                userName = registration.GetLogin();
                if (isAdmin)
                {
                    Close();
                    adminPass?.Invoke(this, null);
                }
                else
                {
                    Close();
                    researcehrPass?.Invoke(this, null);  
                }
            }
            else
            {
                numberOfTries = numberOfTries > 0 ? numberOfTries-1 : 0;
                if (numberOfTries == 0)
                {
                    string[] tempLogPass = dataBase.GetUserLoginPassViaId(1).Split(' ');
                    string adminLogin = tempLogPass[0];
                    string adminPass = tempLogPass[1];

                    registration.DeactivateLoginFunctionality(adminLogin, adminPass);
                }
                else
                {
                    registration.AuthentificationFail(numberOfTries);
                }      
            }
        }
    }
}
