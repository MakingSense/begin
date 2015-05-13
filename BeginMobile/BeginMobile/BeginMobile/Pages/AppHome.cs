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
                new Label { Text = AppResources.AppHomeChildNewsFeed, Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
                Device.OnPlatform("iconwallactive.png", "iconwallactive.png", "iconwallactive.png")));

            Children.Add(
                new Notification(new Label { Text = AppResources.AppHomeChildNotifications, Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
                    Device.OnPlatform("iconnotificationsactive.png", "iconnotificationsactive.png", "iconnotificationsactive.png")));

            Children.Add(
                new MessageListPage(new Label { Text = AppResources.AppHomeChildMessages, Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
                    Device.OnPlatform("iconmessagesactive.png", "iconmessagesactive.png", "iconmessagesactive.png")));

            Children.Add(
                new ContactPage(
                    new Label
                    {
                        Text = AppResources.AppHomeChildFindContacts,
                        Style = BeginApplication.Styles.StyleNavigationTitle
                    }.Text,
                    Device.OnPlatform("iconcontactsactive.png", "iconcontactsactive.png", "iconcontactsactive.png")));

            Children.Add(
                new OptionsPage(new Label { Text = "...", Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
                    Device.OnPlatform("iconmenuactive.png", "iconmenuactive.png", "iconmenuactive.png")));
//#if __ANDROID__
//            Children.Add(
//                new OptionsPage(new Label { Text = "...", Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
//                    "padlock.png"));
//#endif
//#if __IOS__
//            Children.Add(
//                new GroupListPage(new Label { Text = AppResources.AppHomeChildGroups, Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
//                    Device.OnPlatform("Users_three_2.png", "padlock.png", "padlock.png")));

//            Children.Add(
//               new ContactPage(new Label { Text = AppResources.AppHomeChildFindContacts, Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
//                   Device.OnPlatform("Users_three_switch.png", "padlock.png", "padlock.png")));
//#endif
        }
    }
}