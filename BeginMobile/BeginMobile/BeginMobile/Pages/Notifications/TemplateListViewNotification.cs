using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class TemplateListViewNotification : ViewCell
    {
        public TemplateListViewNotification()
        {
            var labelnotificationDesc = new Label
                                          {
                                              VerticalOptions = LayoutOptions.Start,
                                              HorizontalOptions = LayoutOptions.Start,
                                              WidthRequest = 350,
                                              Style = App.Styles.ListItemTextStyle
                                          };

            labelnotificationDesc.SetBinding(Label.TextProperty, "NotificationDescription");
            var detailsLayout = CreateLayoutDetail();

            View = new StackLayout
                   {
                       HeightRequest = 60,
                       Orientation = StackOrientation.Horizontal,
                       Children =
                       {
                           labelnotificationDesc,
                           detailsLayout
                       }
                   };
        }

        private static StackLayout CreateLayoutDetail()
        {
            var labelintervalDate = new Label
                               {
                                   HorizontalOptions = LayoutOptions.FillAndExpand,
                                   Style = App.Styles.ListItemDetailTextStyle
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
    }

    public class NotificationViewModel
    {
        public string NotificationDescription { get; set; }
        public string IntervalDate { get; set; }

        public Button ActionButton { get; set; }

        public Button DeleteButton { get; set; }
    }
}