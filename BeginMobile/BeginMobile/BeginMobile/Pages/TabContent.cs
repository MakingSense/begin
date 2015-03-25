using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class TabContent : ContentPage
    {
        public TabContent(string title, string icon)
        {
            this.Title = title;
            this.Icon = icon;
            this.Padding = new Thickness(0, 0, 0, 0);

        }
    }
}
