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
        
        public Activities()
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationActivities activitiesInformation = App.ProfileServices.GetActivities(currentUser.User.UserName, currentUser.AuthToken);

            StackLayout layoutActivities = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 20,
            };

            foreach (var activity in activitiesInformation.Activies)
            {
                if (activity != null)
                {
                    if (activity.Content != null)
                    {
                        if (activity.UserActivity != null)
                        {

                            layoutActivities.Children.Add(new TableView
                            {
                                HeightRequest = 180,
                                Root = new TableRoot
		                                                 {
		                                                     new TableSection(" ")
		                                                     {
		                                                         new ImageCell
		                                                         {
		                                                             ImageSource =
		                                                                 ImageSource.FromFile("userdefault3.png"),
		                                                             Text = activity.UserActivity.DisplayName.ToString() + " " + activity.Content.ToString(),
		                                                             //"Sara Gomez joined the group Clases de tenis Personalizados"
		                                                             Detail = activity.Date.ToString(), //"6 min ago"

		                                                         }
		                                                     },
		                                                 }
                            });
                            //layoutActivities.Children.Add(new StackLayout
                            //{
                            //    Orientation = StackOrientation.Horizontal,
                            //    HorizontalOptions = LayoutOptions.FillAndExpand,
                            //    Children =
                            //                      {
                            //                          new Button
                            //                          {
                            //                              Text = "comment"
                            //                          },
                            //                          new Button
                            //                          {
                            //                              Text = "delete"
                            //                          },
                            //                          new Button
                            //                          {
                            //                              Text = "Public"
                            //                          },

                            //                      }
                            //});
                        }
                    }
                }
            }
            Content = layoutActivities;

        }

    }

}
