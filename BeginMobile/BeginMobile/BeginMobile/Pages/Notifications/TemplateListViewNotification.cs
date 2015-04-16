using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Utils;
using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class TemplateListViewNotification : ViewCell
    {
        public TemplateListViewNotification(bool isUnread)
        {
            var labelnotificationDesc = new Label
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                WidthRequest = 350,
                Style = BeginApplication.Styles.ListItemTextStyle
            };

            labelnotificationDesc.SetBinding(Label.TextProperty, "NotificationDescription");
            var detailsLayout = CreateLayoutDetail();

            var buttonMarkAsRead = new Button
                                       {
                                           Text = AppResources.ButtonReadNotification,
                                           Style = BeginApplication.Styles.ListViewItemButton,
                                           HorizontalOptions = LayoutOptions.Start,
                                           HeightRequest = Device.OnPlatform(15, 35, 35),
                                           WidthRequest = Device.OnPlatform(70, 70, 70),
                                       };


            buttonMarkAsRead.Clicked += OnMarkAsReadEventHandler;

            var buttonMarkAsUnread = new Button
                                         {
                                             Text = AppResources.ButtonUnReadNotification,
                                             Style = BeginApplication.Styles.ListViewItemButton,
                                             HorizontalOptions = LayoutOptions.Start,
                                             HeightRequest = Device.OnPlatform(15, 35, 35),
                                             WidthRequest = Device.OnPlatform(70, 70, 70)
                                         };

            buttonMarkAsUnread.Clicked += OnMarkAsUnreadEventHandler;

            View = new StackLayout
                   {
                       HeightRequest = 60,
                       Orientation = StackOrientation.Horizontal,

                       Children =
                       {
                           labelnotificationDesc,
                           detailsLayout,
                           isUnread ? buttonMarkAsRead :buttonMarkAsUnread
                       }
                   };

            View.SetBinding(ClassIdProperty, "Id");
        }
        private static StackLayout CreateLayoutDetail()
        {
            var labelintervalDate = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Style = BeginApplication.Styles.ListItemDetailTextStyle
            };

            labelintervalDate.SetBinding(Label.TextProperty, "IntervalDate");

            return new StackLayout
            {
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { labelintervalDate }
            };
        }

        #region Events
        private void OnMarkAsUnreadEventHandler(object sender, System.EventArgs e)
        {
            var current = sender as Button;
            if (current == null) return;

            var notificationId = current.Parent.ClassId;
            SubscribeMarkUnread(notificationId);
        }

        private void OnMarkAsReadEventHandler(object sender, System.EventArgs e)
        {
            var current = sender as Button;
            if(current == null) return;

            var notificationId = current.Parent.ClassId;
            SubscribeMarkRead(notificationId);
        }

        private void SubscribeMarkRead(string notificationId)
        {
            MessagingCenter.Send(this, NotificationMessages.MarkAsRead, notificationId);
            MessagingCenter.Unsubscribe<TemplateListViewNotification, string>(this, NotificationMessages.MarkAsRead);
        }
        private void SubscribeMarkUnread(string notificationId)
        {
            MessagingCenter.Send(this, NotificationMessages.MarkAsUnread, notificationId);
            MessagingCenter.Unsubscribe<TemplateListViewNotification, string>(this, NotificationMessages.MarkAsUnread);
        }

        #endregion
    }
}