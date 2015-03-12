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
					        Text = "Global InSight Software is a software development company that helps to build advanced solutions for companies" +
					               " in different industries from marketing, financial services to network management, enterprise technology providers" +
					               " and other sectors, all over the globe. "
					    }
				}
            };
        }
    }
}
