using System;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Menu;
using BeginMobile.Pages.ContactPages;
using BeginMobile.Pages.MessagePages;
using BeginMobile.Pages.Notifications;
using BeginMobile.Pages.Profile;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Logging;
using BeginMobile.Services.Utils;
using Xamarin.Forms;
using BeginMobile.Pages.Wall;
using System.ComponentModel;

namespace BeginMobile.Pages
{
    public class AppHome : TabbedPage
    {
        private MessageListPage _messages;
        private Notification _notification;
        private readonly ILoggingService _log = Logger.Current;

        public static BindableProperty CounterTextProperty =
        BindableProperty.Create<AppHome, string>(p => p.CounterText, string.Empty);
        public string CounterText
        {
            get{return (string)base.GetValue(CounterTextProperty);}
            set{base.SetValue(CounterTextProperty, value);}
        }

        public AppHome()	
        {
            LoadChilds();
        }

        private void LoadChilds()
        {
            Title = AppResources.AppHomeTitle;
            var topPadding = Device.OnPlatform<double>(20, 0, 0);
            Padding = new Thickness(0, topPadding, 0, 0);
            Children.Add(new WallPage(
                new Label
                {
                    Text =
                        Device.OnPlatform(AppResources.AppHomeChildNewsFeed, AppResources.AppHomeChildNewsFeed,
                            AppResources.AppHomeChildNewsFeed),
                    Style = BeginApplication.Styles.StyleNavigationTitle
                }.Text,
                Device.OnPlatform("iconwallactive.png", "iconwallactive.png", "iconwallactive.png")));

            _messages = new MessageListPage(new Label
                                            {
                                                Text =
                                                    Device.OnPlatform(AppResources.AppHomeChildMessages, AppResources.AppHomeChildMessages,
                                                        AppResources.AppHomeChildMessages),
                                                Style = BeginApplication.Styles.StyleNavigationTitle
                                            }.Text,
                Device.OnPlatform("iconmessagesactive.png", "iconmessagesactive.png", "iconmessagesactive.png"), this);

            _notification = new Notification(new Label
                                                    {
                                                        Text =
                                                            Device.OnPlatform(AppResources.AppHomeChildNotifications,
                                                                AppResources.AppHomeChildNotifications,
                                                                AppResources.AppHomeChildNotifications),
                                                        Style = BeginApplication.Styles.StyleNavigationTitle
                                                    }.Text,
                Device.OnPlatform("iconnotificationsactive.png", "iconnotificationsactive.png",
                    "iconnotificationsactive.png"), this);



            Children.Add(_notification);
            Children.Add(_messages);

            Children.Add(
                new ContactPage(
                    new Label
                    {
                        Text =
                            Device.OnPlatform(AppResources.AppHomeChildFindContacts, AppResources.AppHomeChildFindContacts,
                                AppResources.AppHomeChildFindContacts),
                        Style = BeginApplication.Styles.StyleNavigationTitle
                    }.Text,
                    Device.OnPlatform("iconcontactsactive.png", "iconcontactsactive.png", "iconcontactsactive.png")));

            Children.Add(
                new OptionsPage(
                    new Label
                    {
                        Text = Device.OnPlatform("Menu", "Menu", "Menu"),
                        Style = BeginApplication.Styles.StyleNavigationTitle
                    }.Text,
                    Device.OnPlatform("iconmenuactive.png", "iconmenuactive.png", "iconmenuactive.png")));


            Title = CurrentPage.Title;

            this.CurrentPageChanged += OnPropertyChanging;
        }

        private void OnPropertyChanging(object sender, EventArgs e)
        {
            var item = sender as TabbedPage;

            if (item == null)
            {
                return;
            }

            Title = item.CurrentPage.Title;
            var itemMessage = item.CurrentPage as MessageListPage;

            if (itemMessage != null)
            {
                try
                {
                    _messages.InitMessages();
                }
                catch (Exception exception)
                {
                    _log.Exception(exception);
                    AppContextError.Send(typeof(AppHome).Name, "OnPropertyChanging", exception, null, ExceptionLevel.Application);
                }
            }           
            #region for notifications two tabs discoment this setences
            #endregion
        }

        public void NotifyDateSelected(DateTime dateSelected)
        {
            if (DateSelected != null)
                DateSelected(this, dateSelected);
        }

        public event EventHandler<DateTime> DateSelected;
    }
}