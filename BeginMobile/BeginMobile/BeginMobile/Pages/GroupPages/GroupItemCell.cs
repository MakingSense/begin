using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    public class GroupItemCell: ViewCell
    {
        public GroupItemCell()
        {
            //
            this.StyleId = "Cell";

            var lblGroupName = new Label
            {
                StyleId = "CellGroupName",
                YAlign = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start
            };
            lblGroupName.SetBinding(Label.TextProperty, "Name");

            var arrow = new Image
            {
                StyleId = "CellArrow",
                Source = FileImageSource.FromFile("icon"),
                HorizontalOptions = LayoutOptions.End
            };

            var layout = new StackLayout
            {
                Padding = new Thickness(20, 0, 20, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { lblGroupName, arrow }
            };

            View = layout;
        }
    }
}
