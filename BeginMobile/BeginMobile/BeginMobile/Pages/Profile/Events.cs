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

namespace BeginMobile.Pages.Profile
{
    public class Events : ContentPage
    {
        private Label _labelNoEventsMessage;
        private ListView _eventsListView;

        private readonly List<EventInfoObject> _defaultList = new List<EventInfoObject>();
        private readonly SearchView _searchView;
        private Picker _categoriesPicker;
        private List<string> _categoriesList = new List<string> { AppResources.OptionAllCategories };

        private readonly LoginUser _currentUser;
        private const string DefaultLimit = "10";
        
        private ObservableCollection<ProfileEvent> _profileEvents;

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

            #region Subtitles layout

            var gridEventHeaderTitle = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            gridEventHeaderTitle.Children.Add(new Label
            {
                WidthRequest = 200,
                HeightRequest = 80,
                Text = AppResources.LabelEventName,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Style = BeginApplication.Styles.SubtitleStyle
            }, 0, 1, 0, 1);

            gridEventHeaderTitle.Children.Add(new Label
            {
                Text = AppResources.LabelEventDate,
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.Start,
                Style = BeginApplication.Styles.SubtitleStyle
            }, 1, 2, 0, 1);

            #endregion

            #region List components

            var listEvents = RetrieveEventInfoObjectList(_profileEvents);

            var eventTemplate = new DataTemplate(typeof(TemplateListViewEvents));

            _eventsListView = new ListView
            {
                ItemsSource = listEvents,
                ItemTemplate = eventTemplate
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
                    Spacing = 2,
                    VerticalOptions = LayoutOptions.Start,
                    Children =
                    {
                        _eventsListView
                    }
                }
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Padding = 20,
                Children =
                          {
                              _searchView.Container,
                              gridEventHeaderTitle,
                              scrollView
                          }
            };

            #endregion
        }
        
        private void LoadCategoriesPicker()
        {
            _categoriesPicker = new Picker
            {
                Title = AppResources.PickerEventFilterBycategory,
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
                                                            EventName = eventInfo.Name,
                                                            EventIntervalDate =
                                                                String.Format("{0} - {1}", eventInfo.StartDate,
                                                                    eventInfo.EndDate),
                                                            EventTime =
                                                                String.Format("{0} - {1}", eventInfo.StartTime,
                                                                    eventInfo.EndTime),
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
    }
}