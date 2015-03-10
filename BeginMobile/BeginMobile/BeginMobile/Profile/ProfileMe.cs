using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Profile
{
    public class ProfileMe: ContentPage
    {
        public ProfileMe()
        {
            Title = "Profile";

            
            //Icon = "";

            var profileImagen = new CircleImage
            {
                BorderColor = Color.White,
                BorderThickness = 3,
                HeightRequest = 100,
                WidthRequest = 100,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                Source = "icon.png"
            };

            /*var profileImagen = new Image()
            {
                Source = "icon.png",
                HeightRequest = 60,
                WidthRequest = 60,
            };*/

            var lblTitle = new Label()
            {
                Text = "Profile",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            };

            var lblName = new Label()
            {
                Text = "Juan Perez",
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
