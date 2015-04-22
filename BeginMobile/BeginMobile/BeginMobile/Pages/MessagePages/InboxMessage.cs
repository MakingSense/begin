using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using BeginMobile.Utils;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class InboxMessage : ContentPage, IDisposable
    {
        public static bool IsInbox { get; set; }
        private static ListView _listViewMessages;
        private static LoginUser _currentUser;
        private readonly SearchView _searchView;
        private const string DefaultLimit = "10";

        public InboxMessage()
        {
            _currentUser = (LoginUser) Application.Current.Properties["LoginUser"];
            CallServiceApi();
            Title = "Inbox";
            IsInbox = true;


            _searchView = new SearchView {SearchBar = {Placeholder = "Filter by subject or content"}};
            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            MessagingSubscriptions();

            _listViewMessages = new ListView
                                {
                                    ItemTemplate = new DataTemplate(typeof (ProfileMessagesItem)),
                                    HasUnevenRows = true
                                };


            _listViewMessages.ItemSelected += ListViewItemSelectedEventHandler;


            var stackLayoutMessagesListView = new StackLayout
                                              {
                                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                                  Orientation = StackOrientation.Vertical,
                                                  Children = {_listViewMessages}
                                              };
            var mainStackLayout = new StackLayout
                                  {
                                      Padding = BeginApplication.Styles.LayoutThickness,
                                      Children = {_searchView.Container, stackLayoutMessagesListView},
                                  };
            Content = mainStackLayout;
        }

        private void MessagingSubscriptions()
        {
            MessagingCenter.Subscribe(this, MessageSuscriptionNames.MarkAsReadInboxMessage, MarkAsReadCallback());
            MessagingCenter.Subscribe(this, MessageSuscriptionNames.MarkAsUnreadInboxMessage, MarkAsUnReadCallback());
            MessagingCenter.Subscribe(this, MessageSuscriptionNames.RemoveInboxMessage, RemoveMessageCallback());
        }

        /*
        * Get the Inbox Messages from Inbox Service API, parse the Message to MessageViewModel for add into list and return this list
        */

        public static async Task CallServiceApi()
        {
            var inboxThreads =
                await
                    BeginApplication.ProfileServices.GetProfileThreadMessagesInbox(_currentUser.AuthToken, null,
                        DefaultLimit);
            _listViewMessages.ItemsSource = RetrieveThreadMessages(inboxThreads);
        }

        private static IEnumerable<MessageViewModel> RetrieveThreadMessages(ProfileThreadMessages inboxThreads)
        {
            var inboxMessageData = new List<MessageViewModel>();
            var threads = inboxThreads;
            if (!threads.Threads.Any()) return new ObservableCollection<MessageViewModel>();
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
                                   ? EnumMessageStates.Unread.ToString()
                                   : EnumMessageStates.Read.ToString()
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
            if (item.ThreadUnRead.Equals(EnumMessageStates.Unread.ToString()))
            {
                MessageActions.Request(MessageOption.MarkAsRead, _currentUser.AuthToken, item.ThreadId);
                    //TODO: Mark as read 
                await CallServiceApi();
            }
            var messageDetail = new MessageDetail(item)
                                {
                                    BindingContext = item
                                };
            await Navigation.PushAsync(messageDetail);
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
            var q = sender.GetType() == typeof (SearchBar) ? ((SearchBar) sender).Text : _searchView.SearchBar.Text;
            RetrieveLimitSelected(out limit);


            var profileThreadMessages =
                await BeginApplication.ProfileServices.GetProfileThreadMessagesInbox(_currentUser.AuthToken, q, limit);
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
                                 MessageActions.Request(MessageOption.Remove, _currentUser.AuthToken, threadId);
                                 await DisplayAlert("Info", "Removed.", "Ok");
                             }
                         };
        }

        private static Action<ProfileMessagesItem, string> MarkAsReadCallback()
        {
            return async (sender, arg) =>
                         {
                             var threadId = arg;
                             if (string.IsNullOrEmpty(threadId)) return;
                             await BeginApplication.ProfileServices.MarkAsReadByThread(_currentUser.AuthToken, threadId);
                             await CallServiceApi();
                         };
        }

        private static Action<ProfileMessagesItem, string> MarkAsUnReadCallback()
        {
            return async (sender, arg) =>
                         {
                             var threadId = arg;
                             if (string.IsNullOrEmpty(threadId)) return;
                             await
                                 BeginApplication.ProfileServices.MarkAsUnreadByThread(_currentUser.AuthToken, threadId);
                             await CallServiceApi();
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

        public void Dispose()
        {
        }
    }

    public enum EnumMessageStates
    {
        Read,
        Unread
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

    public static class MessageActions
    {
        public const string Read = "read";
        public const string Remove = "read";
        public const string Unread = "unread";

        public static async void Request(MessageOption messageOption, string authToken,
            string threadId)
        {
            switch (messageOption)
            {
                case MessageOption.MarkAsRead:
                    await BeginApplication.ProfileServices.MarkAsReadByThread(authToken, threadId);
                    break;
                case MessageOption.MarkAsUnread:
                    await BeginApplication.ProfileServices.MarkAsUnreadByThread(authToken, threadId);
                    break;
                case MessageOption.Remove:
                    await BeginApplication.ProfileServices.DeleteByThread(authToken, threadId);
                    break;
            }
        }

        public static string RetrieveFriendlyAction(string actionKey)
        {
            return actionKey.Replace("_", " ");
        }
    }
}