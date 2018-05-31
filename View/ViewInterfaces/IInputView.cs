using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.ViewInterfaces
{
    public interface IInputView
    {
        string FullName { get; }
        string Balance { get; }
        string Status { get; }
        string RegistrationDate { get; }
        event EventHandler Add;
        event EventHandler Change;
        void WrongInput();
        void Show();
    }
}
