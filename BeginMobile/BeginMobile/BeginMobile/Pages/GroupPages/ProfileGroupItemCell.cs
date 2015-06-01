using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    public class ProfileGroupItemCell : ViewCell
    {
        public ProfileGroupItemCell(ImageSource imageSourceDefault)
        {
            var circleGroupImage = new CircleImage
                                   {
                                       Style = BeginApplication.Styles.CircleImageCommon,
                                       Source = imageSourceDefault
                                       //TODO:change for group avatar if this exist
                                   };

            var labelTitle = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 Style = BeginApplication.Styles.ListItemTextStyle
                             };

            labelTitle.SetBinding(Label.TextProperty, "Name");

            var labelDesc = new Label
                            {
                                YAlign = TextAlignment.Center,
                                Style = BeginApplication.Styles.ListItemDetailTextStyle
                            };

            labelDesc.SetBinding(Label.TextProperty, "Description");

            //Other section
            var labelStatus = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  HorizontalOptions = LayoutOptions.Start,
                                  Style = BeginApplication.Styles.ListItemDetailTextStyle
                              };

            labelStatus.SetBinding(Label.TextProperty, "Status");

            var labelCreate = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  HorizontalOptions = LayoutOptions.Start,
                                  Style = BeginApplication.Styles.ListItemTextStyle
                              };

            labelCreate.SetBinding(Label.TextProperty, "DateCreated");

            //var stackLayoutPie = new StackLayout
            //                     {
            //                         Spacing = 2,
            //                         Padding = BeginApplication.Styles.LayoutThickness,
            //                         Orientation = StackOrientation.Horizontal,
            //                         HorizontalOptions = LayoutOptions.FillAndExpand,
            //                         Children =
            //                         {
            //                             labelStatus,
            //                             labelCreate
            //                         }
            //                     };

            var stackLayoutCenter = new StackLayout
                                    {
                                        Padding = BeginApplication.Styles.ThicknessBetweenImageAndDetails,
                                        Orientation = StackOrientation.Vertical,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Children =
                                        {
                                            labelTitle,
                                            labelDesc,
                                            //labelStatus,
                                            labelCreate
                                        }
                                    };

            var stackLayoutItem = new StackLayout
                                  {
                                      Padding = BeginApplication.Styles.ThicknessInsideListView,
                                      Orientation = StackOrientation.Horizontal,
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      Children =
                                      {                                          
                                          circleGroupImage,
                                          stackLayoutCenter
                                      }
                                  };

            View = stackLayoutItem;
        }
    }
}