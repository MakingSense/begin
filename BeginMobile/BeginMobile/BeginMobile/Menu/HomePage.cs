using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages;
using BeginMobile.Services.DTO;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.Pages.Wall;
using BeginMobile.Pages.MessagePages;
using BeginMobile.Pages.ContactPages;

namespace BeginMobile.Menu
{
    public class HomePage : MasterDetailPage
    {
        private readonly LoginUser _loginUser;
        private Services.GlobalService _globalService;

        public HomePage(LoginUser loginUser)
        {
            Style = BeginApplication.Styles.PageStyle;
            LoadInitialServices();

            _loginUser = loginUser;
            Title = AppResources.HomePageTitle;

            Icon = Device.OS != TargetPlatform.iOS ? null : new FileImageSource {File = "Icon-57.png"};
            Master = new Menu(OnToggleRequest);
            Detail = new AppHome(); 

			MessageSubscribes ();
        }

        private async void LoadInitialServices()
        {
            _globalService = BeginApplication.GlobalService;
        }

        private void OnToggleRequest()
        {
            IsPresented = !IsPresented;
        }

		private void MessageSubscribes(){

			MessagingCenter.Subscribe<WallPage, string>(typeof(WallPage), "masterTitle", OnMasterTitle);
			MessagingCenter.Subscribe<BeginMobile.Pages.Notifications.Notification, string>(typeof(BeginMobile.Pages.Notifications.Notification), "masterTitle", OnMasterTitle);
			MessagingCenter.Subscribe<MessageListPage, string>(typeof(MessageListPage), "masterTitle", OnMasterTitle);
			MessagingCenter.Subscribe<ContactPage, string>(typeof(ContactPage), "masterTitle", OnMasterTitle);
			MessagingCenter.Subscribe<OptionsPage, string>(typeof(OptionsPage), "masterTitle", OnMasterTitle);
		}

		void OnMasterTitle (TabContent sender, string title)
		{
			Title = title;
		}
    }
}
