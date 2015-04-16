using Xamarin.Forms;

namespace BeginMobile.Utils
{
    public class SearchView
    {
        private const int LimitMax = 100;

        public SearchView()
        {
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
                            Opacity = 5.0
                        };

            Container.Children.Add(SearchBar);
            Container.Children.Add(Limit);
        }

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
}