using System;
using System.Collections.Generic;
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
        private const string DefaultImageUser ="userdefault3.png";
        public MessageDetail(MessageViewModel messageViewModel)
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            if (messageViewModel != null) MessageViewModel = messageViewModel;
            Title = "Message Detail";

            var listDataMessages = new List<MessageViewModel>();
            foreach (var message in MessageViewModel.Messages)
                listDataMessages.Add(new MessageViewModel
                                     {
                                         Sender = message.Sender.NameSurname, DateSent = message.DateSent, MessageContent = message.MessageContent
                                     });


            var listViewMessages = new ListView
                                   {
                                       //BackgroundColor = Color.Aqua,
                                       VerticalOptions = LayoutOptions.Start,
                                       ItemsSource = listDataMessages,
                                       ItemTemplate = new DataTemplate(typeof (MessageTemplate)),
                                       HasUnevenRows = true
                                   };
            listViewMessages.ItemSelected += ItemSelectedEventHandler;           

            var image = new CircleImage
            {
                BorderColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                BorderThickness = Device.OnPlatform(2, 3, 3),
                HeightRequest = Device.OnPlatform(20, 70, 70),
                WidthRequest = Device.OnPlatform(20, 70, 70),
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Start,
                Source = DefaultImageUser
            };
            var labelThisUserNameSurname = new Label
                                           {
                                               Text = currentUser.User.NameSurname,
                                               Style = App.Styles.ListItemTextStyle
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
                              }
            };
            gridReply.Children.Add(image, 0, 0);
            gridReply.Children.Add(labelThisUserNameSurname, 1, 0);

            var editorReplyContent = new Editor
            {
                HeightRequest = 100,
                Style = App.Styles.MessageContentStyle
            };
            var buttonReply = new Button
                              {
                                  Text = "Send Reply",
                                  Style = App.Styles.DefaultButton
                              };
            buttonReply.Clicked += ButtonReplyEventHandler;

            var stackLayoutList = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = { listViewMessages, gridReply, editorReplyContent, buttonReply }
            };
            Content = new StackLayout
                      {
                          Padding = App.Styles.LayoutThickness,
                          Children =
                          {
                              stackLayoutList//stackLayoutEditorReply
                          }
                      };
        }

        public void ButtonReplyEventHandler(object sender, EventArgs e)
        {
            
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
                              Style = App.Styles.ListItemDetailTextStyle,
                              HorizontalOptions = LayoutOptions.StartAndExpand
                          };

        labelSender.SetBinding(Label.TextProperty, "Sender", stringFormat: "From: {0}");

        var labelCreate = new Label
                          {
                              YAlign = TextAlignment.Center,
                              Style = App.Styles.ListItemDetailTextStyle,
                              HorizontalOptions = LayoutOptions.End
                          };

        labelCreate.SetBinding(Label.TextProperty, "DateSent", stringFormat: "Date: {0}");

        var labelContent = new Label
                           {
                               YAlign = TextAlignment.Center,
                               Style = App.Styles.ListItemDetailTextStyle,
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
                                  Padding = App.Styles.LayoutThickness,
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