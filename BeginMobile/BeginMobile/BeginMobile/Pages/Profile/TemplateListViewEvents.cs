using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class TemplateListViewEvents : ViewCell
    {
        public TemplateListViewEvents()
        {
            var circleEventListImage = new CircleImage
                                  {
                                      Style = BeginApplication.Styles.CircleImageCommon,
                                  };

            circleEventListImage.SetBinding(Image.SourceProperty, new Binding("Icon"));
            var eventDetailsLayout = CreateDetailsLayout();

            var gridComponents = new Grid
                   {
                       Padding = BeginApplication.Styles.ThicknessInsideListView,
                       
                        RowDefinitions = new RowDefinitionCollection
                                                   {
                                                       new RowDefinition {Height = GridLength.Auto},
                                                       new RowDefinition {Height = GridLength.Auto},
                                                       new RowDefinition {Height = GridLength.Auto},
                                                  },
                       ColumnDefinitions = new ColumnDefinitionCollection
                                                   {
                                                      new ColumnDefinition{ Width = GridLength.Auto}                                                                                                            
                                                  }  
                   };
            gridComponents.Children.Add(circleEventListImage, 0, 0);
            gridComponents.Children.Add(eventDetailsLayout, 1, 0);
            View = gridComponents;
        }

        private static Grid CreateDetailsLayout()
        {
            var labelEventName = new Label
                                 {
                                     HorizontalOptions = LayoutOptions.Start,
                                     Style = BeginApplication.Styles.ListItemTextStyle
                                 };
            labelEventName.SetBinding(Label.TextProperty, "EventName");
            var labelEventOwnerUsername = new Label
                                          {
                                              HorizontalOptions = LayoutOptions.Start,
                                              Style = BeginApplication.Styles.ListItemDetailTextStyle
                                          };
            labelEventOwnerUsername.SetBinding(Label.TextProperty, "EventOwnerUserName");
            var labelEventIntervalDate = new Label
                                         {
                                             HorizontalOptions = LayoutOptions.FillAndExpand,
                                             Style = BeginApplication.Styles.ListItemDetailTextStyle
                                         };
            labelEventIntervalDate.SetBinding(Label.TextProperty, "EventIntervalDateAndTime");

            //var labelEventTime = new Label
            //                     {
            //                         HorizontalOptions = LayoutOptions.FillAndExpand,
            //                         Style = BeginApplication.Styles.ListItemDetailTextStyle
            //                     };
            //labelEventTime.SetBinding(Label.TextProperty, "EventTime");

            var gridDetails = new Grid
                              {
                                  Padding = BeginApplication.Styles.ThicknessBetweenImageAndDetails,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions = new RowDefinitionCollection
                                                   {
                                                       new RowDefinition {Height = GridLength.Auto},
                                                       new RowDefinition {Height = GridLength.Auto},
                                                       new RowDefinition {Height = GridLength.Auto},
                                                   }
                              };

            gridDetails.Children.Add(labelEventName, 0, 0);
            gridDetails.Children.Add(labelEventOwnerUsername, 0, 1);
            gridDetails.Children.Add(labelEventIntervalDate, 0, 2);
            return gridDetails;
        }
    }

    public class EventInfoObject
    {
        public string Icon { get; set; }
        public string EventName { get; set; }
        public string EventIntervalDateAndTime { get; set; }
        public string EventOwnerUserName { get; set; }
        public string EventTime { get; set; }
        public ProfileEvent EventInfo { get; set; }
    }
}