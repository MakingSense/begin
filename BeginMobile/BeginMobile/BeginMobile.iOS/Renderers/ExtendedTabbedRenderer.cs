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
        private const string LimitCounter = "9+";

        private UITabBarController tabBarController;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _appHome = (AppHome)Element;
            if (_appHome == null) return;
            if (!_appHome.Children.Any()) return;

            SetUpTabs();
        }

        private void SetUpTabs()
        {
            //var tabBarController = ViewController as UITabBarController;
            if (tabBarController == null) return;

            foreach (PageRenderer viewController in tabBarController.ViewControllers)
            {
                var childFromTab = viewController.Element;
                if (childFromTab == null) continue;

                viewController.Title = "";

                //viewController.HidesBottomBarWhenPushed = true;
                //viewController.NavigationController.PushViewController(tabBarController, true);

                var type = childFromTab.GetType();

                if (type == typeof(WallPage))
                {
                    viewController.TabBarItem.SelectedImage = UIImage.FromBundle("iconwallactive.png");
                }
                else if (type == typeof(MessageListPage))
                {
                    viewController.TabBarItem.SelectedImage = UIImage.FromBundle("iconmessagesactive.png");

                    var numMessages = ((MessageListPage)childFromTab).LabelCounter.Text;

                    if (string.IsNullOrEmpty
                        (numMessages)) continue;

                    var counter = int.Parse(numMessages);
                    if (counter > 0)
                    {
                        viewController.TabBarItem.BadgeValue = counter > 9 ? LimitCounter : numMessages;
                    }
                }

                else if (type == typeof(Notification))
                {
                    viewController.TabBarItem.SelectedImage = UIImage.FromBundle("iconnotificationsactive.png");

                    var numNotifications = ((Notification)childFromTab).LabelCounter.Text;

                    if (string.IsNullOrEmpty (numNotifications)) continue;
                    var counter = int.Parse(numNotifications);
                    if (counter > 0)
                    {
                        viewController.TabBarItem.BadgeValue = counter > 9 ? LimitCounter : numNotifications;
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
          
            var page = (AppHome)Element;

            if (!page.Children.Any ()) {
                return;
            }

            TabBar.TintColor = UIColor.FromRGB (68, 68, 68);
            TabBar.BarTintColor = UIColor.White;

            Element.PropertyChanged += (s_, e_) => ElementPropertyChanged(s_, e_);
        }


        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == AppHome.CounterTextProperty.PropertyName)
            {
                SetUpTabs();
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            tabBarController = ViewController as UITabBarController;
            
            //NavigationController.PushViewController(tabBarController, true);
            //AppDelegate.TabBarController = _uiTabBarController;
        }
    }
}