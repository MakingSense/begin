using BeginMobile.Pages.MessagePages;
using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Messages : ContentPage
    {
        private ListView _listViewMessages;
        private RelativeLayout _relativeLayoutMain;
        private ProfileInformationMessages _profileMessage;
        public Messages()
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = "Messages";

            var currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];
            _profileMessage = BeginApplication.ProfileServices.GetMessagesInfo(currentUser.User.UserName, currentUser.AuthToken);

            _listViewMessages = new ListView
                             {
                                 ItemTemplate = new DataTemplate(typeof (ProfileMessagesItem)),
                                 ItemsSource = _profileMessage.GroupingMessage.MessagesGroup,
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
            Content = _relativeLayoutMain;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.Content = null;
            _profileMessage = null;
        }
    }
}