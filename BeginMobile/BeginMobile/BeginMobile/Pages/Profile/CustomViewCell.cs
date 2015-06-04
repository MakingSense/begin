#define FriendshipActions

using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.Services.DTO;
using BeginMobile.Utils;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class CustomViewCell : ViewCell
    {
        private Image _buttonRemoveFriend;
        private readonly LoginUser _loginUser;


        public CustomViewCell(LoginUser loginUser)
        {
            _loginUser = loginUser;

            var circleIconImage = new CircleImage
                                  {
                                      Style = BeginApplication.Styles.CircleImageCommon
                                  };

            circleIconImage.SetBinding(Image.SourceProperty, new Binding("Icon"));


            var optionLayout = CreateOptionLayout();

            var gridComponents = new Grid
                                 {
                                     Padding = BeginApplication.Styles.ThicknessInsideListView,
                                     HorizontalOptions = LayoutOptions.FillAndExpand,
                                     VerticalOptions = LayoutOptions.FillAndExpand,
                                     RowDefinitions =
                                     {
                                         new RowDefinition {Height = GridLength.Auto},
                                     },
                                     ColumnDefinitions =
                                     {
                                         new ColumnDefinition {Width = GridLength.Auto},
                                         new ColumnDefinition {Width = GridLength.Auto}
                                     }
                                 };
            gridComponents.Children.Add(circleIconImage, 0, 0);
            gridComponents.Children.Add(optionLayout, 1, 0);

            View = gridComponents;
        }

        private Grid CreateOptionLayout()
        {
            //_buttonRemoveFriend = new Button
            //                      {
            //                          //Text = AppResources.ButtonRemoveFriend,
            //                          //Style = BeginApplication.Styles.ListViewItemButton
            //                          Image = BeginApplication.Styles.ContactAddedIcon,
            //                          Style = BeginApplication.Styles.ButtonContactsListView,
            //                          HorizontalOptions = LayoutOptions.End,
            //                      };
            //_buttonRemoveFriend.Clicked += RemoveFriendEventHandler;

            var tappedGestureRemoveFriend = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };
            tappedGestureRemoveFriend.Tapped += RemoveFriendEventHandler;           
            _buttonRemoveFriend = new Image
            {
                Source = BeginApplication.Styles.ContactAddedIcon,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.End,
                Style = BeginApplication.Styles.ImageButtonContactsListView,
            };
            _buttonRemoveFriend.GestureRecognizers.Add(tappedGestureRemoveFriend);
            var labelNameSurname = new Label
                                   {
                                       HorizontalOptions = LayoutOptions.FillAndExpand,
                                       YAlign = TextAlignment.Start,
                                       Style = BeginApplication.Styles.ListItemTextStyle
                                   };

            var labelUserName = new Label
                                {
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    YAlign = TextAlignment.Start,
                                    Style = BeginApplication.Styles.ListItemDetailTextStyle
                                };

            var labelProfession = new Label
                                  {
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      YAlign = TextAlignment.Start,
                                      Style = BeginApplication.Styles.ListItemDetailTextStyle
                                  };


            labelNameSurname.SetBinding(Label.TextProperty, "NameSurname");
            labelUserName.SetBinding(Label.TextProperty, "UserName");
            labelProfession.SetBinding(Label.TextProperty, "Profession");

            var grid = new Grid
                       {
                           Padding = BeginApplication.Styles.ThicknessBetweenImageAndDetails,
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
                              new ColumnDefinition {Width = new GridLength(Device.OnPlatform(150, 120, 120), GridUnitType.Absolute)},
                              new ColumnDefinition {Width = GridLength.Auto},
                           }
                       };


            grid.Children.Add(labelNameSurname, 0, 0);
            grid.Children.Add(labelUserName, 0, 1);
            grid.Children.Add(labelProfession, 0, 2);
            grid.Children.Add(_buttonRemoveFriend, 1, 2);
            return grid;
        }

        #region Events

        //private void AddFriendEventHandler(object sender, EventArgs eventArgs)
        //{
        //    var objectSender = sender as Button;

        //    if (objectSender == null) return;

        //    var parentGrid = objectSender.Parent as Grid;

        //    if (parentGrid == null) return;
        //    var itemGridUserName = parentGrid.Children[1] as Label;

        //    if (itemGridUserName != null)
        //    {
        //        var username = itemGridUserName.Text;
        //        var responseErrors = FriendshipActions.Request(FriendshipOption.Send, _loginUser.AuthToken, username);

        //        if (responseErrors.Any())
        //        {
        //            SubscribeAlert(responseErrors);
        //        }

        //        else
        //        {
        //            objectSender.IsVisible = false;
        //            parentGrid.Children.Remove(objectSender);
        //            parentGrid.Children.Add(_buttonRemoveFriend, 1, 0);
        //            _buttonRemoveFriend.IsVisible = true;
        //        }
        //    }
        //}

        private void RemoveFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var objectSender = sender as Image;

            if (objectSender == null) return;

            var parentGrid = objectSender.Parent as Grid;

            if (parentGrid == null) return;
            var itemGridUserName = parentGrid.Children[1] as Label;

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

        #endregion
    }
}