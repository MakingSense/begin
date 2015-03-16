using BeginMobile.Interfaces;
using BeginMobile.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class LoginModalPage: CarouselPage
    {
        ContentPage login, register, termsAndConditions, forgotPassword;
        public LoginModalPage(ILoginManager iloginManager)
        {
            bool isLoadByLogin = true;

            login = new Login(iloginManager);
            register = new Register(iloginManager);
            termsAndConditions = new TermsAndConditions(isLoadByLogin);
            forgotPassword = new ForgotPassword();

            this.Children.Add(login);
            this.Children.Add(register);
            this.Children.Add(termsAndConditions);
            this.Children.Add(forgotPassword);

            MessagingCenter.Subscribe<ContentPage>(this, "Login", (sender) =>
            {
                this.SelectedItem = login;
            });
            MessagingCenter.Subscribe<ContentPage>(this, "Register", (sender) =>
            {
                this.SelectedItem = register;
            });

            MessagingCenter.Subscribe<ContentPage>(this, "TermsAndConditions", (sender) =>
            {
                this.SelectedItem = termsAndConditions;
            });
            MessagingCenter.Subscribe<ContentPage>(this, "ForgotPassword", (sender) =>
            {
                this.SelectedItem = forgotPassword;
            });
 
        }
    }
}
