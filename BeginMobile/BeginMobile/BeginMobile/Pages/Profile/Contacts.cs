using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using BeginMobile.Utils;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Contacts : ContentPage
    {
        private const string UserDefault = "userdefault3.png";
        private ListView _listViewContacts;
        private Label _labelNoContactsMessage;
        private readonly List<Contact> _defaultList = new List<Contact>();
        private readonly SearchView _searchView;
        private readonly LoginUser _currentUser;
        private List<User> _profileInformationContacts;

        private const string DefaultLimit = "10";

        private Dictionary<string, string> _sortOptionsDictionary = new Dictionary<string, string>
                                                                    {
                                                                        {"last_active", "Last Active"},
                                                                        {"newest_registered", "Newest Registered"},
                                                                        {"alpha", "Alphabetical"},
                                                                        {string.Empty, "None"}
                                                                    };

        private Picker _sortPicker;

        public Contacts()
        {
            Title = "Contacts";
            _searchView = new SearchView();
            _currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            Init();
        }

        private async Task Init()
        {
            _profileInformationContacts =
                await BeginApplication.ProfileServices.GetContacts(_currentUser.AuthToken, limit: DefaultLimit);

            var contactsList = new List<Contact>();

            LoadSortOptionsPicker();

            contactsList.AddRange(RetrieveContacts(_profileInformationContacts));

            var contactListViewTemplate = new DataTemplate(() => new CustomViewCell(_currentUser));
            MessagingSubscriptions();

            _listViewContacts = new ListView
                                {
                                    ItemsSource = new ObservableCollection<Contact>(contactsList),
                                    ItemTemplate = contactListViewTemplate,
                                    HasUnevenRows = true
                                };

            _listViewContacts.ItemSelected += async (sender, eventArgs) =>
                                                    {
                                                        if (eventArgs.SelectedItem == null)
                                                        {
                                                            return;
                                                        }

                                                        var contactItem = (Contact) eventArgs.SelectedItem;

                                                        var contentPageContactDetail = new ContactDetail(contactItem)
                                                                                       {
                                                                                           BindingContext = contactItem
                                                                                       };

                                                        await Navigation.PushAsync(contentPageContactDetail);
                                                        ((ListView) sender).SelectedItem = null;
                                                    };

            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            _sortPicker.SelectedIndexChanged += SearchItemEventHandler;

            _labelNoContactsMessage = new Label();

            var stackLayoutContactsList = new StackLayout
                                          {
                                              Padding = BeginApplication.Styles.LayoutThickness,
                                              VerticalOptions = LayoutOptions.FillAndExpand,
                                              Orientation = StackOrientation.Vertical,
                                              Children =
                                              {
                                                  _listViewContacts
                                              }
                                          };

            Content = new StackLayout
                      {
                          Spacing = 2,
                          VerticalOptions = LayoutOptions.Start,
                          Children =
                          {
                              _searchView.Container,
                              _labelNoContactsMessage,
                              stackLayoutContactsList
                          }
                      };
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
            string sort;

            var q = sender.GetType() == typeof(SearchBar) ? ((SearchBar)sender).Text : _searchView.SearchBar.Text;

            RetrieveLimitSelected(out limit);
            RetrieveSortOptionSelected(out sort);

            var list = await BeginApplication.ProfileServices.GetContacts(_currentUser.AuthToken, q, sort, limit) ?? new List<User>();
            
            if (list.Any())
            {
                _listViewContacts.ItemsSource = new ObservableCollection<Contact>(RetrieveContacts(list));
                _labelNoContactsMessage.Text = string.Empty;
            }

            else
            {
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
                              VerticalOptions = LayoutOptions.CenterAndExpand
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
                _sortOptionsDictionary = new Dictionary<string, string> { { "last_active", "Last Active" } };
            }

            _searchView.Container.Children.Add(_sortPicker);
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
            return profileInformationContacts.Select(contact => new Contact
                                                                {
                                                                    Icon = UserDefault,
                                                                    NameSurname = contact.NameSurname,
                                                                    Email =contact.Email,
                                                                    Url = contact.Url,
                                                                    UserName = contact.UserName,
                                                                    Registered = contact.Registered,
                                                                    Id = contact.Id.ToString(),
                                                                    Relationship = contact.Relationship,
                                                                    IsOnline = contact.IsOnline
                                                                });
        }

        private void MessagingSubscriptions()
        {
            MessagingCenter.Subscribe(this, FriendshipMessages.DisplayAlert, DisplayAlertCallBack());
            MessagingCenter.Subscribe(this, FriendshipMessages.RemoveContact, RemoveContactCallback());
        }

        private Action<CustomViewCell, string> DisplayAlertCallBack()
        {
            return async (sender, arg) => { await DisplayAlert("Error", arg, "Ok"); };
        }

        private Action<CustomViewCell, string> RemoveContactCallback()
        {
            return async (sender, arg) =>
                         {
                             var removeUsername = arg;

                             if (!string.IsNullOrEmpty(removeUsername))
                             {

                                 var confirm = await DisplayAlert("Confirm",
                                     string.Format("Are you sure you want to remove '{0}' from contacts?", removeUsername), "Yes", "No");
                                 
                                 if (confirm)
                                 {
                                     var responseErrors = FriendshipActions.Request(FriendshipOption.Send,
                                         _currentUser.AuthToken, removeUsername);

                                     if (responseErrors.Any())
                                     {
                                         var message = responseErrors.Aggregate(string.Empty,
                                             (current, contactServiceError) =>
                                                 current + (contactServiceError.ErrorMessage + "\n"));
                                         await
                                             DisplayAlert("Error", message, "Ok");
                                     }

                                     else
                                     {
                                         var contacts = ((ObservableCollection<Contact>)_listViewContacts.ItemsSource);
                                         var toRemove =
                                             contacts.FirstOrDefault(contact => contact.UserName == removeUsername);

                                         if (toRemove != null && contacts.Remove(toRemove))
                                         {
                                             _listViewContacts.ItemsSource = contacts;
                                             await
                                                 DisplayAlert("Info",
                                                     string.Format("'{0}' Removed.", removeUsername), "Ok");
                                         } 
                                     }
                                 }
                             }
                         };
        }

        #endregion
    }
}