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


        public TabViewExposure()
        {
            Style = BeginApplication.Styles.PageStyle;
          
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
                FontSize = BeginApplication.Styles.TextFontSizeLarge,                
            };

            _tabTwo = new Label
            {
                Text = string.Empty,
                XAlign = TextAlignment.Center,
                FontSize = BeginApplication.Styles.TextFontSizeLarge
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

            var mainGrid = new Grid
                           {
                               Padding = BeginApplication.Styles.LayoutThickness,
                               BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                               HorizontalOptions = LayoutOptions.Center,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               RowDefinitions = new RowDefinitionCollection
                                                {
                                                    new RowDefinition {Height = GridLength.Auto},
                                                    new RowDefinition {Height = GridLength.Auto},
                                                },
                               ColumnDefinitions = new ColumnDefinitionCollection
                                                   {
                                                       new ColumnDefinition {Width = GridLength.Auto}
                                                   }
                           };

            var gridControls = new Grid
                               {
                                   HorizontalOptions = LayoutOptions.FillAndExpand,
                                   VerticalOptions = LayoutOptions.Start,
                                   RowDefinitions = new RowDefinitionCollection
                                                    {
                                                        new RowDefinition {Height = new GridLength(10,GridUnitType.Auto)},
                                                        new RowDefinition {Height = GridLength.Auto},
                                                        new RowDefinition {Height = new GridLength(0.3,GridUnitType.Star)}
                                                    },
                                   ColumnDefinitions = new ColumnDefinitionCollection
                                                       {
                                                           new ColumnDefinition {Width = new GridLength(5, GridUnitType.Star)},                                                           
                                                           new ColumnDefinition {Width = new GridLength(5, GridUnitType.Star)}
                                                       }
                               };
            gridControls.Children.Add(_tabOne, 0, 0);
            gridControls.Children.Add(_boxViewLineSelectedTabOne, 0, 1);
            gridControls.Children.Add(_tabTwo, 1, 0);
            gridControls.Children.Add(_boxViewLineSeletedTabTwo, 1, 1);
            _gridResults = new Grid();

            mainGrid.Children.Add(gridControls, 0, 0);
            mainGrid.Children.Add(_gridResults, 0, 1);
            Content = mainGrid;
        }        

        public ContentPage PageOne { get; set; }
        public ContentPage PageTwo { get; set; }
        public String TabOneName { get; set; }
        public String TabTwoName { get; set; }
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
            _boxViewLineSeletedTabTwo.IsVisible = false;
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
            _boxViewLineSeletedTabTwo.IsVisible = true;
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