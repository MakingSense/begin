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

        private const string LimitCounter = "9+";
        private const string SpaceCounter = " ";

        public CustomTabbedRenderer()
        {
            //this.SetWillNotDraw(false);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _activity = this.Context as Activity;
        }

        protected override void OnDraw(global::Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
            var appHome = (AppHome)this.Element;
            ActionBar actionBar = _activity.ActionBar;

            SetUpTabs(actionBar, appHome);
        }

        //protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        //{
        //    base.DispatchDraw(canvas);
        //    var appHome = (AppHome)this.Element;
        //    ActionBar actionBar = _activity.ActionBar;

        //    SetUpTabs(actionBar, appHome);
        //}

        //protected override void OnWindowVisibilityChanged(ViewStates visibility)
        //{
        //    base.OnWindowVisibilityChanged(visibility);
        //    var appHome = (AppHome)this.Element;
        //    ActionBar actionBar = _activity.ActionBar;

        //    SetUpTabs(actionBar, appHome);
        //}


        # region "Helper methods"
        private bool TabIsEmpty(ActionBar.Tab tab)
        {
            if (tab != null)
                if (tab.CustomView == null)
                    return true;
            return false;
        }

        private void SetUpTabs(ActionBar actionBar, AppHome appHome)
        {
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
                            var tabMessage = (MessageListPage)childTab;
                            int counter;

                            if (int.TryParse(tabMessage.CounterText.Text, out counter))
                            {
                                if (counter > 0)
                                {
                                    tabBadge.Text = counter >= 9 ?
                                        LimitCounter : SpaceCounter + tabMessage.CounterText.Text + SpaceCounter;

                                    tabTextAux.Gravity = GravityFlags.Bottom | GravityFlags.Left;
                                }
                                else
                                {
                                    tabBadge.Visibility = ViewStates.Invisible;
                                }
                            }
                            else
                            {
                                tabBadge.Visibility = ViewStates.Invisible;
                            }


                        }
                        else if (childTab is Pages.Notifications.Notification)
                        {
                            var tabNotification = (Pages.Notifications.Notification)childTab;
                            int counter;

                            if (int.TryParse(tabNotification.CounterText.Text, out counter))
                            {
                                if (counter > 0)
                                {
                                    tabBadge.Text = counter >= 9
                                        ? LimitCounter
                                        : SpaceCounter + tabNotification.CounterText.Text + SpaceCounter;

                                    tabTextAux.Gravity = GravityFlags.Bottom | GravityFlags.Left;
                                }
                                else
                                {
                                    tabBadge.Visibility = ViewStates.Invisible;
                                }
                            }
                            else
                            {
                                tabBadge.Visibility = ViewStates.Invisible;
                            }
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
        #endregion
    }
}