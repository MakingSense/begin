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
            var currentUser = ((LoginUser)App.Current.Properties["LoginUser"]).User;

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
                Text = "What is the new," + currentUser.UserName + "?",

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
            var commentEditor = new Editor
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var activityEditor = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { userInfoTableViewActivity, commentEditor }
            };

            var activitiesScroollView = new Activities(currentUser);
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
