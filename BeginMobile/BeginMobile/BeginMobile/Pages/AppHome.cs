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
                Text = "New Feed Body"
            };

            var labelB = new Label()
            {
                Text = "Message Body"
            };

            var labelC = new Label()
            {
                Text = "Notifications Body"
            };

            var labelD = new Label()
            {
                Text = "Groups Body"
            };

            var labelM = new Label()
            {
                Text = "Sub Options"
            };

            
            this.Children.Add(new TabContent("", "", labelA));
            this.Children.Add(new TabContent("", "", labelB));
            this.Children.Add(new TabContent("", "", labelC));
            this.Children.Add(new TabContent("", "", labelD));
            this.Children.Add(new TabContent("", "", labelM));

        }
    }
}
