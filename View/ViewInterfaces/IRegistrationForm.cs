using System;

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
