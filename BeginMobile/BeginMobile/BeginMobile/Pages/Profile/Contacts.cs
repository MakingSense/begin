using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.Services.DTO;
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

        public Contacts()
        {
            Title = "Contacts";
            _searchView = new SearchView("Profesional", "Personal", "Job", "Important", "Needs Attention", "Other");

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationContacts profileInformationContacts = App.ProfileServices.GetContacts(currentUser.User.UserName, currentUser.AuthToken);

            _contactsList = new List<Contact>();

            foreach (var contact in profileInformationContacts.Contacts)
            {
                _contactsList.Add(new Contact { Icon = UserDefault, NameSurname = contact.NameSurname, References = String.Format("e-mail: {0}", contact.Email) });
            }

            var contactListViewTemplate = new DataTemplate(typeof(CustomViewCell));

            _listViewContacts = new ListView
                               {
                                   ItemsSource = _contactsList,
                                   ItemTemplate = contactListViewTemplate,
                                   HasUnevenRows = true
                               };

            _listViewContacts.ItemSelected += (s, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                // TODO: Add here the logic to go the contact details
                ((ListView)s).SelectedItem = null;
            };

            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _labelNoContactsMessage = new Label();
            
            var scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Spacing = 2,
                    VerticalOptions = LayoutOptions.Start,
                    Children = {
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
        void SearchItemEventHandler(object sender, EventArgs args)
        {
            string limit;
            string cat;

            var q = sender.GetType() == typeof (SearchBar) ? ((SearchBar) sender).Text : _searchView.SearchBar.Text;

            RetrieveLimitSelected(out limit);
            RetrieveCategorySelected(out cat);

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
        private void RetrieveCategorySelected(out string cat)
        {
            var catSelectedIndex = _searchView.Category.SelectedIndex;
            var catLastIndex = _searchView.Category.Items.Count - 1;

            cat = catSelectedIndex == -1 || catSelectedIndex == catLastIndex
                ? null
                : _searchView.Category.Items[catSelectedIndex];
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