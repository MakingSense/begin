using System;
using BeginMobile.LocalizeResources.Resources;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using System.IO;

namespace BeginMobile.Pages.MessagePages
{
    public class ProfileMessagesItem : ViewCell
    {
        public ProfileMessagesItem(ImageSource imageSourceMail)
        {
            var circleShopImage = new CircleImage
                                  {
                                      Style = BeginApplication.Styles.CircleImageCommon,
                                      Source = imageSourceMail
                                  };

            var labelSender = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  Style = BeginApplication.Styles.ListItemTextStyle,
                                  HorizontalOptions = LayoutOptions.StartAndExpand
                              };

            labelSender.SetBinding(Label.TextProperty, "SenderName");

            var labelSubject = new Label
                               {
                                   YAlign = TextAlignment.Center,
                                   Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                   HorizontalOptions = LayoutOptions.Start
                               };
            labelSubject.SetBinding(Label.TextProperty, "Subject");

            var labelCreate = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                  HorizontalOptions = LayoutOptions.StartAndExpand
                              };

            labelCreate.SetBinding(Label.TextProperty, "DateSent", stringFormat: "Date: {0}");

            var labelContent = new Label
                               {
                                   YAlign = TextAlignment.Center,
                                   Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                   HorizontalOptions = LayoutOptions.StartAndExpand
                               };
            labelContent.SetBinding(Label.TextProperty, "MessageContent");

            var labelMarkedAs = new Label
                                {
                                    YAlign = TextAlignment.Center,
                                    Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                    TextColor = Color.FromHex("000000"),
                                    BackgroundColor = Color.FromHex("F6B94D"),
                                    XAlign = TextAlignment.Center,
                                    FontAttributes = FontAttributes.Bold,
                                    //WidthRequest = 40,
                                    HorizontalOptions = LayoutOptions.StartAndExpand
                                };

            labelMarkedAs.SetBinding(Label.TextProperty, "ThreadUnRead");


            var buttonRemove = new Button
                               {
                                   Text = AppResources.ButtonRemoveFriend,
                                   Style = BeginApplication.Styles.ListViewItemButton
                               };
            buttonRemove.Clicked += RemoveEventHandler;

            var gridDetails = new Grid
                              {
                                  Padding = BeginApplication.Styles.ThicknessBetweenImageAndDetails,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      //new RowDefinition {Height = GridLength.Auto},
                                      //new RowDefinition {Height = GridLength.Auto},
//                                      new RowDefinition {Height = GridLength.Auto}
                                  }
                              };

            gridDetails.Children.Add(labelSender, 0, 0);
            gridDetails.Children.Add(labelContent, 0, 1);
            gridDetails.Children.Add(labelMarkedAs, 0, 2);


            var stackLayoutView = new StackLayout
                                  {
                                      Padding = BeginApplication.Styles.ThicknessInsideListView,
                                      Orientation = StackOrientation.Horizontal,
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      VerticalOptions = LayoutOptions.FillAndExpand,
                                      Children =
                                      {
                                          circleShopImage,
                                          gridDetails
                                      }
                                  };

            View = stackLayoutView;
            View.SetBinding(ClassIdProperty, "ThreadId");
        }


        public void RemoveEventHandler(object sender, EventArgs e)
        {
            var current = sender as Button;
            if (current == null) return;

            var threadId = current.Parent.Parent.ClassId;
            if (InboxMessage.IsInbox)
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.RemoveInboxMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this,
                    MessageSuscriptionNames.RemoveInboxMessage);
            }
            else
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.RemoveSentMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this, MessageSuscriptionNames.RemoveSentMessage);
            }
        }
    }


    //for sent
    public class ProfileMessageSentItem : ViewCell
    {
        public ProfileMessageSentItem(ImageSource imageSourceMail)
        {
            var circleShopImage = new CircleImage
                                  {
                                      Style = BeginApplication.Styles.CircleImageCommon,
                                      Source = imageSourceMail
                                  };

            var labelSender = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  Style = BeginApplication.Styles.ListItemTextStyle,
                                  HorizontalOptions = LayoutOptions.StartAndExpand
                              };

            labelSender.SetBinding(Label.TextProperty, "SenderName", stringFormat: "To: {0}");

            var labelSubject = new Label
                               {
                                   YAlign = TextAlignment.Center,
                                   Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                   HorizontalOptions = LayoutOptions.Start
                               };
            labelSubject.SetBinding(Label.TextProperty, "Subject");

            var labelCreate = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                  HorizontalOptions = LayoutOptions.StartAndExpand
                              };

            labelCreate.SetBinding(Label.TextProperty, "DateSent", stringFormat: "Date: {0}");

            var labelContent = new Label
                               {
                                   YAlign = TextAlignment.Center,
                                   Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                   HorizontalOptions = LayoutOptions.StartAndExpand
                               };
            labelContent.SetBinding(Label.TextProperty, "MessageContent");

            var labelMarkedAs = new Label
                                {
                                    YAlign = TextAlignment.Center,
                                    Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                    TextColor = Color.FromHex("000000"),
                                    BackgroundColor = Color.FromHex("F6B94D"),
                                    XAlign = TextAlignment.Center,
                                    FontAttributes = FontAttributes.Bold,
                                    //WidthRequest = 40,
                                    HorizontalOptions = LayoutOptions.StartAndExpand
                                };

            labelMarkedAs.SetBinding(Label.TextProperty, "ThreadUnRead");


            var buttonRemove = new Button
                               {
                                   Text = AppResources.ButtonRemoveFriend,
                                   Style = BeginApplication.Styles.ListViewItemButton
                               };
            //buttonRemove.Clicked += RemoveEventHandler;

            var gridDetails = new Grid
                              {
                                  Padding = BeginApplication.Styles.ThicknessBetweenImageAndDetails,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      //new RowDefinition {Height = GridLength.Auto},
                                      //new RowDefinition {Height = GridLength.Auto},
//                                      new RowDefinition {Height = GridLength.Auto}
                                  }
                              };

            gridDetails.Children.Add(labelSender, 0, 0);
            gridDetails.Children.Add(labelContent, 0, 1);
            gridDetails.Children.Add(labelMarkedAs, 0, 2);


            var stackLayoutView = new StackLayout
                                  {
                                      Padding = BeginApplication.Styles.ThicknessInsideListView,
                                      Orientation = StackOrientation.Horizontal,
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      VerticalOptions = LayoutOptions.FillAndExpand,
                                      Children =
                                      {
                                          //circleShopImage,
                                          gridDetails
                                      }
                                  };

            View = stackLayoutView;
            View.SetBinding(ClassIdProperty, "ThreadId");
        }
    }
}
