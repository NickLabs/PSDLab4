using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Infrastructure;
using View.ViewInterfaces;

namespace PSDLab4.Presenters
{
    public class SubPres
    {
        public event EventHandler AddedAccount;
        private IModel model;
        private readonly IInputView view;

        public SubPres(IInputView view)
        {
            this.view = view;
        }

        public void SetModel(IModel model)
        {
            this.model = model;
            view.Add += AddToDB;
            view.Change += ChangeInDB;
        }

        public void Run()
        {
             view.Show();
        }

        private void AddToDB(object sender, EventArgs e)
        {
            bool isCorrectInput = true;
            try
            {
                int balance = Convert.ToInt32(view.Balance);
            }
            catch
            {
                view.WrongInput();
                isCorrectInput = false;
            }

            if (isCorrectInput)
            {
                model.AddAccount(view.FullName, view.Balance, view.Status, view.RegistrationDate);
                AddedAccount?.Invoke(this, null);
            }
        }

        private void ChangeInDB(object sender, EventArgs e)
        {

        }
    }
}
