using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using BeginMobile.Services.DTO;

namespace BeginMobile.Profile
{
    public class ProfileMe: ContentPage
    {
        private const string pUserDefault = "userdefault.png";
        public ProfileMe(User user)
        {
            Title = "Profile";
            //Icon = pUserDefault;

            //Toolbar menu item
            var toolMyActivity = new ToolbarItem()
            {
                Icon = "",
                Text = "MyAct",
                Order = ToolbarItemOrder.Primary,
                Command = new Command(() => Navigation.PushAsync(new MyActivity()))

            };

            var toolInformation = new ToolbarItem()
            {
                Icon = "",
                Text = "Info",
                Order = ToolbarItemOrder.Primary,
                Command = new Command(() => Navigation.PushAsync(new Information()))
            };

            var toolMessages = new ToolbarItem()
            {
                Icon = "",
                Text = "Messages",
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Messages()))
            };

            var toolContacts = new ToolbarItem()
            {
                Icon = "",
                Text = "Contacts",
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Contacts()))
            };

            var toolGroups = new ToolbarItem()
            {
                Icon = "",
                Text = "Groups",
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Groups()))
            };

            var toolShop = new ToolbarItem()
            {
                Icon = "",
                Text = "Shop",
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Shop()))
            };

            var toolEvents = new ToolbarItem()
            {
                Icon = "",
                Text = "Events",
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Events()))
            };

            ToolbarItems.Add(toolMyActivity);
            ToolbarItems.Add(toolInformation);
            ToolbarItems.Add(toolMessages);
            ToolbarItems.Add(toolContacts);
            ToolbarItems.Add(toolGroups);
            ToolbarItems.Add(toolShop);
            ToolbarItems.Add(toolEvents);

            //Content
            var profileImagen = new CircleImage
            {
                BorderColor = Color.White,
                BorderThickness = 3,
                HeightRequest = 100,
                WidthRequest = 100,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                Source = pUserDefault
            };

            var lblTitle = new Label()
            {
                Text = "Profile",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            var lblName = new Label()
            {
                //Text = "Juan Perez",
                Text = user.DisplayName,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            var lblJob = new Label()
            {
                Text = "Business Development At Global",
                HorizontalOptions = LayoutOptions.Center
            };

            var lblDirection = new Label()
            {
                Text = "San Francisco, CA",
                HorizontalOptions = LayoutOptions.Center
            };

            var boxViewProfile = new BoxView() {Color = Color.White, WidthRequest = 100, HeightRequest = 2};
            var boxViewProfile1 = new BoxView() { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };

            var profileMe = new StackLayout()
            {
                Padding = 2,
                Spacing = 2,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    profileImagen,
                    boxViewProfile,
                    lblTitle,
                    boxViewProfile1,
                    lblName,
                    lblJob,
                    lblDirection
                }
            };

            Content = profileMe;

        }

        
    }
}
