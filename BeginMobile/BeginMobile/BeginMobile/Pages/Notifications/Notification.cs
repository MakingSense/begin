using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class Notification : TabContent
    {
        private ListView listViewNotifications;
        private List<NotificationViewModel> listNotifications;
        

        public Notification(string title, string iconImg)
            : base(title, iconImg)
        {
            Title = title;
            var notification1 = new NotificationViewModel{
                NotificationDescription = "Admin sent to you a new message",
                IntervalDate = " 5 days, 5Hours ago",
                ActionButton = new Button{Text = "READ"},
                DeleteButton = new Button{Text = "DELETE"}, 
                    
                    };
            notification1.DeleteButton.Clicked += (s, e) => {
                DisplayAlert("message","delete","ok");
                DeleteClickProcess(notification1);

            };
            notification1.ActionButton.Clicked += (s, e) => {
                var sender = (NotificationViewModel)s;
                UnreadClickProcess(sender);
            }; ;

            listNotifications = new List<NotificationViewModel> { 
                   notification1 ,

                   new NotificationViewModel{
                NotificationDescription = "Admin mentioned you",
                IntervalDate = " 2weeks, 3days ago",
                ActionButton = new Button{Text = "UNREAD"},
                DeleteButton = new Button{Text = "DELETE"}
                   
                   },

                   new NotificationViewModel{
                NotificationDescription = "You have a friendship request from Soledad Pietro",
                IntervalDate = " 2weeks, 3days ago",
                ActionButton = new Button{Text = "UNREAD"},
                DeleteButton = new Button{Text = "DELETE"}
                   
                   
                   },

                   new NotificationViewModel{
                NotificationDescription = "Admin mentioned you",
                IntervalDate = " 5 days, 7Hours ago",
                ActionButton = new Button{Text = "READ"},
                DeleteButton = new Button{Text = "DELETE"}
                   
                   },

            };

            listViewNotifications = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(TemplateListViewNotification))
            };



            var gridEventHeaderTitle = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            gridEventHeaderTitle.Children.Add(new Label
            {
                WidthRequest = 350,
                Text = "Notification",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(14, FontAttributes.Bold)
            }, 0, 1, 0, 1);

            gridEventHeaderTitle.Children.Add(new Label
            {
                Text = "Date received",
                HorizontalOptions = LayoutOptions.Start,
                Font = Font.SystemFontOfSize(14, FontAttributes.Bold)
            }, 1, 2, 0, 1);


            StackLayout mainLayout = new StackLayout
            {
                  VerticalOptions = LayoutOptions.Start,
                  Orientation = StackOrientation.Vertical
            };

            mainLayout.Children.Add(gridEventHeaderTitle);
            mainLayout.Children.Add(new ScrollView() { Content = listViewNotifications });

            Content = mainLayout;
        }


        public void DeleteClickProcess(NotificationViewModel model)
        {
            // TODO: to do
            DisplayAlert("Delete",model.NotificationDescription,"ok");

        }

        public void UnreadClickProcess(NotificationViewModel model)
        {
            DisplayAlert("Readed", model.NotificationDescription, "ok");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listViewNotifications.ItemsSource = listNotifications;
            listViewNotifications.ItemSelected += async (s, e) =>
            {
                var item = (NotificationViewModel)e.SelectedItem;
                var itemPage = new NotificationDetail(item.NotificationDescription);
                await Navigation.PushAsync(itemPage);
            };
        }
    }
}
