using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class TermsAndConditions : ContentPage
    {
        public TermsAndConditions()
        {
            Title = "Terms And Conditions";

            Button btBack = null;

            
            btBack = new Button { Text = "Go back", BackgroundColor = Color.FromHex("77D065") };
            btBack.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<ContentPage>(this, "Register");
            };

            


            Content = new StackLayout
            {
                Spacing = 20,
                Padding = 50,
                Children =
                                  {
                                      new Label {Text = "Terms And Conditions"},
                                      btBack
                                  }
            };
        }
    }
}
