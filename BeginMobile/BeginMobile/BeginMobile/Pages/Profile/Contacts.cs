using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.Services.ManagerServices;
using BeginMobile.Services.DTO;

namespace BeginMobile.Pages.Profile
{
    public class Contacts : ContentPage
    {
        private const string userDefault = "userdefault3.png";

        public Contacts()
        {
            Title = "Contacts";

            Label header = new Label
            {
                Text = "My Contacts",
                Font = Font.SystemFontOfSize(50, FontAttributes.Bold),
                HorizontalOptions = LayoutOptions.Center
            };

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationContacts profileInformationContacts = App.ProfileServices.GetContacts(currentUser.User.UserName, currentUser.AuthToken);

            var contactsList = new List<Contact>();

            foreach (var contact in profileInformationContacts.Contacts)
            {
                contactsList.Add(new Contact { Icon = userDefault, NameSurname = contact.NameSurname, References = String.Format("Contacts to: {0} - {1}", contact.Email, contact.Url) });
            }
            var contactListViewTemplate = new DataTemplate(typeof(CustomViewCell));

            ListView contactlistView = new ListView
            {
                ItemsSource = contactsList,
                ItemTemplate = contactListViewTemplate
            };


            ScrollView scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Spacing = 2,
                    VerticalOptions = LayoutOptions.Start,
                    Children = {
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
    }
}
