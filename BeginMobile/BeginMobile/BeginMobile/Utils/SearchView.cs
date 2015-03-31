using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Utils
{
    public class SearchView
    {
        private SearchBar searchBar;
        private Entry limit;
        protected Picker category;
        private StackLayout container;

        public SearchView(params string[] categories)
        {
            Categories = categories;
            limit = new Entry
            {
                Placeholder = "Limit Filter"
            };

            searchBar = new SearchBar
            {
                Placeholder = "Search by Name and Surname"
            };

            category = new Picker
            {
                Title = "Category",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            if (Categories != null)
            {
                foreach (string c in Categories)
                {
                    category.Items.Add(c);
                }
            }
            else
            {
                Categories = new string[] { "All Categories" };
            }

            container = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20, 10, 20, 10),
                BackgroundColor = App.Styles.SearchBackground,
                Opacity = 5.0
            };

            container.Children.Add(searchBar);
            container.Children.Add(limit);
            container.Children.Add(category);
        }

        public SearchBar SearchBar
        {
            get { return searchBar; }
            set { searchBar = value; }
        }

        public Entry Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public Picker Category
        {
            get { return category; }
            set { category = value; }
        }
        public string[] Categories
        {
            get;
            set;
        }
        public StackLayout Container
        {
            get { return container; }
            set { container = value; }
        }

        public void SetPlaceholder(string placeholder)
        {
            if (!string.IsNullOrEmpty(placeholder))
            {
                this.searchBar.Placeholder = placeholder;
            }
        }
    }
}
