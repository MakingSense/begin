using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.ShopPages
{
    public class ProfileShopItemCell: ViewCell
    {
        private string GroupImage
        {
            get
            {
                return Device.OS == TargetPlatform.iOS ? "userdefault.png" : "Icon.png";
            }
        }

        public ProfileShopItemCell()
        {
            var shopImage = new CircleImage()
                            {
                                BorderColor =
                                    Device.OnPlatform<Color>(iOS: Color.Black, Android: Color.White,
                                        WinPhone: Color.White),
                                BorderThickness = Device.OnPlatform<int>(iOS: 2, Android: 3, WinPhone: 3),
                                HeightRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),
                                WidthRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),

                                Aspect = Aspect.AspectFill,
                                HorizontalOptions = LayoutOptions.Start
                            };

            shopImage.SetBinding(CircleImage.SourceProperty, "Thumbnail");
            
            var lblGrTitle = new Label()
            {
                YAlign = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                Style = App.Styles.ListItemTextStyle,

            };
            lblGrTitle.SetBinding(Label.TextProperty, "Name");

            //Left section
            var lblCreateDateTitle = new Label()
            {
                YAlign = TextAlignment.Center,
                Text = "Date:",
                FontAttributes = FontAttributes.Bold,
                Style = App.Styles.ListItemTextStyle,
                HorizontalOptions = LayoutOptions.Start
            };

            var lblCreate = new Label()
            {
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle,
                HorizontalOptions = LayoutOptions.Center

            };

            lblCreate.SetBinding(Label.TextProperty, "CreationDate");

            //Right section
            var lblPriceTitle = new Label()
            {
                YAlign = TextAlignment.Center,
                Text = "Price:",
                FontAttributes = FontAttributes.Bold,
                Style = App.Styles.ListItemTextStyle,
                HorizontalOptions = LayoutOptions.Start
            };

            var lblPrice = new Label()
            {
                YAlign = TextAlignment.Center,
                XAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle,
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