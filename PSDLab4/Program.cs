using System;
using 
using System.Windows.Forms;
using Autofac;

namespace PSDLab4
{
    static class Program
    {
        private static IContainer _container;

        [STAThread]
        static void Main()
        {
            RegisterTypes();

            var presenter = _container.Resolve<Pres>();
            //presenter.view = _container.Resolve<MainForm>();
            //presenter.model = _container.Resolve<Model>();
            try
            {
                presenter.Run();
            }
            catch (Exception e)
            {
                Application.Exit();
            }
        }

        private static void RegisterTypes()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Model>().As<IModel>();
            builder.RegisterType<MainForm>().As<IView>();
            builder.RegisterType<Pres>();
            _container = builder.Build();
        }
    }
}
