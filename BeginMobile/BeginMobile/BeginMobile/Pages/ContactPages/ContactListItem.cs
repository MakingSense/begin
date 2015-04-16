using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.Profile;
using BeginMobile.Services.DTO;
using BeginMobile.Utils;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.ContactPages
{
    public class ContactListItem : ViewCell
    {
        private Button _buttonAddFriend;
        private Button _buttonRemoveFriend;
        private Button _buttonCancelFriend;
        private Button _buttonAcceptFriend;
        private Button _buttonRejectFriend;

        private readonly LoginUser _loginUser;

        private static string Relationship { get; set; }

        private static readonly BindableProperty RelationshipProperty =
            BindableProperty.Create<ContactListItem, string>
                (getter => Relationship, string.Empty, BindingMode.TwoWay,
                    propertyChanging: (bindable, oldValue, newValue) =>
                                      {
                                          Relationship = newValue;
                                      });

        public ContactListItem(LoginUser loginUser)
        {
            _loginUser = loginUser;

            var circleIconImage = new CircleImage
                                  {
                                      HeightRequest = Device.OnPlatform(50, 100, 100),
                                      WidthRequest = Device.OnPlatform(50, 100, 100),
                                      Aspect = Aspect.AspectFill,
                                      HorizontalOptions = LayoutOptions.Start,
                                      BorderThickness = Device.OnPlatform(2, 3, 3)
                                  };

            circleIconImage.SetBinding(Image.SourceProperty, new Binding("Icon"));
            var optionLayout = CreateOptionLayout();

            View = new StackLayout
                   {
                       Orientation = StackOrientation.Horizontal,
                       Children =
                       {
                           circleIconImage,
                           optionLayout
                       }
                   };
        }

        private Grid CreateOptionLayout()
        {
            _buttonAddFriend = new Button
                               {
                                   Text = AppResources.ButtonAddFriend,
                                   Style = BeginApplication.Styles.ListViewItemButton,
                                   HorizontalOptions = LayoutOptions.Start,
                                   HeightRequest = 35,
                                   WidthRequest = 70
                               };

            _buttonRemoveFriend = new Button
                                  {
                                      Text = AppResources.ButtonRemoveFriend,
                                      Style = App.Styles.ListViewItemButton,
                                      HorizontalOptions = LayoutOptions.Start,
                                      HeightRequest = 35,
                                      WidthRequest = 70
                                  };

            _buttonCancelFriend = new Button
                                  {
                                      Text = "Cancel", //AppResources.ButtonCancelFriend,
                                      Style = App.Styles.ListViewItemButton,
                                      HorizontalOptions = LayoutOptions.Start,
                                      HeightRequest = 35,
                                      WidthRequest = 70
                                  };

            _buttonAcceptFriend = new Button
                                  {
                                      Text = "Accept", //AppResources.ButtonAcceptFriend,
                                      Style = App.Styles.ListViewItemButton,
                                      HorizontalOptions = LayoutOptions.Start,
                                      HeightRequest = 35,
                                      WidthRequest = 70
                                  };

            _buttonRejectFriend = new Button
                                  {
                                      Text = "Reject", //AppResources.ButtonRejectFriend,
                                      Style = App.Styles.ListViewItemButton,
                                      HorizontalOptions = LayoutOptions.Start,
                                      HeightRequest = 35,
                                      WidthRequest = 70
                                  };

            _buttonAddFriend.Clicked += AddFriendEventHandler;
            _buttonRemoveFriend.Clicked += RemoveFriendEventHandler;
            _buttonCancelFriend.Clicked += CancelFriendEventHandler;
            _buttonAcceptFriend.Clicked += AcceptFriendEventHandler;
            _buttonRejectFriend.Clicked += RejectFriendEventHandler;

            var labelNameSurname = new Label
                                   {
                                       HorizontalOptions = LayoutOptions.FillAndExpand,
                                       YAlign = TextAlignment.Center,
                                       Style = BeginApplication.Styles.ListItemTextStyle
                                   };

            var labelUserName = new Label
                                {
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    YAlign = TextAlignment.Center,
                                    Style = BeginApplication.Styles.ListItemDetailTextStyle
                                };

            var labelEmail = new Label
                             {
                                 HorizontalOptions = LayoutOptions.FillAndExpand,
                                 YAlign = TextAlignment.Center,
                                 Style = BeginApplication.Styles.ListItemDetailTextStyle
                             };


            labelNameSurname.SetBinding(Label.TextProperty, "NameSurname");
            labelUserName.SetBinding(Label.TextProperty, "UserName");
            labelEmail.SetBinding(Label.TextProperty, "Email");

            var grid = new Grid
                       {
                           Padding = BeginApplication.Styles.ListDetailThickness,
                           HorizontalOptions = LayoutOptions.FillAndExpand,
                           VerticalOptions = LayoutOptions.FillAndExpand,
                           RowDefinitions =
                           {
                               new RowDefinition {Height = GridLength.Auto},
                               new RowDefinition {Height = GridLength.Auto},
                               new RowDefinition {Height = GridLength.Auto},
                               new RowDefinition {Height = GridLength.Auto}
                           },
                           ColumnDefinitions =
                           {
                               new ColumnDefinition {Width = GridLength.Auto},
                               new ColumnDefinition {Width = GridLength.Auto}
                           }
                       };

            SetBinding(RelationshipProperty, new Binding("Relationship", BindingMode.TwoWay));

            grid.Children.Add(labelNameSurname, 0, 0);
            grid.Children.Add(labelUserName, 0, 1);
            grid.Children.Add(labelEmail, 0, 2);

            if (Relationship != "request_received")
            {
                grid.Children.Add(RelationshipButton(), 0, 3);
            }

            else
            {
                grid.Children.Add(_buttonAcceptFriend, 0, 3);
                grid.Children.Add(_buttonRejectFriend, 1, 3);
            }

            return grid;
        }
       
        #region Events

        private void AddFriendEventHandler(object sender, EventArgs eventArgs)
        {
            Label itemGridUserName;
            if (GetListItem(sender, out itemGridUserName)) return;

            if (itemGridUserName != null)
            {
                var username = itemGridUserName.Text;
                var responseErrors = FriendshipActions.Request(FriendshipOption.Send, _loginUser.AuthToken, username);

                if (responseErrors.Any())
                {
                    SubscribeAlert(responseErrors);
                }

                else
                {
                    SubscribeAddContact(username);
                }
            }
        }
        private void RemoveFriendEventHandler(object sender, EventArgs eventArgs)
        {
            Label itemGridUserName;
            if (GetListItem(sender, out itemGridUserName)) return;

            if (itemGridUserName != null)
            {
                var username = itemGridUserName.Text;
                var responseErrors = FriendshipActions.Request(FriendshipOption.Remove, _loginUser.AuthToken, username);

                if (responseErrors.Any())
                {
                    SubscribeAlert(responseErrors);
                }

                else
                {
                    SubscribeRemoveContact(username);
                }
            }
        }
        private void CancelFriendEventHandler(object sender, EventArgs eventArgs)
        {
            Label itemGridUserName;
            if (GetListItem(sender, out itemGridUserName)) return;

            if (itemGridUserName != null)
            {
                var username = itemGridUserName.Text;
                var responseErrors = FriendshipActions.Request(FriendshipOption.Cancel, _loginUser.AuthToken, username);

                if (responseErrors.Any())
                {
                    SubscribeAlert(responseErrors);
                }

                else
                {
                    SubscribeRemoveContact(username);
                }
            }
        }
        private void AcceptFriendEventHandler(object sender, EventArgs eventArgs)
        {
            Label itemGridUserName;
            if (GetListItem(sender, out itemGridUserName)) return;

            if (itemGridUserName != null)
            {
                var username = itemGridUserName.Text;
                var responseErrors = FriendshipActions.Request(FriendshipOption.Accept, _loginUser.AuthToken, username);

                if (responseErrors.Any())
                {
                    SubscribeAlert(responseErrors);
                }

                else
                {
                    SubscribeRemoveContact(username);
                }
            }
        }
        private void RejectFriendEventHandler(object sender, EventArgs eventArgs)
        {
            Label itemGridUserName;
            if (GetListItem(sender, out itemGridUserName)) return;

            if (itemGridUserName != null)
            {
                var username = itemGridUserName.Text;
                var responseErrors = FriendshipActions.Request(FriendshipOption.Reject, _loginUser.AuthToken, username);

                if (responseErrors.Any())
                {
                    SubscribeAlert(responseErrors);
                }

                else
                {
                    SubscribeRemoveContact(username);
                }
            }
        }
        private void SubscribeAlert(IEnumerable<ServiceError> responseErrors)
        {
            var message = responseErrors.Aggregate(string.Empty,
                (current, contactServiceError) => current + (contactServiceError.ErrorMessage + "\n"));

            MessagingCenter.Send(this, FriendshipMessages.DisplayAlert, message);
            MessagingCenter.Unsubscribe<CustomViewCell, string>(this, FriendshipMessages.DisplayAlert);
        }
        private void SubscribeRemoveContact(string username)
        {
            MessagingCenter.Send(this, FriendshipMessages.RemoveContact, username);
            MessagingCenter.Unsubscribe<CustomViewCell, string>(this, FriendshipMessages.RemoveContact);
        }
        private void SubscribeAddContact(string username)
        {
            MessagingCenter.Send(this, FriendshipMessages.AddContact, username);
            MessagingCenter.Unsubscribe<CustomViewCell, string>(this, FriendshipMessages.AddContact);
        }
        private static bool GetListItem(object sender, out Label itemGridUserName)
        {
            itemGridUserName = null;

            var objectSender = sender as Button;
            if (objectSender == null) return true;
            var parentGrid = objectSender.Parent as Grid;

            if (parentGrid == null) return true;
            itemGridUserName = parentGrid.Children[1] as Label;
            return false;
        }
        private Button RelationshipButton()
        {
            if (string.IsNullOrEmpty(Relationship))
            {
                return _buttonAddFriend;
            }

            if (Relationship == "contacts")
            {
                return _buttonRemoveFriend;
            }

            if (Relationship == "request_sent")
            {
                return _buttonCancelFriend;
            }

            return _buttonAddFriend;
        }

        #endregion
    }
}