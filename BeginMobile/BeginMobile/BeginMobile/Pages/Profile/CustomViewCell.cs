using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Abstractions;
namespace BeginMobile.Pages.Profile
{
    public class CustomViewCell : ViewCell
    {
        public CustomViewCell()
        {
            var icon = new CircleImage
            {
                HeightRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),
                WidthRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                BorderThickness = Device.OnPlatform<int>(iOS: 2, Android: 3, WinPhone: 3),
            };

            icon.SetBinding(Image.SourceProperty, new Binding("Icon"));

            var optionLayout = CreateOptionLayout();
            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                               {
                                   icon,
                                   optionLayout
                               }
            };
        }

        public static Grid CreateOptionLayout()
        {
            var optionText = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemTextStyle                
            };
            var optionDetail = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle
            };

            optionText.SetBinding(Label.TextProperty, "NameSurname");
            optionDetail.SetBinding(Label.TextProperty, "References");

            var gridDetails = new Grid()
            {
                Padding = App.Styles.ListDetailThickness,
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
            gridDetails.Children.Add(optionText, 0, 0);
            gridDetails.Children.Add(optionDetail, 0,1);

            return gridDetails;
        }
    }

    public class Contact
    {
        public string Icon { get; set; }
        public string NameSurname { get; set; }
        public string FirstName
        {
            get
            {
                return NameSurname.Split(' ')[0];
            }            
        }
        public string References { get; set; }
    }
}
