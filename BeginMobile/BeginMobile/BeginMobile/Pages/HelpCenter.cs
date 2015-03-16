using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class HelpCenter : ContentPage
    {
        public HelpCenter()
        {
            Title = "Help Center";

            Content = new StackLayout
            {
                Spacing = 20,
                Padding = 50,
                Children = {
					new Label { Text = "" }
				}
            };
        }
    }
}
