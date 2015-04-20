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
        private bool _isUnread = true;
        public static bool IsInbox { get; set; }
        private static ListView _listViewMessages;
        private static LoginUser _currentUser;

        public InboxMessage()
        {
            Title = "Inbox";
            IsInbox = true;
            _currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            CallServiceApi();

            MessagingSubscriptions();

            _listViewMessages = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(ProfileMessagesItem)),
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
                                      Children = {stackLayoutMessagesListView},
                                  };
            Content = mainStackLayout;
        }

        private void MessagingSubscriptions()
        {
            MessagingCenter.Subscribe(this, MessageSuscriptionNames.MarkAsReadInboxMessage, MarkAsCallback(MessageOption.MarkAsRead, "Marked as Read."));
            MessagingCenter.Subscribe(this, MessageSuscriptionNames.MarkAsUnreadInboxMessage, MarkAsUnReadCallback());
            MessagingCenter.Subscribe(this, MessageSuscriptionNames.RemoveInboxMessage, RemoveMessageCallback());
        }

        /*
        * Get the Inbox Messages from Inbox Service API, parse the Message to MessageViewModel for add into list and return this list
        */

        public static async Task CallServiceApi()
        {
            var inboxMessageData = new List<MessageViewModel>();
            var inboxThreads =
                await BeginApplication.ProfileServices.GetProfileThreadMessagesInbox(_currentUser.AuthToken);
            var threads = inboxThreads;
            if (!threads.Threads.Any()) return;
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
                                           ThreadUnRead = threadMessage.Unread.Equals("1")?"UnRead":"Readed",
                                       }).OrderByDescending(c => c.DateSent));

            var listCollection = new ObservableCollection<MessageViewModel>(inboxMessageData);
            _listViewMessages.ItemsSource = listCollection;
        }

        public async void ListViewItemSelectedEventHandler(object sender, SelectedItemChangedEventArgs eventArgs)
        {
            if (eventArgs.SelectedItem == null)
            {
                return;
            }               
            var item = (MessageViewModel)eventArgs.SelectedItem;
            if (item.ThreadUnRead.Equals("UnRead"))
            {
                MessageActions.Request(MessageOption.MarkAsRead, _currentUser.AuthToken, item.ThreadId); //TODO: Mark as read 
                await InboxMessage.CallServiceApi();
            }            
            var messageDetail = new MessageDetail(item);
            await Navigation.PushAsync(messageDetail);
            ((ListView) sender).SelectedItem = null;
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
                        (ObservableCollection<MessageViewModel>)_listViewMessages.ItemsSource;

                    var toRemove = messagesListView.FirstOrDefault(threadMessage => threadMessage.ThreadId == threadId);

                    if (toRemove == null || !messagesListView.Remove(toRemove)) return;
                    MessageActions.Request(MessageOption.Remove, _currentUser.AuthToken, threadId);
                    await DisplayAlert("Info", "Removed.", "Ok");
                }
            };
        }

        private Action<ProfileMessagesItem, string> MarkAsCallback(MessageOption messageOption, string message)
        {
            return async (sender, arg) =>
            {
                var threadId = arg;

                if (!string.IsNullOrEmpty(threadId))
                {
                    var listMessagesViewModels =
                        (ObservableCollection<MessageViewModel>)_listViewMessages.ItemsSource;

                    var toMark = listMessagesViewModels.FirstOrDefault(messageViewModel => messageViewModel.ThreadId == threadId);

                    if (toMark != null && listMessagesViewModels.Remove(toMark))
                    {
                        MessageActions.Request(messageOption, _currentUser.AuthToken, threadId);
                        await DisplayAlert("Info", message, "Ok");
                    }
                }
            };
        }
        private Action<ProfileMessagesItem, string> MarkAsUnReadCallback()
        {
            return async (sender, arg) =>
            {
                var threadId = arg;

                if (!string.IsNullOrEmpty(threadId))
                {
                    var listMessagesViewModels =
                        (ObservableCollection<MessageViewModel>)_listViewMessages.ItemsSource;

                    var toMark = listMessagesViewModels.FirstOrDefault(messageViewModel => messageViewModel.ThreadId == threadId);

                    if (toMark != null && listMessagesViewModels.Remove(toMark))
                    {
                        MessageActions.Request(MessageOption.MarkAsUnread, _currentUser.AuthToken, threadId);

                        await DisplayAlert("Info", "Marked as UnRead.", "Ok");
                    }
                }
            };
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

    public static class MessageActions
    {
        public const string Read = "read";
        public const string Remove = "read";
        public const string Unread = "unread";

        //private static readonly Dictionary<string, NotificationComponent> ComponentsDictionary =
        //    new Dictionary<string, NotificationComponent>
        //    {
        //        {"new_message", NotificationComponent.Message},
        //        {"group_invite", NotificationComponent.Group},
        //        {"friendship_request", NotificationComponent.Contact},
        //        {"friendship_accepted", NotificationComponent.Contact},
        //        {"friendship_rejected", NotificationComponent.Contact},
        //        {"friendship_removed", NotificationComponent.Contact},
        //        {"new_at_mention", NotificationComponent.Activity }
        //    };

        public async static void Request(MessageOption notificationOption, string authToken,
            string threadId)
        {
            switch (notificationOption)
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
        //public static NotificationComponent RetrieveComponent(string actionKey)
        //{
        //    return ComponentsDictionary[actionKey];
        //}

        public static string RetrieveFriendlyAction(string actionKey)
        {
            return actionKey.Replace("_", " ");
        }
    }
}