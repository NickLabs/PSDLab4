using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Infrastructure;
using View.ViewInterfaces;
using PSDLab4.Presenters;

namespace Presenters.Presenters
{
    public class Pres
    {
        private readonly IMainView view;
        private readonly IModel model;
        private readonly SubPres sub;

        public Pres(IMainView view, IModel model, SubPres sub)
        {
            this.view = view;
            this.model = model;
            this.sub = sub;        
            this.sub.SetModel(this.model);
            ButtonEventActivator();
            ShowAccounts();
        }

        private void ButtonEventActivator()
        {
            sub.AddedAccount += Sub_AddedAccount;
            view.Add += View_Add;
            view.Change += View_Change;
        }

        private void View_Change(object sender, EventArgs e)
        {
            
            sub.Run();
        }

        private void View_Add(object sender, EventArgs e)
        {
            sub.Run();
        }

        private void Sub_AddedAccount(object sender, EventArgs e)
        {
            ShowAccounts();
        }

        public void Run()
        {
            view.Start();
        }

        public void ShowAccounts()
        {
            view.ShowAccounts(model.GetAccounts());
        }

    }
}
