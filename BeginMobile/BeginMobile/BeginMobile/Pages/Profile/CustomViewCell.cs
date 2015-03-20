using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace BeginMobile.Pages.Profile
{
    public class CustomViewCell : ViewCell
    {
        public CustomViewCell()
        {
            var icon = new Image
            {
                HorizontalOptions = LayoutOptions.Start
            };
            icon.SetBinding(Image.SourceProperty, new Binding("Icon"));
            icon.WidthRequest = icon.HeightRequest = 40;

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

        public static StackLayout CreateOptionLayout()
        {
            var optionText = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            var optionDetail = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            optionText.SetBinding(Label.TextProperty, "NameSurname");
            optionDetail.SetBinding(Label.TextProperty, "References");

            var optionLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { optionText, optionDetail }
            };
            return optionLayout;
        }
    }

    public class Contact
    {
        public string Icon { get; set; }
        public string NameSurname { get; set; }
        public string References { get; set; }
    }
}
