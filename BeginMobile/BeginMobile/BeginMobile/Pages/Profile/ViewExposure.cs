using System;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ViewExposure : ContentPage
    {
        private Grid _gridResults;
        private readonly Label _optionsAll;
        private readonly Label _optionsMe;
        private readonly BoxView _boxViewSelectedAllOption;
        private readonly BoxView _boxViewSeletedMeOption;
        private Grid _gridMeItems;
        private Grid _gridAllItems;

        public ViewExposure()
        {
            BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor;
            var tapGestureRecognizerAll = new TapGestureRecognizer
                                          {
                                              NumberOfTapsRequired = 1
                                          };
            var tapGestureRecognizerMe = new TapGestureRecognizer
                                         {
                                             NumberOfTapsRequired = 1
                                         };

            tapGestureRecognizerAll.Tapped += ShowAllEventHandler;
            tapGestureRecognizerMe.Tapped += ShowOnlyMeEventHandler;

            _optionsAll = new Label
                             {
                                 Text = "All",
                                 Style = BeginApplication.Styles.SubtitleStyle,
                                 XAlign = TextAlignment.Start,
                                 HorizontalOptions = LayoutOptions.Start
                             };
            _optionsAll.GestureRecognizers.Add(tapGestureRecognizerAll);

            _optionsMe = new Label
                            { 
                                Text = "Detail",
                                Style = BeginApplication.Styles.SubtitleStyle,
                                XAlign = TextAlignment.Start,
                                HorizontalOptions = LayoutOptions.Start
                            };
            _optionsMe.GestureRecognizers.Add(tapGestureRecognizerMe);

            _boxViewSelectedAllOption = new BoxView
                                        {
                                            Style = BeginApplication.Styles.TabUnderLine,
                                            IsVisible = false
                                        };
            _boxViewSeletedMeOption = new BoxView
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
                                                        new RowDefinition {Height = GridLength.Auto}
                                                    },
                                   ColumnDefinitions = new ColumnDefinitionCollection
                                                       {
                                                           new ColumnDefinition {Width = GridLength.Auto},
                                                           new ColumnDefinition {Width = GridLength.Auto}
                                                       }
                               };
            gridControls.Children.Add(_optionsAll, 0, 0);
            gridControls.Children.Add(_optionsMe, 1, 0);
            _gridResults = new Grid();

            mainGrid.Children.Add(gridControls, 0, 0);
            mainGrid.Children.Add(_gridResults, 0, 1);
            Content = mainGrid;
        }

        public void SetViewToExpose(Grid gridOfAllItems, Grid gridOnlyMeItems, string allOptionsName,
            string onlyMeOptionsName)
        {
            _optionsAll.Text = allOptionsName;
            _optionsMe.Text = onlyMeOptionsName;            
            _boxViewSelectedAllOption.IsVisible = true;
            _boxViewSeletedMeOption.IsVisible = false;
            _gridAllItems = gridOfAllItems;
            _gridMeItems = gridOnlyMeItems;
            _gridResults.Children.Add(gridOfAllItems,0,0);
           
        }

        private void ShowAllEventHandler(object sender, EventArgs e)
        {
            var thisSeleceted = sender as Label;
            if (thisSeleceted != null) thisSeleceted.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _optionsMe.TextColor = BeginApplication.Styles.DefaultColorButton;
            _boxViewSelectedAllOption.IsVisible = true;
            _boxViewSeletedMeOption.IsVisible = false;
            if (_gridAllItems != null) _gridResults.Children.Add(_gridAllItems, 0, 0);
        }

        private void ShowOnlyMeEventHandler(object sender, EventArgs e)
        {
            var thisSeleceted = sender as Label;
            if (thisSeleceted != null) thisSeleceted.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _optionsAll.TextColor = BeginApplication.Styles.DefaultColorButton;

            _boxViewSeletedMeOption.IsVisible = true;
            _boxViewSelectedAllOption.IsVisible = false;
            if (_gridMeItems != null) _gridResults.Children.Add(_gridMeItems, 0, 0);
        }
    }
}