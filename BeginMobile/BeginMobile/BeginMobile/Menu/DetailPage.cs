using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BeginMobile.Menu
{
    public class DetailPage : ContentPage
        {
            public DetailPage()
            {
                Title = "HOME";
                Icon = null;

                BackgroundColor = Color.FromHex("8A8A8A");

                var myHomeHeader = new Label
                {
                    Text = "Lorem",
                    HorizontalOptions = LayoutOptions.Center
                };

                Content = new StackLayout
                {
                    Children =
                                  {
                                      myHomeHeader,
                                      new Label
                                          {
                                              Text = "Wellcome to Lorem Page"
                                          }
                                  },
                };
            }
        }
	}
