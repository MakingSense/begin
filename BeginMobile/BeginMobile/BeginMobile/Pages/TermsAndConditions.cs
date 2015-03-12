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

            Content = new StackLayout
            {
                Spacing = 20,
                Padding = 50,
                Children =
                                  {
                                      new Label {Text = "Terms And Conditions"}
                                  }
            };
        }
    }
}
