using BeginMobile.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.UploadPages
{
    public class PictureUploader: ContentPage
    {
        public PictureUploader()
        {
            var user = (LoginUser) BeginApplication.Current.Properties["LoginUser"];

            Title = "Upload avatar";


            var buttonGoHome = new Button()
            {
                Text = "Go Home",
                Style = BeginApplication.Styles.DefaultButton
            };

            buttonGoHome.Clicked += (e, s) =>
            {
                BeginApplication.CurrentBeginApplication.ShowMainPage(user);
            };

            Content = new StackLayout()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    new Label()
                    {
                        Text = "Upload your Picture",
                    },
                    new Label()
                    {
                        Text = "a Good Looking One...."
                    },
                    buttonGoHome,
                }
            };
        }
    }
}
