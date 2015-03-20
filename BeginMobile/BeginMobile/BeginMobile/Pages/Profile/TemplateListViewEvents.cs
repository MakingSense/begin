using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Services.DTO;

namespace BeginMobile.Pages.Profile
{
    public class TemplateListViewEvents:ViewCell
    {
        public TemplateListViewEvents()
        {
            var eventTitle = new Label
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = 50,
                WidthRequest = 200,
                TextColor = Color.FromHex("77D065"),
                Font = Font.SystemFontOfSize(15,FontAttributes.Bold)

            };
            eventTitle.SetBinding(Label.TextProperty, "EventName");
            var eventDetailsLayout = CreateDetailsLayout();

            View = new StackLayout
            {
                HeightRequest = 60,
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
                HeightRequest = 50,
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

        public ProfileEvent eventInfo { get; set; }
    }
}
