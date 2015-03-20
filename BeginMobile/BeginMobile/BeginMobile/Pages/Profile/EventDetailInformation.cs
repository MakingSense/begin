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
                Text = "Event publicated for: " + eventInfo.Owner.NameSurname,
                Font = Font.SystemFontOfSize(14, FontAttributes.None)
            };
            Label eventDay = new Label
            {
                Text = "Date Of Event: " + (eventInfo.StartDate.Split('-'))[2].ToString() + " FEBRARY " + (eventInfo.StartDate.Split('-'))[0].ToString(),
                Font = Font.SystemFontOfSize(14, FontAttributes.None)
            };
            Label time = new Label
            {
                Text = "Time: " + eventInfo.StartTime,
                Font = Font.SystemFontOfSize(14, FontAttributes.None)
            };

            Image image = new Image
            {
                Source = ImageSource.FromFile("Icon.png"),
                Aspect = Aspect.AspectFit,
                HeightRequest = 500,
                WidthRequest= 500,
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
