using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.ViewInterfaces
{
    public interface IRegistrationForm
    {
        string login { get; }
        string password { get; }
        event EventHandler authentificationAttempt;
        void AuthentificationFail(int tries_left);
        void DeactivateLoginFunctionality(string adminLogin, string adminPass);
        void Close();
    }
}
