using System;
using System.Collections.Specialized;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Menu;
using BeginMobile.Pages.ContactPages;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Pages.MessagePages;
using BeginMobile.Pages.Notifications;
using BeginMobile.Pages.Wall;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class AppHome : TabbedPage
    {
        private MessageListPage _messages;
        private Notifications.Notification _notification;
        public AppHome()	
        {
            LoadChilds();
        }

        private void LoadChilds()
        {
            Title = AppResources.AppHomeTitle;
            var topPadding = Device.OnPlatform<double>(20, 0, 0);
            Padding = new Thickness(0, topPadding, 0, 0);


            _messages = new MessageListPage(new Label
                                            {
                                                Text =
                                                    Device.OnPlatform(string.Empty, AppResources.AppHomeChildMessages,
                                                        AppResources.AppHomeChildMessages),
                                                Style = BeginApplication.Styles.StyleNavigationTitle
                                            }.Text,
                Device.OnPlatform("iconmessagesactive.png", "iconmessagesactive.png", "iconmessagesactive.png"));

            _notification = new Notification(new Label
                                                    {
                                                        Text =
                                                            Device.OnPlatform(string.Empty,
                                                                AppResources.AppHomeChildNotifications,
                                                                AppResources.AppHomeChildNotifications),
                                                        Style = BeginApplication.Styles.StyleNavigationTitle
                                                    }.Text,
                Device.OnPlatform("iconnotificationsactive.png", "iconnotificationsactive.png",
                    "iconnotificationsactive.png"));

            Children.Add(new WallPage(
                new Label
                {
                    Text =
                        Device.OnPlatform(string.Empty, AppResources.AppHomeChildNewsFeed,
                            AppResources.AppHomeChildNewsFeed),
                    Style = BeginApplication.Styles.StyleNavigationTitle
                }.Text,
                Device.OnPlatform("iconwallactive.png", "iconwallactive.png", "iconwallactive.png")));

            Children.Add(_notification);
            Children.Add(_messages);

            Children.Add(
                new ContactPage(
                    new Label
                    {
                        Text =
                            Device.OnPlatform(string.Empty, AppResources.AppHomeChildFindContacts,
                                AppResources.AppHomeChildFindContacts),
                        Style = BeginApplication.Styles.StyleNavigationTitle
                    }.Text,
                    Device.OnPlatform("iconcontactsactive.png", "iconcontactsactive.png", "iconcontactsactive.png")));

            Children.Add(
                new OptionsPage(
                    new Label
                    {
                        Text = Device.OnPlatform(string.Empty, "...", "..."),
                        Style = BeginApplication.Styles.StyleNavigationTitle
                    }.Text,
                    Device.OnPlatform("iconmenuactive.png", "iconmenuactive.png", "iconmenuactive.png")));

            this.CurrentPageChanged += OnPropertyChanging;
        }

        private void OnPropertyChanging(object sender, EventArgs e)
        {
            var item = sender as TabbedPage;

            if (item != null && item.CurrentPage.Title.Equals(AppResources.AppHomeChildMessages))
            {
                _messages.InitMessages();
            }
            if (item != null && item.CurrentPage.Title.Equals(AppResources.AppHomeChildNotifications))
            {
                _notification.InitilizeNotification();
            }
        }
    }
}