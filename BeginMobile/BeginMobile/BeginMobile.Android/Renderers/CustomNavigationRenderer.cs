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
using BeginMobile.Menu;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NavigationHomePage), typeof(CustomNavigationRenderer))]
namespace BeginMobile.Android.Renderers
{
    public class CustomNavigationRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            var navigationHome = (NavigationHomePage)this.Element;

            if (navigationHome == null)
            {
                return;
            }

            RemoveAppIconFromActionBar(navigationHome);
        }

        void RemoveAppIconFromActionBar(NavigationHomePage navHome)
        {
            var actionBar = ((Activity)Context).ActionBar;
            actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid()));

            /*//var paramters = new ActionBar.LayoutParams(LayoutParams.MatchParent,
            //    LayoutParams.MatchParent,GravityFlags.Center);

            //LayoutInflater inflator = LayoutInflater.From(this.Context);
            //global::Android.Views.View view = inflator.Inflate(Resource.Layout.ActionBar, null);

            actionBar.SetDisplayShowCustomEnabled(true);
            actionBar.SetDisplayOptions(ActionBarDisplayOptions.ShowHome, ActionBarDisplayOptions.ShowHome);
            //actionBar.SetCustomView(view, paramters);
            actionBar.SetCustomView(Resource.Layout.ActionBar);

            var titleText = actionBar.CustomView.FindViewById<TextView>(Resource.Id.action_bar_title);
            titleText.Text = navHome.Title;
            //titleText.Text = "Siiiiiiiiiiiiiiiiiiiiiiiiiiii";*/
        }
    }
}