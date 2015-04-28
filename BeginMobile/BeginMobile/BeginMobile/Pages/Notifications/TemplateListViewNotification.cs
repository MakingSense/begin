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


            var labelintervalDate = new Label
                                    {
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Style = BeginApplication.Styles.ListItemDetailTextStyle
                                    };

            labelintervalDate.SetBinding(Label.TextProperty, "IntervalDate");

            var buttonMarkAsRead = new Button
                                   {
                                       Text = AppResources.ButtonReadNotification,
                                       Style = BeginApplication.Styles.ListViewItemButton
                                   };


            buttonMarkAsRead.Clicked += OnMarkAsReadEventHandler;

            var buttonMarkAsUnread = new Button
                                     {
                                         Text = AppResources.ButtonUnReadNotification,
                                         Style = BeginApplication.Styles.ListViewItemButton
                                     };

            buttonMarkAsUnread.Clicked += OnMarkAsUnreadEventHandler;


            var gridDetails = new Grid
                              {
                                  Padding = BeginApplication.Styles.GridOfListView,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                  },
                                  ColumnDefinitions =
                                  {
                                      new ColumnDefinition {Width = GridLength.Auto}
                                  }
                              };
            gridDetails.Children.Add(labelnotificationDesc, 0, 0);
            gridDetails.Children.Add(labelintervalDate, 1, 0);
            gridDetails.Children.Add(isUnread ? buttonMarkAsRead : buttonMarkAsUnread, 2, 0);
            View = gridDetails;

            View.SetBinding(ClassIdProperty, "Id");
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
            if (current == null) return;

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