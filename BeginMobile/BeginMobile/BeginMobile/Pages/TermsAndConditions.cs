using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class TermsAndConditions : ContentPage
    {
        public TermsAndConditions(bool isLoadByLogin)
        {
            Title = "Terms And Conditions";
            var labelBody = new Label
            {
                Text = "Terms And Conditions",
                Style = App.Styles.BodyStyle
            };

            if (isLoadByLogin == true)
            {                
                var btBack = new Button
                {
                    Text = "Go back",
                    Style = App.Styles.DefaultButton    
                };

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
                       labelBody,
                       btBack
                    }
                };
            }
            else
            {
                Content = new StackLayout
                {
                    Spacing = 20,
                    Padding = 50,
                    Children =
                    {
                        labelBody
                    }
                };
            }
        }
    }
}
