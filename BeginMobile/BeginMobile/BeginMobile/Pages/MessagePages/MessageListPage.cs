using System.Globalization;
using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class MessageListPage : TabContent
    {
        private readonly ListView _listViewMessages;
        private readonly RelativeLayout _relativeLayoutMain;

        public readonly Label LabelCounter;

        public MessageListPage(string title, string iconImg)
            : base(title, iconImg)
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationMessages profileMessage = App.ProfileServices.GetMessagesInfo(currentUser.User.UserName, currentUser.AuthToken);

            LabelCounter = new Label
                          {
                              Text = profileMessage.GroupingMessage.CountByGroup.ToString(CultureInfo.InvariantCulture)
                          };

            _listViewMessages = new ListView
                             {
                                 ItemTemplate = new DataTemplate(typeof (ProfileMessagesItem)),
                                 ItemsSource = profileMessage.GroupingMessage.MessagesGroup,
                                 GroupDisplayBinding = new Binding("Key"),
                                 IsGroupingEnabled = true,
                                 HasUnevenRows = true,
                                 GroupHeaderTemplate = new DataTemplate(typeof (ProfileMessageHeader))
                             };

            _listViewMessages.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };

            _relativeLayoutMain = new RelativeLayout();
            _relativeLayoutMain.Children.Add(_listViewMessages, 
                Constraint.Constant(0), Constraint.Constant(0),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.RelativeToParent(parent => parent.Height));

            Content = new ScrollView { Content = _relativeLayoutMain };
        }
    }
}