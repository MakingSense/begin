using System.Collections.Generic;
using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class Notification : TabContent
    {
        private readonly ListView _listViewNotifications;
        private readonly List<NotificationViewModel> _listNotifications;

        public readonly Label LabelCounter;

        public Notification(string title, string iconImg)
            : base(title, iconImg)
        {
            Title = title;
            var notificationViewModel = new NotificationViewModel
                                {
                                    NotificationDescription = "Admin sent to you a new message",
                                    IntervalDate = " 5 days, 5Hours ago",
                                    ActionButton = new Button { Text = "READ" },
                                    DeleteButton = new Button { Text = "DELETE" }

                                };

            notificationViewModel.DeleteButton.Clicked += (s, e) =>
                                                  {
                                                      DisplayAlert("message", "delete", "ok");
                                                      DeleteClickProcess(notificationViewModel);

                                                  };

            notificationViewModel.ActionButton.Clicked += (s, e) =>
                                                  {
                                                      var sender = (NotificationViewModel)s;
                                                      UnreadClickProcess(sender);
                                                  };

            _listNotifications = new List<NotificationViewModel>
                                {
                                    notificationViewModel,

                                    new NotificationViewModel
                                    {
                                        NotificationDescription = "Admin mentioned you",
                                        IntervalDate = " 2weeks, 3days ago",
                                        ActionButton = new Button {Text = "UNREAD"},
                                        DeleteButton = new Button {Text = "DELETE"}

                                    },

                                    new NotificationViewModel
                                    {
                                        NotificationDescription = "You have a friendship request from Soledad Pietro",
                                        IntervalDate = " 2weeks, 3days ago",
                                        ActionButton = new Button {Text = "UNREAD"},
                                        DeleteButton = new Button {Text = "DELETE"}


                                    },

                                    new NotificationViewModel
                                    {
                                        NotificationDescription = "Admin mentioned you",
                                        IntervalDate = " 5 days, 7Hours ago",
                                        ActionButton = new Button {Text = "READ"},
                                        DeleteButton = new Button {Text = "DELETE"}

                                    }
                                };

            LabelCounter = new Label
                          {
                              Text = _listNotifications.Count.ToString()
                          };

            _listViewNotifications = new ListView
                                    {
                                        ItemTemplate = new DataTemplate(typeof(TemplateListViewNotification))
                                    };


            var gridEventHeaderTitle = new Grid
                                       {
                                           HorizontalOptions = LayoutOptions.FillAndExpand
                                       };

            gridEventHeaderTitle.Children.Add(new Label
                                              {
                                                  WidthRequest = 350,
                                                  HeightRequest = 50,
                                                  Text = "Notification",
                                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                                  Style = App.Styles.SubtitleStyle
                                              }, 0, 1, 0, 1);

            gridEventHeaderTitle.Children.Add(new Label
                                              {
                                                  HeightRequest = 50,
                                                  Text = "Date received",
                                                  HorizontalOptions = LayoutOptions.Start,
                                                  Style = App.Styles.SubtitleStyle
                                              }, 1, 2, 0, 1);


            var mainLayout = new StackLayout
                             {
                                 Spacing = 2,
                                 Padding = App.Styles.LayoutThickness,
                                 VerticalOptions = LayoutOptions.Start,
                                 Orientation = StackOrientation.Vertical
                             };

            mainLayout.Children.Add(gridEventHeaderTitle);
            mainLayout.Children.Add(new ScrollView { Content = _listViewNotifications });

            Content = mainLayout;

            _listViewNotifications.ItemSelected += async (s, e) =>
                                                        {
                                                            if (e.SelectedItem == null)
                                                            {
                                                                return;
                                                            }

                                                            var item = (NotificationViewModel) e.SelectedItem;
                                                            var itemPage =
                                                                new NotificationDetail(item.NotificationDescription);
                                                            await Navigation.PushAsync(itemPage);

                                                            ((ListView) s).SelectedItem = null;

                                                        };
        }
        private void DeleteClickProcess(NotificationViewModel model)
        {
            // TODO: Delete Logic here
            DisplayAlert("Delete", model.NotificationDescription, "ok");
        }

        private void UnreadClickProcess(NotificationViewModel model)
        {
            //TODO: Unread logic here
            DisplayAlert("Readed", model.NotificationDescription, "ok");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _listViewNotifications.ItemsSource = _listNotifications;
        }
    }
}