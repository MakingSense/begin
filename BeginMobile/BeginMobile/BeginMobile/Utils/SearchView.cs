using Xamarin.Forms;

namespace BeginMobile.Utils
{
    public class SearchView
    {
        private const int LimitMax = 100;

        public SearchView(params string[] categories)
        {
            Categories = categories;
            Limit = new Picker
                    {
                        Title = "Filter by Limit",
                    };

            SearchBar = new SearchBar
                        {
                            Placeholder = "Filter by Name"
                        };

            Category = new Picker
                       {
                           Title = "Filter by Category",
                           VerticalOptions = LayoutOptions.CenterAndExpand
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

            if (Categories != null)
            {
                foreach (string c in Categories)
                {
                    Category.Items.Add(c);
                }
            }
            else
            {
                Categories = new[] {"All Categories"};
            }

            Container = new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Orientation = StackOrientation.Vertical,
                            Padding = new Thickness(20, 10, 20, 10),
                            BackgroundColor = App.Styles.SearchBackground,
                            Opacity = 5.0
                        };

            Container.Children.Add(SearchBar);
            Container.Children.Add(Category);
            Container.Children.Add(Limit);
        }

        public SearchBar SearchBar { get; set; }

        public Picker Limit { get; set; }

        public Picker Category { get; set; }

        public string[] Categories { get; set; }

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