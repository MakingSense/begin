using System;
using BeginMobile.Pages.ContactPages;
using BeginMobile.Pages.GroupPages;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ViewExposure : ContentPage
    {
        private readonly Grid _gridResults;
        private readonly Label _tabOne;
        private readonly Label _tabTwo;
        private readonly Label _tabThree;
        private readonly BoxView _boxViewSelectedTabOne;
        private readonly BoxView _boxViewSeletedTabTwo;
        private readonly BoxView _boxViewSeletedTabThree;
        private readonly BoxView _boxViewInactiveTabOne;
        private readonly BoxView _boxViewInactiveTabTwo;
        private readonly BoxView _boxViewInactiveTabThree;
        //private readonly ContactPage _allContacts;
        private readonly Contacts _myContacts;
        //private readonly GroupListPage _allGroups;
        private readonly Groups _myGroups;
        private readonly Shop _shops;
        private readonly Events _myEvents;
        private readonly TabViewExposure _tabViewExposure;
        private string _tabSelected = "";

        public ViewExposure()
        {
            //_allContacts = new ContactPage(String.Empty, String.Empty);
            _myContacts = new Contacts();
            //_allGroups = new GroupListPage(String.Empty, String.Empty);
            _myGroups = new Groups();
            _shops = new Shop();
            _myEvents = new Events();
            _tabViewExposure = new TabViewExposure();
            var help = this.Parent;
           // Style = BeginApplication.Styles.PageStyle;
            BackgroundColor = BeginApplication.Styles.ColorWhiteBackground;

            var tapGestureRecognizerTabOne = new TapGestureRecognizer
                                             {
                                                 NumberOfTapsRequired = 1
                                             };
            var tapGestureRecognizerTabTwo = new TapGestureRecognizer
                                             {
                                                 NumberOfTapsRequired = 1
                                             };
            var tapGestureRecognizerTabThree = new TapGestureRecognizer
                                               {
                                                   NumberOfTapsRequired = 1
                                               };

            tapGestureRecognizerTabOne.Tapped += EventHandlerTabOne;
            tapGestureRecognizerTabTwo.Tapped += EventHandlerTabTwo;
            tapGestureRecognizerTabThree.Tapped += EventHandlerTabThree;

            _tabOne = new Label
                      {
                          Text = string.Empty,
                          XAlign = TextAlignment.Center,
                          FontSize = BeginApplication.Styles.TextFontSizeMedium,
                          VerticalOptions = LayoutOptions.Center,
                          HorizontalOptions = LayoutOptions.Center
                      };
            _tabTwo = new Label
                      {
                          Text = string.Empty,
                          XAlign = TextAlignment.Center,
                          FontSize = BeginApplication.Styles.TextFontSizeMedium,
                          VerticalOptions = LayoutOptions.Center,
                          HorizontalOptions = LayoutOptions.Center
                      };
            _tabThree = new Label
                        {
                            Text = string.Empty,
                            XAlign = TextAlignment.Center,
                            FontSize = BeginApplication.Styles.TextFontSizeMedium,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center
                        };

            _tabOne.GestureRecognizers.Add(tapGestureRecognizerTabOne);
            _tabTwo.GestureRecognizers.Add(tapGestureRecognizerTabTwo);
            _tabThree.GestureRecognizers.Add(tapGestureRecognizerTabThree);


            _boxViewSelectedTabOne = new BoxView
                                     {
                                         Style = BeginApplication.Styles.TabUnderLine,
                                         IsVisible = false
                                     };
            _boxViewSeletedTabTwo = new BoxView
                                     {
                                         Style = BeginApplication.Styles.TabUnderLine,
                                         IsVisible = false
                                     };
            _boxViewSeletedTabThree = new BoxView
                                      {
                                          Style = BeginApplication.Styles.TabUnderLine,
                                          IsVisible = false
                                      };


            _boxViewInactiveTabOne = new BoxView
            {
                Style = BeginApplication.Styles.TabUnderLineInactive,
                IsVisible = false
            };
            _boxViewInactiveTabTwo = new BoxView
            {
                Style = BeginApplication.Styles.TabUnderLineInactive,
                IsVisible = false
            };
            _boxViewInactiveTabThree = new BoxView
            {
                Style = BeginApplication.Styles.TabUnderLineInactive,
                IsVisible = false
            };


            var mainGrid = new Grid
                           {
                               Padding = BeginApplication.Styles.ThicknessMainLayout,
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
                                   HorizontalOptions = LayoutOptions.FillAndExpand,
                                   VerticalOptions = LayoutOptions.Start,
                                   ColumnSpacing = 0,
                                   RowDefinitions = new RowDefinitionCollection
                                                    {
                                                        new RowDefinition {Height = 35},
                                                        new RowDefinition {Height = 5},
                                                        new RowDefinition {Height = 5}
                                                    }
                               };
            gridControls.Children.Add(_tabOne, 0, 0);
            gridControls.Children.Add(_boxViewSelectedTabOne, 0, 1);
            gridControls.Children.Add(_boxViewInactiveTabOne, 0, 1);
            gridControls.Children.Add(_tabTwo, 1, 0);
            gridControls.Children.Add(_boxViewSeletedTabTwo, 1, 1);
            gridControls.Children.Add(_boxViewInactiveTabTwo, 1, 1);
            gridControls.Children.Add(_tabThree, 2, 0);
            gridControls.Children.Add(_boxViewSeletedTabThree, 2, 1);
            gridControls.Children.Add(_boxViewInactiveTabThree, 2, 1);
            _gridResults = new Grid();

            mainGrid.Children.Add(gridControls, 0, 0);
            mainGrid.Children.Add(_gridResults, 0, 1);
            Content = mainGrid;
        }

        public ContentPage PageOne { get; set; }
        public ContentPage PageTwo { get; set; }
        public ContentPage PageThree { get; set; }
        public string TabOneName { get; set; }
        public string TabTwoName { get; set; }
        public string TabThreeName { get; set; }
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
            _tabOne.Text = TabOneName;
            _tabTwo.Text = TabTwoName;
            _tabThree.Text = TabThreeName;
        }

        private void SetTabOneSettings()
        {
            CleanResultsAndToolBarItems();
            SetTabOneSelectedStyle();
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
            SetTabTwoSelectedStyle();
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
            //CleanResultsAndToolBarItems();
            _tabOne.TextColor = BeginApplication.Styles.DefaultColorButton;
            _tabTwo.TextColor = BeginApplication.Styles.DefaultColorButton;
            _tabThree.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewSelectedTabOne.IsVisible = false;
            _boxViewSeletedTabTwo.IsVisible = false;
            _boxViewSeletedTabThree.IsVisible = true;
            _boxViewInactiveTabOne.IsVisible = true;
            _boxViewInactiveTabTwo.IsVisible = true;
            _boxViewInactiveTabThree.IsVisible = false;

            if (ToolbarItemTabThree != null)
            {
#if __ANDROID__ || __IOS__
                ToolbarItems.Add(ToolbarItemTabThree);
#endif
            }
        }

        private void SetTabOneSelectedStyle()
        {

            _tabOne.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _tabTwo.TextColor = BeginApplication.Styles.DefaultColorButton;
            _tabThree.TextColor = BeginApplication.Styles.DefaultColorButton;
            _boxViewSelectedTabOne.IsVisible = true;
            _boxViewSeletedTabTwo.IsVisible = false;
            _boxViewSeletedTabThree.IsVisible = false;
            _boxViewInactiveTabOne.IsVisible = false;
            _boxViewInactiveTabTwo.IsVisible = true;
            _boxViewInactiveTabThree.IsVisible = true;
            _tabSelected = TabOneName;
        }
        private void SetTabTwoSelectedStyle()
        {
            _tabOne.TextColor = BeginApplication.Styles.DefaultColorButton;
            _tabTwo.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _tabThree.TextColor = BeginApplication.Styles.DefaultColorButton;
            _boxViewSelectedTabOne.IsVisible = false;
            _boxViewSeletedTabTwo.IsVisible = true;
            _boxViewSeletedTabThree.IsVisible = false;
            _boxViewInactiveTabOne.IsVisible = true;
            _boxViewInactiveTabTwo.IsVisible = false;
            _boxViewInactiveTabThree.IsVisible = true;
            _tabSelected = TabTwoName;
        }
        private void CleanResultsAndToolBarItems()
        {
            _gridResults.Children.Clear();
            ToolbarItems.Clear();
        }

        private void EventHandlerTabOne(object sender, EventArgs e)
        {
            CleanResultsAndToolBarItems();
            SetTabOneSettings();
        }

        private void EventHandlerTabTwo(object sender, EventArgs e)
        {
            CleanResultsAndToolBarItems();
            SetTabTwoSettings();
        }

        private async void EventHandlerTabThree(object sender, EventArgs e)
        {
            SetTabThreeSettings();

            var action = await DisplayActionSheet(null, MoreOptionsNames.Cancel, null, MoreOptionsNames.Contacts,
                MoreOptionsNames.Groups, MoreOptionsNames.Shops, MoreOptionsNames.Events);

            switch (action)
            {
                case MoreOptionsNames.Contacts:
                    _tabViewExposure.PageOne = _myContacts;
                    _tabViewExposure.PageTwo = _myContacts;
                    _tabViewExposure.TabOneName = TabsNames.Tab1Contacts;
                    _tabViewExposure.TabTwoName = TabsNames.Tab2Contacts;
                    _tabViewExposure.ToolbarItemTabOne = _myContacts.ToolbarItem;
                    _tabViewExposure.ToolbarItemTabTwo = _myContacts.ToolbarItem;
                    _tabViewExposure.SetInitialProperties(TabsNames.Tab1 = TabsNames.Tab1Contacts);
                    //set selected item  
                    await Navigation.PushAsync(_tabViewExposure);
                    break;
                case MoreOptionsNames.Groups:
                    _tabViewExposure.PageOne = _myGroups;
                    _tabViewExposure.PageTwo = _myGroups;
                    _tabViewExposure.TabOneName = TabsNames.Tab1Groups;
                    _tabViewExposure.TabTwoName = TabsNames.Tab2Groups;
                    //_tabViewExposure.ToolbarItemTabOne = _myGroups.ToolbarItem;
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
                    if (string.IsNullOrEmpty(_tabSelected)) return;
                    if (TabOneName != null && _tabSelected.Equals(TabOneName))
                    {
                        SetTabOneSelectedStyle();
                        break;
                    }
                    SetTabTwoSelectedStyle();
                    break;
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
        public const string Tab1Notifications = "Unread";
        public const string Tab2Notifications = "Read";
        public const string TabMore = "More";
        public static string Tab1 { get; set; }
        public static string Tab2 { get; set; }
    }
}