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
            Title = "Home";
            var topPadding = Device.OnPlatform<double>(20, 0, 0);
            Padding = new Thickness(0, topPadding, 0, 0);

            Children.Add(new WallPage(
                new Label { Text = "News Feed", Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
                Device.OnPlatform("RSS.png", "padlock.png", "padlock.png")));

            Children.Add(
                new MessageListPage(new Label { Text = "Messages", Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
                    Device.OnPlatform("Messages.png", "padlock.png", "padlock.png")));

            Children.Add(
                new Notification(new Label { Text = "Notifications", Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
                    Device.OnPlatform("Chat.png", "padlock.png", "padlock.png")));

            Children.Add(
                new GroupListPage(new Label { Text = "Groups", Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
                    Device.OnPlatform("Users_three_2.png", "padlock.png", "padlock.png")));

            Children.Add(
               new ContactPage(new Label { Text = "Find Contacts", Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
                   Device.OnPlatform("Users_three_switch.png", "padlock.png", "padlock.png")));
        }
    }
}