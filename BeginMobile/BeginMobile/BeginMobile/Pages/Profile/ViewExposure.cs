using System;
using System.Linq;
using BeginMobile.Pages.ContactPages;
using BeginMobile.Pages.GroupPages;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ViewExposure : ContentPage
    {
        private Grid _gridResults;
        private readonly Button _buttonTab1;
        private readonly Button _buttonTab2;
        private readonly Button _buttonTab3;
        private readonly BoxView _boxViewLineSelectedTab1;
        private readonly BoxView _boxViewLineSeletedTab2;
        private readonly BoxView _boxViewLineSeletedTab3;
        private readonly Information _information;
        private readonly ContactPage _allContacts;
        private readonly Contacts _requestContacts;
        private readonly MyActivity _activity;
        private readonly GroupListPage _allGroups;
        private readonly Groups _myGroups;
        private readonly Shop _shops;
        private readonly Events _myEvents;
        private readonly TabViewExposure _tabViewExposure;

        public ViewExposure()
        {
            _information = new Information();
            _allContacts = new ContactPage(String.Empty, String.Empty);
            _requestContacts = new Contacts();
            _activity = new MyActivity();
            _allGroups = new GroupListPage(String.Empty, String.Empty);
            _myGroups = new Groups();
            _shops = new Shop();
            _myEvents = new Events();
            _tabViewExposure = new TabViewExposure();

            Style = BeginApplication.Styles.PageStyle;

            _buttonTab1 = new Button
                          {
                              Text = String.Empty,
                              Style = BeginApplication.Styles.LinkButton,
                          };
            _buttonTab1.Clicked += EventHandlerTab1;

            _buttonTab2 = new Button
                          {
                              Text = String.Empty,
                              Style = BeginApplication.Styles.LinkButton,
                          };
            _buttonTab2.Clicked += EventHandlerTab2;

            _buttonTab3 = new Button
                          {
                              Text = String.Empty,
                              Style = BeginApplication.Styles.LinkButton,
                          };
            _buttonTab3.Clicked += EventHandlerTab3;

            _boxViewLineSelectedTab1 = new BoxView
                                       {
                                           Style = BeginApplication.Styles.TabUnderLine,
                                           IsVisible = false
                                       };
            _boxViewLineSeletedTab2 = new BoxView
                                      {
                                          Style = BeginApplication.Styles.TabUnderLine,
                                          IsVisible = false
                                      };
            _boxViewLineSeletedTab3 = new BoxView
                                      {
                                          Style = BeginApplication.Styles.TabUnderLine,
                                          IsVisible = false
                                      };


            var mainGrid = new Grid
                           {
                               Padding = BeginApplication.Styles.LayoutThickness,
                               HorizontalOptions = LayoutOptions.Fill,
                               VerticalOptions = LayoutOptions.Start,
                               RowDefinitions = new RowDefinitionCollection
                                                {
                                                    new RowDefinition {Height = GridLength.Auto},
                                                    new RowDefinition {Height = GridLength.Auto}
                                                }
                           };

            var gridControls = new Grid
                               {
                                   HorizontalOptions = LayoutOptions.Center,
                                   VerticalOptions = LayoutOptions.Start,
                                   RowDefinitions = new RowDefinitionCollection
                                                    {
                                                        new RowDefinition {Height = GridLength.Auto},
                                                        new RowDefinition {Height = GridLength.Auto}
                                                    },
                                   ColumnDefinitions = new ColumnDefinitionCollection
                                                       {
                                                           new ColumnDefinition {Width = GridLength.Auto},
                                                           new ColumnDefinition {Width = GridLength.Auto},
                                                           new ColumnDefinition {Width = GridLength.Auto},
                                                       }
                               };
            gridControls.Children.Add(_buttonTab1, 0, 0);
            gridControls.Children.Add(_boxViewLineSelectedTab1, 0, 1);
            gridControls.Children.Add(_buttonTab2, 1, 0);
            gridControls.Children.Add(_boxViewLineSeletedTab2, 1, 1);
            gridControls.Children.Add(_buttonTab3, 2, 0);
            gridControls.Children.Add(_boxViewLineSeletedTab3, 2, 1);
            _gridResults = new Grid();

            mainGrid.Children.Add(gridControls, 0, 0);
            mainGrid.Children.Add(_gridResults, 0, 1);
            Content = mainGrid;
        }

        public ContentPage PageOne { get; set; }
        public ContentPage PageTwo { get; set; }
        public ContentPage PageThree { get; set; }
        public String TabOneName { get; set; }
        public String TabTwoName { get; set; }
        public String TabThreeName { get; set; }
        public ToolbarItem ToolbarItemTabOne { get; set; }
        public ToolbarItem ToolbarItemTabTwo { get; set; }
        public ToolbarItem ToolbarItemTabThree { get; set; }

        public Grid GridReceived { get; set; }

        public void SetViewToExpose(string tabSelected)
        {
            if (tabSelected.Equals(TabsNames.Tab1Activity))
            {
                SetTabOneSettings();
            }
            else if (tabSelected.Equals(TabsNames.Tab2Information))
            {
                SetTabTwoSettings();
            }
            else
            {
                SetTabThreeSettings();
            }
            _buttonTab1.Text = TabOneName;
            _buttonTab2.Text = TabTwoName;
            _buttonTab3.Text = TabThreeName;
        }

        private void SetTabOneSettings()
        {
            CleanResultsAndToolBarItems();
            _buttonTab1.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _buttonTab2.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonTab3.TextColor = BeginApplication.Styles.DefaultColorButton;
            _boxViewLineSelectedTab1.IsVisible = true;
            _boxViewLineSeletedTab2.IsVisible = false;
            _boxViewLineSeletedTab3.IsVisible = false;
            if (PageOne != null) _gridResults.Children.Add(PageOne.Content, 0, 0);
            if (ToolbarItemTabOne != null)
            {
#if __ANDROID__ || __IOS__
                ToolbarItems.Add(ToolbarItemTabOne);
#endif
            }
        }

        private void SetTabTwoSettings()
        {
            CleanResultsAndToolBarItems();
            _buttonTab1.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonTab2.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _buttonTab3.TextColor = BeginApplication.Styles.DefaultColorButton;
            _boxViewLineSelectedTab1.IsVisible = false;
            _boxViewLineSeletedTab2.IsVisible = true;
            _boxViewLineSeletedTab3.IsVisible = false;

            if (PageTwo != null) _gridResults.Children.Add(PageTwo.Content, 0, 0);
            if (ToolbarItemTabTwo != null)
            {
#if __ANDROID__ || __IOS__
                ToolbarItems.Add(ToolbarItemTabTwo);
#endif
            }
        }

        private void SetTabThreeSettings()
        {
            CleanResultsAndToolBarItems();
            _buttonTab1.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonTab2.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonTab3.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewLineSelectedTab1.IsVisible = false;
            _boxViewLineSeletedTab2.IsVisible = false;
            _boxViewLineSeletedTab3.IsVisible = true;

            if (ToolbarItemTabThree != null)
            {
#if __ANDROID__ || __IOS__
                ToolbarItems.Add(ToolbarItemTabThree);
#endif
            }
        }

        private void CleanResultsAndToolBarItems()
        {
            _gridResults.Children.Clear();
            ToolbarItems.Clear();
        }

        private void EventHandlerTab1(object sender, EventArgs e)
        {
            CleanResultsAndToolBarItems();
            SetTabOneSettings();
        }

        private void EventHandlerTab2(object sender, EventArgs e)
        {
            CleanResultsAndToolBarItems();
            SetTabTwoSettings();
        }

        private async void EventHandlerTab3(object sender, EventArgs e)
        {
            CleanResultsAndToolBarItems();
            SetTabThreeSettings();

            var action = await DisplayActionSheet(null, MoreOptionsNames.Cancel, null, MoreOptionsNames.Contacts,
                MoreOptionsNames.Groups, MoreOptionsNames.Shops, MoreOptionsNames.Events);

            switch (action)
            {
                case MoreOptionsNames.Contacts:
                    _tabViewExposure.PageOne = _allContacts;
                    _tabViewExposure.PageTwo = _requestContacts;
                    _tabViewExposure.TabOneName = TabsNames.Tab1Contacts;
                    _tabViewExposure.TabTwoName = TabsNames.Tab2Contacts;
                    _tabViewExposure.ToolbarItemTabOne = _allContacts.ToolbarItem;
                    _tabViewExposure.ToolbarItemTabTwo = _requestContacts.ToolbarItem;
                    _tabViewExposure.SetInitialProperties(TabsNames.Tab1 = TabsNames.Tab1Contacts);
                    //set selected item  
                    await Navigation.PushAsync(_tabViewExposure);
                    break;
                case MoreOptionsNames.Groups:
                    _tabViewExposure.PageOne = _allGroups;
                    _tabViewExposure.PageTwo = _myGroups;
                    _tabViewExposure.TabOneName = TabsNames.Tab1Groups;
                    _tabViewExposure.TabTwoName = TabsNames.Tab2Groups;
                    _tabViewExposure.ToolbarItemTabOne = _allGroups.ToolbarItem;
                    _tabViewExposure.SetInitialProperties(TabsNames.Tab1 = TabsNames.Tab1Groups); //set selected item   
                    await Navigation.PushAsync(_tabViewExposure);
                    break;
                case MoreOptionsNames.Shops:
                    await Navigation.PushAsync(_shops);
                    break;
                case MoreOptionsNames.Events:
                    _tabViewExposure.PageOne = _myEvents;
                    _tabViewExposure.PageTwo = _myEvents;
                    _tabViewExposure.TabOneName = TabsNames.Tab1Events;
                    _tabViewExposure.TabTwoName = TabsNames.Tab2Events;
                    _tabViewExposure.ToolbarItemTabOne = _myEvents.ToolbarItem;
                    _tabViewExposure.ToolbarItemTabTwo = _myEvents.ToolbarItem;
                    _tabViewExposure.SetInitialProperties(TabsNames.Tab1 = TabsNames.Tab1Events); //set selected item   
                    await Navigation.PushAsync(_tabViewExposure);
                    break;
                case MoreOptionsNames.Cancel:
                    return;

                default:
                    return;
            }
        }
    }

    public static class TabsNames
    {
        //TODO add to resources

        public const string Tab1Activity = "Activity";
        public const string Tab2Information = "Information";
        public const string Tab1Groups = "Memberships";
        public const string Tab2Groups = "Invitations";
        public const string Tab1Contacts = "Contacts";
        public const string Tab2Contacts = "Requests";
        public const string Tab1Events = "My Events";
        public const string Tab2Events = "Attending";
        public const string Tab1Messages = "Inbox";
        public const string Tab2Messages = "Sent";
        public const string TabMore = "More";
        public static string Tab1 { get; set; }
        public static string Tab2 { get; set; }
    }
}