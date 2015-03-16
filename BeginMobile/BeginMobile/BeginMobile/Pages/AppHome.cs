using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class AppHome: TabbedPage
    {
        public AppHome()
        {
            this.Title = "Home";
            this.Padding = new Thickness(0,0,0,0);


            var labelA = new Label()
            {
                Text = "Home"
            };

            var labelB = new Label()
            {
                Text = "Groups"
            };

            var labelC = new Label()
            {
                Text = "Other"
            };

            var labelM = new Label()
            {
                Text = "Sub Options"
            };

            this.Children.Add(new TabContent("A", labelA));
            this.Children.Add(new TabContent("B", labelB));
            this.Children.Add(new TabContent("C", labelC));
            this.Children.Add(new TabContent("M", labelM));


            /*this.Children.Add(new ContentPage()
            {
                Title = "A",
                Content = new Label()
                {
                    Text = "Home"
                }
            });

            this.Children.Add(new ContentPage()
            {
                Title = "B",
                Content = new Label()
                {
                    Text = "Groups"
                }
            });

            this.Children.Add(new ContentPage()
            {
                Title = "C",
                Content = new Label()
                {
                    Text = "Test C"
                }
            });

            this.Children.Add(new ContentPage()
            {
                Title = "M",
                Content = new Label()
                {
                    Text = "Test M"
                }
            });*/

        }
    }
}
