using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class NotificationDetail : ContentPage
    {
        public NotificationDetail(string notificationDescription)
        {
            Content = new StackLayout
                      {
                          Children =
                          {
                              new Label {Text = notificationDescription}
                          }
                      };
        }
    }
}