using System;
using System.Collections.Generic;
using System.Data;
using DomainModel.Infrastructure;
using View.ViewInterfaces;
using PSDLab4.Presenters;

namespace Presenters.Presenters
{
    /*
    public class Pres
    {
        private readonly IAdminForm view;
        private readonly IModel model;
        private readonly SubPres sub;

        public Pres(IAdminForm view, IModel model, SubPres sub)
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
            sub.ChangedAccount += Sub_ChangedAccount;
            view.Add += View_Add;
            view.Change += View_Change;
            view.Delete += View_Delete;
            view.New += View_New;
            view.Open += View_Open;
            view.Help += View_Help;
            view.Save += View_Save;
        }

        #region бинд действий на события
        private void View_Save(object sender, EventArgs e)
        {
            string path = view.OpenFileDialogForSavingEntetiesInfo();
            if(path != "")
            {
                model.SaveDBinTxt(path);
            }
            else
            {
                view.WrongFileSelection();
            }
        }

        private void View_Open(object sender, EventArgs e)
        {
            string pathToDb = view.OpenFileDialogForChangingDB();
            if (pathToDb != "")
            {
                model.OpenDb(pathToDb);
            }
            else
            {
                view.WrongFileSelection();
            }
            ShowAccounts();
        }

        private void View_New(object sender, EventArgs e)
        {
            string pathToDb = view.OpenFileDialogForCreatingDB();
            if (pathToDb != "")
            {
                model.CreateDb(pathToDb);
            }
            else
            {
                view.WrongFileSelection();
            }
            ShowAccounts();
        }

        private void View_Delete(object sender, EventArgs e)
        {
            try
            {
                model.DeleteAccount(Convert.ToInt32(view.IdValue));
            }
            catch
            {
                view.RowSelectionErrorMessage();
            }
            ShowAccounts();
        }

        public void View_Change(object sender, EventArgs e)
        {
            if (!view.IdValue.Equals(""))
            {
                sub.Run(view.IdValue, view.NameValue, view.BalanceValue, view.StatusValue, view.RegistrationDateValue);
            }
            else
            {
                view.RowSelectionErrorMessage();
            }
        }

        private void View_Add(object sender, EventArgs e)
        {
            sub.Run();
        }

        private void View_Help(object sender, EventArgs e)
        {
            view.Greetings();
        }

        private void Sub_ChangedAccount(object sender, EventArgs e)
        {
            ShowAccounts();
        }

        private void Sub_AddedAccount(object sender, EventArgs e)
        {
            ShowAccounts();
        }


        #endregion

        public void Run()
        {
            view.Start();
        }

        public void ShowAccounts()
        {
            view.ShowAccounts(model.GetAccounts());
        }

    }
    */
}

