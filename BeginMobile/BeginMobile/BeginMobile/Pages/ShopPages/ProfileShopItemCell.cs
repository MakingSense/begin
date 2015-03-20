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
                HorizontalOptions = LayoutOptions.Start,
                Source = GroupImage
            };

            //Right section
            var lblGrTitle = new Label()
            {
                YAlign = TextAlignment.Center,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
            };
            lblGrTitle.SetBinding(Label.TextProperty, "Name");


            var lblCreateDateTitle = new Label()
            {
                YAlign = TextAlignment.Center,
                Text = "Date:",
                FontSize = 10,
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

            var sLayoutDate = new StackLayout()
            {
                Children =
                {
                    lblCreateDateTitle, lblCreate
                }
            };

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
                YAlign = TextAlignment.Center,
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center,
                
            };
            lblPrice.SetBinding(Label.TextProperty, "Price");

            var stackLayoutRight = new StackLayout()
            {
                Children =
                {
                    lblPriceTitle, lblPrice
                }
            };

            var stackLayoutFoot = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    sLayoutDate, stackLayoutRight
                }
            };

            var stackLayCenter = new StackLayout()
            {
                Children =
                {
                    lblGrTitle,
                    stackLayoutFoot
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
