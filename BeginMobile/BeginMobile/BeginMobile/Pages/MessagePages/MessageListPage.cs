using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class MessageListPage : TabContent
    {
        public readonly Label LabelCounter;
        private readonly ListView _listViewMessages;
        private readonly LoginUser _currentUser;
        private readonly Button _buttonInbox;
        private readonly Button _buttonSend;
        private readonly Button _buttonSent;
        private bool _isInbox;

        public MessageListPage(string title, string iconImg)
            : base(title, iconImg)
        {
            _currentUser = (LoginUser) Application.Current.Properties["LoginUser"];

            Init();

            LabelCounter = new Label
                           {
                               Text = ThreadCount
                           };

            _listViewMessages = new ListView
                                {
                                    ItemTemplate = new DataTemplate(typeof (ProfileMessagesItem)),
                                    HasUnevenRows = true,
                                };

            _listViewMessages.ItemSelected += ListViewItemSelectedEventHandler;


            _buttonInbox = new Button
                           {
                               Text = "Inbox",
                               TextColor = BeginApplication.Styles.ColorGreenDroidBlueSapphireIos,
                               Style = BeginApplication.Styles.MessageNavigationButton,
                               FontSize = BeginApplication.Styles.TextFontSizeMedium
                           };

            _buttonSent = new Button
                          {
                              Text = "Sent",
                              TextColor = BeginApplication.Styles.ColorWhiteDroidBlueIos,
                              Style = BeginApplication.Styles.MessageNavigationButton,
                              FontSize = BeginApplication.Styles.TextFontSizeMedium
                          };
            _buttonSend = new Button
                          {
                              Text = "Send",
                              TextColor = BeginApplication.Styles.ColorWhiteDroidBlueIos,
                              Style = BeginApplication.Styles.MessageNavigationButton,
                              FontSize = BeginApplication.Styles.TextFontSizeMedium
                          };

            _buttonInbox.Clicked += InboxEventHandler;
            _buttonSent.Clicked += SentEventHandler;
            _buttonSend.Clicked += SendEventHandler;

            var stackLayoutButtons = new StackLayout
                                     {
                                         Spacing = 20,
                                         Padding = 20,
                                         Orientation = StackOrientation.Horizontal,
                                         Children = {_buttonInbox, _buttonSent, _buttonSend},
                                         HorizontalOptions = LayoutOptions.StartAndExpand
                                     };

            var stackLayoutMessagesListView = new StackLayout
                                              {
                                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                                  Orientation = StackOrientation.Vertical,
                                                  Children = {_listViewMessages}
                                              };
            var mainStackLayout = new StackLayout
                                  {
                                      Children = {stackLayoutButtons, stackLayoutMessagesListView},
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      VerticalOptions = LayoutOptions.FillAndExpand
                                  };
            Content = mainStackLayout;
        }

        private async Task Init()
        {
            SuscribeMessages(await ListDataWithInboxMessages());
        }

        public async void ListViewItemSelectedEventHandler(object sender, SelectedItemChangedEventArgs eventArgs)
        {
            if (eventArgs.SelectedItem == null)
            {
                return;
            }
            if (_isInbox)
            {
                var item = (MessageViewModel) eventArgs.SelectedItem;
                var messageDetail = new MessageDetail(item);
                await Navigation.PushAsync(messageDetail);
            }
            ((ListView) sender).SelectedItem = null;
        }

        /// <summary>
        /// Returns the quantity unreaded Messages
        /// </summary>
        public string ThreadCount { get; set; }

        /// <summary>
        /// Manage the Inbox button click action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void InboxEventHandler(object sender, EventArgs e)
        {
            var thisButton = (Button) sender;
            thisButton.TextColor = BeginApplication.Styles.ColorGreenDroidBlueSapphireIos;
            _buttonSend.TextColor = BeginApplication.Styles.ColorWhiteDroidBlueIos;
            _buttonSent.TextColor = BeginApplication.Styles.ColorWhiteDroidBlueIos;
            SuscribeMessages(await ListDataWithInboxMessages());
        }

        /// <summary>
        ///  Manage the Sent button click action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SentEventHandler(object sender, EventArgs e)
        {
            var thisButton = (Button) sender;
            thisButton.TextColor = BeginApplication.Styles.ColorGreenDroidBlueSapphireIos;
            _buttonInbox.TextColor = BeginApplication.Styles.ColorWhiteDroidBlueIos;
            _buttonSend.TextColor = BeginApplication.Styles.ColorWhiteDroidBlueIos;
            SuscribeMessages(await ListDataWithSentMessages());
        }

        /// <summary>
        ///  Manage the Send button click action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SendEventHandler(object sender, EventArgs e)
        {
            var thisButton = (Button) sender;
            thisButton.TextColor = BeginApplication.Styles.ColorGreenDroidBlueSapphireIos;
            _buttonInbox.TextColor = BeginApplication.Styles.ColorWhiteDroidBlueIos;
            _buttonSent.TextColor = BeginApplication.Styles.ColorWhiteDroidBlueIos;
            await Navigation.PushAsync(new SendMessage());
        }

        /*
         * Update the list View of messages with the list received 
         */

        private void SuscribeMessages(List<MessageViewModel> listDataMessageViewModels)
        {
            MessagingCenter.Subscribe<MessageListPage, List<MessageViewModel>>(this, "messages", (page, args) =>
                                                                                                 {
                                                                                                     if (args ==
                                                                                                         null)
                                                                                                         return;

                                                                                                     UpdateList(
                                                                                                         args);
                                                                                                 });
            MessagingCenter.Send(this, "messages", listDataMessageViewModels);
            MessagingCenter.Unsubscribe<MessageListPage, List<MessageViewModel>>(this, "messages");
        }

        /*
         * Get the Inbox Messages from Inbox Service API, parse the Message to MessageViewModel for add into list and return this list
         */

        private async Task<List<MessageViewModel>> ListDataWithInboxMessages()
        {
            _isInbox = true;
            var inboxMessageData = new List<MessageViewModel>();
            var inboxThreads = await BeginApplication.ProfileServices.GetProfileThreadMessagesInbox(_currentUser.AuthToken);
            ThreadCount = inboxThreads.ThreadCount;
            if (!inboxThreads.Threads.Any()) return inboxMessageData;
            var threadMessages = inboxThreads.Threads;
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
            return inboxMessageData;
        }

        /*
         * Get the Sent Messages from SentBox Service API, parse the Message to MessageViewModel for add into list and return this list
        */

        private async Task<List<MessageViewModel>> ListDataWithSentMessages()
        {
            _isInbox = false;
            var listDataForSentMessage = new List<MessageViewModel>();
            var profileThreadMessagesSent =
                await BeginApplication.ProfileServices.GetProfileThreadMessagesSent(_currentUser.AuthToken);
            var sentThreads = profileThreadMessagesSent.Threads;

            if (sentThreads.Any())
            {
                listDataForSentMessage.AddRange((from inboxThread in sentThreads
                    select inboxThread.Messages.FirstOrDefault()
                    into message
                    where message != null
                    select new MessageViewModel
                           {
                               Id = message.Id,
                               ThreadId = message.ThreadId,
                               DateSent = message.DateSent,
                               MessageContent = message.MessageContent,
                               SenderName = message.Sender.NameSurname,
                               Subject = message.Subject
                           }).OrderByDescending(c => c.DateSent));
            }
            return listDataForSentMessage;
        }

        /*
         * Updates the ListView with corresponding list received, this list might be: Inbox messages list or Sent ones list according the option selected
         */

        private void UpdateList(List<MessageViewModel> listDataMessages)
        {
            _listViewMessages.ItemsSource = listDataMessages;
        }
    }
}