using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.App;
using Android.Views;
using BeginMobile.Android.Renderers;
using BeginMobile.Pages;
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

        public CustomTabbedRenderer()
        {
            this.SetWillNotDraw(false);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _activity = this.Context as Activity;
        }

        public override void Draw(global::Android.Graphics.Canvas canvas)
        {
            base.Draw(canvas);
            var appHome = (AppHome)this.Element;

            ActionBar actionBar = _activity.ActionBar;

            actionBar.NavigationMode = ActionBarNavigationMode.Tabs;
                
            if (actionBar.TabCount > 0)
            {
                var count = 0;
                while (count < actionBar.TabCount)
                {
                    ActionBar.Tab tabOne = actionBar.GetTabAt(count);

                    if (TabIsEmpty(tabOne))
                    {
                        var imageIcon = count == (actionBar.TabCount - 1)
                        ? Resource.Drawable.menunav
                        : Resource.Drawable.padlock;

                        tabOne.SetIcon(imageIcon);
                    }
                        
                    //padlock
                    count++;
                }
            }

            
         }

        private bool TabIsEmpty(ActionBar.Tab tab)
        {
            if (tab != null)
                if (tab.CustomView == null)
                    return true;
            return false;
        }

        /*protected override void OnWindowVisibilityChanged(ViewStates visibility)
        {
            base.OnWindowVisibilityChanged(visibility);
            ActionBar actionBar = _activity.ActionBar;

            actionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            if (actionBar.TabCount > 0)
            {
                var count = 0;
                while (count < actionBar.TabCount)
                {
                    ActionBar.Tab tabOne = actionBar.GetTabAt(count);

                    var imageIcon = count == (actionBar.TabCount - 1)
                        ? Resource.Drawable.menunav
                        : Resource.Drawable.padlock;

                    tabOne.SetIcon(imageIcon);

                    //padlock
                    count++;
                }
            }
        }*/

        
    }
}