﻿using BeginMobile.LocalizeResources.Resources;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class Privacy : ContentPage
    {
        public Privacy()
        {
            Title = AppResources.PrivacyTitle;
            Content = new StackLayout
                      {
                          Spacing = 20,
                          Padding = 50,
                          Children = { new Label { Text = string.Empty } }
                      };
        }
    }
}