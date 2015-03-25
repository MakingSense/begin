using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.App;
using Android.Views;
using Android.Widget;
using BeginMobile.Android.Renderers;
using BeginMobile.Pages;
using BeginMobile.Pages.MessagePages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(AppHome), typeof(CustomTabbedRenderer))]
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
                        var childTab = (TabContent)appHome.Children[count];

                        tabOne.SetCustomView(Resource.Layout.BarTabLayout);

                        var tabTextAux = tabOne.CustomView.FindViewById<TextView>(Resource.Id.tab_title);
                        var tabBadge = tabOne.CustomView.FindViewById<TextView>(Resource.Id.tab_badge);
                        tabTextAux.Text = childTab.Title;

                        if (childTab is MessageListPage)
                        {
                            var tabMessage = (MessageListPage) childTab;
                            var counter = int.Parse(tabMessage.CounterText.Text);

                            tabBadge.Text = counter > 9 ?
                                tabMessage.CounterText.Text + "+" : " " + tabMessage.CounterText.Text + " ";
                        } 
                        else if(childTab is Pages.Notifications.Notification)
                        {
                            var tabNotification = (Pages.Notifications.Notification)childTab;
                            var counter = int.Parse(tabNotification.CounterText.Text);

                            tabBadge.Text = counter > 9 ?
                                tabNotification.CounterText.Text + "+" : " "+tabNotification.CounterText.Text+" ";
                        }
                        else
                        {
                            tabBadge.Visibility = ViewStates.Invisible;
                        }

                    }
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