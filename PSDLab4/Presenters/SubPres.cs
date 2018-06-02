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
        public event EventHandler ChangedAccount;
        private IModel model;
        private readonly IInputView view;
        private int id;

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

        public void Run(params string[] data)
        {
            if(data.Length > 0)
            {
                view.AddToChangeButton();
                id = Convert.ToInt32(data[0]);
                view.FullName = data[1];
                view.Balance = data[2];
                view.Status = data[3];
                view.RegistrationDate = data[4];
            }
            else
            {
                view.ChangeToAddButton();
                view.FullName = "";
                view.Balance = "";
                view.Status = "";
                view.RegistrationDate = "";
            }
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
                view.Hide();
            }
        }

        private void ChangeInDB(object sender, EventArgs e)
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
                model.ChangeAccount(id, view.FullName, view.Balance, view.Status, view.RegistrationDate);
                ChangedAccount?.Invoke(this, null);
                view.Hide();
            }
        }
    }
}
