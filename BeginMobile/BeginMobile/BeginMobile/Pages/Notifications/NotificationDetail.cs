using BeginMobile.Services.Models;
using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class NotificationDetail : ContentPage
    {
        private readonly NotificationViewModel _notificationViewModel;

        public NotificationDetail(NotificationViewModel notificationViewModel)
        {
            Style = BeginApplication.Styles.PageStyle;
            _notificationViewModel = notificationViewModel;

            Content = new StackLayout
            {
                Children =
                          {
                              new Label {Text = _notificationViewModel.NotificationDescription}
                          }
            };
        }
    }
}