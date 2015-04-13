using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using BeginMobile.Services.Utils;
using BeginMobile.Utils;
using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class Notification : TabContent
    {
        private ListView _listViewNotifications;
        public readonly Label LabelCounter;
        private readonly LoginUser _currentUser;
        
        private readonly SearchView _searchView;
        private Picker _statusPicker;


        private Dictionary<string, string> _statusOptionsDictionary = new Dictionary<string, string>
                                                                      {
                                                                          {"unread", "Unread"},
                                                                          {"read", "Read"}
                                                                      };

        private const string DefaultLimit = "10";

        public Notification(string title, string iconImg)
            : base(title, iconImg)
        {
            Title = title;
           
            LabelCounter = new Label();

            _searchView = new SearchView
                          {
                              SearchBar =
                              {
                                  IsEnabled = false,
                                  IsVisible = false
                              }
                          };

            _currentUser = (LoginUser)App.Current.Properties["LoginUser"];

            Init();
        }

        #region Private Methods

        private async Task Init()
        {
            var profileNotification =
                await
                    App.ProfileServices.GetProfileNotification(_currentUser.AuthToken, DefaultLimit);
           
            LabelCounter.Text = profileNotification.UnreadCount;

            LoadStatusOptionsPicker();

            var listViewDataTemplate = new DataTemplate(typeof(TemplateListViewNotification));
            
            _listViewNotifications = new ListView
                                     {
                                         ItemTemplate = listViewDataTemplate,
                                         ItemsSource = RetrieveNotifications(profileNotification),
                                         HasUnevenRows = true
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
                                             Text = AppResources.LabelNotification,
                                             Style = App.Styles.SubtitleStyle
                                         }, 0, 0);

            gridHeaderTitle.Children.Add(new Label
                                         {
                                             HeightRequest = 50,
                                             Text = AppResources.LabelDateReceived,
                                             Style = App.Styles.SubtitleStyle
                                         }, 1, 0);

            var mainLayout = new StackLayout
                             {
                                 Spacing = 2,
                                 Padding = App.Styles.LayoutThickness,
                                 VerticalOptions = LayoutOptions.Start,
                                 Orientation = StackOrientation.Vertical
                             };

            mainLayout.Children.Add(new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.FillAndExpand,
                                        Orientation = StackOrientation.Vertical,
                                        Children =
                                        {
                                            _searchView.Container,
                                            gridHeaderTitle,
                                            _listViewNotifications
                                        }
                                    });

            Content = mainLayout;
        }

        private void LoadStatusOptionsPicker()
        {
            _statusPicker = new Picker
            {
                Title = "Load only",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            if (_statusOptionsDictionary != null)
            {
                foreach (var op in _statusOptionsDictionary.Values)
                {
                    _statusPicker.Items.Add(op);
                }
            }

            else
            {
                _statusOptionsDictionary = new Dictionary<string, string> { { "unread", "Unread" } };
            }

            _searchView.Container.Children.Add(_statusPicker);
        }
        private static IEnumerable<NotificationViewModel> RetrieveNotifications(ProfileNotification profileNotification)
        {
            return profileNotification.Notifications.Select(model => new NotificationViewModel
            {
                Id = model.ItemId,
                IntervalDate = RetrieveTimeSpan(model),
                NotificationDescription =
                    RetrieveDescription(model)
            });
        }

        private static string RetrieveTimeSpan(Services.DTO.Notification model)
        {
            return DateConverter.GetTimeSpan(DateTime.Parse(model.DateNotified));
        }

        private static string RetrieveDescription(Services.DTO.Notification model)
        {
            return string.Format("You have a '{0}' from '{1}'", model.Action,
                model.User == null ? string.Empty : model.User.DisplayName);
        }

        #endregion
    }
}

#region TODO


//_listViewNotifications.ItemSelected += async (sender, eventArgs) =>
//{
//    //if (eventArgs.SelectedItem == null)
//    //{
//    //    return;
//    //}

//    //MessagingCenter.Subscribe<ContentPage, Contact>(this,
//    //    "friend", (s, arg) =>
//    //    {
//    //        if (arg == null) return;
//    //        var contactDetail =
//    //            new ContactDetail(arg);
//    //        Navigation.PushAsync(contactDetail);
//    //    });

//    //var item = (NotificationViewModel)eventArgs.SelectedItem;

//    //var currentUser =
//    //    (LoginUser)App.Current.Properties["LoginUser"];

//    //var contactList =
//    //    await
//    //        App.ProfileServices.GetContacts(
//    //            _currentUser.AuthToken) ?? new List<User>();

//    //var friend = (from contact in RetrieveContacts(contactList)
//    //              where contact.Id.Equals(item.Id)
//    //              select contact).FirstOrDefault();

//    //MessagingCenter.Send<ContentPage, Contact>(this, "friend",
//    //    friend);

//    //MessagingCenter.Unsubscribe<ContentPage, Contact>(this,
//    //    "friend");

//    ((ListView)sender).SelectedItem = null;
//};


#endregion