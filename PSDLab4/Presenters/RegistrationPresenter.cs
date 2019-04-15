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
        private readonly IContainer container;
        private int numberOfTries = 5;

        public RegistrationPresenter(IRegistrationForm registration, IDataBaseModel dataBase)
        {
            this.registration = registration;
            this.dataBase = dataBase;

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ResearcherPresenter>();
            builder.RegisterType<AdministratorPresenter>();
            builder.RegisterInstance(dataBase).As<IDataBaseModel>().ExternallyOwned();
            builder.RegisterType<IMathModel>().As<IMathModel>();
            container = builder.Build();
        }

        private void Close()
        {
            //Перед выходом, смотреть, если число попыток равно нулю, то бросать это число в базу
            this.registration.Close();
        }

        private void EventActivator()
        {
            this.registration.authentificationAttempt += AuthentificationAttempt;
        }

        private void AuthentificationAttempt(object sender, EventArgs e)
        {
            bool isSuccesful = true;
            bool isAdmin = false;
            bool[] temporary = this.dataBase.DoesUserExist(this.registration.login.ToLower(), this.registration.password);
            isSuccesful = temporary[0];
            isAdmin = temporary[1];           
            //Какой-то код с вызовом базы
            if (isSuccesful)
            {
                if (isAdmin)
                {
                    container.Resolve<AdministratorPresenter>().Start();
                    this.Close();
                }
                else
                {
                    container.Resolve<ResearcherPresenter>().Start(this.registration.login.ToLower());
                    this.Close();
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
