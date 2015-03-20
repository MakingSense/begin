using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class TemplateListViewEvents:ViewCell
    {
        public TemplateListViewEvents()
        {
            var eventTitle = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.FromHex("77D065")

            };
            eventTitle.SetBinding(Label.TextProperty, "EventName");
            var eventDetailsLayout = CreateDetailsLayout();

            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { 
                    eventTitle, eventDetailsLayout
                }
            };

        }

        public static StackLayout CreateDetailsLayout()
        {
            var eventIntervalDate = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            var eventTime = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            eventIntervalDate.SetBinding(Label.TextProperty, "EventIntervalDate");
            eventTime.SetBinding(Label.TextProperty, "EventTime");

            return new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { eventIntervalDate, eventTime }
            };
        }
    }


    public class EventInfoObject
    {
        public string EventName { get; set; }
        public string EventIntervalDate { get; set; }
        public string EventTime { get; set; }
    }
}
