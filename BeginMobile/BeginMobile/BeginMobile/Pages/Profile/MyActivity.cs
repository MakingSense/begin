using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;

namespace BeginMobile.Pages.Profile
{
    public class MyActivity : ContentPage
    {
        private const string userDefault = "userdefault3.png";

        public MyActivity()
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            var profileActivity = App.ProfileServices.GetActivities(currentUser.User.UserName, currentUser.AuthToken);

            Title = "My activity";

            Label header = new Label
            {
                Text = "My Activities",
                Style = App.Styles.TitleStyle,
                HorizontalOptions = LayoutOptions.Center
            };

            var listDataSource = new List<ActivityViewModel>();

            if (profileActivity != null)
            {
                foreach (var activity in profileActivity.Activities)
                {
                    if (activity.Component.Equals("activity", StringComparison.InvariantCultureIgnoreCase))
                    {
                        listDataSource.Add(new ActivityViewModel
                        {
                            Icon = userDefault,
                            NameSurname = profileActivity.NameSurname,
                            ActivityDescription = activity.Content,
                            ActivityType = activity.Type,
                            DateAndTime = activity.Date
                        });
                    }
                }
            }

            var listViewTemplate = new DataTemplate(typeof(Activities));
            var listViewActivities = new ListView
            {
                ItemsSource = listDataSource,
                ItemTemplate = listViewTemplate
            };

            listViewActivities.ItemSelected += (s, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                ((ListView)s).SelectedItem = null;
            };

            listViewActivities.HasUnevenRows = true;

            ScrollView scroll = new ScrollView()
            {

                Content = new StackLayout
                {
                    Spacing = 2,
                    VerticalOptions = LayoutOptions.Start,
                    //HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children =
                                  {
                                    listViewActivities
                                  }
                }
            };
            StackLayout stackLayout = new StackLayout
            {
                Spacing = 2,
                VerticalOptions = LayoutOptions.Start,
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                                  {
                                    scroll
                                  }
            };


            Content = stackLayout;


            //Content = new ScrollView
            //{               
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.StartAndExpand,
            //        Orientation = StackOrientation.Vertical,                                     
            //        Children = {                      
            //            listViewActivities
            //        }
            //    }
            //};
        }
    }
}
