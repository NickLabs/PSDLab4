using System;
using DomainModel.Infrastructure;
using DomainModel.Service;

using View.Forms;
using View.ViewInterfaces;
using System.Windows.Forms;
using Autofac;
using PSDLab4.Presenters;

namespace PSDLab4
{
    static class Program
    {
        private static IContainer _container;
        private static ResearcherPresenter researcherPresenter;
        private static AdministratorPresenter administratorPresenter;
        private static RegistrationPresenter registrationPresenter;

        private static void RegisterTypes()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AdministratorPresenter>();
            builder.RegisterType<RegistrationPresenter>();
            builder.RegisterType<ResearcherPresenter>();
            builder.RegisterType<AdminForm>().As<IAdminForm>();
            builder.RegisterType<ResearcherForm>().As<IResearcherForm>();
            builder.RegisterType<RegistrationForm>().As<IRegistrationForm>();
            builder.RegisterType<DatabaseParser>();
            builder.RegisterType<FlowModelDataBase>().As<IDataBaseModel>();
            builder.RegisterType<MathModel>().As<IMathModel>();
            _container = builder.Build();
        }

        [STAThread]
        static void Main()
        {
            RegisterTypes();

            researcherPresenter = _container.Resolve<ResearcherPresenter>();
            administratorPresenter = _container.Resolve<AdministratorPresenter>();//new AdministratorPresenter(new AdminForm(), new FlowModelDataBase(), new DatabaseParser());
            registrationPresenter = _container.Resolve<RegistrationPresenter>();

            researcherPresenter.changeUser += RegistrationFormOpen;
            administratorPresenter.changeUser += RegistrationFormOpen;
            registrationPresenter.adminPass += AdminFormOpen;
            registrationPresenter.researcehrPass += ResearcherFormOpen;

            registrationPresenter.Start();
        }

        private static void RegistrationFormOpen(object sender, EventArgs e)
        {
            registrationPresenter.Start();
        }

        private static void AdminFormOpen(object sender, EventArgs e)
        {
            administratorPresenter.Start();
        }

        private static void ResearcherFormOpen(object sender, EventArgs e)
        {
            researcherPresenter.Start(registrationPresenter.GetUserName()); 
        }
    }
}
