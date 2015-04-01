using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;


namespace BeginMobile.Pages.Profile
{
    public class EventDetailInformation:ContentPage
    {
        private ProfileEvent _profileEvent;
        private const string AllCategories = "No Categories";
        private const string TextCategories = "Categories";
        private const string TextDateAndHour = "Date/Hour";
        private const string TextContent= "Event Description";

        public EventDetailInformation(ProfileEvent eventInfo)
        {
            this.SetBinding(TitleProperty, "Name", stringFormat: "Event - {0}");

            //var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            //_profileEvent = App.ProfileServices.GetEvent(currentUser.AuthToken, eventInfo.EventId);
            _profileEvent = eventInfo;

            if (_profileEvent == null)
            {
                return;
            }

            var profileEventViewModel = GetEvenViewModel(_profileEvent); 

            Label ownerName = new Label
            {
                Text = "Public by " + eventInfo.Owner.NameSurname,
                Style = App.Styles.SubtitleStyle,
                HorizontalOptions = LayoutOptions.CenterAndExpand
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


            var lblTextDateAndHour = new Label
            {
                YAlign = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                Style = App.Styles.ListItemTextStyle,
                Text = TextDateAndHour
            }; 

            var lblDates = new Label
            {
                YAlign = TextAlignment.End,
                Style = App.Styles.ListItemDetailTextStyle,
                Text= profileEventViewModel.TextDates
            };

            var lblTimes = new Label
            {
                YAlign = TextAlignment.End,
                Style = App.Styles.ListItemDetailTextStyle,
                Text= profileEventViewModel.TextTimes
            };

            var lblTextCategories = new Label
            {
                YAlign = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                Style = App.Styles.ListItemTextStyle,
                Text = TextCategories
            }; 

            var lblCategories = new Label
            {
                YAlign = TextAlignment.End,
                Style = App.Styles.ListItemDetailTextStyle,
                Text = AllCategories
            };

            var lblTextContent = new Label()
                {
                    YAlign = TextAlignment.End,
                    Style = App.Styles.LabelLargeTextTitle,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start,
                    Text = TextContent
                };

            var lblEventDescription= new Label()
                {
                    YAlign = TextAlignment.End,
                    Style = App.Styles.ListItemDetailTextStyle,
                    Text = profileEventViewModel.EventDescription
                };


            var gridMainContent = new Grid()
            {
                Padding = 5,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                RowDefinitions =
                {
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = GridLength.Auto},
                    new RowDefinition() {Height = GridLength.Auto},
                }
            };


            var stackLayoutSectionCategories = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    lblTextCategories,
                    lblCategories
                }
            };

            gridMainContent.Children.Add(ownerName, 0, 0);
            gridMainContent.Children.Add(image, 0, 1);
            gridMainContent.Children.Add(lblTextDateAndHour, 0, 2);
            gridMainContent.Children.Add(lblDates, 0, 3);
            gridMainContent.Children.Add(lblTimes, 0, 4);
            gridMainContent.Children.Add(stackLayoutSectionCategories, 0, 5);
            gridMainContent.Children.Add(lblTextContent, 0, 6);
            gridMainContent.Children.Add(lblEventDescription, 0, 7);

            Content = new ScrollView
            {
                Content = gridMainContent
            };
        }

        public EventViewModel GetEvenViewModel(ProfileEvent profileEvent)
        {
            {
                var modelView = new EventViewModel()
                {
                    UserFullName = profileEvent.Owner.NameSurname,
                    TextDates = profileEvent.StartDate + " to " + profileEvent.EndDate,
                    TextTimes = profileEvent.StartTime + " - " + profileEvent.EndTime,
                    Name = profileEvent.Name,
                    Categories = AllCategories,
                    EventDescription = profileEvent.Content,
                };

                return modelView;

            }
        }
    }
}
