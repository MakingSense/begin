using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.Wall
{
    public class WallItemCell: ViewCell
    {
        private const string UserImage = "userdefault.png";
        public WallItemCell()
        {
            //Do something

            var obj = (BeginMobile.Services.DTO.Wall)this.BindingContext;

            

            var lblName = new Label()
            {
                YAlign = TextAlignment.Center,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
            };
            lblName.SetBinding(Label.TextProperty, "Name");

            var lblExtraText = new Label()
            {
                YAlign = TextAlignment.Center,
                FontSize = 12,
            };
            lblExtraText.SetBinding(Label.TextProperty, "ExtraText");

            var lblReason = new Label()
            {
                YAlign = TextAlignment.Center,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
            };
            lblReason.SetBinding(Label.TextProperty, "Reason");

            var lblDescription = new Label()
            {
                YAlign = TextAlignment.Center,
                FontSize = 12,
            };
            lblDescription.SetBinding(Label.TextProperty, "Description");

            var lblDate = new Label()
            {
                YAlign = TextAlignment.Center,
                FontSize = 12,
            };
            lblDate.SetBinding(Label.TextProperty, "Date");


            var stackTopTitle = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    lblName, lblExtraText, lblReason, lblDescription
                }
            };


            var gridDetails = new Grid()
            {
                Padding = new Thickness(10, 5, 10, 5),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto },
                }
            };

            gridDetails.Children.Add(stackTopTitle, 0, 0);
            gridDetails.Children.Add(lblDate, 0, 1);

            var shopImage = new CircleImage()
            {
                BorderColor = Color.White,
                BorderThickness = 3,
                HeightRequest = 100,
                WidthRequest = 100,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Source = UserImage
            };

            var starImage = new Image()
            {
                VerticalOptions = LayoutOptions.Start
            };

            var layoutItem = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    shopImage, gridDetails
                }
            };

            View = layoutItem;
        }
    }
}
