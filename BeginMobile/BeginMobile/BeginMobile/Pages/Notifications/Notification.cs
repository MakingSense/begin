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
        private RelativeLayout rLayout;

        public Notification(string title, string iconImg)
            : base(title, iconImg)
        {
            Title = title;

            listNotifications = new List<NotificationViewModel> { 
                    new NotificationViewModel{
                NotificationDescription = "Admin sent to you a new message",
                IntervalDate = " 5 days, 5Hours ago",
                actionButton = new Button{Text = "READ"},
                deleteButton = new Button{Text = "DELETE"}
                    
                    },

                   new NotificationViewModel{
                NotificationDescription = "Admin mentioned you",
                IntervalDate = " 2weeks, 3days ago",
                actionButton = new Button{Text = "UNREAD"},
                deleteButton = new Button{Text = "DELETE"}
                   
                   },

                   new NotificationViewModel{
                NotificationDescription = "You have a friendship request from Soledad Pietro",
                IntervalDate = " 2weeks, 3days ago",
                actionButton = new Button{Text = "UNREAD"},
                deleteButton = new Button{Text = "DELETE"}
                   
                   
                   },

                   new NotificationViewModel{
                NotificationDescription = "Admin mentioned you",
                IntervalDate = " 5 days, 7Hours ago",
                actionButton = new Button{Text = "READ"},
                deleteButton = new Button{Text = "DELETE"}
                   
                   },

            };

            listViewNotifications = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(TemplateListViewNotification))
            };


            rLayout = new RelativeLayout();
            rLayout.Children.Add(listViewNotifications,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            Content = new ScrollView() { Content = rLayout };

            //Content = new ScrollView
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Start,
            //        Orientation = StackOrientation.Vertical,
            //        Children = { listViewNotifications }
            //    }
            //};

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
