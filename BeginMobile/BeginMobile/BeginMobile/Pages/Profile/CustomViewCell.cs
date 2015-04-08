#define FriendshipActions

using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using BeginMobile.Utils;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class CustomViewCell : ViewCell
    {
        private Button _buttonAddFriend;
        private Button _buttonCancelRequestFriend;
        private readonly LoginUser _loginUser;
        public CustomViewCell(LoginUser loginUser)
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
                Text = AppResources.ButtonAddFriend
            };

            _buttonAddFriend.Clicked += AddFriendEventHandler;

            _buttonCancelRequestFriend = new Button
                                        {
                                            Text = AppResources.ButtonCancelRequestFriend
                                        };

            _buttonCancelRequestFriend.Clicked += CancelFriendEventHandler;

            var labelNameSurname = new Label
                             {
                                 HorizontalOptions = LayoutOptions.FillAndExpand,
                                 YAlign = TextAlignment.Center,
                                 Style = App.Styles.ListItemTextStyle
                             };

            var labelUserName = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle
            };

            var labelEmail = new Label
                               {
                                   HorizontalOptions = LayoutOptions.FillAndExpand,
                                   YAlign = TextAlignment.Center,
                                   Style = App.Styles.ListItemDetailTextStyle
                               };


            labelNameSurname.SetBinding(Label.TextProperty, "NameSurname");
            labelUserName.SetBinding(Label.TextProperty, "UserName");
            labelEmail.SetBinding(Label.TextProperty, "Email");

            var grid = new Grid
                              {
                                  Padding = App.Styles.ListDetailThickness,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
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


            grid.Children.Add(labelNameSurname, 0, 0);
            grid.Children.Add(labelUserName, 0, 1);
            grid.Children.Add(labelEmail, 0, 2);
            //Keep button at the end
            grid.Children.Add(_buttonAddFriend, 1, 0);

            return grid;
        }

        #region Events
        private void AddFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var objectSender = sender as Button;

            if (objectSender == null) return;

            var parentGrid = objectSender.Parent as Grid;

            if (parentGrid == null) return;
            var itemGridUserName = parentGrid.Children[1] as Label;

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
                    objectSender.IsVisible = false;
                    parentGrid.Children.Remove(objectSender);
                    parentGrid.Children.Add(_buttonCancelRequestFriend, 1, 0);
                    _buttonCancelRequestFriend.IsVisible = true;
                }
            }
        }

        private void CancelFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var objectSender = sender as Button;

            if (objectSender == null) return;

            var parentGrid = objectSender.Parent as Grid;

            if (parentGrid == null) return;
            var itemGridUserName = parentGrid.Children[1] as Label;

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
                    objectSender.IsVisible = false;
                    parentGrid.Children.Remove(objectSender);
                    parentGrid.Children.Add(_buttonAddFriend, 1, 0);
                    _buttonAddFriend.IsVisible = true;
                }
            }
        }

        private void SubscribeAlert(IEnumerable<ContactServiceError> responseErrors)
        {
            var message = responseErrors.Aggregate(string.Empty,
                (current, contactServiceError) => current + (contactServiceError.Message + "\n"));

            MessagingCenter.Send(this, FriendshipMessages.DisplayAlert, message);
        }

        #endregion
    }
}