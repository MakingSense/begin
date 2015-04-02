using BeginMobile.Pages.MessagePages;
using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Messages : ContentPage
    {
        private ListView _lViewMessages;
        private RelativeLayout _sLayoutMain;
        public Messages()
        {
            Title = "Messages";

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationMessages profileMessage = App.ProfileServices.GetMessagesInfo(currentUser.User.UserName, currentUser.AuthToken);

            _lViewMessages = new ListView
                             {
                                 ItemTemplate = new DataTemplate(typeof (ProfileMessagesItem)),
                                 ItemsSource = profileMessage.GroupingMessage.MessagesGroup,
                                 GroupDisplayBinding = new Binding("Key"),
                                 IsGroupingEnabled = true,
                                 HasUnevenRows = true,
                                 GroupHeaderTemplate = new DataTemplate(typeof (ProfileMessageHeader))
                             };

            _lViewMessages.ItemSelected += (sender, e) =>
                                                 {
                                                     ((ListView)sender).SelectedItem = null;
                                                 };

            _sLayoutMain = new RelativeLayout();
            _sLayoutMain.Children.Add(_lViewMessages,
                Constraint.Constant(0), Constraint.Constant(0),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.RelativeToParent(parent => parent.Height));

            Content = new ScrollView { Content = _sLayoutMain };
        }
    }
}