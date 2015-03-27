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
        private const string UserImage = "userdefault3.png";
        private const string StarImage = "ratingoff.png";
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
            lblName.SetBinding(Label.TextProperty, "DisplayName");

            var lblExtraText = new Label()
                               {
                                   YAlign = TextAlignment.Center,
                                   Style = App.Styles.ListItemDetailTextStyle,
                               };
            lblExtraText.SetBinding(Label.TextProperty, "ExtraText");

            var lblNameTwo = new Label()
            {
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemTextStyle,
                FontAttributes = FontAttributes.Bold,
            };
            lblNameTwo.SetBinding(Label.TextProperty, "DisplayNameTwo");

            var lblReason = new Label()
                            {
                                YAlign = TextAlignment.Center,
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
                    lblName, lblExtraText, lblNameTwo, lblReason, lblDescription
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
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.Start,
                Source = StarImage
            };


            var layoutItem = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    shopImage, gridDetails, starImage
                }
            };

            View = layoutItem;
        }
    }
}
