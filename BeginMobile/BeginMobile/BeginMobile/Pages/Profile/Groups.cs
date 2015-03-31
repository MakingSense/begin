using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.Utils;
using BeginMobile.Utils.Extensions;

namespace BeginMobile.Pages.Profile
{
    public class Groups : ContentPage
    {
        private ListView _lViewGroup;
        private RelativeLayout _rLayout;
        private ProfileInformationGroups groupInformation;
        private Label noGroupsMessage;
        private List<Group> defaultList = new List<Group>();

        private Picker sectionsPicker;
        private SearchView searchView;
        private List<string> sections = new List<String> { "Members", "Activities", "All Sections" };

        private LoginUser currentUser;
        public Groups()
        {
            Title = "Groups";

            searchView = new SearchView("All Categories");
            searchView.SetPlaceholder("Search by group name");
            sectionsPicker = new Picker
                             {
                                 Title = "Sections",
                                 VerticalOptions = LayoutOptions.CenterAndExpand
                             };

            foreach (var item in sections)
            {
                sectionsPicker.Items.Add(item);
            }

            searchView.Container.Children.Add(sectionsPicker);

            currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            groupInformation = App.ProfileServices.GetGroups(currentUser.User.UserName, currentUser.AuthToken);

            _lViewGroup = new ListView() { };

            _lViewGroup.ItemTemplate = new DataTemplate(typeof(ProfileGroupItemCell));
            _lViewGroup.ItemsSource = groupInformation.Groups;

            _lViewGroup.HasUnevenRows = true;

            _lViewGroup.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                var groupItem = (Group)e.SelectedItem;
                var groupPage = new GroupItemPage(groupItem);
                groupPage.BindingContext = groupItem;
                await Navigation.PushAsync(groupPage);

                // clears the 'selected' background
                ((ListView)sender).SelectedItem = null;
            };

            #region Search components

            searchView.SearchBar.TextChanged += CommonSearchItemChanged;
            searchView.Category.SelectedIndexChanged += CommonSearchItemChanged;
            searchView.Limit.SelectedIndexChanged += CommonSearchItemChanged;
            sectionsPicker.SelectedIndexChanged += CommonSearchItemChanged;

            noGroupsMessage = new Label();

            #endregion

            StackLayout mainLayout = new StackLayout
            {
                Padding = 10,
                Spacing = 2,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical
            };

            mainLayout.Children.Add(searchView.Container);
            mainLayout.Children.Add(new ScrollView()
                                    {
                                        Content = _lViewGroup
                                    });

            Content = mainLayout;
        }

        #region Events

        private void CommonSearchItemChanged(object sender, EventArgs args)
        {
            string q;
            string limit;
            string cat;
            string sections;

            if (sender.GetType() == typeof(SearchBar))
            {
                q = ((SearchBar)sender).Text;
            }

            else
            {
                q = searchView.SearchBar.Text;
            }

            RetrieveLimitSelected(out limit);
            RetrieveCategorySelected(out cat);
            RetrieveSectionSelected(out sections);
            
            List<Group> groupsList =
                App.ProfileServices.GetGroupsByParams(currentUser.AuthToken, q, cat, limit, sections);

            if (groupsList.Any())
            {
                _lViewGroup.ItemsSource = groupsList;
                noGroupsMessage.Text = string.Empty;
            }

            else
            {
                _lViewGroup.ItemsSource = defaultList;
            }
        }

        private void RetrieveSectionSelected(out string sections)
        {
            var sectionSelectedIndex = sectionsPicker.SelectedIndex;
            var sectionLastIndex = sectionsPicker.Items.Count - 1;

            if (sectionSelectedIndex == -1 || sectionSelectedIndex == sectionLastIndex)
            {
                sections = null;
            }

            else
            {
                sections = sectionsPicker.Items[sectionSelectedIndex];
            }
        }

        private void RetrieveCategorySelected(out string cat)
        {
            var catSelectedIndex = searchView.Category.SelectedIndex;
            var catLastIndex = searchView.Category.Items.Count - 1;

            if (catSelectedIndex == -1 || catSelectedIndex == catLastIndex)
            {
                cat = null;
            }

            else
            {
                cat = searchView.Category.Items[catSelectedIndex];
            }
        }

        private void RetrieveLimitSelected(out string limit)
        {
            var limitSelectedIndex = searchView.Limit.SelectedIndex;
            var limitLastIndex = searchView.Limit.Items.Count - 1;

            if (limitSelectedIndex == -1 || limitSelectedIndex == limitLastIndex)
            {
                limit = null;
            }

            else
            {
                limit = searchView.Limit.Items[limitSelectedIndex];
            }
        }

        private void OnSearchBarButtonPressed(object sender, EventArgs args)
        {
            var groupsList = groupInformation.Groups;

            SearchBar searchBar = (SearchBar)sender;
            string searchText = searchBar.Text; // recovery the text of search bar

            if (!string.IsNullOrEmpty(searchText) || !string.IsNullOrWhiteSpace(searchText))
            {

                if (groupsList.Count == 0)
                {
                    noGroupsMessage.Text = "There is no groups";
                }

                else
                {
                    List<Group> list =
                        (from g in groupsList
                         where g.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
                         select g).ToList<Group>();

                    if (list.Any())
                    {
                        _lViewGroup.ItemsSource = list;
                        noGroupsMessage.Text = "";
                    }

                    else
                    {
                        _lViewGroup.ItemsSource = defaultList;
                    }
                }
            }
            else
            {
                _lViewGroup.ItemsSource = groupInformation.Groups;
            }

        }

        #endregion
    }
}