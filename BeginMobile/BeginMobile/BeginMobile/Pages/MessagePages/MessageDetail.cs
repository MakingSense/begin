using System;
using System.Linq;
using BeginMobile;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class MessageDetail : ContentPage
    {
        private readonly Editor _editorReplyContent;
        private readonly LoginUser _currentUser;

        public MessageDetail(MessageViewModel messageViewModel)
        {
            if (messageViewModel != null) MessageViewModel = messageViewModel;
            Title = AppResources.MessageDetailTitle;
            _currentUser = (LoginUser) Application.Current.Properties["LoginUser"];
            var listDataMessages = MessageViewModel.Messages.Select(message => new MessageViewModel
                                                                               {
                                                                                   SenderName =
                                                                                       message.Sender.NameSurname,
                                                                                   DateSent = message.DateSent,
                                                                                   MessageContent =
                                                                                       message.MessageContent
                                                                               }).ToList();
            var listViewMessages = new ListView
                                   {
                                       VerticalOptions = LayoutOptions.Start,
                                       ItemsSource = listDataMessages,
                                       ItemTemplate = new DataTemplate(typeof (MessageTemplate)),
                                       RowHeight = 120,
                                   };
            listViewMessages.ItemSelected += ItemSelectedEventHandler;

            var image = new CircleImage
                        {
                            BorderColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                            BorderThickness = Device.OnPlatform(2, 3, 3),
                            HeightRequest = Device.OnPlatform(30, 50, 70),
                            WidthRequest = Device.OnPlatform(30, 50, 70),
                            Aspect = Aspect.AspectFit,
                            HorizontalOptions = LayoutOptions.Start,
                            Source = MessageViewModel.Sender.Avatar ?? BeginApplication.Styles.MessageIcon
                        };
            var labelThisUserNameSurname = new Label
                                           {
                                               Text = _currentUser.User.DisplayName,
                                               Style = BeginApplication.Styles.ListItemTextStyle,
                                               YAlign = TextAlignment.Center
                                           };
            var gridReply = new Grid
                            {
                                Padding = new Thickness(10, 5, 10, 5),
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                RowDefinitions =
                                {
                                    new RowDefinition {Height = GridLength.Auto},
                                    new RowDefinition {Height = GridLength.Auto}
                                },
                                ColumnDefinitions =
                                {
                                    new ColumnDefinition {Width = GridLength.Auto},
                                    new ColumnDefinition {Width = GridLength.Auto},
                                }
                            };
            gridReply.Children.Add(image, 0, 0);
            gridReply.Children.Add(labelThisUserNameSurname, 1, 0);

            _editorReplyContent = new Editor
                                  {
                                      HeightRequest = 100,
                                      Style = BeginApplication.Styles.MessageContentStyle
                                  };
            var buttonReply = new Button
                              {
                                  Text = AppResources.ButtonSendReply,
                                  Style = BeginApplication.Styles.DefaultButton
                              };
            buttonReply.Clicked += ButtonReplyEventHandler;
            var buttonMarkUsUnread = new Button
                                     {
                                         Text = AppResources.MarkAsUnread,
                                         Style = BeginApplication.Styles.ListViewItemButton,
                                         HorizontalOptions = LayoutOptions.End,
                                         HeightRequest = Device.OnPlatform(15, 35, 35),
                                         WidthRequest = Device.OnPlatform(150, 150, 150)
                                     };
            buttonMarkUsUnread.Clicked += ButtonMarkUsUnreadEventHandler;

            var gridComponents = new Grid
                                 {
                                     Padding = BeginApplication.Styles.LayoutThickness,
                                     HorizontalOptions = LayoutOptions.FillAndExpand,
                                     VerticalOptions = LayoutOptions.FillAndExpand,
                                     RowDefinitions =
                                     {
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto}
                                     }
                                 };
            
            if (InboxMessage.IsInbox)
            {
                gridComponents.Children.Add(buttonMarkUsUnread, 0, 0);                               
            }
            gridComponents.Children.Add(listViewMessages, 0, 1);
            gridComponents.Children.Add(gridReply, 0, 2);
            gridComponents.Children.Add(_editorReplyContent, 0, 3);
            gridComponents.Children.Add(buttonReply, 0, 4);

            Content = gridComponents;
        }

        public async void ButtonMarkUsUnreadEventHandler(object sender, EventArgs e)
        {
            await
                BeginApplication.ProfileServices.MarkAsUnreadByThread(_currentUser.AuthToken, MessageViewModel.ThreadId);
            if (InboxMessage.IsInbox)
            {
                await Navigation.PopAsync();
                await InboxMessage.CallServiceApi();
               
            } //TODO: Refactor this code to refresh the service call when user click on navigation back button 
        }

        public async void ButtonReplyEventHandler(object sender, EventArgs e)
        {
            var sendMessageManager =
                await
                    BeginApplication.ProfileServices.SendMessage(_currentUser.AuthToken,
                        MessageViewModel.Sender.UserName,
                        MessageViewModel.Subject, _editorReplyContent.Text, MessageViewModel.ThreadId);

            if (sendMessageManager != null)
            {
                var errorMessage = sendMessageManager.Errors.Aggregate("",
                    (current, serviceError) => current + (serviceError.ErrorMessage + "\n"));
                await DisplayAlert(AppResources.ApplicationValidationError, errorMessage, AppResources.AlertOk);
            }
            else
            {
                await
                    DisplayAlert(AppResources.ServerMessageSuccess, AppResources.ServerMessageSendSuccess,
                        AppResources.AlertOk);
                if (InboxMessage.IsInbox)
                {
                    await InboxMessage.CallServiceApi();
                }
                else
                {
                    await SentMessage.CallServiceApi();
                    InboxMessage.IsInbox = false;
                }
                await Navigation.PopAsync();
            }
        }

        public void ItemSelectedEventHandler(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            ((ListView) sender).SelectedItem = null;
        }

        public MessageViewModel MessageViewModel { get; set; }

        //protected override bool OnBackButtonPressed()
        //{
        //    if(InboxMessage.IsInbox)
        //    {
        //        var callServiceApi = InboxMessage.CallServiceApi();
        //    }
        //    else
        //    {
        //        var callServiceApi = SentMessage.CallServiceApi();
        //    }

        //    return base.OnBackButtonPressed();

        //}
    }
}

public class MessageTemplate : ViewCell
{
    public MessageTemplate()
    {
        var circleShopImage = new CircleImage
                              {
                                  BorderColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                                  BorderThickness = Device.OnPlatform(2, 3, 3),
                                  HeightRequest = Device.OnPlatform(50, 100, 100),
                                  WidthRequest = Device.OnPlatform(50, 100, 100),
                                  Aspect = Aspect.AspectFit,
                                  HorizontalOptions = LayoutOptions.Start,
                                  Source = BeginApplication.Styles.MessageIcon
                              };

        var labelSender = new Label
                          {
                              YAlign = TextAlignment.Center,
                              Style = BeginApplication.Styles.ListItemDetailTextStyle,
                              HorizontalOptions = LayoutOptions.StartAndExpand
                          };

        labelSender.SetBinding(Label.TextProperty, "SenderName", stringFormat: "Re: {0}");

        var labelCreate = new Label
                          {
                              YAlign = TextAlignment.Center,
                              Style = BeginApplication.Styles.ListItemDetailTextStyle,
                              HorizontalOptions = LayoutOptions.End
                          };

        labelCreate.SetBinding(Label.TextProperty, "DateSent", stringFormat: "Date: {0}");

        var labelContent = new Label
                           {
                               YAlign = TextAlignment.Center,
                               Style = BeginApplication.Styles.ListItemDetailTextStyle,
                               HorizontalOptions = LayoutOptions.StartAndExpand
                           };
        labelContent.SetBinding(Label.TextProperty, "MessageContent");


        var gridDetails = new Grid
                          {
                              Padding = new Thickness(10, 5, 10, 5),
                              HorizontalOptions = LayoutOptions.FillAndExpand,
                              VerticalOptions = LayoutOptions.FillAndExpand,
                              RowDefinitions =
                              {
                                  new RowDefinition {Height = GridLength.Auto},
                                  new RowDefinition {Height = GridLength.Auto},
                                  new RowDefinition {Height = GridLength.Auto}
                              }
                          };
        gridDetails.Children.Add(labelSender, 0, 0);
        gridDetails.Children.Add(labelContent, 0, 1);
        gridDetails.Children.Add(labelCreate, 0, 2);
        var stackLayoutView = new StackLayout
                              {
                                  Spacing = 2,
                                  Padding = BeginApplication.Styles.LayoutThickness,
                                  Orientation = StackOrientation.Horizontal,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  Children =
                                  {
                                      circleShopImage,
                                      gridDetails
                                  }
                              };

        View = stackLayoutView;
    }
}