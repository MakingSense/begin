using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.ShopPages
{
    public class ProfileShopItemCell: ViewCell
    {
        private const string GroupImage = "Icon.png";
        

        public ProfileShopItemCell()
        {
            var shopImage = new CircleImage()
            {
                BorderColor = Color.White,
                BorderThickness = 3,
                HeightRequest = 100,
                WidthRequest = 100,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start
            };
            shopImage.SetBinding(CircleImage.SourceProperty, "Thumbnail");
            
            var lblGrTitle = new Label()
            {
                YAlign = TextAlignment.Center,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
            };
            lblGrTitle.SetBinding(Label.TextProperty, "Name");

            //Left section
            var lblCreateDateTitle = new Label()
            {
                YAlign = TextAlignment.Center,
                Text = "Date:",
                FontSize = 12,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start
            };

            var lblCreate = new Label()
            {
                YAlign = TextAlignment.Center,
                FontSize = 12,
                HorizontalOptions = LayoutOptions.Center
            };
            lblCreate.SetBinding(Label.TextProperty, "CreationDate");

            //Right section
            var lblPriceTitle = new Label()
            {
                YAlign = TextAlignment.Center,
                Text = "Price:",
                FontSize = 12,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start
            };

            var lblPrice = new Label()
            {
                FontSize = 18,
                YAlign = TextAlignment.Center,
                XAlign = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.End
            };
            lblPrice.SetBinding(Label.TextProperty, "Price");

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

            gridDetails.Children.Add(lblCreateDateTitle, 0, 0);
            gridDetails.Children.Add(lblCreate, 0, 1);

            gridDetails.Children.Add(lblPriceTitle, 1, 0);
            gridDetails.Children.Add(lblPrice, 1, 1);

            var stackLayCenter = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    lblGrTitle,
                    gridDetails
                }
            };

            var sLayItem = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    shopImage, stackLayCenter
                }
            };

            View = sLayItem;
        }
    }
}
