using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using BeginMobile.Utils;
using BeginMobile.Utils.Extensions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Contacts : ContentPage
    {
        private const string UserDefault = "userdefault3.png";
        private readonly ListView _listViewContacts;
        private readonly List<Contact> _contactsList;
        private readonly Label _labelNoContactsMessage;
        private readonly List<Contact> _defaultList = new List<Contact>();
        private readonly SearchView _searchView;

        private Dictionary<string, string> _sortOptionsDictionary = new Dictionary<string, string>
                                                                  {
                                                                      {"last_active", "Last Active"},
                                                                      {"newest_registered", "Newest Registered"},
                                                                      {"alpha", "Alphabetical"}
                                                                  };
        private Picker _sortPicker;

        public Contacts()
        {
            Title = "Contacts";
            _searchView = new SearchView();

            var currentUser = (LoginUser) App.Current.Properties["LoginUser"];
            ProfileInformationContacts profileInformationContacts =
                App.ProfileServices.GetContacts(currentUser.User.UserName, currentUser.AuthToken);

            _contactsList = new List<Contact>();

            LoadSortOptionsPicker();

            foreach (var contact in profileInformationContacts.Contacts)
            {
                _contactsList.Add(new Contact
                                  {
                                      Icon = UserDefault,
                                      NameSurname = contact.NameSurname,
                                      Email = String.Format("e-mail: {0}", contact.Email),
                                      Url = contact.Url,
                                      Username = contact.UserName,
                                      Registered = contact.Registered,
                                      Id = contact.Id.ToString()
                                  });
            }

            var contactListViewTemplate = new DataTemplate(typeof (CustomViewCell));

            _listViewContacts = new ListView
                                {
                                    ItemsSource = _contactsList,
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
                                                                                           BindingContext
                                                                                               =
                                                                                               contactItem
                                                                                       };
                                                        await Navigation.PushAsync(contentPageContactDetail);
                                                        ((ListView) sender).SelectedItem = null;
                                                    };

            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _labelNoContactsMessage = new Label();

            var scrollView = new ScrollView
                             {
                                 Content = new StackLayout
                                           {
                                               Spacing = 2,
                                               VerticalOptions = LayoutOptions.Start,
                                               Children =
                                               {
                                                   _searchView.Container,
                                                   _labelNoContactsMessage,
                                                   _listViewContacts
                                               }
                                           }
                             };

            Content = new StackLayout
                      {
                          Padding = 10,
                          VerticalOptions = LayoutOptions.Start,
                          Children =
                          {
                              scrollView
                          }
                      };
        }

        #region Events

        /// <summary>
        /// Common handler when an searchBar item has changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SearchItemEventHandler(object sender, EventArgs args)
        {
            string limit;
            string sort;

            var q = sender.GetType() == typeof (SearchBar) ? ((SearchBar) sender).Text : _searchView.SearchBar.Text;

            RetrieveLimitSelected(out limit);
            RetrieveSortOptionSelected(out sort);

            //TODO: request API
            List<Contact> list = (from c in _contactsList
                where (c.NameSurname.Contains(q, StringComparison.InvariantCultureIgnoreCase) ||
                       c.FirstName.Contains(q, StringComparison.InvariantCultureIgnoreCase))
                select c).ToList<Contact>();

            if (list.Any())
            {
                _listViewContacts.ItemsSource = list;
                _labelNoContactsMessage.Text = string.Empty;
            }

            else
            {
                _listViewContacts.ItemsSource = _defaultList;
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
            var catSelectedIndex =_sortPicker.SelectedIndex;
            var catLastIndex = _sortPicker.Items.Count - 1;

            sort = catSelectedIndex == -1 || catSelectedIndex == catLastIndex
                ? null
                : _sortPicker.Items[catSelectedIndex];
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