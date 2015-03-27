using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Services.DTO;


namespace BeginMobile.Pages.Profile
{
    public class EventDetailInformation:ContentPage
    {

        public EventDetailInformation(ProfileEvent eventInfo)
        {
            Title = "Event Detail";

            Label ownerName = new Label
            {
                Text = "Public: " + eventInfo.Owner.NameSurname,
                Style = App.Styles.SubtitleStyle
            };
            Label eventDay = new Label
            {
                Text = "Date: " + (eventInfo.StartDate.Split('-'))[2].ToString() + " FEBRARY " + (eventInfo.StartDate.Split('-'))[0].ToString(),
                Style = App.Styles.SubtitleStyle
            };
            Label time = new Label
            {
                Text = "Time: " + eventInfo.StartTime,
                Style = App.Styles.SubtitleStyle
            };

            Image image = new Image
                          {
                              Source = Device.OS == TargetPlatform.iOS
                                  ? ImageSource.FromFile("Icon-76.png")
                                  : ImageSource.FromFile("Icon.png"),
                              Aspect = Aspect.AspectFit,
                              HeightRequest = 500,
                              WidthRequest = 500,
                          };

            Content = new ScrollView
            {
                Content = new StackLayout
                    {
                        Padding= 10,
                        VerticalOptions = LayoutOptions.Start,
                        Children = { ownerName, eventDay, time, image }
                    }
            };
        }
    }
}
