using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.Services.ManagerServices;
using BeginMobile.Services.DTO;
using BeginMobile.Utils.Extensions;
using BeginMobile.Utils;

namespace BeginMobile.Pages.Profile
{
    public class Contacts : ContentPage
    {
        private const string userDefault = "userdefault3.png";
        private ListView contactlistView;
        private List<Contact> contactsList;
        private Label noContactsMessage;
        private List<Contact> defaultList = new List<Contact>();
        private SearchView searchView;

        private LoginUser currentUser;

        public Contacts()
        {
            Title = "Contacts";
            searchView = new SearchView("Profesional", "Personal", "Job", "Important", "Needs Attention", "Other");

            Label header = new Label
            {
                Text = "My Contacts",
                HorizontalOptions = LayoutOptions.Center,
                Style = App.Styles.TitleStyle
            };

            currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationContacts profileInformationContacts = App.ProfileServices.GetContacts(currentUser.User.UserName, currentUser.AuthToken);

            contactsList = new List<Contact>();

            foreach (var contact in profileInformationContacts.Contacts)
            {
                contactsList.Add(new Contact { Icon = userDefault, NameSurname = contact.NameSurname, References = String.Format("e-mail: {0}", contact.Email) });
            }

            var contactListViewTemplate = new DataTemplate(typeof(CustomViewCell));

            contactlistView = new ListView
            {
                ItemsSource = contactsList,
                ItemTemplate = contactListViewTemplate
            };
            contactlistView.HasUnevenRows = true;
            contactlistView.ItemSelected += (s, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                // TODO: Add here the logic to go the contact details
                ((ListView)s).SelectedItem = null;
            };

            searchView.SearchBar.TextChanged += CommonSearchItemChanged;
            searchView.Limit.SelectedIndexChanged += CommonSearchItemChanged;
            searchView.Category.SelectedIndexChanged += CommonSearchItemChanged;
            noContactsMessage = new Label();
            /**/
            ScrollView scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Spacing = 2,
                    VerticalOptions = LayoutOptions.Start,
                    Children = {
                        searchView.Container,
                        noContactsMessage,
                        contactlistView
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
        void CommonSearchItemChanged(object sender, EventArgs args)
        {
            string q;
            string limit;
            string cat;
            string sort;

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

            //TODO: request API
            List<Contact> list = (from c in contactsList
                where (c.NameSurname.Contains(q, StringComparison.InvariantCultureIgnoreCase) ||
                       c.FirstName.Contains(q, StringComparison.InvariantCultureIgnoreCase))
                select c).ToList<Contact>();

            if (list.Any())
            {
                contactlistView.ItemsSource = list;
                noContactsMessage.Text = string.Empty;
            }

            else
            {
                contactlistView.ItemsSource = defaultList;
            }
        }

        void OnSearchBarButtonPressed(object sender, EventArgs args)
        {
            SearchBar searchBar = (SearchBar)sender;
            string searchText = searchBar.Text; // recovery the text of search bar

            if (!string.IsNullOrEmpty(searchText) || !string.IsNullOrWhiteSpace(searchText))
            {

                if (contactsList.Count == 0)
                {
                    noContactsMessage.Text = "There is no contacts";
                }

                else
                {
                    List<Contact> list = (from c in contactsList
                                          where (c.NameSurname.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) ||
                                                 c.FirstName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                                          select c).ToList<Contact>();

                    if (list.Any())
                    {
                        contactlistView.ItemsSource = list;
                        noContactsMessage.Text = "";
                    }

                    else
                    {
                        contactlistView.ItemsSource = defaultList;
                    }
                }
            }
            else
            {
                contactlistView.ItemsSource = contactsList;
            }
        }

        #endregion

        #region Private methods
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

        #endregion

    }
}
