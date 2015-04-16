using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class InboxMessage : ContentPage, IDisposable
    {
        public static bool IsInbox { get; set; }
        private static ListView _listViewMessages;
        private static LoginUser _currentUser;

        public InboxMessage()
        {
            Title = "Inbox";
            IsInbox = true;
            _currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            Init();

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
                                      Children = {stackLayoutMessagesListView},
                                  };
            Content = mainStackLayout;
        }

        /*
        * Get the Inbox Messages from Inbox Service API, parse the Message to MessageViewModel for add into list and return this list
        */

        public static async Task Init()
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
                           Messages = threadMessage.Messages
                       }).OrderByDescending(c => c.DateSent));
            _listViewMessages.ItemsSource = inboxMessageData;
        }

        public async void ListViewItemSelectedEventHandler(object sender, SelectedItemChangedEventArgs eventArgs)
        {
            if (eventArgs.SelectedItem == null)
            {
                return;
            }
            var item = (MessageViewModel) eventArgs.SelectedItem;
            var messageDetail = new MessageDetail(item);
            await Navigation.PushAsync(messageDetail);
            ((ListView) sender).SelectedItem = null;
        }

        /// <summary>
        /// Returns the quantity unreaded Messages
        /// </summary>
        public string ThreadCount { get; set; }

        public void Dispose()
        {
        }
    }
}