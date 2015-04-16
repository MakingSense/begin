using System;
using System.Linq;
using BeginMobile;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class MessageDetail : ContentPage
    {
        private readonly Editor _editorReplyContent;
        private const string DefaultImageUser = "userdefault3.png";
        private LoginUser _currentUser;

        public MessageDetail(MessageViewModel messageViewModel)
        {
            if (messageViewModel != null) MessageViewModel = messageViewModel;
            Title = "Message Detail";
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
                            Source = DefaultImageUser
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
                                  Text = "Send Reply",
                                  Style = BeginApplication.Styles.DefaultButton
                              };
            buttonReply.Clicked += ButtonReplyEventHandler;

            var stackLayoutList = new StackLayout
                                  {
                                      VerticalOptions = LayoutOptions.StartAndExpand,
                                      Children = {listViewMessages, gridReply, _editorReplyContent, buttonReply}
                                  };
            Content = new StackLayout
                      {
                          Padding = BeginApplication.Styles.LayoutThickness,
                          Children =
                          {
                              stackLayoutList //stackLayoutEditorReply
                          }
                      };
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
                await DisplayAlert("Validation Error", errorMessage, "Ok");
            }
            else
            {
                await DisplayAlert("Successfull!", "Your message has successfully sent!", "ok");
                if (InboxMessage.IsInbox)
                {
                    await InboxMessage.Init();
                }
                else
                {
                    await SentMessage.Init();
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
    }
}

public class MessageTemplate : ViewCell
{
    private static string DefaultImage
    {
        get { return "userdefault3.png"; }
    }

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
                                  Source = DefaultImage
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