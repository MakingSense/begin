using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.Wall
{
    public class WallItemCell : ViewCell
    {
        private const string UserImage = "userdefault.png";
        public WallItemCell()
        {
            //Do something

            var obj = (BeginMobile.Services.DTO.Wall)this.BindingContext;

            var lblName = new Label()
                          {
                              YAlign = TextAlignment.Center,
                              FontAttributes = FontAttributes.Bold,
                              Style = App.Styles.ListItemTextStyle
                          };

            lblName.SetBinding(Label.TextProperty, "Name");

            var lblExtraText = new Label()
                               {
                                   YAlign = TextAlignment.Center,
                                   Style = App.Styles.ListItemDetailTextStyle,
                               };

            lblExtraText.SetBinding(Label.TextProperty, "ExtraText");

            var lblReason = new Label()
                            {
                                YAlign = TextAlignment.Center,
                                FontAttributes = FontAttributes.Bold,
                                Style = App.Styles.ListItemTextStyle,
                            };

            lblReason.SetBinding(Label.TextProperty, "Reason");

            var lblDescription = new Label()
                                 {
                                     YAlign = TextAlignment.Center,
                                     Style = App.Styles.ListItemDetailTextStyle
                                 };

            lblDescription.SetBinding(Label.TextProperty, "Description");

            var lblDate = new Label()
                          {
                              YAlign = TextAlignment.Center,
                              Style = App.Styles.ListItemDetailTextStyle
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
                //BorderColor = Color.White,
                //BorderThickness = 3,
                //HeightRequest = 100,
                //WidthRequest = 100,

                BorderColor = Device.OnPlatform<Color>(iOS: Color.Black, Android: Color.White, WinPhone: Color.White),
                BorderThickness = Device.OnPlatform<int>(iOS: 2, Android: 3, WinPhone: 3),
                HeightRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),
                WidthRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),

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
