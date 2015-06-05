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

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var navigationHome = (NavigationLogin)this.Element;

            if (navigationHome == null)
            {
                return;
            }

            GetCustomActionBar(navigationHome);
        }

        private void GetCustomActionBar(NavigationLogin navHome)
        {
            var activity = (Activity) Context;

            //var actionBar = ((Activity) Context).ActionBar;
            //actionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid()));

            //Set paramas
            activity.ActionBar.SetIcon(new ColorDrawable(Color.Transparent.ToAndroid()));
            
            /* activity.ActionBar.SetDisplayShowCustomEnabled(true);
            activity.ActionBar.Title = "";
            
            var linearLayout = new LinearLayout(activity);
            linearLayout.SetGravity(GravityFlags.Center | GravityFlags.CenterVertical);

            var textViewParameters =
                new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

            textViewParameters.RightMargin = (int)(40 * activity.Resources.DisplayMetrics.Density);
            var modelTitle = new TextView(activity);
            modelTitle.Text = navHome.CurrentPage.Title;
            modelTitle.SetTextColor(global::Android.Graphics.Color.Black);
            modelTitle.SetTypeface(null, global::Android.Graphics.TypefaceStyle.Bold);
            modelTitle.SetTextSize(global::Android.Util.ComplexUnitType.Sp, 16);
            modelTitle.Gravity = GravityFlags.Center;
            linearLayout.AddView(modelTitle, textViewParameters);

            var actionbarParams =
                new ActionBar.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

            activity.ActionBar.SetCustomView(linearLayout, actionbarParams);*/
        }

    }
}