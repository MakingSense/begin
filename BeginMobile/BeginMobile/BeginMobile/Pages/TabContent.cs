using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class TabContent : ContentPage
    {
        public TabContent(string title, View content)
        {
            this.Title = title;
            this.Padding = new Thickness(0, 0, 0, 0);
            this.Content = new ScrollView()
            {
                Content = content
            };
        }
    }
}
