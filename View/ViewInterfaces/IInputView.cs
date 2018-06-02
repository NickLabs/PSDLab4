using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.ViewInterfaces
{
    public interface IInputView
    {
        string FullName { get; set; }
        string Balance { get; set; }
        string Status { get; set; }
        string RegistrationDate { get; set; }
        event EventHandler Add;
        event EventHandler Change;
        void WrongInput();
        void Show();
        void Hide();
        void ChangeToAddButton();
        void AddToChangeButton();
    }
}
