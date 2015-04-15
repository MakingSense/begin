using BeginMobile.Services.Models;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class MessageDetail : ContentPage
    {
        public MessageDetail(MessageViewModel messageViewModel)
        {
            Title = "Message Detail";
            //TODO

            Content = new StackLayout
                      {
                          Children =
                          {
                          }
                      };
        }
    }
}