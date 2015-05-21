using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.Interfaces;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Logging;
using BeginMobile.Services.Models;
using BeginMobile.Services.Utils;
using BeginMobile.Utils;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class InboxMessage : BaseContentPage, IDisposable
    {
        public static bool IsInbox { get; set; }
        private static ListView _listViewMessages;
        private static LoginUser _currentUser;
        private readonly SearchView _searchView;
        private readonly Grid _gridComponents;

        private readonly ILoggingService _log = Logger.Current;
        //Paginator
        private ActivityIndicator _activityIndicatorLoading;
        private StackLayout _stackLayoutLoadingIndicator;
        private static ObservableCollection<MessageViewModel> _inboxMessages;
        private static List<MessageViewModel> _defaultListModel;
        private const int DefaultLimit = 10;
        private bool _isLoading;
        private static int _offset = 0;
        private static string _name;
        private static int _limit = DefaultLimit;
        private static bool _areLastItems;
        private ImageSource _imageSourceMailByDefault;


        public InboxMessage()
        {
            try
            {
                Style = BeginApplication.Styles.PageStyle;
                _currentUser = (LoginUser) Application.Current.Properties["LoginUser"];

                LoadDeafultImage();
                CallServiceApi();

                Title = AppResources.MessageInboxTitle;
                IsInbox = true;
                _areLastItems = false;

                _defaultListModel = new List<MessageViewModel>();


                _searchView = new SearchView
                              {
                                  SearchBar =
                                  {
                                      Placeholder =
                                          AppResources.PlaceholderFilterBySubjectOrContent
                                  }
                              };
                _searchView.SearchBar.TextChanged += SearchItemEventHandler;
                _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
                MessagingSubscriptions();

                _stackLayoutLoadingIndicator = CreateStackLayoutWithLoadingIndicator(ref _activityIndicatorLoading);

                _listViewMessages = new ListView
                                    {
                                        ItemTemplate =
                                            new DataTemplate(() => new ProfileMessagesItem(_imageSourceMailByDefault)),
                                        HasUnevenRows = true,
                                        ClassId = "InboxMessageId",
                                    };


                _listViewMessages.ItemSelected += ListViewItemSelectedEventHandler;
                _listViewMessages.ItemAppearing += ItemOnAppearing;

                _gridComponents = new Grid
                                  {
                                      HeightRequest = 500,
                                      WidthRequest = 800,
                                      Padding = BeginApplication.Styles.LayoutThickness,
                                      HorizontalOptions = LayoutOptions.Fill,
                                      VerticalOptions = LayoutOptions.Fill,
                                      RowDefinitions =
                                      {
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto}
                                      }
                                  };

                var relativeLayoutListView = new RelativeLayout() {VerticalOptions = LayoutOptions.FillAndExpand};
                relativeLayoutListView.Children.Add(_listViewMessages,
                    xConstraint: Constraint.Constant(0),
                    yConstraint: Constraint.Constant(0),
                    widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                    heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

                _gridComponents.Children.Add(_searchView.Container, 0, 0);
                _gridComponents.Children.Add(relativeLayoutListView, 0, 1);
                _gridComponents.Children.Add(_stackLayoutLoadingIndicator, 0, 2);

                ToolbarItem = new ToolbarItem("Filter", BeginApplication.Styles.FilterIcon, async () =>
                                                                                                  {
                                                                                                      _searchView
                                                                                                          .Container
                                                                                                          .IsVisible
                                                                                                          = true;
                                                                                                  });

                ToolbarItemSendMessage = new ToolbarItem("SendMessage", BeginApplication.Styles.FilterIcon, async () =>
                                                                                                             {
                                                                                                                 await Navigation
                                                                                                                     .PushAsync
                                                                                                                     (new SendMessage
                                                                                                                         ());
                                                                                                             });

                Content = _gridComponents;
            }
            catch (Exception exception)
            {
                _log.Exception(exception);
                AppContextError.Send(typeof (InboxMessage).Name, "InboxMessage", exception, null,
                    ExceptionLevel.Application);
            }
        }

        public ToolbarItem ToolbarItem { get; set; }
        public ToolbarItem ToolbarItemSendMessage { get; set; }


        public Grid GetGrid()
        {
            return _gridComponents;
        }

        #region Paginator Helper

        private void RemoveLoadingIndicator(StackLayout stackLayoutLoading)
        {
            _gridComponents.RowDefinitions[2].Height = GridLength.Auto;
            if (_gridComponents.Children.Contains(stackLayoutLoading))
            {
                _gridComponents.Children.Remove(stackLayoutLoading);
            }
        }

        private void AddLoadingIndicator(StackLayout stackLayoutLoading)
        {
            _gridComponents.RowDefinitions[2].Height = Device.OnPlatform<double>(33, 43, 43);
            if (!_gridComponents.Children.Contains(stackLayoutLoading))
            {
                _gridComponents.Children.Add(stackLayoutLoading, 0, 2);
            }
        }

        private async void LoadItems()
        {
            _offset += _limit;
            _isLoading = true;

            _activityIndicatorLoading.IsRunning = true;
            _activityIndicatorLoading.IsVisible = true;

            var resultRequest = await
                BeginApplication.ProfileServices.GetProfileThreadMessagesInbox(_currentUser.AuthToken, _name,
                    _limit.ToString(), _offset.ToString());

            if (resultRequest != null)
            {
                if (resultRequest.Threads != null && resultRequest.Threads.Any())
                {
                    Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                                                               {
                                                                   foreach (
                                                                       var message in
                                                                           RetrieveThreadMessages(resultRequest))
                                                                   {
                                                                       _inboxMessages.Add(message);
                                                                   }

                                                                   _activityIndicatorLoading.IsRunning = false;
                                                                   _activityIndicatorLoading.IsVisible = false;
                                                                   RemoveLoadingIndicator(_stackLayoutLoadingIndicator);

                                                                   _isLoading = false;
                                                                   return false;
                                                               });
                }
                else
                {
                    _activityIndicatorLoading.IsRunning = false;
                    _activityIndicatorLoading.IsVisible = false;
                    RemoveLoadingIndicator(_stackLayoutLoadingIndicator);

                    _isLoading = false;
                    _areLastItems = true;
                }
            }
            else
            {
                _activityIndicatorLoading.IsRunning = false;
                _activityIndicatorLoading.IsVisible = false;
                RemoveLoadingIndicator(_stackLayoutLoadingIndicator);

                _isLoading = false;
            }
        }

        private async void ItemOnAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (_isLoading == true ||
                _inboxMessages.Count == 0 ||
                _areLastItems == true) return;

            var appearingItem = (MessageViewModel) e.Item;
            var lastItem = _inboxMessages[_inboxMessages.Count - 1];

            if ((appearingItem.ThreadId == lastItem.ThreadId) &&
                (appearingItem.Id == lastItem.Id))
            {
                AddLoadingIndicator(_stackLayoutLoadingIndicator);
                LoadItems();
            }
        }

        #endregion

        private void MessagingSubscriptions()
        {
            MessagingCenter.Subscribe(this, MessageSuscriptionNames.RemoveInboxMessage, RemoveMessageCallback());
        }

        /*
        * Get the Inbox Messages from Inbox Service API, parse the Message to MessageViewModel for add into list and return this list
        */

        public static async Task CallServiceApi()
        {
            _offset = 0;
            _name = null;
            _areLastItems = false;

            var inboxThreads =
                await
                    BeginApplication.ProfileServices.GetProfileThreadMessagesInbox(_currentUser.AuthToken, _name,
                        _limit.ToString(), _offset.ToString());
            if (inboxThreads != null)
            {
                _inboxMessages = RetrieveThreadMessages(inboxThreads);
                _listViewMessages.ItemsSource = _inboxMessages;
            }
            else
            {
                _inboxMessages = new ObservableCollection<MessageViewModel>(_defaultListModel);
            }
        }

        private static ObservableCollection<MessageViewModel> RetrieveThreadMessages(ProfileThreadMessages inboxThreads)
        {
            var inboxMessageData = new List<MessageViewModel>();
            var threads = inboxThreads;

            if (threads == null)
            {
                return new ObservableCollection<MessageViewModel>();
            }
            else if (threads.Threads == null)
            {
                return new ObservableCollection<MessageViewModel>();
            }

            var threadMessages = threads.Threads;
            inboxMessageData.AddRange((from threadMessage in threadMessages
                let message = threadMessage.Messages.FirstOrDefault()
                where message != null
                select new MessageViewModel
                       {
                           Id = message.Id,
                           ThreadId = message.ThreadId,
                           DateSent = message.DateSent,
                           MessageContent = message.MessageContent,
                           SenderName = message.Sender.NameSurname,
                           Subject = message.Subject,
                           Sender = message.Sender,
                           Messages = threadMessage.Messages,
                           ThreadUnRead =
                               threadMessage.Unread.Equals("1")
                                   ? AppResources.New
                                   : string.Empty
                       }).OrderByDescending(c => c.DateSent));

            return new ObservableCollection<MessageViewModel>(inboxMessageData);
        }

        public async void ListViewItemSelectedEventHandler(object sender, SelectedItemChangedEventArgs eventArgs)
        {
            if (eventArgs.SelectedItem == null)
            {
                return;
            }
            var item = (MessageViewModel) eventArgs.SelectedItem;

            var messageDetail = new MessageDetail(item)
                                {
                                    BindingContext = item
                                };
            await Navigation.PushAsync(messageDetail);
            if (item.ThreadUnRead.Equals(AppResources.New))
            {
                await BeginApplication.ProfileServices.MarkAsReadByThread(_currentUser.AuthToken, item.ThreadId);
                    //Marked as read 
                await CallServiceApi();
            }
            ((ListView) sender).SelectedItem = null;
        }

        /// <summary>
        /// Common handler when an searchBar item has changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private async void SearchItemEventHandler(object sender, EventArgs eventArgs)
        {
            string limit;
            _offset = 0;
            _areLastItems = false;

            _name = sender.GetType() == typeof (SearchBar) ? ((SearchBar) sender).Text : _searchView.SearchBar.Text;
            RetrieveLimitSelected(out limit);
            _limit = string.IsNullOrEmpty(limit) ? DefaultLimit : int.Parse(limit);

            var profileThreadMessages =
                await
                    BeginApplication.ProfileServices.GetProfileThreadMessagesInbox(_currentUser.AuthToken, _name, limit,
                        _offset.ToString());
            if (profileThreadMessages != null)
            {
                _listViewMessages.ItemsSource = profileThreadMessages.Threads != null &&
                                                profileThreadMessages.Threads.Any()
                    ? new ObservableCollection<MessageViewModel>(RetrieveThreadMessages(profileThreadMessages))
                    : new ObservableCollection<MessageViewModel>(new List<MessageViewModel>());
            }

            else
            {
                _listViewMessages.ItemsSource = new ObservableCollection<MessageViewModel>(new List<MessageViewModel>());
            }
        }

        /// <summary>
        /// Returns the quantity unreaded Messages
        /// </summary>
        public string ThreadCount { get; set; }

        private Action<ProfileMessagesItem, string> RemoveMessageCallback()
        {
            return async (sender, arg) =>
                         {
                             var threadId = arg;

                             if (!string.IsNullOrEmpty(threadId))
                             {
                                 var messagesListView =
                                     (ObservableCollection<MessageViewModel>) _listViewMessages.ItemsSource;

                                 var toRemove =
                                     messagesListView.FirstOrDefault(threadMessage => threadMessage.ThreadId == threadId);

                                 if (toRemove == null || !messagesListView.Remove(toRemove)) return;
                                 await BeginApplication.ProfileServices.DeleteByThread(_currentUser.AuthToken, threadId);
                                 await
                                     DisplayAlert(AppResources.AlertInfoTitle, AppResources.ServerRemovedSuccess,
                                         AppResources.AlertOk);
                             }
                         };
        }

        private void RetrieveLimitSelected(out string limit)
        {
            var limitSelectedIndex = _searchView.Limit.SelectedIndex;
            var limitLastIndex = _searchView.Limit.Items.Count - 1;

            limit = limitSelectedIndex == -1 || limitSelectedIndex == limitLastIndex
                ? null
                : _searchView.Limit.Items[limitSelectedIndex];
        }

        public async void LoadDeafultImage()
        {
#if __ANDROID__
    //var imageArray = await ImageResizer.GetResizeImage(BeginApplication.Styles.MessageIcon);
    //this._imageSourceMailByDefault = ImageSource.FromStream(() => new MemoryStream(imageArray));
            this._imageSourceMailByDefault = BeginApplication.Styles.MessageIcon;
            #endif
#if __IOS__
            this._imageSourceMailByDefault = BeginApplication.Styles.MessageIcon;
            #endif
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void Dispose()
        {
        }
    }


    public static class MessageSuscriptionNames
    {
        public const string MarkAsReadInboxMessage = "MarkAsReadInboxMessage";
        public const string MarkAsUnreadInboxMessage = "MarkAsUnreadInboxMessage";
        public const string RemoveInboxMessage = "RemoveInboxMessage";
        public const string MarkAsReadSentMessage = "MarkAsReadSentMessage";
        public const string MarkAsUnreadSentMessage = "MarkAsUnreadSentMessage";
        public const string RemoveSentMessage = "RemoveSentMessage";
    }

    public enum MessageOption
    {
        MarkAsRead = 0,
        MarkAsUnread = 1,
        Remove = 2
    }
}