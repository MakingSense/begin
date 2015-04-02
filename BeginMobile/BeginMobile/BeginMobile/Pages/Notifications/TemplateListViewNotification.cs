using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class TemplateListViewNotification : ViewCell
    {
        public TemplateListViewNotification()
        {
            var notificationDescription = new Label
                                          {
                                              VerticalOptions = LayoutOptions.Start,
                                              HorizontalOptions = LayoutOptions.Start,
                                              WidthRequest = 350,
                                              Style = App.Styles.ListItemTextStyle
                                          };

            notificationDescription.SetBinding(Label.TextProperty, "NotificationDescription");
            var detailsLayout = CreateLayoutDetail();

            View = new StackLayout
                   {
                       HeightRequest = 60,
                       Orientation = StackOrientation.Horizontal,
                       Children =
                       {
                           notificationDescription,
                           detailsLayout
                       }
                   };
        }

        private static StackLayout CreateLayoutDetail()
        {
            var intervalDate = new Label
                               {
                                   HorizontalOptions = LayoutOptions.FillAndExpand,
                                   Style = App.Styles.ListItemDetailTextStyle
                               };

            intervalDate.SetBinding(Label.TextProperty, "IntervalDate");

            return new StackLayout
                   {
                       HeightRequest = 50,
                       HorizontalOptions = LayoutOptions.FillAndExpand,
                       Orientation = StackOrientation.Horizontal,
                       Children = { intervalDate }
                   };
        }
    }

    public class NotificationViewModel
    {
        public string NotificationDescription { get; set; }
        public string IntervalDate { get; set; }

        public Button ActionButton { get; set; }

        public Button DeleteButton { get; set; }
    }
}