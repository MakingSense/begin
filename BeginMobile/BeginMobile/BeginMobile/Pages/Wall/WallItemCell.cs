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
            var labelTitle = new Label()
                          {
                              YAlign = TextAlignment.End,
                              FontAttributes = FontAttributes.Bold,
                              Style = BeginApplication.Styles.ListItemTextStyle
                          };
            labelTitle.SetBinding(Label.TextProperty, "Title");

            var labelReason = new Label()
                            {
                                YAlign = TextAlignment.End,
                                Style = BeginApplication.Styles.ListItemTextStyle,
                            };
            labelReason.SetBinding(Label.TextProperty, "Reason");

            var labelDescription = new Label()
                                 {
                                     YAlign = TextAlignment.End,
                                     Style = BeginApplication.Styles.ListItemDetailTextStyle
                                 };

            labelDescription.SetBinding(Label.TextProperty, "Description");

            var labelDate = new Label()
                          {
                              YAlign = TextAlignment.End,
                              Style = BeginApplication.Styles.ListItemDetailTextStyle
                          };

            labelDate.SetBinding(Label.TextProperty, "Date");

            var gridDetails = new Grid()
            {
                Padding = new Thickness(10, 5, 10, 5),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                }
            };

            gridDetails.Children.Add(labelTitle, 0, 0);
            gridDetails.Children.Add(labelReason, 0, 2);
            gridDetails.Children.Add(labelDescription, 0, 3);
            gridDetails.Children.Add(labelDate, 0, 4);

            var circleImageShop = new CircleImage()
            {
                BorderColor = Device.OnPlatform<Color>(iOS: Color.Black, Android: Color.White, WinPhone: Color.White),
                BorderThickness = Device.OnPlatform<int>(iOS: 2, Android: 3, WinPhone: 3),
                HeightRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),
                WidthRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),

                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Source = UserImage
            };

            var imageStar = new Image()
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.Start,
                Source = StarImage
            };


            var layoutStackItem = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    circleImageShop, gridDetails, imageStar
                }
            };

            View = layoutStackItem;
        }
    }
}
