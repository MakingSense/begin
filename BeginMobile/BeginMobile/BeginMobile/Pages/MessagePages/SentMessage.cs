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
    public class SentMessage : ContentPage, IDisposable
    {
        private static LoginUser _currentUser;
        private static ListView _listViewMessages;
        private readonly SearchView _searchView;
        private const string DefaultLimit = "10";

        public SentMessage()
        {
            Title = "Sent";
            InboxMessage.IsInbox = false;
            _currentUser = (LoginUser) Application.Current.Properties["LoginUser"];

            CallServiceApi();
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
                                      Children = {_searchView.Container, stackLayoutMessagesListView}
                                  };

            Content = mainStackLayout;
        }

        /*         
         * Get the Sent Messages from SentBox Service API, parse the Message to MessageViewModel for add into list and return this list
         */

        public static async Task CallServiceApi()
        {
            var profileThreadMessagesSent =
                await
                    BeginApplication.ProfileServices.GetProfileThreadMessagesSent(_currentUser.AuthToken, null,
                        DefaultLimit);            
            _listViewMessages.ItemsSource = RetrieveThreadMessages(profileThreadMessagesSent);
        }

        private static IEnumerable<MessageViewModel> RetrieveThreadMessages(
            ProfileThreadMessages profileThreadMessagesSent)
        {
            var threads = profileThreadMessagesSent;
            if (!threads.Threads.Any()) return new ObservableCollection<MessageViewModel>();
            ;
            var threadMessages = threads.Threads;

            var listDataSentMessage = (from sentThread in threadMessages
                let message = sentThread.Messages.FirstOrDefault()
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
                           Messages = sentThread.Messages
                       }).ToList();
            return new ObservableCollection<MessageViewModel>(listDataSentMessage);
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
                await BeginApplication.ProfileServices.GetProfileThreadMessagesSent(_currentUser.AuthToken, q, limit);
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

            ((ListView) sender).SelectedItem = null;
        }

        private void MessagingSubscriptions()
        {
            MessagingCenter.Subscribe(this, MessageSuscriptionNames.RemoveSentMessage, RemoveSentMessageCallback());
        }

        private Action<ProfileMessagesItem, string> RemoveSentMessageCallback()
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
}