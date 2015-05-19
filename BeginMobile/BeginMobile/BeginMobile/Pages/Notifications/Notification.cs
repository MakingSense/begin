using System;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.Profile;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Logging;
using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class Notification : TabContent
    {
        public readonly Label LabelCounter;
        private readonly LoginUser _currentUser;
        public string MasterTitle { get; set; }
        private readonly UnreadNotification _unreadNotifications;
        private readonly ReadNotifications _readNotifications;
        private readonly TabViewExposure _tabViewExposure;
        private readonly ILoggingService _log = Logger.Current;

        public Notification(string title, string iconImg)
            : base(title, iconImg)
        {
            Title = title;
            MasterTitle = AppResources.AppHomeChildNotifications;
            LabelCounter = new Label();
            _currentUser = (LoginUser) Application.Current.Properties["LoginUser"];
            _tabViewExposure = new TabViewExposure();
            _readNotifications = new ReadNotifications();
            _unreadNotifications = new UnreadNotification();
            Init();
        }

        private async void Init()
        {
            var profileNotification =
                await
                    BeginApplication.ProfileServices.GetProfileNotification(_currentUser.AuthToken);
            if (profileNotification != null)
            {
                LabelCounter.Text = profileNotification.UnreadCount;
            }
        }

        public void InitilizeNotification()
        {
            try
            {
                _tabViewExposure.PageOne = _unreadNotifications;
                _tabViewExposure.PageTwo = _readNotifications;
                _tabViewExposure.TabOneName = TabsNames.Tab1Notifications;
                _tabViewExposure.TabTwoName = TabsNames.Tab2Notifications;
                _tabViewExposure.SetInitialProperties(TabsNames.Tab1 = TabsNames.Tab1Notifications);
                Content = _tabViewExposure.Content;
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message);
            }
        }
    }
}