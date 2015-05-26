using System;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.Profile;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Logging;
using BeginMobile.Services.Utils;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class MessageListPage : TabContent
    {
        public readonly Label LabelCounter;
        private readonly InboxMessage _inbox;
        private readonly SentMessage _sent;        
        private readonly TabViewExposure _tabViewExposure;
        private readonly ILoggingService _log = Logger.Current;
        public string MasterTitle { get; set; }

        private AppHome _appHome;

        public MessageListPage(string title, string iconImg, AppHome parentHome)
            : base(title, iconImg)
        {
            _appHome = parentHome;
            BackgroundColor = BeginApplication.Styles.ColorWhiteBackground;
            Title = title;
            MasterTitle = AppResources.AppHomeChildMessages;
            LabelCounter = new Label();
            _tabViewExposure = new TabViewExposure();
            _inbox = new InboxMessage();
            _sent = new SentMessage();

            ToolbarItems.Add(new ToolbarItem("SendMessage", BeginApplication.Styles.WriteIcon, async () =>
            {
                await Navigation
                    .PushAsync
                    (new SendMessage
                        ());
            }, ToolbarItemOrder.Primary));
            
            Init();
        }

        private async void Init()
        {
            var currentUser = (LoginUser) Application.Current.Properties["LoginUser"];
            var inboxThreads =
                await BeginApplication.ProfileServices.GetProfileThreadMessagesInbox(currentUser.AuthToken);
            if (inboxThreads != null)
            {
                LabelCounter.Text = inboxThreads.ThreadCount;
                _appHome.CounterText = inboxThreads.ThreadCount;
            }
        }

        public void InitMessages()
        {
            try
            {
                _tabViewExposure.PageOne = _inbox;
                _tabViewExposure.PageTwo = _sent;
                _tabViewExposure.TabOneName = TabsNames.Tab1Messages;
                _tabViewExposure.TabTwoName = TabsNames.Tab2Messages;
                _tabViewExposure.ToolbarItemTabOne = _inbox.ToolbarItem;
                _tabViewExposure.ToolbarItemTabTwo = _sent.ToolbarItem;
                _tabViewExposure.ToolbarItemTabThree = _inbox.ToolbarItemSendMessage;   
                _tabViewExposure.SetInitialProperties(TabsNames.Tab1 = TabsNames.Tab1Messages);
                Content = _tabViewExposure.Content;
            }
            catch (Exception exception)
            {
                _log.Exception(exception);
                AppContextError.Send(typeof(MessageListPage).Name, "InitMessages", exception, null, ExceptionLevel.Application);
            }           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var title = MasterTitle;

            MessagingCenter.Send(this, "masterTitle", title);
            MessagingCenter.Unsubscribe<MessageListPage, string>(this, "masterTitle");
        }
    }


    //        Title = title;
    //        CalServiceApi();

    //        LabelCounter = new Label
    //                       {
    //                           Text = ThreadCount
    //                       };

    //        var listMessageMenuItems = new ObservableCollection<MenuItemViewModel>
    //                                   {
    //                                       new MenuItemViewModel
    //                                       {
    //                                           Icon =
    //                                               DefaultIcon,
    //                                           OptionName =
    //                                               MenuItemsNames
    //                                               .Inbox
    //                                       },
    //                                       new MenuItemViewModel
    //                                       {
    //                                           Icon = DefaultIcon,
    //                                           OptionName =
    //                                               MenuItemsNames
    //                                               .Sent
    //                                       },
    //                                       new MenuItemViewModel
    //                                       {
    //                                           Icon = DefaultIcon,
    //                                           OptionName =
    //                                               MenuItemsNames
    //                                               .Send
    //                                       }
    //                                   };
    //        var dataTemplateMenuOptions = new DataTemplate(typeof (MenuDataTemplate));
    //        var listViewMainControls = new ListView
    //                                   {
    //                                       VerticalOptions = LayoutOptions.Start,
    //                                       ItemsSource = listMessageMenuItems,
    //                                       ItemTemplate = dataTemplateMenuOptions,
    //                                       HasUnevenRows = true
    //                                   };

    //        listViewMainControls.ItemSelected += async (sender, eventArgs) =>
    //                                                   {
    //                                                       if (eventArgs.SelectedItem == null)
    //                                                       {
    //                                                           return;
    //                                                       }
    //                                                       var selectedItemOptionName =
    //                                                           ((MenuItemViewModel) eventArgs.SelectedItem).OptionName;

    //                                                       switch (selectedItemOptionName)
    //                                                       {
    //                                                           case MenuItemsNames.Inbox:
    //                                                               using (
    //                                                                   var contentPageInboxMessage = new InboxMessage())
    //                                                               {
    //                                                                   InboxMessage.IsInbox = true;
    //                                                                   SentMessage.IsSent = false;
    //                                                                   await
    //                                                                       Navigation.PushAsync(contentPageInboxMessage);
    //                                                                   break;
    //                                                               }
    //                                                           case MenuItemsNames.Sent:
    //                                                               using (var contentPageSentMessage = new SentMessage()
    //                                                                   )
    //                                                               {
    //                                                                   SentMessage.IsSent = true;
    //                                                                   InboxMessage.IsInbox = false;
    //                                                                   await
    //                                                                       Navigation.PushAsync(contentPageSentMessage);
    //                                                                   break;
    //                                                               }
    //                                                           case MenuItemsNames.Send:
    //                                                               using (var contentPageSentMessage = new SendMessage()
    //                                                                   )
    //                                                               {
    //                                                                   await
    //                                                                       Navigation.PushAsync(contentPageSentMessage);
    //                                                                   break;
    //                                                               }
    //                                                       }
    //                                                       ((ListView) sender).SelectedItem = null;
    //                                                   };

    //        var stackLayoutMessagesListView = new StackLayout
    //                                          {
    //                                              VerticalOptions = LayoutOptions.FillAndExpand,
    //                                              Orientation = StackOrientation.Vertical,
    //                                              Children = {listViewMainControls}
    //                                          };
    //        var mainStackLayout = new StackLayout
    //                              {
    //                                  Children = {stackLayoutMessagesListView},
    //                                  HorizontalOptions = LayoutOptions.FillAndExpand,
    //                                  VerticalOptions = LayoutOptions.FillAndExpand
    //                              };
    //        Content = mainStackLayout;
    //    }

    //    private async void CalServiceApi()
    //    {
    //        var currentUser = (LoginUser) BeginApplication.Current.Properties["LoginUser"];
    //        var inboxThreads =
    //            await BeginApplication.ProfileServices.GetProfileThreadMessagesInbox(currentUser.AuthToken);
    //        if (inboxThreads != null)
    //        {
    //            var threads = inboxThreads;
    //            ThreadCount = threads.ThreadCount;
    //            LabelCounter.Text = ThreadCount;
    //        }
    //    }

    //    public static class MenuItemsNames
    //    {
    //        public const string Inbox = "Inbox";
    //        public const string Sent = "Sent";
    //        public const string Send = "Send Message";
    //    }

    //    /// <summary>
    //    /// Returns the quantity unreaded Messages
    //    /// </summary>
    //    public string ThreadCount { get; set; }

    //    //protected async override void OnAppearing()
    //    //{
    //    //    base.OnAppearing();

    //    //    if (this.BindingContext == null)
    //    //        this.BindingContext = await (Application.Current)();
    //    //}
    //}
}