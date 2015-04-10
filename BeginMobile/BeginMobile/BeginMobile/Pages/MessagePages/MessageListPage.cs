using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class MessageListPage : TabContent
    {
        private readonly ListView _listViewMessages;
        private readonly RelativeLayout _relativeLayoutMain;

        public readonly Label LabelCounter;


        private IEnumerable<Message> _listData;

        //"Inbox", "Sent", "Compose"
        public MessageListPage(string title, string iconImg)
            : base(title, iconImg)
        {
            var currentUser = (LoginUser) App.Current.Properties["LoginUser"];
            ProfileInformationMessages profileMessage = App.ProfileServices.GetMessagesInfo(currentUser.User.UserName,
                currentUser.AuthToken); // TODO: Update 

            LabelCounter = new Label
                           {
                               Text = profileMessage.GroupingMessage.CountByGroup.ToString(CultureInfo.InvariantCulture)
                               //TODO: sum of three inbux + send + sendBox
                           };

            _listViewMessages = new ListView
                                {
                                    ItemTemplate = new DataTemplate(typeof (ProfileMessagesItem)),
                                    HasUnevenRows = true
                                };

            _listViewMessages.ItemSelected += (sender, e) => { ((ListView) sender).SelectedItem = null; };


            MessagingCenter.Subscribe<MessageListPage, IEnumerable<Message>>(this, "messages", (page, args) =>
                                                                                               {
                                                                                                   if (args ==
                                                                                                       null)
                                                                                                       return;

                                                                                                   UpdateList(args);
                                                                                               });

            _listData = from inboxMessage in profileMessage.Messages
                where (inboxMessage.Type.Equals("Inbox"))
                select inboxMessage; // TODO: get inbox list from services for inbox

            MessagingCenter.Send(this, "messages", _listData);
            MessagingCenter.Unsubscribe<MessageListPage, IEnumerable<Message>>(this, "messages");
            var buttonInbox = new Button
                              {
                                  Text = "Inbox",
                                  TextColor = Color.White,
                                  Style = App.Styles.LinkButton
                              };

            buttonInbox.Clicked +=
                (sender, e) =>
                {
                    var thisButton = (Button) sender;
                    thisButton.TextColor = Color.Green;

                    MessagingCenter.Subscribe<MessageListPage, IEnumerable<Message>>(this, "messages", (page, args) =>
                                                                                                       {
                                                                                                           if (args ==
                                                                                                               null)
                                                                                                               return;

                                                                                                           UpdateList(
                                                                                                               args);
                                                                                                       });
                    _listData = from inboxMessage in profileMessage.Messages
                        where (inboxMessage.Type.Equals("Inbox"))
                        select inboxMessage; // TODO: replace the api for inbox list 
                    MessagingCenter.Send(this, "messages", _listData);
                    MessagingCenter.Unsubscribe<MessageListPage, IEnumerable<Message>>(this, "messages");
                };

            var buttonSendBox = new Button
                                {
                                    Text = "SendBox",
                                    Style = App.Styles.LinkButton
                                };

            buttonSendBox.Clicked += (sender, e) =>
                                     {
                                         var thisButton = (Button) sender;
                                         thisButton.TextColor = Color.Green;

                                         MessagingCenter.Subscribe<MessageListPage, IEnumerable<Message>>(this,
                                             "messages",
                                             (page, args) =>
                                             {
                                                 if (args ==
                                                     null)
                                                     return;

                                                 UpdateList(args);
                                             });
                                         _listData = from inboxMessage in profileMessage.Messages
                                             where (inboxMessage.Type.Equals("Sent"))
                                             select inboxMessage; // TODO: replace the api for inbox list 
                                         MessagingCenter.Send(this, "messages", _listData);

                                         MessagingCenter.Unsubscribe<MessageListPage, IEnumerable<Message>>(this,
                                             "messages");
                                     };

            var buttonSend = new Button
                             {
                                 Text = "Send",
                                 Style = App.Styles.LinkButton
                             };

            buttonSend.Clicked += (sender, e) =>
                                  {
                                      var thisButton = (Button) sender;
                                      thisButton.TextColor = Color.Green;
                                      MessagingCenter.Subscribe<MessageListPage, IEnumerable<Message>>(this,
                                          "messages", (page, args) =>
                                                      {
                                                          if (args ==
                                                              null)
                                                              return;

                                                          UpdateList(args);
                                                      });
                                      _listData = from inboxMessage in profileMessage.Messages
                                          where (inboxMessage.Type.Equals("Compose"))
                                          select inboxMessage; // TODO: replace the api for inbox list 
                                      MessagingCenter.Send(this, "messages", _listData);
                                      MessagingCenter.Unsubscribe<MessageListPage, IEnumerable<Message>>(this,
                                          "messages");
                                  };


            var stackLayoutButtons = new StackLayout
                                     {
                                         Spacing = 20,
                                         Padding = 20,
                                         Orientation = StackOrientation.Horizontal,
                                         Children = { buttonInbox, buttonSend, buttonSendBox },
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

        private void UpdateList(IEnumerable<Message> listDataMessages)
        {
            _listViewMessages.ItemsSource = listDataMessages;
        }
    }
}