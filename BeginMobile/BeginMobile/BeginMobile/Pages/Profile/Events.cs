using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;

namespace BeginMobile.Pages.Profile
{
    public class Events: ContentPage
    {
        public Events()
        {
            Title = "Events";

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationEvents profileInformationEvents = App.ProfileServices.GetEvents(currentUser.User.UserName, currentUser.AuthToken);

            Label header = new Label
            {
                Text = "My Events",
                Font = Font.SystemFontOfSize(50, FontAttributes.Bold),
                HorizontalOptions = LayoutOptions.Center
            };

            var gridEventHeaderTitle = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,

            };
            // TODO: Add to header of events
          //  gridEventHeaderTitle.ChildAdded(new Label { Text = "Date and Time"},);

            var listEvents = new List<EventInfoObject>();

            foreach(var eventInfo in profileInformationEvents.Events){
                listEvents.Add(new EventInfoObject
                {
                    EventName = eventInfo.Name,
                    EventIntervalDate = String.Format("{0} - {1}", eventInfo.StartDate, eventInfo.EndDate),
                    EventTime = String.Format("{0} - {1}", eventInfo.StartTime, eventInfo.EndTime)
                });
            }

            var eventTemplate = new DataTemplate(typeof(TemplateListViewEvents));

            var eventsListView = new ListView
            {
                ItemsSource = listEvents,
                ItemTemplate = eventTemplate
            };

            ScrollView scrollView  = new ScrollView{
                Content = new StackLayout{
                    Spacing = 2,
                    VerticalOptions = LayoutOptions.Start,
                    Children =
                    {
                        eventsListView
                    }
                }
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Children =
                {
                    header,scrollView
                }
            };

        }
    }
}
