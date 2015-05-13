using System;
using Xamarin.Forms;

namespace BeginMobile.Utils
{
    public class SearchView
    {
        private const int LimitMax = 100;
        
        public SearchView()
        {
            ButtonCloseSearch = new Button
                                {
                                    Text = "X",
                                    HeightRequest = 40,
                                    WidthRequest = 40,
                                    HorizontalOptions = LayoutOptions.End
                                };
            ButtonCloseSearch.Clicked += CloseSearchEventHandler;
            Limit = new Picker
                    {
                        Title = "Max number rows",
                    };

            SearchBar = new SearchBar
                        {
                            Placeholder = "Filter by Name"
                        };

            for (var i = 1; i <= LimitMax; i++)
            {
                var iterator = i.ToString();

                Limit.Items.Add(iterator);
                if (i == LimitMax)
                {
                    Limit.Items.Add("All items");
                }
            }

        
            Container = new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Orientation = StackOrientation.Vertical,
                            Padding = new Thickness(20, 10, 20, 10),
                            BackgroundColor = BeginApplication.Styles.SearchBackground,
                            IsVisible = false
                        };
            Container.Children.Add(ButtonCloseSearch);
            Container.Children.Add(SearchBar);
            Container.Children.Add(Limit);
        }

        private void CloseSearchEventHandler(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            Container.IsVisible = false;            
        }

        public Button ButtonCloseSearch { get; set; }
        public SearchBar SearchBar { get; set; }
        public Picker Limit { get; set; }
        public StackLayout Container { get; set; }
        public void SetPlaceholder(string placeholder)
        {
            if (!string.IsNullOrEmpty(placeholder))
            {
                SearchBar.Placeholder = placeholder;
            }
        }
    }

    public static class ButtonSearchNames
    {
        public const string HideSearch = "HideSearch";
        public const string VisibleSearch = "VisibleSearch";
    }
}