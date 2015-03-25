﻿using BeginMobile.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class MessageListPage:TabContent
    {
        private ListView _lViewMessages;
        private RelativeLayout _sLayoutMain;

        public Label CounterText;

        public MessageListPage(string title, string iconImg): base(title, iconImg)
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationMessages profileMessage = App.ProfileServices.GetMessagesInfo(currentUser.User.UserName, currentUser.AuthToken);

            CounterText = new Label()
            {
                Text = profileMessage.GroupingMessage.CountByGroup.ToString()
            };

            _lViewMessages = new ListView()
            {
                RowHeight = Device.OnPlatform<int>(iOS: 60, Android: 40, WinPhone: 40)
            };

            _lViewMessages.ItemTemplate = new DataTemplate(typeof(ProfileMessagesItem));
            _lViewMessages.ItemsSource = profileMessage.GroupingMessage.MessagesGroup;

            _lViewMessages.GroupDisplayBinding = new Binding("Key");
            _lViewMessages.IsGroupingEnabled = true;

            _lViewMessages.HasUnevenRows = true;
            _lViewMessages.GroupHeaderTemplate = new DataTemplate(typeof(ProfileMessageHeader));

            _lViewMessages.ItemSelected += async (sender, e) =>
            {
                /*if (e.SelectedItem == null)
                {
                    return;
                }

                var groupItem = (ProfileShop)e.SelectedItem;
                var groupPage = new ShopItemPage();
                groupPage.BindingContext = groupItem;
                await Navigation.PushAsync(groupPage);*/

                ((ListView)sender).SelectedItem = null;
            };

            _sLayoutMain = new RelativeLayout();
            _sLayoutMain.Children.Add(_lViewMessages,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            Content = new ScrollView() { Content = _sLayoutMain };
        }
    }
}
