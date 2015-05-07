using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ProfileMe : ContentPage
    {
        public ProfileMe(User user)
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = AppResources.LabelProfileMeTitle;
                       
            //Toolbar menu item
            InitToolBar();

            //Content
            InitializeComponents(user);
        }

        private void InitializeComponents(User user)
        {
            //if (user != null)
            //{
            //    var userAvatarUrl = user.Avatar;

            //    if (!string.IsNullOrEmpty(userAvatarUrl))
            //    {
            //        userAvatar = userAvatarUrl;
            //    }
            //}
            
            
            var userAvatar = BeginApplication.Styles.DefaultProfileUserIconName;
            var circleProfileImage = new CircleImage
            {
                Style = BeginApplication.Styles.CircleImageForDetails,
                Source = userAvatar
            };

            var labelTitle = new Label
            {
                Text = AppResources.LabelTitleProfile,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                Style = BeginApplication.Styles.TitleStyle
            };

            var labelName = new Label
            {
                Text = user.DisplayName,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                Style = BeginApplication.Styles.TitleStyle
            };

            var labelJob = new Label
            {
                Text = "Web Developer",//user.Profession,
                HorizontalOptions = LayoutOptions.Center,
                Style = BeginApplication.Styles.SubtitleStyle
            };

            var labelDirection = new Label
            {
                Text = "@"+user.UserName,
                HorizontalOptions = LayoutOptions.Center,
                Style = BeginApplication.Styles.TextBodyStyle
            };

            var mainLayout = new Grid
                             {
                                 HorizontalOptions = LayoutOptions.Center,
                                 VerticalOptions = LayoutOptions.Center,
                                 RowDefinitions =
                                 {
                                     new RowDefinition{ Height = GridLength.Auto},
                                     new RowDefinition{ Height = GridLength.Auto},
                                     new RowDefinition{ Height = GridLength.Auto},
                                     new RowDefinition{ Height = GridLength.Auto}
                                 },
                                 ColumnDefinitions =
                                 {
                                     new ColumnDefinition{ Width = GridLength.Auto}
                                 }

                             };
            mainLayout.Children.Add(circleProfileImage,0,0);
            mainLayout.Children.Add(labelName,0,1);
            mainLayout.Children.Add(labelJob,0,2);
            mainLayout.Children.Add(labelDirection,0,3);

            Content = new ScrollView
                      {
                          Content = mainLayout
                      };
            
        }

        private void InitToolBar()
        {
            var toolBarItemMyActivity = new ToolbarItem
                                 {
                                     Icon = "Icon.png",
                                     Text = "",//AppResources.ToolBarProfileMeMyAct,
                                     Order = ToolbarItemOrder.Primary,
                                     Command = new Command(() => Navigation.PushAsync(new MyActivity()))
                                 };

            var toolBarItemInformation = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeInfo,
                Order = ToolbarItemOrder.Primary,
                Command = new Command(() => Navigation.PushAsync(new Information()))
            };

            var toolBarItemMessages = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeMessages,
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Messages()))
            };

            var toolBarItemContacts = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeContacts,
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Contacts()))
            };

            var toolBarItemGroups = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeGroups,
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Groups()))
            };

            var toolBarItemShop = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeShop,
                Order = ToolbarItemOrder.Primary,
                Command = new Command(() => Navigation.PushAsync(new Shop()))
            };

            var toolBarItemEvents = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeEvents,
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Events()))
            };

            ToolbarItems.Add(toolBarItemMyActivity);
            ToolbarItems.Add(toolBarItemInformation);
            ToolbarItems.Add(toolBarItemMessages);
            ToolbarItems.Add(toolBarItemContacts);
            ToolbarItems.Add(toolBarItemGroups);
            ToolbarItems.Add(toolBarItemShop);
            ToolbarItems.Add(toolBarItemEvents);

        }
    }
}