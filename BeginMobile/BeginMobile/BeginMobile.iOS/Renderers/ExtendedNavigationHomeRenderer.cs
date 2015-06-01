using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeginMobile.iOS.Renderers;
using BeginMobile.Menu;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationHomePage), typeof(ExtendedNavigationHomeRenderer))]
namespace BeginMobile.iOS.Renderers
{
    class ExtendedNavigationHomeRenderer: NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationBar.TintColor = UIColor.White;
            this.NavigationBar.BarTintColor = UIColor.FromRGB(60, 186, 133);
        }
    }
}