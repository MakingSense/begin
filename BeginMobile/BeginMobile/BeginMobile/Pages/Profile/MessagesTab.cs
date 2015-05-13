using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content.Res;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
	public class MessagesTab : ContentPage
	{
		public MessagesTab ()
		{
		    BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor;
            var templateData = new DataTemplate(typeof(TemplateTabs));
            var listData = new List<TabViewModel>
                           {
                               new TabViewModel{TabName = "Inbox"},
                               new TabViewModel{TabName = "Sent"}
                           };
            var  listViewTabs = new ListView
                                {
                                    ItemsSource = listData,
                                    ItemTemplate = templateData,
                                    Rotation = 90,
                                    HorizontalOptions = LayoutOptions.Start,
                                    VerticalOptions = LayoutOptions.Start,
                                      HasUnevenRows = true,
                                      

                                };

            var mainGrid = new Grid
            {
                Padding = BeginApplication.Styles.LayoutThickness,
                BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                RowDefinitions = new RowDefinitionCollection
                                                {
                                                    new RowDefinition {Height = GridLength.Auto},
                                                    new RowDefinition {Height = GridLength.Auto}
                                                },
                ColumnDefinitions = new ColumnDefinitionCollection
                                                   {
                                                       new ColumnDefinition {Width = GridLength.Auto}
                                                   }
            };

            var gridResults = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = new RowDefinitionCollection
                                                    {
                                                        new RowDefinition {Height = GridLength.Auto}
                                                    },
                ColumnDefinitions = new ColumnDefinitionCollection
                                                       {
                                                           new ColumnDefinition {Width = GridLength.Auto}                                                          
                                                       }
            };
		    var listViewStackLayout = new StackLayout
		                              {
                                          HorizontalOptions = LayoutOptions.FillAndExpand,
                                          VerticalOptions = LayoutOptions.FillAndExpand,
                                          Orientation = StackOrientation.Horizontal,
                                          Children = { listViewTabs}
		                              };
            mainGrid.Children.Add(listViewStackLayout, 0, 0);
            mainGrid.Children.Add(gridResults,0,0);

		    Content = mainGrid;
		}
	}
    public class TemplateTabs : ViewCell
    {
        public TemplateTabs()
        {
            var labelTab1 = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                YAlign = TextAlignment.Center,
                Style = BeginApplication.Styles.ListItemTextStyle
            };

            labelTab1.SetBinding(Label.TextProperty, "TabName");

            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                       {                           
                           labelTab1
                       }
            };
        }
    }
    //View model
    public class TabViewModel
    {
        public string TabName { get; set; }

    }
}
