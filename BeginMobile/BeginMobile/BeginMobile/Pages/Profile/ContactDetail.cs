using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using BeginMobile.Services.Utils;
using BeginMobile.Utils;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ContactDetail : ContentPage
    {
        private readonly Contact _contact;
        private readonly LoginUser _loginUser;
        private readonly string Relationship;
        
        public ContactDetail(Contact contact)
        {
            _contact = contact;
            _loginUser = (LoginUser) Application.Current.Properties["LoginUser"];

            if (_contact == null) throw new ArgumentNullException("contact");

            Relationship = _contact.Relationship;

            var imageContact = new CircleImage
                               {
                                   Style = BeginApplication.Styles.CircleImageForDetails,
                                   Source = BeginApplication.Styles.DefaultContactIcon,
                               };

            var gridImage = new Grid();

            var stackLayoutLinesRight = new StackLayout
                                        {
                                            VerticalOptions = LayoutOptions.CenterAndExpand,
                                            HorizontalOptions = LayoutOptions.FillAndExpand,
                                            Children =
                                            {
                                                BoxViewLine(),
                                                BoxViewLine()
                                            }
                                        };
            var stackLayoutLinesLeft = new StackLayout
                                       {
                                           VerticalOptions = LayoutOptions.CenterAndExpand,
                                           HorizontalOptions = LayoutOptions.FillAndExpand,
                                           Children =
                                           {
                                               BoxViewLine(),
                                               BoxViewLine()
                                           }
                                       };

            gridImage.Children.Add(stackLayoutLinesLeft, 0, 0);
            gridImage.Children.Add(imageContact, 1, 0);
            gridImage.Children.Add(new Image
                                   {
                                       HeightRequest = 15,
                                       WidthRequest = 15,
                                       
                                       VerticalOptions = LayoutOptions.Start,
                                       Source =
                                           _contact.IsOnline
                                               ? ImageSource.FromFile("online_icon.png")
                                               : ImageSource.FromFile("offline_icon.png")
                                   }, 1, 1);

            gridImage.Children.Add(stackLayoutLinesRight, 2, 0);

            var labelTextNameAndSurname = new Label
                                          {
                                              YAlign = TextAlignment.End,
                                              XAlign = TextAlignment.End,
                                              FontAttributes = FontAttributes.Bold,
                                              Style = BeginApplication.Styles.ListItemTextStyle,
                                              //Text = AppResources.LabelNameSurname
                                              Text = "Name Surname:"
                                          };
            var labelTextEmail = new Label
                                 {
                                     YAlign = TextAlignment.End,
                                     XAlign = TextAlignment.End,
                                     FontAttributes = FontAttributes.Bold,
                                     Style = BeginApplication.Styles.ListItemTextStyle,
                                     Text = "Email:"
                                 };
            var labelTextUsername = new Label
                                    {
                                        YAlign = TextAlignment.End,
                                        XAlign = TextAlignment.End,
                                        FontAttributes = FontAttributes.Bold,
                                        Style = BeginApplication.Styles.ListItemTextStyle,
                                        Text = "User Name:"
                                    };
            var labelTextRegistered = new Label
                                      {
                                          YAlign = TextAlignment.End,
                                          XAlign = TextAlignment.End,
                                          FontAttributes = FontAttributes.Bold,
                                          Style = BeginApplication.Styles.ListItemTextStyle,
                                          Text = "Registered:"
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
                                      Text = DateConverter.GetTimeSpan(Convert.ToDateTime(_contact.Registered))
                                  };

            var buttonAddFriend = new Button()
                                  {
                                      Text = AppResources.ButtonAddFriend,
                                      Style = BeginApplication.Styles.ListViewItemButton,
                                      HorizontalOptions = LayoutOptions.Start,
                                      HeightRequest = 35,
                                      WidthRequest = 70
                                  };

            var buttonCancelFriend = new Button
                                     {
                                         Text = AppResources.ButtonCancel,
                                         Style = BeginApplication.Styles.ListViewItemButton,
                                         HorizontalOptions = LayoutOptions.Start,
                                         HeightRequest = 35,
                                         WidthRequest = 70
                                     };

            var buttonAcceptFriend = new Button
                                     {
                                         Text = AppResources.ButtonAcceptFriend,
                                         Style = BeginApplication.Styles.ListViewItemButton,
                                         HorizontalOptions = LayoutOptions.Start,
                                         HeightRequest = 35,
                                         WidthRequest = 70
                                     };

            var buttonRemoveFriend = new Button
                                     {
                                         Text = AppResources.ButtonRemoveFriend,
                                         Style = BeginApplication.Styles.ListViewItemButton,
                                         HorizontalOptions = LayoutOptions.Start,
                                         HeightRequest = 35,
                                         WidthRequest = 70
                                     };

            var buttonRejectFriend = new Button
                                     {
                                         Text = AppResources.ButtonRejectFriend,
                                         Style = BeginApplication.Styles.ListViewItemButton,
                                         HorizontalOptions = LayoutOptions.Start,
                                         HeightRequest = 35,
                                         WidthRequest = 70
                                     };

            buttonCancelFriend.Clicked += CancelFriendEventHandler;
            buttonAcceptFriend.Clicked += AcceptFriendEventHandler;
            buttonRejectFriend.Clicked += RejectFriendEventHandler;
            buttonRemoveFriend.Clicked += RemoveEventHandler;

            var gridButtons = new Grid
                              {
                                  VerticalOptions = LayoutOptions.Start,
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

            if (string.IsNullOrEmpty(Relationship))
            {
                gridButtons.Children.Add(buttonAddFriend, 0, 0);
            }

            if (Relationship == "contacts")
            {
                gridButtons.Children.Add(buttonRemoveFriend, 0, 0);
            }

            if (Relationship == "request_sent")
            {
                gridButtons.Children.Add(buttonCancelFriend, 0, 0);
            }

            if (Relationship == "request_received")
            {
                gridButtons.Children.Add(buttonAcceptFriend, 0, 0);
                gridButtons.Children.Add(buttonRejectFriend, 0, 1);
            }
            
            var gridComponents = new Grid
                                 {
                                     Padding = BeginApplication.Styles.GridOfListView,
                                     HorizontalOptions = LayoutOptions.Center,
                                     RowDefinitions =
                                     {
                                         new RowDefinition {Height = GridLength.Auto},
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

            gridComponents.Children.Add(labelTextNameAndSurname, 0, 0);
            gridComponents.Children.Add(labelCompleteName, 1, 0);
            gridComponents.Children.Add(labelTextUsername, 0, 1);
            gridComponents.Children.Add(labelUsername, 1, 1);
            gridComponents.Children.Add(labelTextEmail, 0, 2);
            gridComponents.Children.Add(labelEmail, 1, 2);
            gridComponents.Children.Add(labelTextRegistered, 0, 3);
            gridComponents.Children.Add(labelRegistered, 1, 3);
            gridComponents.Children.Add(gridButtons, 0, 4);


            Content = new StackLayout
                      {
                          HorizontalOptions = LayoutOptions.CenterAndExpand,
                          Orientation = StackOrientation.Vertical,
                          Children =
                          {
                              gridImage,
                              gridComponents
                          }
                      };
        }

        #region Events

        private void AddFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var responseErrors = FriendshipActions.Request(FriendshipOption.Send, _loginUser.AuthToken,
                _contact.UserName);
            if (!responseErrors.Any())
            {
                //TODO: Logic here
                return;
            }

            DisplayResponseErrors(responseErrors);
        }

        private void AcceptFriendEventHandler(object senser, EventArgs eventArgs)
        {
            var responseErrors = FriendshipActions.Request(FriendshipOption.Accept, _loginUser.AuthToken,
                _contact.UserName);
            if (!responseErrors.Any())
            {
                //TODO: Logic here
                return;
            }

            DisplayResponseErrors(responseErrors);
        }

        private void RejectFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var responseErrors = FriendshipActions.Request(FriendshipOption.Reject, _loginUser.AuthToken,
                _contact.UserName);
            if (!responseErrors.Any())
            {
                //TODO: Logic here
                return;
            }

            DisplayResponseErrors(responseErrors);
        }

        private void CancelFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var responseErrors = FriendshipActions.Request(FriendshipOption.Remove, _loginUser.AuthToken,
                _contact.UserName);

            if (!responseErrors.Any())
            {
                //TODO: Logic here
                return;
            }

            DisplayResponseErrors(responseErrors);
        }

        private void RemoveEventHandler(object sender, EventArgs eventArgs)
        {
            var responseErrors = FriendshipActions.Request(FriendshipOption.Remove, _loginUser.AuthToken,
                _contact.UserName);

            if (!responseErrors.Any())
            {
                //TODO: Logic here
                return;
            }

            DisplayResponseErrors(responseErrors);
        }

        #endregion

        #region Private methods

        private void DisplayResponseErrors(IEnumerable<ServiceError> addResponseErrors)
        {
            var message = addResponseErrors.Aggregate(String.Empty,
                (current, contactServiceError) => current + (contactServiceError.ErrorMessage + "\n"));

            DisplayAlert("Error", message, "Ok");
        }

        private static BoxView BoxViewLine()
        {
            return new BoxView {Color = BeginApplication.Styles.ColorLine, WidthRequest = 100, HeightRequest = 2};
        }

        #endregion
    }
}