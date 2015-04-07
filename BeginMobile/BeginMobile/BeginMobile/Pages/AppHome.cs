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
                new Label { Text = "News Feed", Style = App.Styles.StyleNavigationTitle }.Text,
                Device.OnPlatform("RSS.png", "padlok.png", "padlok.png")));

            Children.Add(
                new MessageListPage(new Label { Text = "Message", Style = App.Styles.StyleNavigationTitle }.Text,
                    Device.OnPlatform("Messages.png", "padlok.png", "padlok.png")));

            Children.Add(
                new Notification(new Label { Text = "Notifications", Style = App.Styles.StyleNavigationTitle }.Text,
                    Device.OnPlatform("Chat.png", "padlok.png", "padlok.png")));

            Children.Add(
                new GroupListPage(new Label { Text = "Groups", Style = App.Styles.StyleNavigationTitle }.Text,
                    Device.OnPlatform("Users_three_2.png", "padlok.png", "padlok.png")));

            Children.Add(new TabContent(new Label { Text = "Opt", Style = App.Styles.StyleNavigationTitle }.Text,
                Device.OnPlatform("More.png", "padlok.png", "padlok.png")));
        }
    }
}