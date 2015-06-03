using System;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class TabViewExposure : ContentPage
    {
        private readonly Grid _gridResults;
        private readonly Label _tabOne;
        private readonly Label _tabTwo;

        private readonly BoxView _boxViewLineSelectedTabOne;
        private readonly BoxView _boxViewLineSeletedTabTwo;
        private readonly BoxView _boxViewLineInactiveTabOne;
        private readonly BoxView _boxViewLineInactiveTabTwo;

        public TabViewExposure()
        {
            //Style = BeginApplication.Styles.PageStyle;
            BackgroundColor = BeginApplication.Styles.ColorWhiteBackground;

            var tapGestureRecognizerTabOne = new TapGestureRecognizer
                                             {
                                                 NumberOfTapsRequired = 1
                                             };
            var tapGestureRecognizerTabTwo = new TapGestureRecognizer
                                             {
                                                 NumberOfTapsRequired = 1
                                             };

            tapGestureRecognizerTabOne.Tapped += EventHandlerTabOne;
            tapGestureRecognizerTabTwo.Tapped += EventHandlerTabTwo;


            _tabOne = new Label
                      {
                          Text = string.Empty,
                          XAlign = TextAlignment.Center,
                          FontSize = BeginApplication.Styles.TextFontSizeMedium,
                      };

            _tabTwo = new Label
                      {
                          Text = string.Empty,
                          XAlign = TextAlignment.Center,
                          FontSize = BeginApplication.Styles.TextFontSizeMedium
                      };

            _tabOne.GestureRecognizers.Add(tapGestureRecognizerTabOne);
            _tabTwo.GestureRecognizers.Add(tapGestureRecognizerTabTwo);

            _boxViewLineSelectedTabOne = new BoxView
                                         {
                                             Style = BeginApplication.Styles.TabUnderLine,
                                             IsVisible = false
                                         };
            _boxViewLineSeletedTabTwo = new BoxView
                                        {
                                            Style = BeginApplication.Styles.TabUnderLine,
                                            IsVisible = false
                                        };

            _boxViewLineInactiveTabOne = new BoxView
                                         {
                                             Style = BeginApplication.Styles.TabUnderLineInactive,
                                             IsVisible = false
                                         };
            _boxViewLineInactiveTabTwo = new BoxView
                                         {
                                             Style = BeginApplication.Styles.TabUnderLineInactive,
                                             IsVisible = false
                                         };

            var mainGrid = new Grid
                           {
                               Padding = BeginApplication.Styles.ThicknessMainLayout,
                               BackgroundColor = BeginApplication.Styles.ColorWhite,
                               HorizontalOptions = LayoutOptions.Fill,
                               VerticalOptions = LayoutOptions.Start,
                               RowDefinitions = new RowDefinitionCollection
                                                {
                                                    new RowDefinition {Height = GridLength.Auto},
                                                    new RowDefinition {Height = GridLength.Auto},
                                                },
                               //ColumnDefinitions = new ColumnDefinitionCollection
                               //                    {
                               //                        new ColumnDefinition {Width = GridLength.Auto}
                               //                    }
                           };

            var gridControls = new Grid
                               {
                                   VerticalOptions = LayoutOptions.Start,
                                   HorizontalOptions = LayoutOptions.FillAndExpand,
                                   ColumnSpacing = 0,
                                   RowDefinitions = new RowDefinitionCollection
                                                    {
                                                        new RowDefinition {Height = GridLength.Auto},
                                                        new RowDefinition {Height = GridLength.Auto}
                                                    }
                               };
            gridControls.Children.Add(_tabOne, 0, 0);
            gridControls.Children.Add(_boxViewLineSelectedTabOne, 0, 1);
            gridControls.Children.Add(_boxViewLineInactiveTabOne, 0, 1);
            gridControls.Children.Add(_tabTwo, 1, 0);
            gridControls.Children.Add(_boxViewLineSeletedTabTwo, 1, 1);
            gridControls.Children.Add(_boxViewLineInactiveTabTwo, 1, 1);
            _gridResults = new Grid();

            mainGrid.Children.Add(gridControls, 0, 0);
            mainGrid.Children.Add(_gridResults, 0, 1);
            Content = mainGrid;
        }

        public ContentPage PageOne { get; set; }
        public ContentPage PageTwo { get; set; }
        public string TabOneName { get; set; }
        public string TabTwoName { get; set; }
        public ToolbarItem ToolbarItemTabOne { get; set; }
        public ToolbarItem ToolbarItemTabTwo { get; set; }
        public ToolbarItem ToolbarItemTabThree { get; set; }

        public void SetInitialProperties(string tabSelected)
        {
            if (tabSelected.Equals(TabsNames.Tab1))
            {
                SetTabOneSettings();
            }
            else if (tabSelected.Equals(TabsNames.Tab2))
            {
                SetTabTwoSettings();
            }
            _tabOne.Text = TabOneName;
            _tabTwo.Text = TabTwoName;
        }

        private void SetTabOneSettings()
        {
            CleanResultsAndToolBarItems();
            _tabOne.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _tabTwo.TextColor = BeginApplication.Styles.DefaultColorButton;
            _boxViewLineSelectedTabOne.IsVisible = true;
            _boxViewLineInactiveTabOne.IsVisible = false;
            _boxViewLineSeletedTabTwo.IsVisible = false;
            _boxViewLineInactiveTabTwo.IsVisible = true;
            if (PageOne != null) _gridResults.Children.Add(PageOne.Content, 0, 0);
            if (ToolbarItemTabOne != null)
            {
#if __ANDROID__ || __IOS__
                ToolbarItems.Add(ToolbarItemTabOne);
#endif
            }
            if (ToolbarItemTabThree != null)
            {
#if __ANDROID__ || __IOS__
                ToolbarItems.Add(ToolbarItemTabThree);
#endif
            }
        }

        private void SetTabTwoSettings()
        {
            CleanResultsAndToolBarItems();
            _tabOne.TextColor = BeginApplication.Styles.DefaultColorButton;
            _tabTwo.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewLineSelectedTabOne.IsVisible = false;
            _boxViewLineInactiveTabOne.IsVisible = true;
            _boxViewLineSeletedTabTwo.IsVisible = true;
            _boxViewLineInactiveTabTwo.IsVisible = false;

            if (PageTwo != null) _gridResults.Children.Add(PageTwo.Content, 0, 0);
            if (ToolbarItemTabTwo != null)
            {
#if __ANDROID__ || __IOS__
                ToolbarItems.Add(ToolbarItemTabTwo);
#endif
            }
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

        private void EventHandlerTabOne(object sender, EventArgs e)
        {
            //CleanResultsAndToolBarItems();
            SetTabOneSettings();
        }

        private void EventHandlerTabTwo(object sender, EventArgs e)
        {
            //CleanResultsAndToolBarItems();
            SetTabTwoSettings();
        }
    }
}