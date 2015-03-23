using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class ProfileMessagesItem : ViewCell
    {
        private const string GroupImage = "userdefault3.png";
        public ProfileMessagesItem()
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
            //shopImage.SetBinding(CircleImage.SourceProperty, "Thumbnail");

            var lblGrTitle = new Label()
            {
                YAlign = TextAlignment.Center,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start
            };
            lblGrTitle.SetBinding(Label.TextProperty, "Title");

            var lblCreate = new Label()
            {
                YAlign = TextAlignment.Center,
                FontSize = 12,
                HorizontalOptions = LayoutOptions.End
            };
            lblCreate.SetBinding(Label.TextProperty, "CreateDate", stringFormat:"Date: {0}");

            var lblContent = new Label()
            {
                YAlign = TextAlignment.Center,
                FontSize = 12,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            lblContent.SetBinding(Label.TextProperty, "Content");

            var gridDetails = new Grid()
            {
                Padding = new Thickness(10, 5, 10, 5),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
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

            gridDetails.Children.Add(lblGrTitle, 0, 0);
            gridDetails.Children.Add(lblContent, 0, 1);

            gridDetails.Children.Add(lblCreate, 1, 0);
            //gridDetails.Children.Add("", 1, 1);

            var sLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    shopImage, gridDetails
                }
            };

            View = sLayout;
        }
    }
}
