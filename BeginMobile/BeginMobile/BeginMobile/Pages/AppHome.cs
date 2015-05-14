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

			Children.Add (new WallPage (
				new Label {
					Text = Device.OnPlatform (string.Empty, AppResources.AppHomeChildNewsFeed, AppResources.AppHomeChildNewsFeed),
					Style = BeginApplication.Styles.StyleNavigationTitle
				}.Text,
				Device.OnPlatform ("iconwallactive.png", "iconwallactive.png", "iconwallactive.png")));

			Children.Add (
				new Notification (new Label {
					Text = Device.OnPlatform (string.Empty, AppResources.AppHomeChildNotifications, AppResources.AppHomeChildNotifications),
					Style = BeginApplication.Styles.StyleNavigationTitle
				}.Text,
					Device.OnPlatform ("iconnotificationsactive.png", "iconnotificationsactive.png", "iconnotificationsactive.png")));

			Children.Add (
				new MessageListPage (new Label {
					Text = Device.OnPlatform (string.Empty, AppResources.AppHomeChildMessages, AppResources.AppHomeChildMessages),
					Style = BeginApplication.Styles.StyleNavigationTitle
				}.Text,
					Device.OnPlatform ("iconmessagesactive.png", "iconmessagesactive.png", "iconmessagesactive.png")));
            
			Children.Add (
				new ContactPage (
					new Label {
						Text = Device.OnPlatform (string.Empty, AppResources.AppHomeChildFindContacts, AppResources.AppHomeChildFindContacts),
						Style = BeginApplication.Styles.StyleNavigationTitle
					}.Text,
					Device.OnPlatform ("iconcontactsactive.png", "iconcontactsactive.png", "iconcontactsactive.png")));

            Children.Add(
                new OptionsPage(new Label { Text = Device.OnPlatform(string.Empty, "..." , "..."), Style = BeginApplication.Styles.StyleNavigationTitle }.Text,
                    Device.OnPlatform("iconmenuactive.png", "iconmenuactive.png", "iconmenuactive.png")));

        }
    }
}