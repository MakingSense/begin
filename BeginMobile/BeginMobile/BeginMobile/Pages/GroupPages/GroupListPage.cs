using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using BeginMobile.Utils;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace BeginMobile.Pages.GroupPages
{
    public class GroupListPage : TabContent
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

        private ObservableCollection<Group> _groupInformation;
        private ImageSource _imageSourceGroupByDefault;

        private readonly LoginUser _currentUser;
        public GroupListPage(string title, string iconImg): base(title, iconImg)
        {
            _searchView = new SearchView();
            _searchView.SetPlaceholder("Search by group name");
            LoadDeafultImage();

            _currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];
            Init();
        }

        private async Task Init()
        {
            

            _groupInformation = await BeginApplication
                .ProfileServices.GetGroupsByParams(_currentUser.AuthToken, limit: DefaultLimit)
                ?? new ObservableCollection<Group>(_defaultList);

            LoadSectionsPicker();
            LoadCategoriesPicker();

            #region Search components

            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _categoriesPicker.SelectedIndexChanged += SearchItemEventHandler;
            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            _sectionsPicker.SelectedIndexChanged += SearchItemEventHandler;

            _labelNoGroupsMessage = new Label();

            #endregion

            _listViewGroup = new ListView
            {
                ItemTemplate = new DataTemplate(() => new ProfileGroupItemCell(_imageSourceGroupByDefault)),
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

            var mainLayout = new StackLayout
            {
                Padding = BeginApplication.Styles.LayoutThickness,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    _searchView.Container,
                    _listViewGroup
                    
                }
            };

            Content = mainLayout;
        }

        private void LoadSectionsPicker()
        {
            _sectionsList = BeginApplication.GlobalService.GroupSections;

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

            _groupInformation =
                await BeginApplication.ProfileServices.GetGroupsByParams(_currentUser.AuthToken, q, cat, limit, sections);

            if (_groupInformation != null && _groupInformation.Any())
            {
                _listViewGroup.ItemsSource = _groupInformation;
                _labelNoGroupsMessage.Text = string.Empty;
            }

            else
            {
                _groupInformation = new ObservableCollection<Group>(_defaultList);
                _listViewGroup.ItemsSource = _groupInformation;
            }
        }
        private void RetrieveSectionSelected(out string sections)
        {
            var sectionSelectedIndex = _sectionsPicker.SelectedIndex;

            sections = sectionSelectedIndex == -1
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

        public async void LoadDeafultImage()
        {
            #if __ANDROID__
            var imageArray = await ImageResizer.GetResizeImage(BeginApplication.Styles.DefaultGroupIcon);
                        this._imageSourceGroupByDefault = ImageSource.FromStream(() => new MemoryStream(imageArray));
            #endif
            #if __IOS__
                        this._imageSourceGroupByDefault = BeginApplication.Styles.DefaultGroupIcon;
            #endif
        }
    }
}
