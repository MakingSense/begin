using BeginMobile.LocalizeResources.Resources;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class SendMessage : ContentPage
    {
        public SendMessage()
        {
            var entryUserName = new Entry
                                {
                                    Placeholder = AppResources.EntryUsernamePlaceholderMessage
                                };
            var entrySubject = new Entry
                               {
                                   Placeholder = AppResources.EntrySubjectPlaceholder
                               };
            var entryMessageContent = new Entry
                                      {
                                          Placeholder = AppResources.EditorMessagePlaceholder,
                                          MinimumHeightRequest = 100,
                                          MinimumWidthRequest = 300,
                                      };
            var buttonSend = new Button
                             {
                                 Text = AppResources.ButtonSendMessage,
                                 Style = App.Styles.DefaultButton
                             };

            var gridComponents = new Grid
                                 {
                                     VerticalOptions = LayoutOptions.StartAndExpand,
                                     HorizontalOptions = LayoutOptions.CenterAndExpand,
                                     RowDefinitions =
                                     {
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto}
                                     },
                                     ColumnDefinitions =
                                     {
                                         new ColumnDefinition {Width = GridLength.Auto}
                                     }
                                 };
            gridComponents.Children.Add(entryUserName, 0, 0);
            gridComponents.Children.Add(entrySubject, 0, 1);
            gridComponents.Children.Add(entryMessageContent, 0, 2);
            gridComponents.Children.Add(buttonSend, 0, 3);

            Content = gridComponents;
        }
    }
}