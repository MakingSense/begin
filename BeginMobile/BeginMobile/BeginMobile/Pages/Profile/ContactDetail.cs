using System;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.Models;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ContactDetail : ContentPage
    {
        private const string UserDefault = "userdefault3.png";

        public ContactDetail(Contact contact)
        {
            var contact1 = contact;
            if (contact1 == null) throw new ArgumentNullException("contact1");

            var circleImageIcon = new CircleImage
                                  {
                                      HeightRequest = Device.OnPlatform(50, 100, 100),
                                      WidthRequest = Device.OnPlatform(50, 100, 100),
                                      Aspect = Aspect.AspectFill,
                                      HorizontalOptions = LayoutOptions.Start,
                                      BorderThickness = Device.OnPlatform(2, 3, 3),
                                      Source = UserDefault
                                  };
            var labelCompleteName = new Label
                                    {
                                        Text = contact1.NameSurname
                                    };

            var labelEmail = new Label
                             {
                                 Text = contact1.Email
                             };
            var labelUsername = new Label
                                {
                                    Text = contact1.Username
                                };
            var labelRegistered = new Label
                                  {
                                      Text = contact1.Registered
                                  };

            var buttonAddFriend = new Button
                                  {
                                      Text = "Add Friend"//AppResources.ButtonAddFriend
                                  };


            Content = new StackLayout
                      {
                          HorizontalOptions = LayoutOptions.FillAndExpand,
                          Orientation = StackOrientation.Vertical,
                          Children =
                          {
                              labelCompleteName,
                              circleImageIcon,
                              labelUsername,
                              labelEmail,
                              labelRegistered,
                              buttonAddFriend
                          }
                      };
        }
    }
}