using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.Services.DTO;
using BeginMobile.Utils;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.Utils;

namespace BeginMobile.Pages.Profile
{
    public class Events : ContentPage,IDisposable
    {
        private Label _labelNoEventsMessage;
        private ListView _eventsListView;

        private readonly List<EventInfoObject> _defaultList = new List<EventInfoObject>();
        private readonly SearchView _searchView;
        private Picker _categoriesPicker;
        private List<string> _categoriesList = new List<string> { AppResources.OptionAllCategories };

        private readonly LoginUser _currentUser;
        private const string DefaultLimit = "10";
        private Grid _gridMainComponents;
        private ObservableCollection<ProfileEvent> _profileEvents;
        private const string Aroba = "@";
        public Events()
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = AppResources.LabelEventTitle;

            _searchView = new SearchView();
            _searchView.SetPlaceholder(AppResources.SearchViewEventSearchByName);

            LoadCategoriesPicker();

            _currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            Init();
        }

        private async Task Init()
        {
            _profileEvents =
                await BeginApplication.ProfileServices.GetEventsByParams(_currentUser.AuthToken,limit: DefaultLimit);

            #region Search components

            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _categoriesPicker.SelectedIndexChanged += SearchItemEventHandler;
            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;

            _labelNoEventsMessage = new Label();

            #endregion

            #region List components

            var listEvents = RetrieveEventInfoObjectList(_profileEvents);

            var eventTemplate = new DataTemplate(typeof(TemplateListViewEvents));

            _eventsListView = new ListView
            {
                ItemsSource = listEvents,
                ItemTemplate = eventTemplate,
                HasUnevenRows = true,
            };

            _eventsListView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                var item = (EventInfoObject)e.SelectedItem;

                var itemPageProfile = new EventDetailInformation(item.EventInfo) { BindingContext = item.EventInfo };

                await Navigation.PushAsync(itemPageProfile);
                ((ListView)sender).SelectedItem = null;

            };
            #endregion

            #region Main layout
            var scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Start,
                    Children =
                    {
                        _eventsListView
                    }
                }
            };

            ToolbarItem = new ToolbarItem("Filter", BeginApplication.Styles.FilterIcon, async () =>
            {
                _searchView
                    .Container
                    .IsVisible
                    = true;
            });
#if __ANDROID__ || __IOS__
            ToolbarItems.Add(ToolbarItem);
#endif
            _gridMainComponents = new Grid
            {
                Padding = BeginApplication.Styles.ThicknessMainLayout,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      
                                  }
            };
            _gridMainComponents.Children.Add(_searchView.Container, 0, 0);
            _gridMainComponents.Children.Add(scrollView, 0, 1);
            Content = _gridMainComponents;

            #endregion
        }

        public ToolbarItem ToolbarItem { get; set; }
        public Grid GetGrid()
        {
            return _gridMainComponents;
        }
        private void LoadCategoriesPicker()
        {
            _categoriesPicker = new Picker
            {
                Title = AppResources.PickerEventFilterBycategory,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = BeginApplication.Styles.ColorWhite
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
                _categoriesList = new List<string> { AppResources.OptionAllCategories };
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

            var q = sender.GetType() == typeof(SearchBar) ? ((SearchBar)sender).Text : _searchView.SearchBar.Text;

            RetrieveLimitSelected(out limit);
            RetrieveCategorySelected(out cat);

            var profileEventList =
                await BeginApplication.ProfileServices.GetEventsByParams(_currentUser.AuthToken, q, cat, limit);

            if (profileEventList != null && profileEventList.Any())
            {
                _eventsListView.ItemsSource = RetrieveEventInfoObjectList(profileEventList);
                _labelNoEventsMessage.Text = string.Empty;
            }

            else
            {
                _eventsListView.ItemsSource = _defaultList;
            }
        }

        #endregion

        #region Private methods
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
        private static ObservableCollection<EventInfoObject> RetrieveEventInfoObjectList(IEnumerable<ProfileEvent> profileEventList)
        {
            var listEvents = profileEventList!=null? 
                            profileEventList.Select(eventInfo => new EventInfoObject
                                                        {
                                                            Icon = BeginApplication.Styles.DefaultEventIcon,
                                                            EventName = eventInfo.Name,
                                                            EventOwnerUserName = string.Format("{0}{1}", Aroba, eventInfo.Owner.UserName),
                                                            EventIntervalDateAndTime = string.Format("{0} - {1}",
                                                            string.Format("{0:yyyy, MMMM d}", DateTime.Parse(eventInfo.StartDate)),
                                                            string.Format("{0:t}",DateTime.Parse(eventInfo.StartTime))),
                                                            EventInfo = eventInfo
                                                        }): new List<EventInfoObject>();
            return new ObservableCollection<EventInfoObject>(listEvents);
        }

        #endregion


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           // this.Content = null;
            _profileEvents = null;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}