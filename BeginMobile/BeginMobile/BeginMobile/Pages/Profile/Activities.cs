using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Activities : ScrollView
    {
        
        public Activities(User user)
        {

            StackLayout layoutActivities = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 50,
            };

            for (int i = 0; i <= 5; i++)
            {
                layoutActivities.Children.Add(new TableView
                {
                    HeightRequest = 180,
                    Root = new TableRoot
		                                                 {
		                                                     new TableSection
		                                                     {
		                                                         new ImageCell
		                                                         {
		                                                             ImageSource =
		                                                                 ImageSource.FromFile("userdefault3.png"),
		                                                             Text = user.DisplayName,
		                                                             //"Sara Gomez joined the group Clases de tenis Personalizados"
		                                                             Detail = user.Email, //"6 min ago"

		                                                         }
		                                                     },
		                                                 }
                });
                layoutActivities.Children.Add(new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children =
		                                          {
		                                              new Button
		                                              {
		                                                  Text = "comment"
		                                              },
		                                              new Button
		                                              {
		                                                  Text = "delete"
		                                              },
		                                              new Button
		                                              {
		                                                  Text = "Public"
		                                              },

		                                          }
                });
            }

            Content = layoutActivities;

        }

    }

}
