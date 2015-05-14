using System.Linq;
using BeginMobile.iOS.Renderers;
using BeginMobile.Menu;
using BeginMobile.Pages;
using BeginMobile.Pages.ContactPages;
using BeginMobile.Pages.MessagePages;
using BeginMobile.Pages.Notifications;
using BeginMobile.Pages.Wall;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AppHome), typeof(ExtendedTabbedRenderer))]
namespace BeginMobile.iOS.Renderers
{
    public class ExtendedTabbedRenderer : TabbedRenderer
    {
        private AppHome _appHome;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _appHome = (AppHome)Element;
            if (_appHome == null) return;
            if (!_appHome.Children.Any()) return;

            var tabBarController = ViewController as UITabBarController;
            if (tabBarController == null) return;

            foreach (var viewController in tabBarController.ViewControllers)
            {
                var navItemTitle = viewController.NavigationItem.Title;

                var childFromTab = _appHome.Children.FirstOrDefault(x => x.Title == navItemTitle);
                if (childFromTab == null) continue;

                var type = childFromTab.GetType();

                if (type == typeof(WallPage))
                {
                    viewController.TabBarItem.SelectedImage = UIImage.FromBundle("iconwallactive.png"); 
                }

                else if (type == typeof(MessageListPage))
                {
                    viewController.TabBarItem.SelectedImage = UIImage.FromBundle("iconmessagesactive.png"); 
                    if (string.IsNullOrEmpty
                        (((MessageListPage) childFromTab).LabelCounter.Text)) continue;

                    var counter = int.Parse(((MessageListPage) childFromTab).LabelCounter.Text);
                    if (counter > 0)
                    {
                        viewController.TabBarItem.BadgeValue = ((MessageListPage)childFromTab).LabelCounter.Text;
                    }
                }

                else if (type == typeof(Notification))
                {
                    viewController.TabBarItem.SelectedImage = UIImage.FromBundle("iconnotificationsactive.png"); 

                    if (string.IsNullOrEmpty
                        (((Notification)childFromTab).LabelCounter.Text)) continue;

                    var counter = int.Parse(((Notification)childFromTab).LabelCounter.Text);
                    if (counter > 0)
                    {
                        viewController.TabBarItem.BadgeValue = ((Notification)childFromTab).LabelCounter.Text;
                    }
                }

                else if (type == typeof(ContactPage))
                {
                    viewController.TabBarItem.SelectedImage = UIImage.FromBundle("iconcontactsactive.png"); 
                }

                else if (type == typeof(OptionsPage))
                {
                    viewController.TabBarItem.SelectedImage = UIImage.FromBundle("iconmenuactive.png"); 
                }
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            _appHome = e.NewElement as AppHome;
        }
    }
}