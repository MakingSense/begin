using System;
using BeginMobile.LocalizeResources.Resources;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class ProfileMessagesItem : ViewCell
    {
        private static string GroupImage
        {
            get { return "userdefault3.png"; }
        }

        public ProfileMessagesItem()
        {
            var circleShopImage = new CircleImage
            {
                BorderColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                BorderThickness = Device.OnPlatform(2, 3, 3),
                HeightRequest = Device.OnPlatform(50, 100, 100),
                WidthRequest = Device.OnPlatform(50, 100, 100),
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Start,
                Source = GroupImage
            };
            
            var labelSender = new Label
            {
                YAlign = TextAlignment.Center,
                Style = BeginApplication.Styles.ListItemDetailTextStyle,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            labelSender.SetBinding(Label.TextProperty, "SenderName", stringFormat: "From: {0}");

            var labelSubject = new Label
            {
                YAlign = TextAlignment.Center,
                Style = BeginApplication.Styles.ListItemTextStyle,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start
            };
            labelSubject.SetBinding(Label.TextProperty, "Subject");

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

            //var checkBoxUnRead = new CheckBox
            //{
            //    Checked = true,
            //    DefaultText="Normal length text again.",
            //    HorizontalOptions= LayoutOptions.Start,
            //    FontSize=25,
            //    CheckedText = "IS checked",
            //    TextColor =  Device.OnPlatform( Color.Black,Color.White,Color.White),								 
            //    HeightRequest = Device.OnPlatform(15, 35, 35),
            //    WidthRequest = Device.OnPlatform(70, 70, 70)
            //};

            //checkBoxUnRead.SetBinding(CheckBox.DefaultTextProperty, "FullName", BindingMode.TwoWay);
            //checkBoxUnRead.SetBinding(CheckBox.CheckedProperty, "Selected", BindingMode.TwoWay);

            //checkBoxUnRead.SetBinding(CheckBox.DefaultTextProperty, "threadMessage");
            //checkBoxUnRead.SetBinding(CheckBox.CheckedProperty, "threadMessage");
            var labelMarkedAs = new Label
            {
                YAlign = TextAlignment.Center,
                Style = BeginApplication.Styles.ListItemDetailTextStyle,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            labelMarkedAs.SetBinding(Label.TextProperty, "ThreadUnRead", stringFormat: "Status: {0}");
            var buttonRemove = new Button
                               {
                Text = AppResources.ButtonRemoveFriend,
                Style = BeginApplication.Styles.ListViewItemButton,
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = Device.OnPlatform(15, 35, 35),
                WidthRequest = Device.OnPlatform(70, 70, 70)
            };
            buttonRemove.Clicked += RemoveEventHandler;

            var buttonMarkAsRead = new Button
            {
                Text = AppResources.ButtonReadNotification,
                Style = BeginApplication.Styles.ListViewItemButton,
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = Device.OnPlatform(15, 35, 35),
                WidthRequest = Device.OnPlatform(70, 70, 70)
            };


            buttonMarkAsRead.Clicked += OnMarkAsReadEventHandler;

            var buttonMarkAsUnread = new Button
            {
                Text = AppResources.ButtonUnReadNotification,
                Style = BeginApplication.Styles.ListViewItemButton,
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = Device.OnPlatform(15, 35, 35),
                WidthRequest = Device.OnPlatform(70, 70, 70)
            };

            buttonMarkAsUnread.Clicked += OnMarkAsUnreadEventHandler;

            var gridDetails = new Grid
                              {
                                  Padding = new Thickness(10, 5, 10, 5),
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                       new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  }
                              };
            gridDetails.Children.Add(labelSender, 0, 0);
            gridDetails.Children.Add(labelSubject, 0, 1);
            gridDetails.Children.Add(labelContent, 0, 2);
            gridDetails.Children.Add(labelCreate, 0, 3);            
            gridDetails.Children.Add(labelMarkedAs, 0, 4);
            gridDetails.Children.Add(buttonRemove, 0, 5);
           // gridDetails.Children.Add(isUnread ? buttonMarkAsRead : buttonMarkAsUnread, 0, 5);
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
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this, MessageSuscriptionNames.RemoveInboxMessage);
            }
            else
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.RemoveSentMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this, MessageSuscriptionNames.RemoveSentMessage);
            }
        }

        private void OnMarkAsUnreadEventHandler(object sender, EventArgs e)
        {
            var current = sender as Button;
            if (current == null) return;

            var threadId = current.Parent.Parent.ClassId;
            if (InboxMessage.IsInbox)
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.MarkAsUnreadInboxMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this, MessageSuscriptionNames.MarkAsUnreadInboxMessage);
            }
            else
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.MarkAsUnreadSentMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this, MessageSuscriptionNames.MarkAsUnreadSentMessage);
            }
        }

        private void OnMarkAsReadEventHandler(object sender, EventArgs e)
        {
            var current = sender as Button;
            if (current == null) return;

            var threadId = current.Parent.Parent.ClassId;
            if (InboxMessage.IsInbox)
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.MarkAsReadInboxMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this, MessageSuscriptionNames.MarkAsReadInboxMessage);
            }
            else
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.MarkAsReadSentMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this, MessageSuscriptionNames.MarkAsReadSentMessage);
            }
        }
    }
}