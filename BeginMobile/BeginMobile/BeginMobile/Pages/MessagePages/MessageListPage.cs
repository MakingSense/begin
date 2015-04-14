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
        //private readonly RelativeLayout _relativeLayoutMain;

        public readonly Label LabelCounter;


        //"Inbox", "Sent", "Compose"
        public MessageListPage(string title, string iconImg)
            : base(title, iconImg)
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationMessages profileMessage = App.ProfileServices.GetMessagesInfo(currentUser.User.UserName,
                currentUser.AuthToken); // TODO: Update 

            LabelCounter = new Label
                           {
                               Text = profileMessage.GroupingMessage.CountByGroup.ToString(CultureInfo.InvariantCulture)
                               //TODO: sum of three inbux + send + sendBox
                           };

            _listViewMessages = new ListView
                                {
                                    ItemTemplate = new DataTemplate(typeof(ProfileMessagesItem)),
                                    HasUnevenRows = true
                                };

            _listViewMessages.ItemSelected += (sender, e) => { ((ListView)sender).SelectedItem = null; };


            MessagingCenter.Subscribe<MessageListPage, IEnumerable<Message>>(this, "messages", (page, args) =>
                                                                                               {
                                                                                                   if (args ==
                                                                                                       null)
                                                                                                       return;

                                                                                                   UpdateList(args);
                                                                                               });

            IEnumerable<Message> listData = from inboxMessage in profileMessage.Messages
                                            where (inboxMessage.Type.Equals("Inbox"))
                                            select inboxMessage;

            MessagingCenter.Send(this, "messages", listData);
            MessagingCenter.Unsubscribe<MessageListPage, IEnumerable<Message>>(this, "messages");
            var buttonInbox = new Button
                              {
                                  Text = "Inbox",
                                  TextColor = App.Styles.ColorGreenDroidBlueSapphireIos,
                                  Style = App.Styles.LinkButton,
                                  FontSize = App.Styles.TextFontSizeMedium
                              };
            var buttonSend = new Button
                             {
                                 Text = "Send",
                                 TextColor = App.Styles.ColorWhiteDroidBlueIos,
                                 Style = App.Styles.LinkButton,
                                 FontSize = App.Styles.TextFontSizeMedium
                             };
            var buttonSendBox = new Button
                                {
                                    Text = "SendBox",
                                    TextColor = App.Styles.ColorWhiteDroidBlueIos,
                                    Style = App.Styles.LinkButton,
                                    FontSize = App.Styles.TextFontSizeMedium
                                };

            buttonInbox.Clicked +=
                (sender, e) =>
                {
                    var thisButton = (Button)sender;
                    thisButton.TextColor = App.Styles.ColorGreenDroidBlueSapphireIos;
                    buttonSend.TextColor = App.Styles.ColorWhiteDroidBlueIos;
                    buttonSendBox.TextColor = App.Styles.ColorWhiteDroidBlueIos;
                    MessagingCenter.Subscribe<MessageListPage, IEnumerable<Message>>(this, "messages", (page, args) =>
                                                                                                       {
                                                                                                           if (args ==
                                                                                                               null)
                                                                                                               return;

                                                                                                           UpdateList(
                                                                                                               args);
                                                                                                       });
                    listData = from inboxMessage in profileMessage.Messages
                               where (inboxMessage.Type.Equals("Inbox"))
                               select inboxMessage; // TODO: replace the api for inbox list 
                    MessagingCenter.Send(this, "messages", listData);
                    MessagingCenter.Unsubscribe<MessageListPage, IEnumerable<Message>>(this, "messages");
                };
            buttonSendBox.Clicked += async (sender, e) =>
                                     {
                                         var thisButton = (Button)sender;
                                         thisButton.TextColor = App.Styles.ColorGreenDroidBlueSapphireIos;
                                         buttonInbox.TextColor = App.Styles.ColorWhiteDroidBlueIos;
                                         buttonSend.TextColor = App.Styles.ColorWhiteDroidBlueIos;

                                         //MessagingCenter.Subscribe<MessageListPage, IEnumerable<Message>>(this,
                                         //    "messages",
                                         //    (page, args) =>
                                         //    {
                                         //        if (args ==
                                         //            null)
                                         //            return;

                                         //        UpdateList(args);
                                         //    });
                                         //_listData = from inboxMessage in profileMessage.Messages
                                         //    where (inboxMessage.Type.Equals("Sent"))
                                         //    select inboxMessage; // TODO: replace the api for inbox list 
                                         //MessagingCenter.Send(this, "messages", _listData);

                                         //MessagingCenter.Unsubscribe<MessageListPage, IEnumerable<Message>>(this,
                                         //    "messages");
                                         await Navigation.PushAsync(new SendMessage());
                                     };
            buttonSend.Clicked += (sender, e) =>
                                  {
                                      var thisButton = (Button)sender;
                                      thisButton.TextColor = App.Styles.ColorGreenDroidBlueSapphireIos;
                                      buttonInbox.TextColor = App.Styles.ColorWhiteDroidBlueIos;
                                      buttonSendBox.TextColor = App.Styles.ColorWhiteDroidBlueIos;
                                      MessagingCenter.Subscribe<MessageListPage, IEnumerable<Message>>(this,
                                          "messages", (page, args) =>
                                                      {
                                                          if (args ==
                                                              null)
                                                              return;

                                                          UpdateList(args);
                                                      });
                                      listData = from inboxMessage in profileMessage.Messages
                                                 where (inboxMessage.Type.Equals("Compose"))
                                                 select inboxMessage; // TODO: replace the api for inbox list 
                                      MessagingCenter.Send(this, "messages", listData);
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
                                                  Children = { _listViewMessages }
                                              };


            var mainStackLayout = new StackLayout
                                  {
                                      Children = { stackLayoutButtons, stackLayoutMessagesListView },
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