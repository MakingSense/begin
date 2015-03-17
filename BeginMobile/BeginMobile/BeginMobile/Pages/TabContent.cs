using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class TabContent : ContentPage
    {
        public TabContent(string title, string icon, View content)
        {

            var lblTitle = new Label()
            {
                FontSize = 8,
                TextColor =  Color.Blue,
            };

            var buttonStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter {Property = Label.BackgroundColorProperty, Value = Color.Yellow}
                }
            };

            this.Title = title;
            this.Icon = icon;
            this.Style = buttonStyle;

            
            this.Padding = new Thickness(0, 0, 0, 0);
            this.Content = new ScrollView()
            {
                Content = content
            };
        }
    }
}
