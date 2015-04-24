using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.LocalizeResources.Resources;
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
            Title = AppResources.MessageInboxTitle;
            IsInbox = true;


            _searchView = new SearchView {SearchBar = {Placeholder = AppResources.PlaceholderFilterBySubjectOrContent}};
            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            MessagingSubscriptions();

            _listViewMessages = new ListView
                                {
                                    ItemTemplate = new DataTemplate(typeof (ProfileMessagesItem)),
                                    HasUnevenRows = true
                                };


            _listViewMessages.ItemSelected += ListViewItemSelectedEventHandler;

            var gridComponents = new Grid
            {
                Padding = BeginApplication.Styles.LayoutThickness,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                                     {
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto}
                                     }
            };

            gridComponents.Children.Add(_searchView.Container, 0, 0);
            gridComponents.Children.Add(_listViewMessages, 0, 1);

            Content = gridComponents;
        }

        private void MessagingSubscriptions()
        {
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
            if (inboxThreads != null)
            {
                _listViewMessages.ItemsSource = RetrieveThreadMessages(inboxThreads);
            }
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
            if (item.ThreadUnRead.Equals(AppResources.New))
            {
                await BeginApplication.ProfileServices.MarkAsReadByThread(_currentUser.AuthToken, item.ThreadId);//Marked as read 
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
                                 await BeginApplication.ProfileServices.DeleteByThread(_currentUser.AuthToken, threadId);
                                 await DisplayAlert(AppResources.AlertInfoTitle,AppResources.ServerRemovedSuccess, AppResources.AlertOk);
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