using System.Collections.Generic;
using System.Threading.Tasks;
using BeginMobile.Menu;
using BeginMobile.Services.DTO;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace BeginMobile.Pages.MessagePages
{
    public class MessageListPage : TabContent
    {
        public readonly Label LabelCounter;
        private const string DefaultIcon = "userprofile.png";

        public MessageListPage(string title, string iconImg)
            : base(title, iconImg)
        {
            Title = title;
            CalServiceApi();

            LabelCounter = new Label
                           {
                               Text = ThreadCount
                           };

            var listMessageMenuItems = new ObservableCollection<MenuItemViewModel>
                                       {
                                           new MenuItemViewModel
                                           {
                                               Icon =
                                                   DefaultIcon,
                                               OptionName =
                                                   MenuItemsNames
                                                   .Inbox
                                           },
                                           new MenuItemViewModel
                                           {
                                               Icon = DefaultIcon,
                                               OptionName =
                                                   MenuItemsNames
                                                   .Sent
                                           },
                                           new MenuItemViewModel
                                           {
                                               Icon = DefaultIcon,
                                               OptionName =
                                                   MenuItemsNames
                                                   .Send
                                           }
                                       };
            var dataTemplateMenuOptions = new DataTemplate(typeof (MenuDataTemplate));
            var listViewMainControls = new ListView
                                       {
                                           VerticalOptions = LayoutOptions.Start,
                                           ItemsSource = listMessageMenuItems,
                                           ItemTemplate = dataTemplateMenuOptions,
                                           BackgroundColor = BeginApplication.Styles.MenuBackground,
                                           HasUnevenRows = true
                                       };

            listViewMainControls.ItemSelected += async (sender, eventArgs) =>
                                                 {
                                                     if (eventArgs.SelectedItem == null)
                                                     {
                                                         return;
                                                     }
                                                     var selectedItemOptionName =
                                                         ((MenuItemViewModel) eventArgs.SelectedItem).OptionName;

                                                     switch (selectedItemOptionName)
                                                     {
                                                         case MenuItemsNames.Inbox:
                                                             using (
                                                                 var contentPageInboxMessage = new InboxMessage())
                                                             {
                                                                 await InboxMessage.CallServiceApi();
                                                                 await Navigation.PushAsync(contentPageInboxMessage);                                                                 
                                                                 break;
                                                             }
                                                         case MenuItemsNames.Sent:
                                                             using (var contentPageSentMessage = new SentMessage()
                                                                 )
                                                             {
                                                                 await Navigation.PushAsync(contentPageSentMessage);
                                                                 break;
                                                             }
                                                         case MenuItemsNames.Send:
                                                             using (var contentPageSentMessage = new SendMessage()
                                                                 )
                                                             {
                                                                 await Navigation.PushAsync(contentPageSentMessage);
                                                                 break;
                                                             }
                                                     }
                                                     ((ListView) sender).SelectedItem = null;
                                                 };

            var stackLayoutMessagesListView = new StackLayout
                                              {
                                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                                  Orientation = StackOrientation.Vertical,
                                                  Children = {listViewMainControls}
                                              };
            var mainStackLayout = new StackLayout
                                  {
                                      Children = {stackLayoutMessagesListView},
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      VerticalOptions = LayoutOptions.FillAndExpand
                                  };
            Content = mainStackLayout;
        }

        private async void CalServiceApi()
        {
            var currentUser = (LoginUser) BeginApplication.Current.Properties["LoginUser"];
            var inboxThreads =
                await BeginApplication.ProfileServices.GetProfileThreadMessagesInbox(currentUser.AuthToken);
            var threads = inboxThreads;
            ThreadCount = threads.ThreadCount;
            LabelCounter.Text = ThreadCount;
        }

        public static class MenuItemsNames
        {
            public const string Inbox = "Inbox";
            public const string Sent = "Sent";
            public const string Send = "Send Message";
        }

        /// <summary>
        /// Returns the quantity unreaded Messages
        /// </summary>
        public string ThreadCount { get; set; }

        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    if (this.BindingContext == null)
        //        this.BindingContext = await (Application.Current)();
        //}
    }
}