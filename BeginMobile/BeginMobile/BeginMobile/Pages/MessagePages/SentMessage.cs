﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class SentMessage : ContentPage, IDisposable
    {
        private static LoginUser _currentUser;
        private static ListView _listViewMessages;

        public SentMessage()
        {
            Title = "Sent";

            _currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            CallServiceApi();
            MessagingSubscriptions();
            _listViewMessages = new ListView
                                {
                                    ItemTemplate = new DataTemplate(typeof (ProfileMessagesItem)),
                                    HasUnevenRows = true
                                };


            _listViewMessages.ItemSelected += ListViewItemSelectedEventHandler;

            var stackLayoutMessagesListView = new StackLayout
                                              {
                                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                                  Orientation = StackOrientation.Vertical,
                                                  Children = {_listViewMessages}
                                              };
            var mainStackLayout = new StackLayout
                                  {
                                      Padding = BeginApplication.Styles.LayoutThickness,
                                      Children = {stackLayoutMessagesListView},
                                  };

            Content = mainStackLayout;
        }

        /*         
         * Get the Sent Messages from SentBox Service API, parse the Message to MessageViewModel for add into list and return this list
         */

        public static async Task CallServiceApi()
        {
            var profileThreadMessagesSent =
                await BeginApplication.ProfileServices.GetProfileThreadMessagesSent(_currentUser.AuthToken);
            var threads = profileThreadMessagesSent;
            if (!threads.Threads.Any()) return;
            var threadMessages = threads.Threads;

            var listDataSentMessage = (from sentThread in threadMessages
                let message = sentThread.Messages.FirstOrDefault()
                where message != null
                select new MessageViewModel
                       {
                           Id = message.Id,
                           ThreadId = message.ThreadId,
                           DateSent = message.DateSent,
                           MessageContent = message.MessageContent,
                           SenderName = message.Sender.NameSurname,
                           Subject = message.Subject,
                           Sender = message.Sender,
                           Messages = sentThread.Messages
                       }).ToList();
            _listViewMessages.ItemsSource = listDataSentMessage;
        }

        public async void ListViewItemSelectedEventHandler(object sender, SelectedItemChangedEventArgs eventArgs)
        {
            if (eventArgs.SelectedItem == null)
            {
                return;
            }
            var item = (MessageViewModel) eventArgs.SelectedItem;
            var messageDetail = new MessageDetail(item);
            await Navigation.PushAsync(messageDetail);

            ((ListView) sender).SelectedItem = null;
        }

        private void MessagingSubscriptions()
        {
            MessagingCenter.Subscribe(this, MessageSuscriptionNames.MarkAsReadSentMessage, MarkAsReadSentMessageCallback());
            MessagingCenter.Subscribe(this, MessageSuscriptionNames.MarkAsUnreadSentMessage, MarkAsUnreadSentMessageCallback());
            MessagingCenter.Subscribe(this, MessageSuscriptionNames.RemoveSentMessage, RemoveSentMessageCallback());
        }
        private Action<ProfileMessagesItem, string> RemoveSentMessageCallback()
        {
            return async (sender, arg) =>
            {
                var threadId = arg;

                if (!string.IsNullOrEmpty(threadId))
                {
                    var messagesListView =
                        (List<MessageViewModel>)_listViewMessages.ItemsSource;

                    var toRemove = messagesListView.FirstOrDefault(threadMessage => threadMessage.ThreadId == threadId);

                    if (toRemove == null || !messagesListView.Remove(toRemove)) return;
                    MessageActions.Request(MessageOption.Remove, _currentUser.AuthToken, threadId);
                    await DisplayAlert("Successfull!", "Removed.", "Ok");
                }
            };
        }

        private Action<ProfileMessagesItem, string> MarkAsReadSentMessageCallback()
        {
            return async (sender, arg) =>
            {
                var threadId = arg;

                if (!string.IsNullOrEmpty(threadId))
                {
                    var messagesListView =
                        (List<MessageViewModel>)_listViewMessages.ItemsSource;

                    var toRemove = messagesListView.FirstOrDefault(threadMessage => threadMessage.ThreadId == threadId);

                    if (toRemove == null || !messagesListView.Remove(toRemove)) return;
                    MessageActions.Request(MessageOption.Remove, _currentUser.AuthToken, threadId);
                    await DisplayAlert("Successfull!", "Removed.", "Ok");
                }
            };
        }

        private Action<ProfileMessagesItem, string> MarkAsUnreadSentMessageCallback()
        {
            return async (sender, arg) =>
            {
                var threadId = arg;

                if (!string.IsNullOrEmpty(threadId))
                {
                    var messagesListView =
                        (List<MessageViewModel>)_listViewMessages.ItemsSource;

                    var toRemove = messagesListView.FirstOrDefault(threadMessage => threadMessage.ThreadId == threadId);

                    if (toRemove == null || !messagesListView.Remove(toRemove)) return;
                    MessageActions.Request(MessageOption.Remove, _currentUser.AuthToken, threadId);
                    await DisplayAlert("Successfull!", "Removed.", "Ok");
                }
            };
        }

        public void Dispose()
        {
        }
    }
}