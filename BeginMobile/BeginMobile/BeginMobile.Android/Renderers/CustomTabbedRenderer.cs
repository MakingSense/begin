using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.App;
using Android.Views;
using Android.Widget;
using BeginMobile.Android.Renderers;
using BeginMobile.Menu;
using BeginMobile.Pages;
using BeginMobile.Pages.ContactPages;
using BeginMobile.Pages.MessagePages;
using BeginMobile.Pages.Wall;
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

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            base.DispatchDraw(canvas);
            _activity = this.Context as Activity;

            var element = this.Element;
            if (element == null)
            {
                return;
            }

            if (_activity != null)
            {
                var actionBar = _activity.ActionBar;
                var appHome = (AppHome)this.Element;
                //actionBar.SetDisplayHomeAsUpEnabled(true);
                SetUpTabs(actionBar, appHome);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _activity = this.Context as Activity;

            if (_activity != null && e.PropertyName ==
                AppHome.CounterTextProperty.PropertyName)
            {
                    var actionBar = _activity.ActionBar;
                    var appHome = (AppHome)this.Element;

                    SetUpTabs(actionBar, appHome);
            }
        }

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
                var selectedTab = actionBar.SelectedTab;
                var numTabSelected = selectedTab != null ? selectedTab.Position : 0 ;

                while (count < actionBar.TabCount)
                {
                    var tabOne = actionBar.GetTabAt(count);

                    if (TabIsEmpty(tabOne))
                    {
                        var childTab = (TabContent) appHome.Children[count];

                        tabOne.SetCustomView(Resource.Layout.BarTabLayout);

                        var tabIcontAux = tabOne.CustomView.FindViewById<ImageView>(Resource.Id.tab_icon);
                        var tabBadge = tabOne.CustomView.FindViewById<TextView>(Resource.Id.tab_badge);

                        var typeChild = childTab.GetType();

                        if (typeChild == typeof (WallPage))
                        {
                            var tabMessage = (WallPage) childTab;
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconwallactive
                                : Resource.Drawable.iconwallinactive);

                            tabBadge.Visibility = ViewStates.Invisible;
                        }
                        else if (typeChild == typeof (MessageListPage))
                        {
                            var tabMessage = (MessageListPage) childTab;
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconmessagesactive
                                : Resource.Drawable.iconmessagesinactive);

                            int counter;

                            if (int.TryParse(tabMessage.LabelCounter.Text, out counter))
                            {
                                if (counter > 0)
                                {
                                    tabBadge.Text = counter >= 9
                                        ? LimitCounter
                                        : SpaceCounter + tabMessage.LabelCounter.Text + SpaceCounter;

                                    //tabTextAux.Gravity = GravityFlags.Bottom | GravityFlags.Left;
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
                        else if (typeChild == typeof (Pages.Notifications.Notification))
                        {
                            var tabNotification = (Pages.Notifications.Notification) childTab;
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconnotificationsactive
                                : Resource.Drawable.iconnotificationsinactive);

                            int counter;

                            if (int.TryParse(tabNotification.LabelCounter.Text, out counter))
                            {
                                if (counter > 0)
                                {
                                    tabBadge.Text = counter >= 9
                                        ? LimitCounter
                                        : SpaceCounter + tabNotification.LabelCounter.Text + SpaceCounter;
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
                        else if (typeChild == typeof (ContactPage))
                        {
                            var tabMessage = (ContactPage) childTab;
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconcontactsactive
                                : Resource.Drawable.iconcontactsinactive);
                            tabBadge.Visibility = ViewStates.Invisible;
                        }
                        else if (typeChild == typeof (OptionsPage))
                        {
                            var tabMessage = (OptionsPage) childTab;
                            tabIcontAux.SetImageResource(numTabSelected == count
                                ? Resource.Drawable.iconmenuactive
                                : Resource.Drawable.iconmenuinactive);
                            tabBadge.Visibility = ViewStates.Invisible;
                        }

                    }
                    else
                    {
                        SetUpTabSelected(appHome, tabOne, count, numTabSelected);
                    }

                    count++;
                }
            }
        }

        private void SetUpTabSelected(AppHome appHome, ActionBar.Tab tabOne, int indexTab, int numTabSelected)
        {
            var tabIcontAux = tabOne.CustomView.FindViewById<ImageView>(Resource.Id.tab_icon);

            var childTab = (TabContent)appHome.Children[indexTab];
            var typeChild = childTab.GetType();

            var tabBadge = tabOne.CustomView.FindViewById<TextView>(Resource.Id.tab_badge);

            if (typeChild == typeof(WallPage))
            {
                tabIcontAux.SetImageResource(numTabSelected == indexTab
                        ? Resource.Drawable.iconwallactive
                        : Resource.Drawable.iconwallinactive);
            }
            else if (typeChild == typeof(MessageListPage))
            {
                tabIcontAux.SetImageResource(numTabSelected == indexTab
                        ? Resource.Drawable.iconmessagesactive
                        : Resource.Drawable.iconmessagesinactive);

                var tabMessage = (MessageListPage)childTab;
                int counter;

                if (int.TryParse(tabMessage.LabelCounter.Text, out counter))
                {
                    if (counter > 0)
                    {
                        tabBadge.Text = counter >= 9
                            ? LimitCounter
                            : SpaceCounter + tabMessage.LabelCounter.Text + SpaceCounter;

                        tabBadge.Visibility = ViewStates.Visible;
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
            else if (typeChild == typeof(Pages.Notifications.Notification))
            {
                tabIcontAux.SetImageResource(numTabSelected == indexTab
                        ? Resource.Drawable.iconnotificationsactive
                        : Resource.Drawable.iconnotificationsinactive);

                var tabNotification = (Pages.Notifications.Notification)childTab;
                int counter;

                if (int.TryParse(tabNotification.LabelCounter.Text, out counter))
                {
                    if (counter > 0)
                    {
                        tabBadge.Text = counter >= 9
                            ? LimitCounter
                            : SpaceCounter + tabNotification.LabelCounter.Text + SpaceCounter;

                        tabBadge.Visibility = ViewStates.Visible;
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
            else if (typeChild == typeof(ContactPage))
            {
                tabIcontAux.SetImageResource(numTabSelected == indexTab
                        ? Resource.Drawable.iconcontactsactive
                        : Resource.Drawable.iconcontactsinactive);
            }
            else if (typeChild == typeof(OptionsPage))
            {
                tabIcontAux.SetImageResource(numTabSelected == indexTab
                        ? Resource.Drawable.iconmenuactive
                        : Resource.Drawable.iconmenuinactive);
            }
        }
        #endregion
    }
}