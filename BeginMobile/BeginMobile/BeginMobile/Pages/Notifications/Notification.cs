using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Pages.MessagePages;
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
                                                                          {NotificationActions.Unread, "Unread"},
                                                                          {NotificationActions.Read, "Read"}
                                                                      };

        private bool _isUnread = true;
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
                await
                    App.ProfileServices.GetProfileNotification(_currentUser.AuthToken, limit, status);

            LabelCounter.Text = profileNotification.UnreadCount;

            _listViewNotifications.ItemsSource = profileNotification.Notifications.Any()
                ? new ObservableCollection<NotificationViewModel>(RetrieveNotifications(profileNotification))
                : new ObservableCollection<NotificationViewModel>(_defaulList);
        }

        /// <summary>
        /// When item was selected redirect component Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private async void NotificationRedirectEventHandler(object sender, EventArgs eventArgs)
        {
            var listView = sender as ListView;
            if (listView == null) return;
            if ((NotificationViewModel)listView.SelectedItem == null) return;

            var item = (NotificationViewModel)listView.SelectedItem;
            var target = NotificationActions.RetrieveComponent(item.Action);

            switch (target)
            {
                case NotificationComponent.Contact:
                    await DisplayAlert("Info", "Goes to Friendship (Contact) Details", "Ok");
                    break;

                case NotificationComponent.Group:

                    MessagingCenter.Subscribe<ContentPage, Group>(this,
                        "group", async (s, arg) =>
                                       {
                                           if (arg == null) return;
                                           var groupDetail = new GroupItemPage(arg);
                                           await Navigation.PushAsync(groupDetail);
                                           ((ListView)sender).SelectedItem = null;
                                       });

                    MessagingCenter.Send<ContentPage, Group>(this, "group", item.GroupViewModel);
                    MessagingCenter.Unsubscribe<ContentPage, Group>(this, "group");

                    break;

                case NotificationComponent.Message:
                    MessagingCenter.Subscribe<ContentPage, MessageViewModel>(this,
                        "message", async (s, arg) =>
                                         {
                                             if (arg == null) return;
                                             var groupDetail = new MessageDetail(arg);
                                             await Navigation.PushAsync(groupDetail);
                                             ((ListView) sender).SelectedItem = null;
                                         });

                   //TODO: Load Message
                     MessagingCenter.Send<ContentPage, MessageViewModel>(this, "message", new MessageViewModel());
                     MessagingCenter.Unsubscribe<ContentPage, MessageViewModel>(this, "message");
                    break;
            }

            ((ListView)sender).SelectedItem = null;

            //Update notification status (Maybe put it for each messaging subscribe)
            var notificationId = item.Id;
            NotificationActions.Request(_isUnread
                ? NotificationOption.MarkAsRead
                : NotificationOption.MarkAsUnread,
                _currentUser.AuthToken, notificationId);
        }

        #endregion

        #region Private Methods

        private async void Init()
        {
            var profileNotification =
                await App.ProfileServices.GetProfileNotification(_currentUser.AuthToken, DefaultLimit);

            LabelCounter.Text = profileNotification.UnreadCount;
            LabelCounter.BindingContext = profileNotification.UnreadCount;

            LoadStatusOptionsPicker();

            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            _statusPicker.SelectedIndexChanged += SearchItemEventHandler;

            var listViewDataTemplate = new DataTemplate(() => new TemplateListViewNotification(_isUnread));

            MessagingSubscriptions();

            _listViewNotifications = new ListView
                                     {
                                         ItemTemplate = listViewDataTemplate,
                                         ItemsSource = new ObservableCollection<NotificationViewModel>(RetrieveNotifications(profileNotification)),
                                         HasUnevenRows = true
                                     };

            _listViewNotifications.ItemSelected += NotificationRedirectEventHandler;

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
                _statusOptionsDictionary = new Dictionary<string, string> { { NotificationActions.Unread, "Unread" } };
            }

            _searchView.Container.Children.Add(_statusPicker);
        }
        private static IEnumerable<NotificationViewModel> RetrieveNotifications(ProfileNotification profileNotification)
        {
            return profileNotification.Notifications.Select(model => new NotificationViewModel
                                                                     {
                                                                         Id = model.NotificationId,
                                                                         ItemId = model.ItemId,
                                                                         Action = model.Action,
                                                                         Component = model.Component,
                                                                         IntervalDate = RetrieveTimeSpan(model),
                                                                         NotificationDescription =
                                                                             RetrieveDescription(model),
                                                                         GroupViewModel = model.Group
                                                                     });
        }
        private static string RetrieveTimeSpan(Services.DTO.Notification model)
        {
            return DateConverter.GetTimeSpan(DateTime.Parse(model.DateNotified));
        }
        private static string RetrieveDescription(Services.DTO.Notification model)
        {
            return model.User == null
                ? string.Format("You have a {0}", NotificationActions.RetrieveFriendlyAction(model.Action))
                : string.Format("You have a {0} from {1}", NotificationActions.RetrieveFriendlyAction(model.Action),
                    model.User.DisplayName);
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
            _isUnread = status == NotificationActions.Unread;
        }
        private void MessagingSubscriptions()
        {
            MessagingCenter.Subscribe(this, NotificationMessages.DisplayAlert, DisplayAlertCallBack());
            MessagingCenter.Subscribe(this, NotificationMessages.MarkAsRead, MarkAsReadCallback());
            MessagingCenter.Subscribe(this, NotificationMessages.MarkAsUnread, MarkAsUnreadCallback());
        }
        private Action<TemplateListViewNotification, string> DisplayAlertCallBack()
        {
            return async (sender, arg) => { await DisplayAlert("Error", arg, "Ok"); };
        }
        private Action<TemplateListViewNotification, string> MarkAsUnreadCallback()
        {
            return async (sender, arg) =>
            {
                var notificationId = arg;

                if (!string.IsNullOrEmpty(notificationId))
                {
                    var notifications =
                        (ObservableCollection<NotificationViewModel>)_listViewNotifications.ItemsSource;

                    var toMark = notifications.FirstOrDefault(n => n.Id == notificationId);

                    if (toMark != null && notifications.Remove(toMark))
                    {
                        NotificationActions
                            .Request(NotificationOption.MarkAsUnread, _currentUser.AuthToken, notificationId);

                        await DisplayAlert("Info", "Marked as Unread.", "Ok");
                    }
                }
            };
        }
        private Action<TemplateListViewNotification, string> MarkAsReadCallback()
        {
            return async (sender, arg) =>
                         {

                             var notificationId = arg;

                             if (!string.IsNullOrEmpty(notificationId))
                             {
                                 var notifications =
                                     (ObservableCollection<NotificationViewModel>)_listViewNotifications.ItemsSource;

                                 var toMark = notifications.FirstOrDefault(n => n.Id == notificationId);

                                 if (toMark != null && notifications.Remove(toMark))
                                 {
                                     NotificationActions
                                         .Request(NotificationOption.MarkAsRead, _currentUser.AuthToken, notificationId);

                                     await DisplayAlert("Info", "Marked as Read.", "Ok");

                                 }
                             }
                         };
        }

        #endregion
    }
}