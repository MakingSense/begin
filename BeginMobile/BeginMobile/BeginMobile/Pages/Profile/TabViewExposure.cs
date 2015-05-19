using System;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class TabViewExposure : ContentPage
    {
        private Grid _gridResults;
        private readonly Button _buttonTab1;
        private readonly Button _buttonTab2;
        private readonly BoxView _boxViewLineSelectedTab1;
        private readonly BoxView _boxViewLineSeletedTab2;


        public TabViewExposure()
        {
            Style = BeginApplication.Styles.PageStyle;

            _buttonTab1 = new Button
                          {
                              Text = String.Empty,
                              Style = BeginApplication.Styles.LinkButton
                          };
            _buttonTab1.Clicked += EventHandlerTab1;

            _buttonTab2 = new Button
                          {
                              Text = String.Empty,
                              Style = BeginApplication.Styles.LinkButton,
                          };
            _buttonTab2.Clicked += EventHandlerTab2;

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

            var mainGrid = new Grid
                           {
                               Padding = BeginApplication.Styles.LayoutThickness,
                               BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                               HorizontalOptions = LayoutOptions.Fill,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               RowDefinitions = new RowDefinitionCollection
                                                {
                                                    new RowDefinition {Height = GridLength.Auto},
                                                    new RowDefinition {Height = GridLength.Auto}
                                                },
                               ColumnDefinitions = new ColumnDefinitionCollection
                                                   {
                                                       new ColumnDefinition {Width = GridLength.Auto}
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
                                                           new ColumnDefinition {Width = GridLength.Auto}
                                                       }
                               };
            gridControls.Children.Add(_buttonTab1, 0, 0);
            gridControls.Children.Add(_boxViewLineSelectedTab1, 0, 1);
            gridControls.Children.Add(_buttonTab2, 1, 0);
            gridControls.Children.Add(_boxViewLineSeletedTab2, 1, 1);
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
            _buttonTab1.Text = TabOneName;
            _buttonTab2.Text = TabTwoName;
        }

        private void SetTabOneSettings()
        {
            CleanResultsAndToolBarItems();
            _buttonTab1.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _buttonTab2.TextColor = BeginApplication.Styles.DefaultColorButton;
            _boxViewLineSelectedTab1.IsVisible = true;
            _boxViewLineSeletedTab2.IsVisible = false;
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
            _buttonTab1.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonTab2.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewLineSelectedTab1.IsVisible = false;
            _boxViewLineSeletedTab2.IsVisible = true;
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
    }
}