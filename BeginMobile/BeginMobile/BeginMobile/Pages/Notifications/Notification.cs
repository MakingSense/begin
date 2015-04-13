using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly List<NotificationViewModel> _defaulList = new List<NotificationViewModel>();
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

        #region Events

        /// <summary>
        /// Common handler when an searchBar item has changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private async void SearchItemEventHandler(object sender, EventArgs eventArgs)
        {
            string limit;
            string status;

            RetrieveLimitSelected(out limit);
            RetrieveStatusOptionSelected(out status);

            var profileNotification =
                await App.ProfileServices.GetProfileNotification(_currentUser.AuthToken, limit, status);

            _listViewNotifications.ItemsSource = profileNotification.Notifications.Any()
                ? new ObservableCollection<NotificationViewModel>(RetrieveNotifications(profileNotification))
                : new ObservableCollection<NotificationViewModel>(_defaulList);

        }

        #endregion
        
        #region Private Methods

        private async Task Init()
        {
            var profileNotification =
                await
                    App.ProfileServices.GetProfileNotification(_currentUser.AuthToken, DefaultLimit);
           
            LabelCounter.Text = profileNotification.UnreadCount;

            LoadStatusOptionsPicker();

            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            _statusPicker.SelectedIndexChanged += SearchItemEventHandler;

            var listViewDataTemplate = new DataTemplate(typeof(TemplateListViewNotification));
            
            _listViewNotifications = new ListView
                                     {
                                         ItemTemplate = listViewDataTemplate,
                                         ItemsSource = new ObservableCollection<NotificationViewModel>(RetrieveNotifications(profileNotification)),
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
            return model.User == null
                ? string.Format("You have a '{0}'", model.Action)
                : string.Format("You have a '{0}' from '{1}'", model.Action, model.User.DisplayName);
        }
        private void RetrieveLimitSelected(out string limit)
        {
            var limitSelectedIndex = _searchView.Limit.SelectedIndex;
            var limitLastIndex = _searchView.Limit.Items.Count - 1;

            limit = limitSelectedIndex == -1 || limitSelectedIndex == limitLastIndex
                ? null
                : _searchView.Limit.Items[limitSelectedIndex];
        }
        private void RetrieveStatusOptionSelected(out string status)
        {
            var catSelectedIndex = _statusPicker.SelectedIndex;
            
            var selected = catSelectedIndex == -1
                ? null
                : _statusPicker.Items[catSelectedIndex];

            status = selected == null ? null : _statusOptionsDictionary.FirstOrDefault(s => s.Value == selected).Key;
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