using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BeginMobile.Android.Renderers;
using Xamarin.Forms;
using BeginMobile.Menu;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NavigationLogin), typeof(CustomNavigationLoginRenderer))]
namespace BeginMobile.Android.Renderers
{
    public class CustomNavigationLoginRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            var navigationHome = (NavigationLogin)this.Element;

            if (navigationHome == null)
            {
                return;
            }

            RemoveAppIconFromActionBar(navigationHome);
        }

        private void RemoveAppIconFromActionBar(NavigationLogin navHome)
        {
            var actionBar = ((Activity) Context).ActionBar;
            actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid()));
            actionBar.SetBackgroundDrawable(new ColorDrawable(Color.Yellow.ToAndroid()));
        }
    }
}