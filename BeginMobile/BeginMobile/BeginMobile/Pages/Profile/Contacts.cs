using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.Services.ManagerServices;
using BeginMobile.Services.DTO;
using BeginMobile.Utils.Extensions;

namespace BeginMobile.Pages.Profile
{
    public class Contacts : ContentPage
    {
        private const string userDefault = "userdefault3.png";
        private ListView contactlistView;
        private List<Contact> contactsList;
        private Label noContactsMessage;
        public Contacts()
        {
            Title = "Contacts";

            Label header = new Label
            {
                Text = "My Contacts",
                HorizontalOptions = LayoutOptions.Center,
                Style = App.Styles.TitleStyle
            };

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
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

            /*Search component */
            SearchBar searchBar = new SearchBar
            {
                Placeholder = "Search by Name and Surname",
            };
            searchBar.TextChanged += OnSearchBarButtonPressed;
            
            noContactsMessage = new Label();
            /**/
            ScrollView scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Spacing = 2,
                    VerticalOptions = LayoutOptions.Start,
                    Children = {
                        searchBar,
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

        //Method that to the search
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
                        contactlistView.ItemsSource = contactsList;
                    }
                }
            }
            else
            {
                contactlistView.ItemsSource = contactsList;
            }
        }

    }
}
