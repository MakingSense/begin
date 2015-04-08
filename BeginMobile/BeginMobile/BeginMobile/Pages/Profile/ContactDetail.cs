using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using BeginMobile.Utils;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ContactDetail : ContentPage
    {
        private const string UserDefault = "userdefault3.png";
        private readonly Contact _contact;
        private readonly LoginUser _loginUser;

        public ContactDetail(Contact contact, LoginUser loginUser)
        {
            _contact = contact;
            _loginUser = loginUser;

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

            var buttonCancelFriend = new Button
                                     {
                                         Text = AppResources.ButtonCancelRequestFriend
                                     };

            var buttonAcceptFriend = new Button
                                     {
                                         Text = AppResources.ButtonAcceptFriend
                                     };

            var buttonRemoveFriend = new Button
                                     {
                                         Text = AppResources.ButtonRemoveFriend
                                     };

            var buttonRejectFriend = new Button
                                     {
                                         Text = AppResources.ButtonRejectFriend
                                     };

            buttonAddFriend.Clicked += AddFriendEventHandler;
            buttonCancelFriend.Clicked += CancelFriendEventHandler;
            buttonAcceptFriend.Clicked += AcceptFriendEventHandler;
            buttonRejectFriend.Clicked += RejectFriendEventHandler;
            buttonRemoveFriend.Clicked += RemoveEventHandler;

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
            gridButtons.Children.Add(buttonRemoveFriend, 1, 0);

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

        #region Events
        private void AddFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var responseErrors = FriendshipAction(Friendship.Send);
            if (!responseErrors.Any())
            {
                //TODO: Logic here
                return;
            }
            DisplayResponseErrors(responseErrors);
        }
        private void AcceptFriendEventHandler(object senser, EventArgs eventArgs)
        {
            var responseErrors = FriendshipAction(Friendship.Accept);
            if (!responseErrors.Any())
            {
                //TODO: Logic here
                return;
            }
            DisplayResponseErrors(responseErrors);
        }
        private void RejectFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var responseErrors = FriendshipAction(Friendship.Reject);
            if (!responseErrors.Any())
            {
                //TODO: Logic here
                return;
            }
            DisplayResponseErrors(responseErrors);
        }
        private void CancelFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var responseErrors = FriendshipAction(Friendship.Remove);
            if (!responseErrors.Any())
            {
                //TODO: Logic here
                return;
            }
            DisplayResponseErrors(responseErrors);
        }
        private void RemoveEventHandler(object sender, EventArgs eventArgs)
        {
            var responseErrors = FriendshipAction(Friendship.Remove);
            if (!responseErrors.Any())
            {
                //TODO: Logic here
                return;
            }
            DisplayResponseErrors(responseErrors);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Execute friendship action.
        /// </summary>/
        /// <param name="friendship"></param>
        private List<ContactServiceError> FriendshipAction(Friendship friendship)
        {
            var responseErrors = new List<ContactServiceError>();

            switch (friendship)
            {
                case Friendship.Send:
                    responseErrors = App.ProfileServices.SendRequest(_loginUser.AuthToken, _contact.UserName);
                    break;

                case Friendship.Cancel:
                    //TODO: Cancel request here
                    //responseErrors = App.ProfileServices.CancelFriendship(_loginUser.AuthToken, _contact.UserName);
                    break;

                case Friendship.Accept:
                    responseErrors = App.ProfileServices.AcceptRequest(_loginUser.AuthToken, _contact.UserName);
                    break;

                case Friendship.Reject:
                    responseErrors = App.ProfileServices.RejectRequest(_loginUser.AuthToken, _contact.UserName);
                    break;

                case Friendship.Remove:
                    responseErrors = App.ProfileServices.RemoveFriendship(_loginUser.AuthToken, _contact.UserName);
                    break;
            }

            return responseErrors;
        }
        private void DisplayResponseErrors(IEnumerable<ContactServiceError> addResponseErrors)
        {
            var message = addResponseErrors.Aggregate(string.Empty,
                (current, contactServiceError) => current + (contactServiceError.Message + "\n"));

            DisplayAlert("Error", message, "ok");
        }

        #endregion
    }
}