using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeginMobile.iOS.Renderers;
using BeginMobile.Menu;
using BeginMobile.Pages;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationHomePage), typeof(ExtendedNavigationHomeRenderer))]
namespace BeginMobile.iOS.Renderers
{
    class ExtendedNavigationHomeRenderer: NavigationRenderer
    {
        private UIBarButtonItem _uiBarButtonItem;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            _uiBarButtonItem = new UIBarButtonItem(string.Empty, UIBarButtonItemStyle.Bordered, null, null);

            Element.PropertyChanged += (s_, e_) => ElementPropertyChanged(s_, e_);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationBar.TintColor = UIColor.White;
            NavigationBar.BarTintColor = UIColor.FromRGB(60, 186, 133);
        }

        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            var navigationHomePage = (NavigationHomePage)Element;
            if (navigationHomePage == null)
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