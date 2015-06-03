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
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            Element.PropertyChanged += (s_, e_) => ElementPropertyChanged(s_, e_);
        }

        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.NavigationBar.TintColor = UIColor.Black;
            this.NavigationBar.BarTintColor = UIColor.White;
            this.NavigationBar.BackgroundColor = UIColor.White;
            this.NavigationBar.BarStyle = UIBarStyle.BlackTranslucent;
        }
    }
}