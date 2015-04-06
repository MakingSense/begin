using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ProfileMe : ContentPage
    {
        private const string UserDefault = "userdefault3.png";
        public ProfileMe(User user)
        {
            Title = "Profile";

            //Toolbar menu item
            var toolBarItemMyActivity = new ToolbarItem
                                 {
                                     Icon = "",
                                     Text = "MyAct",
                                     Order = ToolbarItemOrder.Primary,
                                     Command = new Command(() => Navigation.PushAsync(new MyActivity()))
                                 };

            var toolBarItemInformation = new ToolbarItem
                                  {
                                      Icon = "",
                                      Text = "Info",
                                      Order = ToolbarItemOrder.Primary,
                                      Command = new Command(() => Navigation.PushAsync(new Information()))
                                  };

            var toolBarItemMessages = new ToolbarItem
                               {
                                   Icon = "",
                                   Text = "Messages",
                                   Order = ToolbarItemOrder.Secondary,
                                   Command = new Command(() => Navigation.PushAsync(new Messages()))
                               };

            var toolBarItemContacts = new ToolbarItem
                               {
                                   Icon = "",
                                   Text = "Contacts",
                                   Order = ToolbarItemOrder.Secondary,
                                   Command = new Command(() => Navigation.PushAsync(new Contacts()))
                               };

            var toolBarItemGroups = new ToolbarItem
                             {
                                 Icon = "",
                                 Text = "Groups",
                                 Order = ToolbarItemOrder.Secondary,
                                 Command = new Command(() => Navigation.PushAsync(new Groups()))
                             };

            var toolBarItemShop = new ToolbarItem
                           {
                               Icon = "",
                               Text = "Shop",
                               Order = ToolbarItemOrder.Primary,
                               Command = new Command(() => Navigation.PushAsync(new Shop()))
                           };

            var toolBarItemEvents = new ToolbarItem
                             {
                                 Icon = "",
                                 Text = "Events",
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
                BorderColor = Color.White,
                BorderThickness = 3,
                HeightRequest = 100,
                WidthRequest = 100,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                Source = UserDefault
            };

            var labelTitle = new Label
                           {
                               Text = "Profile",
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