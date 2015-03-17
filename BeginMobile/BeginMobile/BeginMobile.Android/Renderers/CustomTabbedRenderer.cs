using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedRenderer))]
namespace BeginMobile.Android.Renderers
{
    public class CustomTabbedRenderer: TabbedRenderer
    {
        private Activity _activity;
        private bool _isFirstDesign = true;
        private const string Color = "#FFFFFF";


        public static void Init() { }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _activity = this.Context as Activity;
        }

        protected override void OnWindowVisibilityChanged(ViewStates visibility)
        {
            base.OnWindowVisibilityChanged(visibility);
            if (_isFirstDesign)
            {
                ActionBar actionBar = _activity.ActionBar;

                var colorDrawable = global::Android.Graphics.Color.ParseColor(Color);

                if (actionBar.TabCount > 0)
                {
                    global::Android.App.ActionBar.Tab tabOne = actionBar.GetTabAt(0);
                }

                _isFirstDesign = false;
            }

        }

    }
}