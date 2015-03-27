using System.Linq;
using BeginMobile.iOS.Renderers;
using BeginMobile.Pages;
using BeginMobile.Pages.MessagePages;
using BeginMobile.Pages.Notifications;
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

                if (type == typeof(MessageListPage))
                {
                    if (string.IsNullOrEmpty
                        (((MessageListPage) childFromTab).CounterText.Text)) continue;

                    var counter = int.Parse(((MessageListPage) childFromTab).CounterText.Text);
                    if (counter > 0)
                    {
                        viewController.TabBarItem.BadgeValue = ((MessageListPage)childFromTab).CounterText.Text;
                    }
                }

                else if (type == typeof(Notification))
                {
                    if (string.IsNullOrEmpty
                        (((Notification) childFromTab).CounterText.Text)) continue;

                    var counter = int.Parse(((Notification)childFromTab).CounterText.Text);
                    if (counter > 0)
                    {
                        viewController.TabBarItem.BadgeValue = ((Notification)childFromTab).CounterText.Text;
                    }
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