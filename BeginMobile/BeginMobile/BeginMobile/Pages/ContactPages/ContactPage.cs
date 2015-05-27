using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.Profile;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using BeginMobile.Services.Utils;
using BeginMobile.Utils;
using Xamarin.Forms;

namespace BeginMobile.Pages.ContactPages
{
    public class ContactPage : TabContent, IDisposable
    {
        private readonly ListView _listViewContacts;
        private Label _labelNoContactsMessage;
        private readonly List<Contact> _defaultList = new List<Contact>();
        private readonly SearchView _searchView;
        private readonly LoginUser _currentUser;
        private ObservableCollection<Contact> _contactsList;

        //Paginator
        private readonly ActivityIndicator _activityIndicatorLoading;
        private readonly Grid _gridLayoutMain;
        private readonly StackLayout _stackLayoutLoadingIndicator;
        private bool _isLoading;
        private int _offset = 0;
        private int _limit = DefaultLimit;
        private string _name;
        private string _sort;
        private const int DefaultLimit = 10;
        private bool _areLastItems;


        private Dictionary<string, string> _sortOptionsDictionary = new Dictionary<string, string>
                                                                    {
                                                                        {"last_active", "Last Active"},
                                                                        {"newest_registered", "Newest Registered"},
                                                                        {"alpha", "Alphabetical"},
                                                                        {string.Empty, "None"}
                                                                    };
        private Picker _sortPicker;

		public string MasterTitle{ get; set; }


        public ContactPage(string title, string icon)
            : base(title, icon)
        {
            Title = title;
			MasterTitle = AppResources.AppHomeChildFindContacts;

            _searchView = new SearchView();
            _currentUser = (LoginUser) BeginApplication.Current.Properties["LoginUser"];

            _areLastItems = false;
            _listViewContacts = new ListView
                                {ClassId = "ContactPageId"};

            _listViewContacts.ItemAppearing += async (sender, e) =>
                                                     {
                                                         if (_isLoading == true ||
                                                             _contactsList.Count == 0 ||
                                                             _areLastItems == true)
                                                         {
                                                             return;
                                                         }
                                                         var appearingItem = (Contact) e.Item;
                                                         var lastItem = _contactsList[_contactsList.Count - 1];

                                                         if ((appearingItem.Id == lastItem.Id) &&
                                                             (appearingItem.Registered == lastItem.Registered))
                                                         {
                                                             addLoadingIndicator(_stackLayoutLoadingIndicator);
                                                             LoadItems();
                                                         }
                                                     };

            _listViewContacts.ItemTapped += async (sender, eventArgs) =>
                                                  {
                                                      if (eventArgs.Item == null)
                                                      {
                                                          return;
                                                      }

                                                      var contactItem = (Contact) eventArgs.Item;

                                                      var contentPageContactDetail = new ContactDetail(contactItem)
                                                                                     {
                                                                                         BindingContext = contactItem
                                                                                     };

                                                      await Navigation.PushAsync(contentPageContactDetail);
                                                      ((ListView) sender).SelectedItem = null;
                                                  };


            _gridLayoutMain = new Grid()
                              {
                                  //Padding = BeginApplication.Styles.ThicknessMainLayout,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  }
                              };

            _stackLayoutLoadingIndicator = CreateStackLayoutWithLoadingIndicator(ref _activityIndicatorLoading);
            _gridLayoutMain.Children.Add(_stackLayoutLoadingIndicator, 0, 3);

            Content = _gridLayoutMain;
            Init();
        }

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			var title = MasterTitle;

			MessagingCenter.Send (this, "masterTitle", title);
			MessagingCenter.Unsubscribe<ContactPage, string>(this, "masterTitle");
		}

        private async Task Init()
        {
            var profileInformationContacts =
                await
                    BeginApplication.ProfileServices.GetContacts(_currentUser.AuthToken, _name, _sort, _limit.ToString(),
                        _offset.ToString());

            _contactsList = new ObservableCollection<Contact>(RetrieveContacts(profileInformationContacts));

            LoadSortOptionsPicker();

            var contactListViewTemplate = new DataTemplate(() => new ContactListItem(_currentUser));
            MessagingSubscriptions();

            _listViewContacts.ItemsSource = _contactsList;
            _listViewContacts.ItemTemplate = contactListViewTemplate;
            _listViewContacts.HasUnevenRows = true;


            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            _sortPicker.SelectedIndexChanged += SearchItemEventHandler;

            _labelNoContactsMessage = new Label();

            var stackLayoutContactsList = new StackLayout
                                          {
                                              VerticalOptions = LayoutOptions.FillAndExpand,
                                              Orientation = StackOrientation.Vertical,
                                              Children =
                                              {
                                                  _listViewContacts
                                              }
                                          };

            _gridLayoutMain.Children.Add(_searchView.Container, 0, 0);
            _gridLayoutMain.Children.Add(_labelNoContactsMessage, 0, 1);
            _gridLayoutMain.Children.Add(stackLayoutContactsList, 0, 2);

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
            Content = _gridLayoutMain;

            removeLoadingIndicator(_stackLayoutLoadingIndicator);
        }

        public ToolbarItem ToolbarItem { get; set; }
        public Grid GetGrid()
        {
            return _gridLayoutMain;
        }

        #region Events

        #region Method of the Paginator

        private void removeLoadingIndicator(View loadingIndicator)
        {
            _gridLayoutMain.RowDefinitions[3].Height = GridLength.Auto;
            if (_gridLayoutMain.Children.Contains(loadingIndicator))
            {
                _gridLayoutMain.Children.Remove(loadingIndicator);
            }
        }

        private void addLoadingIndicator(View loadingIndicator)
        {
            _gridLayoutMain.RowDefinitions[3].Height = Device.OnPlatform<double>(33, 43, 43);
            if (!_gridLayoutMain.Children.Contains(loadingIndicator))
            {
                _gridLayoutMain.Children.Add(loadingIndicator, 0, 3);
            }
        }

        private async void LoadItems()
        {
            _offset += _limit;
            _isLoading = true;

            _activityIndicatorLoading.IsRunning = true;
            _activityIndicatorLoading.IsVisible = true;

            var resultList = await BeginApplication
                .ProfileServices.GetContacts(_currentUser.AuthToken, _name, _sort, _limit.ToString(), _offset.ToString());

            if (resultList != null)
            {
                if (resultList.Any())
                {
                    Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                                                               {
                                                                   foreach (var contact in RetrieveContacts(resultList))
                                                                   {
                                                                       _contactsList.Add(contact);
                                                                   }

                                                                   _activityIndicatorLoading.IsRunning = false;
                                                                   _activityIndicatorLoading.IsVisible = false;
                                                                   removeLoadingIndicator(_stackLayoutLoadingIndicator);

                                                                   _isLoading = false;
                                                                   return false;
                                                               });
                }
                else
                {
                    _activityIndicatorLoading.IsRunning = false;
                    _activityIndicatorLoading.IsVisible = false;
                    removeLoadingIndicator(_stackLayoutLoadingIndicator);

                    _isLoading = false;
                    _areLastItems = true;
                }
            }
            else
            {
                _activityIndicatorLoading.IsRunning = false;
                _activityIndicatorLoading.IsVisible = false;
                removeLoadingIndicator(_stackLayoutLoadingIndicator);

                _isLoading = false;
            }
        }

        #endregion

        /// <summary>
        /// Common handler when an searchBar item has changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void SearchItemEventHandler(object sender, EventArgs args)
        {
            string limit;
            _offset = 0;
            _areLastItems = false;

            _name = sender.GetType() == typeof (SearchBar) ? ((SearchBar) sender).Text : _searchView.SearchBar.Text;

            RetrieveLimitSelected(out limit);
            _limit = string.IsNullOrEmpty(limit) ? DefaultLimit : int.Parse(limit);

            RetrieveSortOptionSelected(out _sort);

            var list =
                await
                    BeginApplication.ProfileServices.GetContacts(_currentUser.AuthToken, _name, _sort, limit,
                        _offset.ToString()) ?? new List<User>();

            if (list.Any())
            {
                _contactsList = new ObservableCollection<Contact>(RetrieveContacts(list));
                _listViewContacts.ItemsSource = _contactsList;
                _labelNoContactsMessage.Text = string.Empty;
            }

            else
            {
                _contactsList = new ObservableCollection<Contact>(_defaultList);
                _listViewContacts.ItemsSource = new ObservableCollection<Contact>(_defaultList);
            }
        }

        #endregion

        #region Private methods

        private void LoadSortOptionsPicker()
        {
            _sortPicker = new Picker
                          {
                              Title = "Sort by",
                              VerticalOptions = LayoutOptions.CenterAndExpand,
                              BackgroundColor = BeginApplication.Styles.ColorWhite
                          };

            if (_sortOptionsDictionary != null)
            {
                foreach (var op in _sortOptionsDictionary.Values)
                {
                    _sortPicker.Items.Add(op);
                }
            }

            else
            {
                _sortOptionsDictionary = new Dictionary<string, string> {{"last_active", "Last Active"}};
            }

            //_searchView.Container.Children.Add(_sortPicker);
        }

        private void RetrieveSortOptionSelected(out string sort)
        {
            var catSelectedIndex = _sortPicker.SelectedIndex;
            var catLastIndex = _sortPicker.Items.Count - 1;

            var selected = catSelectedIndex == -1 || catSelectedIndex == catLastIndex
                ? null
                : _sortPicker.Items[catSelectedIndex];

            sort = selected == null ? null : _sortOptionsDictionary.FirstOrDefault(s => s.Value == selected).Key;
        }

        private void RetrieveLimitSelected(out string limit)
        {
            var limitSelectedIndex = _searchView.Limit.SelectedIndex;
            var limitLastIndex = _searchView.Limit.Items.Count - 1;

            limit = limitSelectedIndex == -1 || limitSelectedIndex == limitLastIndex
                ? null
                : _searchView.Limit.Items[limitSelectedIndex];
        }

        private static IEnumerable<Contact> RetrieveContacts(IEnumerable<User> profileInformationContacts)
        {
            IEnumerable<Contact> resultList = null;

            if (profileInformationContacts != null)
            {
                resultList = profileInformationContacts.Select(contact => new Contact
                                                                          {
                                                                              Icon =
                                                                                  BeginApplication.Styles
                                                                                  .DefaultContactIcon,
                                                                              NameSurname = contact.NameSurname,
                                                                              Email =
                                                                                  string.Format("e-mail: {0}",
                                                                                      contact.Email),
                                                                              Url = contact.Url,
                                                                              UserName = contact.UserName,
                                                                              Registered = DateConverter.GetTimeSpan(DateTime.Parse(contact.Registered)),
                                                                              Id = contact.Id.ToString(),
                                                                              Relationship = contact.Relationship,
                                                                              IsOnline = contact.IsOnline,
                                                                              Profession = contact.Profession
                                                                          });
            }
            else
            {
                resultList = new List<Contact>();
            }

            return resultList;
        }

        private void MessagingSubscriptions()
        {
            MessagingCenter.Subscribe(this, FriendshipMessages.DisplayAlert, DisplayAlertCallBack());
            MessagingCenter.Subscribe(this, FriendshipMessages.AddContact, AddContactCallback());
        }

        private Action<CustomViewCell, string> DisplayAlertCallBack()
        {
            return
                async (sender, arg) =>
                      {
                          await DisplayAlert(AppResources.ErrorMessageTitle, arg, AppResources.AlertOk);
                      };
        }

        private Action<ContactListItem, string> AddContactCallback()
        {
            return async (sender, arg) =>
                         {
                             var removeUsername = arg;

                             if (!string.IsNullOrEmpty(removeUsername))
                             {
                                 var confirm = await DisplayAlert(AppResources.AlertConfirmTitle,
                                     string.Format(AppResources.AlertContactRequestConfirm,
                                         removeUsername), AppResources.AlertYes, AppResources.AlertNo);

                                 if (confirm)
                                 {
                                     var contacts = ((ObservableCollection<Contact>) _listViewContacts.ItemsSource);
                                     var toAdd =
                                         contacts.FirstOrDefault(contact => contact.UserName == removeUsername);

                                     if (toAdd != null && contacts.Remove(toAdd))
                                     {
                                         _listViewContacts.ItemsSource = contacts;
                                         await
                                             DisplayAlert(AppResources.AlertInfoTitle,
                                                 string.Format(AppResources.AlertContactFriendAddedSuccessfull,
                                                     removeUsername), AppResources.AlertOk);
                                     }
                                 }
                             }
                         };
        }

        private Action<CustomViewCell, string> RemoveContactCallback()
        {
            return async (sender, arg) =>
                         {
                             var removeUsername = arg;

                             if (!string.IsNullOrEmpty(removeUsername))
                             {
                                 var confirm = await DisplayAlert(AppResources.AlertConfirmTitle,
                                     string.Format(AppResources.AlertContactRemoveConfirmMessage,
                                         removeUsername), AppResources.AlertYes, AppResources.AlertNo);

                                 if (confirm)
                                 {
                                     var contacts = ((ObservableCollection<Contact>) _listViewContacts.ItemsSource);
                                     var toRemove =
                                         contacts.FirstOrDefault(contact => contact.UserName == removeUsername);

                                     if (toRemove != null && contacts.Remove(toRemove))
                                     {
                                         _listViewContacts.ItemsSource = contacts;
                                         await
                                             DisplayAlert(AppResources.AlertInfoTitle,
                                                 string.Format(AppResources.AlertContactRemoveMessage, removeUsername),
                                                 AppResources.AlertOk);
                                     }
                                 }
                             }
                         };
        }

        #endregion

        public void Dispose()
        {
            
        }
    }
}