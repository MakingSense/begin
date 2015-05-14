using System;
using BeginMobile.Pages.ContactPages;
using BeginMobile.Pages.GroupPages;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ViewExposure : ContentPage
    {
        private readonly Grid _gridResults;
        private readonly Button _buttonTab1;
        private readonly Button _buttonTab2;
        private readonly Button _buttonTab3;
        private readonly BoxView _boxViewLineSelectedTab1;
        private readonly BoxView _boxViewLineSeletedTab2;
        private readonly BoxView _boxViewLineSeletedTab3;
        private readonly Information _information = new Information();
        private readonly ContactPage _contacts = new ContactPage(String.Empty, String.Empty);
        private readonly MyActivity _activity = new MyActivity();
        private readonly GroupListPage _groups = new GroupListPage(String.Empty, String.Empty);
        private readonly Shop _shops = new Shop();
        private readonly Events _events = new Events();

        public ViewExposure()
        {
            BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor;

            _buttonTab1 = new Button
                          {
                              Text = TabsNames.Tab1,
                              Style = BeginApplication.Styles.LinkButton,
                          };
            _buttonTab1.Clicked += EventHandlerTab1;

            _buttonTab2 = new Button
                          {
                              Text = TabsNames.Tab2,
                              Style = BeginApplication.Styles.LinkButton,
                          };
            _buttonTab2.Clicked += EventHandlerTab2;

            _buttonTab3 = new Button
                          {
                              Text = TabsNames.Tab3,
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
                               BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                               HorizontalOptions = LayoutOptions.StartAndExpand,
                               VerticalOptions = LayoutOptions.StartAndExpand,
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
                                   HorizontalOptions = LayoutOptions.FillAndExpand,
                                   VerticalOptions = LayoutOptions.FillAndExpand,
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

        public Grid GridReceived { get; set; }

        public void SetViewToExpose(Grid selectedItems, string tabSelected)
        {
            if (tabSelected.Equals(TabsNames.Tab1))
            {
                _buttonTab1.TextColor = BeginApplication.Styles.TabSelectedTextColor;
                _buttonTab2.TextColor = BeginApplication.Styles.DefaultColorButton;
                _buttonTab3.TextColor = BeginApplication.Styles.DefaultColorButton;
                _boxViewLineSelectedTab1.IsVisible = true;
                _boxViewLineSeletedTab2.IsVisible = false;
                _boxViewLineSeletedTab3.IsVisible = false;
            }
            else if (tabSelected.Equals(TabsNames.Tab2))
            {
                _buttonTab1.TextColor = BeginApplication.Styles.DefaultColorButton;
                _buttonTab2.TextColor = BeginApplication.Styles.TabSelectedTextColor;
                _buttonTab3.TextColor = BeginApplication.Styles.DefaultColorButton;
                _boxViewLineSelectedTab1.IsVisible = false;
                _boxViewLineSeletedTab2.IsVisible = true;
                _boxViewLineSeletedTab3.IsVisible = false;
            }
            else
            {
                _buttonTab1.TextColor = BeginApplication.Styles.DefaultColorButton;
                _buttonTab2.TextColor = BeginApplication.Styles.DefaultColorButton;
                _buttonTab3.TextColor = BeginApplication.Styles.TabSelectedTextColor;
                _boxViewLineSelectedTab1.IsVisible = false;
                _boxViewLineSeletedTab2.IsVisible = false;
                _boxViewLineSeletedTab3.IsVisible = true;
            }

            _gridResults.Children.Add(selectedItems, 0, 0);
        }

        private void EventHandlerTab1(object sender, EventArgs e)
        {
            _gridResults.Children.Clear();
            _buttonTab1.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _buttonTab2.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonTab3.TextColor = BeginApplication.Styles.DefaultColorButton;
            _boxViewLineSelectedTab1.IsVisible = true;
            _boxViewLineSeletedTab2.IsVisible = false;
            _boxViewLineSeletedTab3.IsVisible = false;
            _gridResults.Children.Add(_activity.GetGridActivities, 0, 0);
        }

        private void EventHandlerTab2(object sender, EventArgs e)
        {
            _gridResults.Children.Clear();
            _buttonTab1.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonTab2.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _buttonTab3.TextColor = BeginApplication.Styles.DefaultColorButton;
            _boxViewLineSelectedTab1.IsVisible = false;
            _boxViewLineSeletedTab2.IsVisible = true;
            _boxViewLineSeletedTab3.IsVisible = false;
            _gridResults.Children.Add(_information.GetGridInfo(), 0, 0);
        }

        private async void EventHandlerTab3(object sender, EventArgs e)
        {
            _gridResults.Children.Clear();
            _buttonTab1.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonTab2.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonTab3.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewLineSelectedTab1.IsVisible = false;
            _boxViewLineSeletedTab2.IsVisible = false;
            _boxViewLineSeletedTab3.IsVisible = true;

            var action = await DisplayActionSheet(null, OtherOptions.Cancel, null, OtherOptions.Contacts,
                OtherOptions.Groups, OtherOptions.Shops, OtherOptions.Events);

            switch (action)
            {
                case OtherOptions.Contacts:
                    await Navigation.PushAsync(_contacts);
                    break;
                case OtherOptions.Groups:
                    await Navigation.PushAsync(_groups);
                    break;
                case OtherOptions.Shops:
                    await Navigation.PushAsync(_shops);
                    break;
                case OtherOptions.Events:
                    await Navigation.PushAsync(_events);
                    break;
                case OtherOptions.Cancel:
                    return;

                default:
                    return;
            }
        }
    }

    public static class TabsNames
    {
        public const string Tab1 = "Activities";
        public const string Tab2 = "Information";
        public const string Tab3 = "...";
    }
}