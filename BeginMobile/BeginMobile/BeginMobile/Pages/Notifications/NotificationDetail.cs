using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class NotificationDetail : ContentPage
    {
        public NotificationDetail(string notificationDescription)
        {
            Content = new StackLayout
            {
                Children = {
					new Label { Text = notificationDescription }
				}
            };
        }
    }
}
