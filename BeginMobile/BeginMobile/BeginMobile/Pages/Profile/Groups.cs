using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using BeginMobile.Utils;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace BeginMobile.Pages.Profile
{
    public class Groups : ContentPage
    {
        private ListView _listViewGroup;
        private Label _labelNoGroupsMessage;
        private readonly List<Group> _defaultList = new List<Group>();

        private Picker _sectionsPicker;
        private Picker _categoriesPicker;

        private readonly SearchView _searchView;
        private List<string> _sectionsList;
        private List<string> _categoriesList = new List<string> { "All Categories" };
        private const string DefaultLimit = "10";

        private List<Group> _groupInformation;

        private readonly LoginUser _currentUser;
        public Groups()
        {
            Title = "Groups";
            _searchView = new SearchView();
            _searchView.SetPlaceholder("Search by group name");

            LoadSectionsPicker();
            LoadCategoriesPicker();

            _currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            Init();
        }

        private async Task Init()
        {

            _groupInformation = await App.ProfileServices.GetGroupsByParams(_currentUser.AuthToken, limit: DefaultLimit);

            _listViewGroup = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(ProfileGroupItemCell)),
                ItemsSource = _groupInformation,
                HasUnevenRows = true
            };

            _listViewGroup.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                var groupItem = (Group)e.SelectedItem;
                var groupPage = new GroupItemPage(groupItem) { BindingContext = groupItem };
                await Navigation.PushAsync(groupPage);

                // clears the 'selected' background
                ((ListView)sender).SelectedItem = null;
            };

            #region Search components

            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _categoriesPicker.SelectedIndexChanged += SearchItemEventHandler;
            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            _sectionsPicker.SelectedIndexChanged += SearchItemEventHandler;

            _labelNoGroupsMessage = new Label();

            #endregion

            var mainLayout = new StackLayout
            {
                Padding = 10,
                Spacing = 2,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical
            };

            mainLayout.Children.Add(_searchView.Container);
            mainLayout.Children.Add(new ScrollView
            {
                Content = _listViewGroup
            });

            Content = mainLayout;
        }

        private void LoadSectionsPicker()
        {
            _sectionsList = App.GlobalService.GroupSections;

            _sectionsPicker = new Picker
                              {
                                  Title = "Sections",
                                  VerticalOptions = LayoutOptions.CenterAndExpand
                              };

            foreach (var item in _sectionsList)
            {
                _sectionsPicker.Items.Add(item);
            }

            _searchView.Container.Children.Add(_sectionsPicker);
        }
        private void LoadCategoriesPicker()
        {
            _categoriesPicker = new Picker
                                {
                                    Title = "Filter by Category",
                                    VerticalOptions = LayoutOptions.CenterAndExpand
                                };

            if (_categoriesList != null)
            {
                foreach (string c in _categoriesList)
                {
                    _categoriesPicker.Items.Add(c);
                }
            }

            else
            {
                _categoriesList = new List<string> { "All Categories" };
            }

            _searchView.Container.Children.Add(_categoriesPicker);
        }

        #region Events

        /// <summary>
        /// Common handler when an searchBar item has changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void SearchItemEventHandler(object sender, EventArgs args)
        {
            string limit;
            string cat;
            string sections;

            var q = sender.GetType() == typeof(SearchBar) ? ((SearchBar)sender).Text : _searchView.SearchBar.Text;

            RetrieveLimitSelected(out limit);
            RetrieveCategorySelected(out cat);
            RetrieveSectionSelected(out sections);

            List<Group> groupsList =
                await App.ProfileServices.GetGroupsByParams(_currentUser.AuthToken, q, cat, limit, sections);

            if (groupsList.Any())
            {
                _listViewGroup.ItemsSource = groupsList;
                _labelNoGroupsMessage.Text = string.Empty;
            }

            else
            {
                _listViewGroup.ItemsSource = _defaultList;
            }
        }
        private void RetrieveSectionSelected(out string sections)
        {
            var sectionSelectedIndex = _sectionsPicker.SelectedIndex;
            var sectionLastIndex = _sectionsPicker.Items.Count - 1;

            sections = sectionSelectedIndex == -1 || sectionSelectedIndex == sectionLastIndex
                ? null
                : _sectionsPicker.Items[sectionSelectedIndex];
        }
        private void RetrieveCategorySelected(out string cat)
        {
            var catSelectedIndex = _categoriesPicker.SelectedIndex;
            var catLastIndex = _categoriesPicker.Items.Count - 1;

            cat = catSelectedIndex == -1 || catSelectedIndex == catLastIndex
                ? null
                : _categoriesPicker.Items[catSelectedIndex];
        }
        private void RetrieveLimitSelected(out string limit)
        {
            var limitSelectedIndex = _searchView.Limit.SelectedIndex;
            var limitLastIndex = _searchView.Limit.Items.Count - 1;

            limit = limitSelectedIndex == -1 || limitSelectedIndex == limitLastIndex
                ? null
                : _searchView.Limit.Items[limitSelectedIndex];
        }

        #endregion
    }
}