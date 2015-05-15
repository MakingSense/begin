using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Pages.MessagePages;
using BeginMobile.Pages.Profile;
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
        private const string DefaultStatus = "unread";
		public string MasterTitle{ get; set; }

        public Notification(string title, string iconImg)
            : base(title, iconImg)
        {
            Title = title;
			MasterTitle = AppResources.AppHomeChildNotifications;

            LabelCounter = new Label();

            _searchView = new SearchView
                          {
                              SearchBar =
                              {
                                  IsEnabled = false,
                                  IsVisible = false
                              }
                          };

            _currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            Init();
        }

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			var title = MasterTitle;

			MessagingCenter.Send (this, "masterTitle", title);
			MessagingCenter.Unsubscribe<Notification, string>(this, "masterTitle");
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
                    BeginApplication.ProfileServices.GetProfileNotification(_currentUser.AuthToken, limit, status);
            if (profileNotification!=null)
            {
                LabelCounter.Text = profileNotification.UnreadCount;
            }           
            _listViewNotifications.ItemsSource = profileNotification != null && profileNotification.Notifications.Any()
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

            await RedirectNotificationComponent(sender, target, item);

            ((ListView)sender).SelectedItem = null;

            //Update notification status (Maybe put it for each messaging subscribe)
            if (_isUnread)
            {
                var notificationId = item.Id;
                NotificationActions.Request(NotificationOption.MarkAsRead, _currentUser.AuthToken, notificationId);
            }
        }

        private async Task RedirectNotificationComponent(object sender, NotificationComponent target,
            NotificationViewModel item)
        {
            switch (target)
            {
                case NotificationComponent.Contact:                   
                    MessagingCenter.Subscribe<ContentPage, Contact>(this,
                        "contact", async (s, arg) =>
                                         {
                                             if (arg == null) return;
                                             var contactDetails = new ContactDetail(arg);
                                             await Navigation.PushAsync(contactDetails);
                                             ((ListView) sender).SelectedItem = null;
                                         });

                    var user = BeginApplication.ProfileServices.GetUser(_currentUser.AuthToken,
                        item.UserViewModel.UserId);
                    if (user != null)
                    {
                        var contact = new Contact
                                      {
                                          Icon = BeginApplication.Styles.DefaultContactIcon,//TODO:change for notification avatar if this exist
                                          NameSurname =
                                              user.NameSurname,
                                          Email = user.Email,
                                          Url = user.Url,
                                          UserName =
                                              user.UserName,
                                          Registered =
                                              user.Registered,
                                          Id =
                                              user.Id.ToString()
                                      };

                        MessagingCenter.Send<ContentPage, Contact>(this, "contact", contact);
                    }
                    MessagingCenter.Unsubscribe<ContentPage, Contact>(this, "contact");

                    break;

                case NotificationComponent.Group:

                    MessagingCenter.Subscribe<ContentPage, Group>(this,
                        "group", async (s, arg) =>
                                       {
                                           if (arg == null) return;
                                           var groupDetail = new GroupItemPage(arg);
                                           await Navigation.PushAsync(groupDetail);
                                           ((ListView) sender).SelectedItem = null;
                                       });

                    MessagingCenter.Send<ContentPage, Group>(this, "group", item.GroupViewModel);
                    MessagingCenter.Unsubscribe<ContentPage, Group>(this, "group");

                    break;

                case NotificationComponent.Message:                    
                    MessagingCenter.Subscribe<ContentPage, MessageViewModel>(this,
                        "message", async (s, arg) =>
                                         {
                                             if (arg == null) return;
                                             var messageDetails = new MessageDetail(arg);
                                             await Navigation.PushAsync(messageDetails);
                                             ((ListView) sender).SelectedItem = null;
                                         });
                    // list all message inbox and search the message notification by message id
                    var allInboxMessage = await BeginApplication.ProfileServices.GetProfileThreadMessagesInbox(_currentUser.AuthToken);
                    
                    var threadMessageId = "";
                    foreach (var threadMessagese in allInboxMessage.Threads)
                    {
                        foreach (var message in threadMessagese.Messages.Where(message => message.Id.Equals(item.ItemId)))
                        {
                            threadMessageId = message.ThreadId;
                            break;
                        }
                    }

                    if (threadMessageId != null)
                    {
                        var threadMessages =
                            await BeginApplication.ProfileServices.GetMessagesByThread(_currentUser.AuthToken, threadMessageId);

                        if (threadMessages != null && threadMessages.Any())
                        {
                            foreach (var messageSearched in threadMessages)
                            {
                                if (!messageSearched.Id.Equals(item.ItemId)) continue;
                                var message = new MessageViewModel
                                              {
                                                  Id = messageSearched.Id,
                                                  ThreadId = messageSearched.ThreadId,
                                                  DateSent = messageSearched.DateSent,
                                                  MessageContent = messageSearched.MessageContent,
                                                  SenderName = messageSearched.Sender.NameSurname,
                                                  Subject = messageSearched.Subject,
                                                  Sender = messageSearched.Sender,
                                                  Messages = threadMessages
                                              };
                                MessagingCenter.Send<ContentPage, MessageViewModel>(this, "message", message);
                                MessagingCenter.Unsubscribe<ContentPage, MessageViewModel>(this, "message");
                            }                            
                        }
                        else
                        {
                            await DisplayAlert("Error", "An Error has been happened in the Server.", "Ok");
                        }
                    }
                    break;

                default:
                    await DisplayAlert("Info", string.Format("Goes to {0}", target), "Ok");
                    break;
            }
        }

        #endregion

        #region Private Methods

        private async void Init()
        {
            var profileNotification =
                await BeginApplication.ProfileServices.GetProfileNotification(_currentUser.AuthToken, DefaultLimit, DefaultStatus);

            if (profileNotification!=null)
            {
                LabelCounter.Text = profileNotification.UnreadCount;   
            }               
            LoadStatusOptionsPicker();

            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            _statusPicker.SelectedIndexChanged += SearchItemEventHandler;

            var listViewDataTemplate = new DataTemplate(() => new TemplateListViewNotification(_isUnread));

            MessagingSubscriptions();

            _listViewNotifications = new ListView
                                     {
                                         ItemTemplate = listViewDataTemplate,
                                         ItemsSource = profileNotification!=null
                                         ? new ObservableCollection<NotificationViewModel>(RetrieveNotifications(profileNotification))
                                         : new ObservableCollection<NotificationViewModel>(),
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
                                             Style = BeginApplication.Styles.SubtitleStyle
                                         }, 0, 0);

            gridHeaderTitle.Children.Add(new Label
                                         {
                                             HeightRequest = 50,
                                             Text = AppResources.LabelDateReceived,
                                             Style = BeginApplication.Styles.SubtitleStyle
                                         }, 1, 0);

            var mainLayout = new StackLayout
                             {                                 
                                 Padding = BeginApplication.Styles.LayoutThickness,
                                 VerticalOptions = LayoutOptions.FillAndExpand,
                                 HorizontalOptions = LayoutOptions.FillAndExpand,
                                 Children =
                                        {
                                            _searchView.Container,
                                             //gridHeaderTitle,
                                            _listViewNotifications
                                        }
                             };
                      
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
                                                                             GroupViewModel = model.Group,
                                                                             UserViewModel = model.User
                                                                         
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

                        var updatedNotifications = await BeginApplication.ProfileServices.GetProfileNotification(_currentUser.AuthToken);
                        if (updatedNotifications!=null)
                        {
                            LabelCounter.Text = updatedNotifications.UnreadCount;
                        }                       
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
                                     
                                     var updatedNotifications = await
                                       BeginApplication.ProfileServices.GetProfileNotification(_currentUser.AuthToken);
                                     LabelCounter.Text = updatedNotifications.UnreadCount;
                                 }
                             }
                         };
        }

        #endregion
    }
}