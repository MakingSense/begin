using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.Profile;
using BeginMobile.Services.DTO;
using BeginMobile.Utils;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using BeginMobile.Services.Models;
using BeginMobile.Utils.Extensions;

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

        //private static readonly BindableProperty RelationshipProperty =
        //    BindableProperty.Create<ContactListItem, string>
        //        (getter => Relationship, string.Empty, BindingMode.Default,
        //            propertyChanging: (bindable, oldValue, newValue) =>
        //                              {
        //                                  Relationship = newValue;
        //                              });

        public ContactListItem(LoginUser loginUser)
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
            _buttonAddFriend = new Button
                               {
                                   //Text =  AppResources.ButtonAddFriend,
                                   Image = BeginApplication.Styles.ContactAddIcon,
                                   Style = BeginApplication.Styles.ButtonContactsListView,
                                   //HorizontalOptions = LayoutOptions.End
                                   VerticalOptions = LayoutOptions.Start,
                               };

            _buttonRemoveFriend = new Button
                                  {
                                      //Text =  AppResources.ButtonRemoveFriend,
                                      Image = BeginApplication.Styles.ContactAddedIcon,
                                      Style = BeginApplication.Styles.ButtonContactsListView,
                                      //HorizontalOptions = LayoutOptions.End
                                      VerticalOptions = LayoutOptions.Start
                                  };

            _buttonCancelFriend = new Button
                                  {
                                     // Text = AppResources.ButtonCancelRequestFriend,
                                      Image = BeginApplication.Styles.ContactAddIcon,
                                      Style = BeginApplication.Styles.ButtonContactsListView,
                                      //HorizontalOptions = LayoutOptions.End
                                      VerticalOptions = LayoutOptions.Start
                                  };

            _buttonAcceptFriend = new Button
                                  {
                                     // Text =  AppResources.ButtonAcceptFriend,
                                      Image = BeginApplication.Styles.ContactAddIcon,
                                      Style = BeginApplication.Styles.ButtonContactsListView,
                                      //HorizontalOptions = LayoutOptions.End
                                      VerticalOptions = LayoutOptions.Start
                                  };

            _buttonRejectFriend = new Button
                                  {
                                      //Text =  AppResources.ButtonRejectFriend,
                                      Image = BeginApplication.Styles.ContactAddIcon,
                                      Style = BeginApplication.Styles.ButtonContactsListView,
                                      //HorizontalOptions = LayoutOptions.End
                                      VerticalOptions = LayoutOptions.Start

                                  };

            _buttonAddFriend.Clicked += AddFriendEventHandler;
            _buttonRemoveFriend.Clicked += RemoveFriendEventHandler;
            _buttonCancelFriend.Clicked += CancelFriendEventHandler;
            _buttonAcceptFriend.Clicked += AcceptFriendEventHandler;
            _buttonRejectFriend.Clicked += RejectFriendEventHandler;

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
                               new ColumnDefinition {Width = new GridLength(2, GridUnitType.Star)},
                               new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                           }
                       };

            //SetBinding(RelationshipProperty, new Binding("Relationship"));

            try
            {
                /*grid.Children.Add(labelNameSurname, 0, 0);
                grid.Children.Add(labelUserName, 0, 1);
                grid.Children.Add(labelProfession, 0, 2);*/

                grid.AddChild(labelNameSurname, 0, 0);
                grid.AddChild(labelUserName, 1, 0);
                grid.AddChild(labelProfession, 2, 0);
                

                if (Relationship != "request_received")
                {
                    //grid.Children.AddChild(RelationshipButton(), 1, 1);
                    grid.AddChild(RelationshipButton(), 2, 1, 2, 1);
                }

                else
                {
                    //grid.Children.Add(_buttonAcceptFriend, 1, 2);
                    //grid.Children.Add(_buttonRejectFriend, 1, 1);
                    grid.AddChild(_buttonRejectFriend, 2, 1, 2, 1);

                }
            }

            catch (Exception exception)
            {
                SubscribeAlert(new List<ServiceError>
                               {
                                   new ServiceError
                                   {
                                       ErrorCode = "Error",
                                       ErrorMessage = exception.Message
                                   }
                               });
            }
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
                    //SubscribeAddContact(username);
                    if (parentGrid.Children.Remove(_buttonAddFriend))
                    {
                        parentGrid.Children.Add(_buttonCancelFriend, 1, 2);
                    }
                }
            }
        }

        private void RemoveFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var objectSender = sender as Button;
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
                    //SubscribeRemoveContact(username);
                    if (parentGrid.Children.Remove(_buttonRemoveFriend))
                    {
                        parentGrid.Children.Add(_buttonAddFriend, 1, 2);
                    }
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
                var responseErrors = FriendshipActions.Request(FriendshipOption.Cancel, _loginUser.AuthToken, username);

                if (responseErrors.Any())
                {
                    SubscribeAlert(responseErrors);
                }

                else
                {
                    //SubscribeRemoveContact(username);

                    if (parentGrid.Children.Remove(_buttonCancelFriend))
                    {
                        parentGrid.Children.Add(_buttonAddFriend, 1, 2);
                    }
                }
            }
        }

        private void AcceptFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var objectSender = sender as Button;
            if (objectSender == null) return;
            var parentGrid = objectSender.Parent as Grid;

            if (parentGrid == null) return;
            var itemGridUserName = parentGrid.Children[1] as Label;

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
                    //SubscribeRemoveContact(username);

                    if (parentGrid.Children.Remove(_buttonRejectFriend) &&
                        parentGrid.Children.Remove(_buttonAcceptFriend))
                    {
                        parentGrid.Children.Add(_buttonRemoveFriend, 1, 2);
                    }
                }
            }
        }

        private void RejectFriendEventHandler(object sender, EventArgs eventArgs)
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
                    //SubscribeRemoveContact(username);
                    if (parentGrid.Children.Remove(_buttonRejectFriend) &&
                        parentGrid.Children.Remove(_buttonAcceptFriend))
                    {
                        parentGrid.Children.Add(_buttonAddFriend, 1, 2);
                    }
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

        protected override void OnBindingContextChanged()
        {
            var data = ((Contact) BindingContext);
            Relationship = data.Relationship;
            base.OnBindingContextChanged();
        }
    }
}