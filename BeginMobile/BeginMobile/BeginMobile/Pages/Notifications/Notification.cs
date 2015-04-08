using System.Collections.Generic;
using System.Linq;
using BeginMobile.Pages.Profile;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class Notification : TabContent
    {
        private readonly ListView _listViewNotifications;
        public readonly Label LabelCounter;

        public Notification(string title, string iconImg)
            : base(title, iconImg)
        {
            Title = title;
            LabelCounter = new Label
                           {
                               Text = GetNotificationViewModels().Count().ToString()
                           };

            var listViewDataTemplate = new DataTemplate(typeof (TemplateListViewNotification));

            _listViewNotifications = new ListView
                                     {
                                         ItemTemplate = listViewDataTemplate,
                                         HasUnevenRows = true
                                     };
            _listViewNotifications.ItemSelected += async (sender, eventArgs) =>
                                                         {
                                                             if (eventArgs.SelectedItem == null)
                                                             {
                                                                 return;
                                                             }

                                                             MessagingCenter.Subscribe<ContentPage, Contact>(this,
                                                                 "friend", (s, arg) =>
                                                                           {
                                                                               if (arg == null) return;
                                                                               var contactDetail =
                                                                                   new ContactDetail(arg);
                                                                               Navigation.PushAsync(contactDetail);
                                                                           });

                                                             var item = (NotificationViewModel) eventArgs.SelectedItem;

                                                             var currentUser =
                                                                 (LoginUser) App.Current.Properties["LoginUser"];
                                                             var contactList = await App.ProfileServices.GetContacts(currentUser.AuthToken, null, null, null) ?? new List<User>();                                                                 

                                                             var friend = (from contact in RetrieveContacts(contactList)
                                                                 where contact.Id.Equals(item.Id)
                                                                 select contact).FirstOrDefault();
                                                             MessagingCenter.Send<ContentPage, Contact>(this, "friend",
                                                                 friend);
                                                             MessagingCenter.Unsubscribe<ContentPage, Contact>(this,
                                                                 "friend");

                                                             ((ListView) sender).SelectedItem = null;
                                                         };

            var gridHeaderTitle = new Grid
                                  {
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      RowDefinitions =
                                      {
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto}
                                      },
                                      ColumnDefinitions =
                                      {
                                          new ColumnDefinition {Width = 350},
                                          new ColumnDefinition {Width = 350}
                                      }
                                  };


            gridHeaderTitle.Children.Add(new Label
                                         {
                                             Text = "Notification", // TODO:Add to resources
                                             Style = App.Styles.SubtitleStyle
                                         }, 0, 0);

            gridHeaderTitle.Children.Add(new Label
                                         {
                                             HeightRequest = 50,
                                             Text = "Date received", // TODO:Add to resources
                                             Style = App.Styles.SubtitleStyle
                                         }, 1, 0);

            var mainLayout = new StackLayout
                             {
                                 Spacing = 2,
                                 Padding = App.Styles.LayoutThickness,
                                 VerticalOptions = LayoutOptions.Start,
                                 Orientation = StackOrientation.Vertical
                             };

            mainLayout.Children.Add(gridHeaderTitle);
            mainLayout.Children.Add(new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.FillAndExpand,
                                        Orientation = StackOrientation.Vertical,
                                        Children = {_listViewNotifications}
                                    });

            Content = mainLayout;
        }

        private static IEnumerable<Contact> RetrieveContacts(IEnumerable<User> profileInformationContacts)
        {
            return profileInformationContacts.Select(contact => new Contact
                                                                {
                                                                    Icon = "",
                                                                    NameSurname = contact.NameSurname,
                                                                    Email =
                                                                        string.Format("e-mail: {0}",
                                                                            contact.Email),
                                                                    Url = contact.Url,
                                                                    UserName = contact.UserName,
                                                                    Registered = contact.Registered,
                                                                    Id = contact.Id.ToString()
                                                                });
        }

        private static IEnumerable<NotificationViewModel> GetNotificationViewModels()
        {
            return new List<NotificationViewModel>
                   {
                       new NotificationViewModel
                       {
                           Id = "21",
                           NotificationDescription = "You have a friendship request from Toni Montana",
                           IntervalDate = " 2weeks, 3days ago"
                       },
                       new NotificationViewModel
                       {
                           Id = "30",
                           NotificationDescription = "You have a friendship request from Soledad Pietro",
                           IntervalDate = " 2weeks, 3days ago",
                       },
                       new NotificationViewModel
                       {
                           Id = "31",
                           NotificationDescription = "You have a friendship request from Maria Di Lorenzo",
                           IntervalDate = " 5 days, 7Hours ago",
                       }
                   };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _listViewNotifications.ItemsSource = GetNotificationViewModels();
        }
    }
}