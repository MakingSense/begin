using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class AboutUs : ContentPage
    {
        public AboutUs()
        {
            Title = "About Us";
            Content = new StackLayout()
            {
                Spacing = 20,
                Padding = 50,
                Children = {

					new Label
					    {
					        Text = ""
					    }
				}
            };
        }
    }
}
