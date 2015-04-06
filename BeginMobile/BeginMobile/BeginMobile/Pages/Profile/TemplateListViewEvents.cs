using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class TemplateListViewEvents : ViewCell
    {
        public TemplateListViewEvents()
        {
            var labelEventTitle = new Label
                             {
                                 VerticalOptions = LayoutOptions.Start,
                                 HorizontalOptions = LayoutOptions.Start,
                                 HeightRequest = 50,
                                 WidthRequest = 200,
                                 TextColor = App.Styles.LabelTextColor
                             };

            labelEventTitle.SetBinding(Label.TextProperty, "EventName");
            var eventDetailsLayout = CreateDetailsLayout();

            View = new StackLayout
                   {
                       HeightRequest = 60,
                       Orientation = StackOrientation.Horizontal,
                       Children =
                       {
                           labelEventTitle,
                           eventDetailsLayout
                       }
                   };
        }

        private static StackLayout CreateDetailsLayout()
        {
            var labelEventIntervalDate = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Style = App.Styles.ListItemTextStyle
            };
            var labelEventTime = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Style = App.Styles.ListItemDetailTextStyle
            };

            labelEventIntervalDate.SetBinding(Label.TextProperty, "EventIntervalDate");
            labelEventTime.SetBinding(Label.TextProperty, "EventTime");

            return new StackLayout
            {
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { labelEventIntervalDate, labelEventTime }
            };
        }
    }

    public class EventInfoObject
    {
        public string EventName { get; set; }
        public string EventIntervalDate { get; set; }
        public string EventTime { get; set; }
        public ProfileEvent EventInfo { get; set; }
    }
}