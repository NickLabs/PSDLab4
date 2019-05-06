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

        [STAThread]
        static void Main()
        {
            //RegisterTypes();

            //var presenter = _container.Resolve<Pres>();

            //try
            //{
            //    presenter.Run();
            // }
            // catch (Exception e)
            //{
            //   Application.Exit();
            //}
            var pres = new ResearcherPresenter(new ResearcherForm(), new MathModel() ,new FlowModelDataBase());
            //var pres = new AdministratorPresenter(new AdminForm(), new FlowModelDataBase(), new DatabaseParser());
            pres.Start("nick123");
        }

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
    }
}
