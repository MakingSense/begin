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
            Title = AppResources.LabelProfileMeTitle;
            var userAvatar = BeginApplication.Styles.DefaultProfileUserIconName;

            //if (user != null)
            //{
            //    var userAvatarUrl = user.Avatar;

            //    if (!string.IsNullOrEmpty(userAvatarUrl))
            //    {
            //        userAvatar = userAvatarUrl;
            //    }
            //}
            
            //Toolbar menu item
            var toolBarItemMyActivity = new ToolbarItem
                                 {
                                     Icon = "",
                                     Text = AppResources.ToolBarProfileMeMyAct,
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

            //Content
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
                               HorizontalOptions = LayoutOptions.Center
                           };

            var labelName = new Label
                          {
                              Text = user.DisplayName,
                              FontAttributes = FontAttributes.Bold,
                              HorizontalOptions = LayoutOptions.Center
                          };

            var labelJob = new Label
                         {
                             Text = "Business Development At Global",
                             HorizontalOptions = LayoutOptions.Center
                         };

            var labelDirection = new Label
                               {
                                   Text = "San Francisco, CA",
                                   HorizontalOptions = LayoutOptions.Center
                               };

            var boxViewProfile = new BoxView { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };
            var boxViewProfile1 = new BoxView { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };

            var profileMe = new StackLayout
                            {
                                Padding = 2,
                                Spacing = 2,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                Children =
                                {
                                    circleProfileImage,
                                    boxViewProfile,
                                    labelTitle,
                                    boxViewProfile1,
                                    labelName,
                                    labelJob,
                                    labelDirection
                                }
                            };

            Content = profileMe;
        }
    }
}