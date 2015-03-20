using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    public class ProfileGroupItemCell: ViewCell
    {
        private const string GroupImage = "userdefault3.png";

        public ProfileGroupItemCell()
        {
            var groupImage = new CircleImage
            {
                BorderColor = Color.White,
                BorderThickness = 3,
                HeightRequest = 80,
                WidthRequest = 80,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                Source = GroupImage
            };

            var lblGrTitle = new Label()
            {
                YAlign = TextAlignment.Center,
            };
            lblGrTitle.SetBinding(Label.TextProperty, "Name", stringFormat: "Name: {0}");

            var lblGrDesc = new Label()
            {
                YAlign = TextAlignment.Center,
            };
            lblGrDesc.SetBinding(Label.TextProperty, "Description", stringFormat: "Description: {0}");


            //Other section

            var lblStatus = new Label()
            {
                YAlign = TextAlignment.Center,
                HorizontalOptions =LayoutOptions.Start,
            };
            lblStatus.SetBinding(Label.TextProperty, "Status");

            var lblCreate = new Label()
            {
                YAlign = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.End,
            };
            lblCreate.SetBinding(Label.TextProperty, "DateCreated");

            var stackLayoutPie = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    lblStatus, lblCreate
                }
            };

            var stackLayCenter = new StackLayout()
            {
                Children =
                {
                    lblGrTitle,
                    lblGrDesc,
                    stackLayoutPie
                }
            };

            var sLayItem = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    groupImage, stackLayCenter
                }
            };

            View = sLayItem;

        }
    }
}
