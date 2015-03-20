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
        public MyActivity()
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];

            Title = "My activity";
            Label header = new Label
            {
                Text = "Activities",
                Font = Font.SystemFontOfSize(50, FontAttributes.Bold),
                HorizontalOptions = LayoutOptions.Center
            };

            var userImageActivity = new ImageCell
            {
                ImageSource =
                    ImageSource.FromFile("userdefault3.png"),
                Text = "What is the new," + currentUser.User.NiceName + "?",

            };

            var userInfoTableViewActivity = new TableView
            {
                HeightRequest = 300,
                Root = new TableRoot
                                                       {
                                                           new TableSection
                                                           {
                                                               userImageActivity
                                                           }
                                                       }
            };

            var activityEditor = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { userInfoTableViewActivity}
            };

            var activitiesScroollView = new Activities();
            var stackLayoutMain = new StackLayout
            {
                Orientation = StackOrientation.Vertical,

                Children =
                                      {
                                          header,
                                          activityEditor,
                                          activitiesScroollView
                                      }

            };

            Content = stackLayoutMain;
        }
    }
}
