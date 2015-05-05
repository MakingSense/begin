using System;
using System.Linq;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class SendMessage : ContentPage, IDisposable
    {
        private LoginUser _currentUser;
        private readonly Entry _entryUserName;
        private readonly Entry _entrySubject;
        private readonly Editor _editorMessageContent;

        public SendMessage()
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = "Send Message";

            var labelTextUserName = new Label
                                    {
                                        Text = AppResources.EntryUsernamePlaceholderMessage,
                                        Style = BeginApplication.Styles.SubtitleStyle
                                    };
            var labelTextSubject = new Label
                                   {
                                       Text = AppResources.EntrySubjectPlaceholder,
                                       Style = BeginApplication.Styles.SubtitleStyle
                                   };
            var labelTextMessage = new Label
                                   {
                                       Text = AppResources.EditorMessagePlaceholder,
                                       Style = BeginApplication.Styles.SubtitleStyle
                                   };
            _entryUserName = new Entry
                             {
                                 Style = BeginApplication.Styles.EntryStyle
                             };
            _entrySubject = new Entry
                            {
                                Style = BeginApplication.Styles.EntryStyle
                            };
            _editorMessageContent = new Editor
                                    {
                                        HeightRequest = 100,
                                        Style = BeginApplication.Styles.MessageContentStyle
                                    };
            var buttonSend = new Button
                             {
                                 Text = AppResources.ButtonSendMessage,
                                 Style = BeginApplication.Styles.DefaultButton
                             };
            buttonSend.Clicked += SendMessageEventHandler;

            var gridComponents = new Grid
                                 {
                                     VerticalOptions = LayoutOptions.CenterAndExpand,
                                     HorizontalOptions = LayoutOptions.StartAndExpand,
                                     RowDefinitions =
                                     {
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto}
                                     }
                                 };

            gridComponents.Children.Add(labelTextUserName, 0, 0);
            gridComponents.Children.Add(_entryUserName, 0, 1);
            gridComponents.Children.Add(labelTextSubject, 0, 2);
            gridComponents.Children.Add(_entrySubject, 0, 3);
            gridComponents.Children.Add(labelTextMessage, 0, 4);
            gridComponents.Children.Add(_editorMessageContent, 0, 5);

            Content = new StackLayout
                      {
                          VerticalOptions = LayoutOptions.StartAndExpand,
                          Padding = BeginApplication.Styles.LayoutThickness,
                          Children = {gridComponents, buttonSend}
                      };
        }

        private async void SendMessageEventHandler(object sender, EventArgs e)
        {
            _currentUser = (LoginUser) Application.Current.Properties["LoginUser"];
            var sendMessageManager =
                await BeginApplication.ProfileServices.SendMessage(_currentUser.AuthToken, _entryUserName.Text,
                    _entrySubject.Text, _editorMessageContent.Text);

            if (sendMessageManager != null)
            {
                var errorMessage = sendMessageManager.Errors.Aggregate("",
                    (current, serviceError) => current + (serviceError.ErrorMessage + "\n"));
                await DisplayAlert("Validation Error", errorMessage, "Ok");
            }
            else
            {
                await DisplayAlert("Successfull!", "Your message has successfully sent!", "ok");
                ToEmptyFields();
                await Navigation.PopAsync();
            }
        }

        private void ToEmptyFields()
        {
            _entryUserName.Text = "";
            _entrySubject.Text = "";
            _editorMessageContent.Text = "";
        }

        public void Dispose()
        {
        }
    }
}