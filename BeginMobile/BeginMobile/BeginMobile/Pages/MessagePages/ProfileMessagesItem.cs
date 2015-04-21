using System;
using BeginMobile.LocalizeResources.Resources;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using BeginMobile.Services.Models;

namespace BeginMobile.Pages.MessagePages
{
    public class ProfileMessagesItem : ViewCell
    {
        private readonly Button _buttonMarkAsUnread;
        private readonly Button _buttonMarkAsRead;

        private static string GroupImage
        {
            get { return "userdefault3.png"; }
        }
        private static string MessageState { get; set; }

        private static readonly BindableProperty MessageStateProperty =
            BindableProperty.Create<ProfileMessagesItem, string>
                (getter => MessageState, string.Empty, BindingMode.TwoWay,
                    propertyChanging: (bindable, oldValue, newValue) =>
                                      {
                                          MessageState = newValue;
                                      });        

        public ProfileMessagesItem()
        {
            SetBinding(MessageStateProperty, new Binding("ThreadUnRead"));

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

            _buttonMarkAsRead = new Button
                                {
                                    Text = AppResources.ButtonReadNotification,
                                    Style = BeginApplication.Styles.ListViewItemButton,
                                    HorizontalOptions = LayoutOptions.Start,
                                    HeightRequest = Device.OnPlatform(15, 35, 35),
                                    WidthRequest = Device.OnPlatform(70, 70, 70)
                                };


            _buttonMarkAsRead.Clicked += OnMarkAsReadEventHandler;

            _buttonMarkAsUnread = new Button
                                  {
                                      Text = AppResources.ButtonUnReadNotification,
                                      Style = BeginApplication.Styles.ListViewItemButton,
                                      HorizontalOptions = LayoutOptions.Start,
                                      HeightRequest = Device.OnPlatform(15, 35, 35),
                                      WidthRequest = Device.OnPlatform(70, 70, 70)
                                  };

            _buttonMarkAsUnread.Clicked += OnMarkAsUnreadEventHandler;

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
                                      new RowDefinition {Height = GridLength.Auto}
                                  }
                              };

                gridDetails.Children.Add(labelSender, 0, 0);
                gridDetails.Children.Add(labelSubject, 0, 1);
                gridDetails.Children.Add(labelContent, 0, 2);
                gridDetails.Children.Add(labelCreate, 0, 3);

    

                if (InboxMessage.IsInbox)
                {
                    gridDetails.Children.Add(labelMarkedAs, 0, 4);
                    gridDetails.Children.Add(buttonRemove, 1, 5);

                    if (MessageState != null)
                    {
                        gridDetails.Children.Add(MessageStateButton, 0, 5);
                    }
                }
                else
                {
                    gridDetails.Children.Add(buttonRemove, 0, 4);
                }

            var stackLayoutView = new StackLayout
                                  {
                                      Spacing = 2,
                                      Padding = BeginApplication.Styles.LayoutThickness,
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

        private void OnMarkAsUnreadEventHandler(object sender, EventArgs e)
        {
            var current = sender as Button;
            if (current == null) return;

            var threadId = current.Parent.Parent.ClassId;
            if (InboxMessage.IsInbox)
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.MarkAsUnreadInboxMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this,
                    MessageSuscriptionNames.MarkAsUnreadInboxMessage);
            }
            else
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.MarkAsUnreadSentMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this,
                    MessageSuscriptionNames.MarkAsUnreadSentMessage);
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
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this,MessageSuscriptionNames.MarkAsReadInboxMessage);
            }
            else
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.MarkAsReadSentMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this,
                    MessageSuscriptionNames.MarkAsReadSentMessage);
            }
        }

        private Button MessageStateButton
        {
            get
            {
                if (string.IsNullOrEmpty(MessageState))
                {
                    return new Button();
                }
                return MessageState.Equals(EnumMessageStates.Read.ToString()) ? _buttonMarkAsUnread : _buttonMarkAsRead;
            }
        }

        protected override void OnBindingContextChanged()
        {
            var data = ((MessageViewModel)BindingContext);
            MessageState = data.ThreadUnRead;
            base.OnBindingContextChanged();
        }
    }
}