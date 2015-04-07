using System;
using System.Linq;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ContactDetail : ContentPage
    {
        private const string UserDefault = "userdefault3.png";
        private readonly Contact _contact;

        public ContactDetail(Contact contact)
        {
            _contact = contact;
            if (_contact == null) throw new ArgumentNullException("contact1");

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
                Text = _contact.NameSurname
            };

            var labelEmail = new Label
            {
                Text = _contact.Email
            };
            var labelUsername = new Label
            {
                Text = _contact.UserName
            };
            var labelRegistered = new Label
            {
                Text = _contact.Registered
            };

            var buttonAddFriend = new Button
            {
                Text = AppResources.ButtonAddFriend
            };
            var buttonRemove = new Button
            {
                Text = "Remove" //TODO: Add to resources
            };
            buttonRemove.Clicked += RemoveEventHandler;

            var gridButtons = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto}
                                  },
                ColumnDefinitions =
                                  {
                                      new ColumnDefinition {Width = GridLength.Auto},
                                      new ColumnDefinition {Width = GridLength.Auto},
                                  }
            };

            gridButtons.Children.Add(buttonAddFriend, 0, 0);
            gridButtons.Children.Add(buttonRemove, 1, 0);

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
                              gridButtons
                          }
            };
        }

        private void RemoveEventHandler(object sender, EventArgs eventArgs)
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            var removeResponse = App.ProfileServices.RemoveFriendship(currentUser.AuthToken, _contact.UserName);
            if (removeResponse == null) return;
            var message = removeResponse.Aggregate("",
                (current, contactServiceError) => current + (contactServiceError.Message + "\n"));
            DisplayAlert("Error", message, "ok");
        }
    }
}