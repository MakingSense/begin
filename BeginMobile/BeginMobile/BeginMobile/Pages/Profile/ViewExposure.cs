using System;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ViewExposure : ContentPage
    {
        private Grid _gridResults;
        private readonly Label _optionsForAll;
        private readonly Label _optionsForMe;
        private BoxView _boxViewSelectedAllOption;
        private BoxView _boxViewSeletedMeOption;

        public ViewExposure()
        {
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

            _optionsForAll = new Label
                             {
                                 Style = BeginApplication.Styles.PickerStyle,
                                 XAlign = TextAlignment.Start,
                                 HorizontalOptions = LayoutOptions.Start,
                                 IsVisible = false
                             };
            _optionsForAll.GestureRecognizers.Add(tapGestureRecognizerAll);

            _optionsForMe = new Label
                            {
                                Style = BeginApplication.Styles.PickerStyle,
                                XAlign = TextAlignment.Start,
                                HorizontalOptions = LayoutOptions.Start,
                                IsVisible = false
                            };
            _optionsForMe.GestureRecognizers.Add(tapGestureRecognizerMe);


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
                                   HorizontalOptions = LayoutOptions.StartAndExpand,
                                   VerticalOptions = LayoutOptions.StartAndExpand,
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
            gridControls.Children.Add(_optionsForAll, 0, 0);
            gridControls.Children.Add(_optionsForMe, 0, 0);
            _gridResults = new Grid();

            mainGrid.Children.Add(gridControls, 0, 0);
            mainGrid.Children.Add(_gridResults, 0, 1);
        }

        public void SetViewToExpose(Grid gridOfAllItems, Grid gridOnlyMeItems, string allOptionsName,
            string onlyMeOptionsName)
        {
            _optionsForAll.Text = allOptionsName;
            _optionsForAll.IsVisible = true;

            _optionsForMe.Text = onlyMeOptionsName;
            _optionsForMe.IsVisible = false;

            _gridResults = gridOfAllItems;
        }

        private void ShowAllEventHandler(object sender, EventArgs e)
        {
            //TODO add logic here
        }

        private void ShowOnlyMeEventHandler(object sender, EventArgs e)
        {
            //TODO add logic here
        }
    }
}