using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Services.DTO;

namespace BeginMobile.Pages.Notifications
{
    public class TemplateListViewNotification : ViewCell
    {
        private readonly NotificationViewModel _model;

        public TemplateListViewNotification()
        {
            // _model = model;

            var notificationDescription = new Label
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                //HeightRequest = 50,
                WidthRequest = 350,
                Style = App.Styles.ListItemTextStyle            
            };
            notificationDescription.SetBinding(Label.TextProperty, "NotificationDescription");
            var detailsLayout = CreateLayoutDetail();

            View = new StackLayout
            {
                HeightRequest = 60,
                Orientation = StackOrientation.Horizontal,
                Children = { 
                    notificationDescription, detailsLayout
                }
            };
        }

        public static StackLayout CreateLayoutDetail()
        {
            var intervalDate = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Style = App.Styles.ListItemDetailTextStyle
            };

            var button1 = new Button { Text = "READ" };
            var buttonDelete = new Button { Text = "DELETE" };           

            var label = new Label { Text = " | " };

            intervalDate.SetBinding(Label.TextProperty, "IntervalDate");

            return new StackLayout
            {
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                //Children = { intervalDate, button1, buttonDelete }

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
