using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeginMobile.Accounts;
using BeginMobile.iOS.Renderers;
using BeginMobile.Menu;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationLogin), typeof(ExtendedNavigationLoginRenderer))]
namespace BeginMobile.iOS.Renderers
{
    public class ExtendedNavigationLoginRenderer : NavigationRenderer
    {
        private UIBarButtonItem _uiBarButtonItem;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            NavigationItem.BackBarButtonItem = new UIBarButtonItem(string.Empty, UIBarButtonItemStyle.Bordered, null, null);
            
            _uiBarButtonItem = new UIBarButtonItem(string.Empty, UIBarButtonItemStyle.Bordered, null, null);
            Element.PropertyChanged += (s_, e_) => ElementPropertyChanged(s_, e_);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationBar.TintColor = UIColor.Black;
            NavigationBar.BarTintColor = UIColor.White;
            NavigationBar.BackgroundColor = UIColor.White;
            NavigationBar.BarStyle = UIBarStyle.BlackTranslucent;
        }

        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            var navigationLogin = (NavigationLogin) Element;
            if (navigationLogin == null)
            {
                return;
            }

            if (e.PropertyName == "CurrentPage" && NavigationBar.TopItem.BackBarButtonItem == null)
            {
                NavigationBar.TopItem.BackBarButtonItem = _uiBarButtonItem;
            }
        }
    }
}