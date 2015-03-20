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

            gridEventHeaderTitle.Children.Add(new Label
            {
                WidthRequest = 200,
                Text = "Event",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(24, FontAttributes.Bold)
            }, 0, 1, 0, 1);

            gridEventHeaderTitle.Children.Add(new Label
            {
                Text = "Date and Time",
                HorizontalOptions = LayoutOptions.Start,
                Font = Font.SystemFontOfSize(24,FontAttributes.Bold)
            }, 1, 2, 0, 1);

            var listEvents = new List<EventInfoObject>();

            foreach(var eventInfo in profileInformationEvents.Events){
                listEvents.Add(new EventInfoObject
                {
                    EventName = eventInfo.Name,
                    EventIntervalDate = String.Format("{0} - {1}", eventInfo.StartDate, eventInfo.EndDate),
                    EventTime = String.Format("{0} - {1}", eventInfo.StartTime, eventInfo.EndTime),
                    eventInfo = eventInfo,
                });
            }

            var eventTemplate = new DataTemplate(typeof(TemplateListViewEvents));

            var eventsListView = new ListView
            {
                ItemsSource = listEvents,
                ItemTemplate = eventTemplate
            };

            eventsListView.ItemSelected += async (sender, e) =>
            {
                var item = (EventInfoObject)e.SelectedItem;

                var itemPageProfile = new EventDetailInformation(item.eventInfo);                
                await Navigation.PushAsync(itemPageProfile);

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
                Padding = 20,
                Children =
                {
                    gridEventHeaderTitle,scrollView
                }
            };

        }
    }
}
