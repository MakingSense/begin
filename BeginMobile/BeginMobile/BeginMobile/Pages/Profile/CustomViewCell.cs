using System;
using BeginMobile.LocalizeResources.Resources;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class CustomViewCell : ViewCell
    {
        public CustomViewCell()
        {
            var circleIconImage = new CircleImage
            {
                HeightRequest = Device.OnPlatform(50, 100, 100),
                WidthRequest = Device.OnPlatform(50, 100, 100),
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                BorderThickness = Device.OnPlatform(2, 3, 3)
            };

            circleIconImage.SetBinding(Image.SourceProperty, new Binding("Icon"));

            var optionLayout = CreateOptionLayout();

            View = new StackLayout
                   {
                       Orientation = StackOrientation.Horizontal,
                       Children =
                       {
                           circleIconImage,
                           optionLayout
                       }
                   };
        }
        private static Grid CreateOptionLayout()
        {
            var buttonAddFriend = new Button
            {
                Text = AppResources.ButtonAddFriend
            };

            buttonAddFriend.Clicked += AddFriendEventHandler;

            var labelOptionText = new Label
                             {
                                 HorizontalOptions = LayoutOptions.FillAndExpand,
                                 YAlign = TextAlignment.Center,
                                 Style = App.Styles.ListItemTextStyle
                             };

            var labelOptionDetail = new Label
                               {
                                   HorizontalOptions = LayoutOptions.FillAndExpand,
                                   YAlign = TextAlignment.Center,
                                   Style = App.Styles.ListItemDetailTextStyle
                               };


            labelOptionText.SetBinding(Label.TextProperty, "NameSurname");
            labelOptionDetail.SetBinding(Label.TextProperty, "References");

            var grid = new Grid
                              {
                                  Padding = App.Styles.ListDetailThickness,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  },
                                  ColumnDefinitions =
                                  {
                                      new ColumnDefinition {Width = GridLength.Auto},
                                      new ColumnDefinition {Width = GridLength.Auto}
                                  }
                              };

            grid.Children.Add(labelOptionText, 0, 0);
            grid.Children.Add(labelOptionDetail, 0, 1);
            grid.Children.Add(buttonAddFriend, 1, 0);

            return grid;
        }

        static void AddFriendEventHandler(object sender, EventArgs eventArgs)
        {
            var objectSender = sender;

        }

    }
}