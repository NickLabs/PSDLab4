using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Infrastructure;
using View.ViewInterfaces;

namespace Presenters.Presenters
{
    public class Pres
    {
        private readonly IMainView view;
        private readonly IModel model;

        public Pres(IMainView view, IModel model)
        {
            this.view = view;
            this.model = model;
        }

        public void Run()
        {
            view.Start();
        }

        public void ShowAccounts()
        {
            DataTable table = model.GetAccounts();
            
        }
    }
}
