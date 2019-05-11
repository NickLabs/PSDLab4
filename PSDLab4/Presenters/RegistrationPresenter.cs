using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.ViewInterfaces;
using DomainModel.Infrastructure;
using Autofac;

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
            this.registration.Start();
        }

        public void Close()
        {
            this.registration.Stop();
        }

        private void AuthentificationAttempt(object sender, EventArgs e)
        {
            bool isSuccesful = true;
            bool isAdmin = false;
            bool[] temporary = this.dataBase.DoesUserExist(this.registration.GetLogin().ToLower(), this.registration.GetPassword());
            isSuccesful = temporary[0];
            isAdmin = temporary[1];       
            
            //Какой-то код с вызовом базы
            if (isSuccesful)
            {
                userName = this.registration.GetLogin();
                if (isAdmin)
                {
                    this.Close();
                    adminPass?.Invoke(this, null);
                }
                else
                {
                    this.Close();
                    researcehrPass?.Invoke(this, null);  
                }
            }
            else
            {
                numberOfTries = numberOfTries > 0 ? numberOfTries-1 : 0;
                if (numberOfTries == 0)
                {
                    string[] tempLogPass = this.dataBase.GetUserLoginPassViaId(1).Split(' ');
                    string adminLogin = tempLogPass[0];
                    string adminPass = tempLogPass[1];

                    this.registration.DeactivateLoginFunctionality(adminLogin, adminPass);
                }
                else
                {
                    this.registration.AuthentificationFail(numberOfTries);
                }      
            }
        }
    }
}
