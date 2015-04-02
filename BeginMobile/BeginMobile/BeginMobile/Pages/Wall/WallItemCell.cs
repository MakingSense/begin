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
            var labelName = new Label()
                          {
                              YAlign = TextAlignment.End,
                              FontAttributes = FontAttributes.Bold,
                              Style = App.Styles.ListItemTextStyle
                          };
            labelName.SetBinding(Label.TextProperty, "DisplayName");

            var labelExtraText = new Label()
                               {
                                   YAlign = TextAlignment.End,
                                   Style = App.Styles.ListItemDetailTextStyle,
                               };
            labelExtraText.SetBinding(Label.TextProperty, "ExtraText");

            var labelNameTwo = new Label()
            {
                YAlign = TextAlignment.End,
                Style = App.Styles.ListItemTextStyle,
                FontAttributes = FontAttributes.Bold,
            };
            labelNameTwo.SetBinding(Label.TextProperty, "DisplayNameTwo");

            var labelReason = new Label()
                            {
                                YAlign = TextAlignment.End,
                                Style = App.Styles.ListItemTextStyle,
                            };
            labelReason.SetBinding(Label.TextProperty, "Reason");

            var labelDescription = new Label()
                                 {
                                     YAlign = TextAlignment.End,
                                     Style = App.Styles.ListItemDetailTextStyle
                                 };

            labelDescription.SetBinding(Label.TextProperty, "Description");

            var labelDate = new Label()
                          {
                              YAlign = TextAlignment.End,
                              Style = App.Styles.ListItemDetailTextStyle
                          };

            labelDate.SetBinding(Label.TextProperty, "Date");


            var stackLayoutTopTitle = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    labelName, labelExtraText, labelNameTwo, labelReason
                }
            };

            var gridDetails = new Grid()
            {
                Padding = new Thickness(10, 5, 10, 5),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto },
                }
            };

            gridDetails.Children.Add(stackLayoutTopTitle, 0, 0);
            gridDetails.Children.Add(labelDescription, 0, 1);
            gridDetails.Children.Add(labelDate, 0, 2);

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
