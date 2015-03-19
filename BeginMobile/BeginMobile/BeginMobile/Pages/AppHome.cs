using BeginMobile.Pages.GroupPages;
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
            this.LoadChilds();
        }

        public void LoadChilds()
        {
            this.Title = "Home";
            this.Padding = new Thickness(0, 0, 0, 0);

            this.Children.Add(new TabContent("", "padlock.png"));
            this.Children.Add(new TabContent("", "padlock.png"));
            this.Children.Add(new TabContent("", "padlock.png"));
            this.Children.Add(new GroupListPage("", "padlock.png"));
            this.Children.Add(new TabContent("", "menunav.png"));
        }
    }
}
