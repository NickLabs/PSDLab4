using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.ViewInterfaces
{
    public interface IRegistrationForm
    {
        string GetLogin();
        string GetPassword();
        event EventHandler authentificationAttempt;
        void AuthentificationFail(int tries_left);
        void DeactivateLoginFunctionality(string adminLogin, string adminPass);
        void Start();
        void Stop();
    }
}
