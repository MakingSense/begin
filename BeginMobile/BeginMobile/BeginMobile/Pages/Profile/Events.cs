using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.Services.DTO;
using BeginMobile.Utils;
using BeginMobile.Utils.Extensions;
using BeginMobile.Services.ManagerServices;

namespace BeginMobile.Pages.Profile
{
    public class Events: ContentPage
    {
        private Label noContactsMessage;
        private List<EventInfoObject> listEvents;
        private ListView eventsListView;
        private List<EventInfoObject> defaultList = new List<EventInfoObject>();
        private SearchView searchView;   
        public Events()
        {
            Title = "Events";
            
            searchView = new SearchView("All Categories");
            searchView.SetPlaceholder("Search by event name");

            Label header = new Label
            {
                Text = "My Events",
                Style = App.Styles.TitleStyle,
                HorizontalOptions = LayoutOptions.Center
            };

            #region call api
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationEvents profileInformationEvents = App.ProfileServices.GetEvents(currentUser.User.UserName, currentUser.AuthToken);
            #endregion

            #region search components
          
            searchView.SearchBar.TextChanged += OnSearchBarButtonPressed;
            searchView.Category.SelectedIndexChanged += OnSelectedIndexChanged;
            noContactsMessage = new Label();

            #endregion

            #region subtitles layout
            var gridEventHeaderTitle = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,                
            };

            gridEventHeaderTitle.Children.Add(new Label
                                              {
                                                  WidthRequest = 200,
                                                  HeightRequest = 80,
                                                  Text = "Event Name",
                                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                                  Style = App.Styles.SubtitleStyle
                                              }, 0, 1, 0, 1);

            gridEventHeaderTitle.Children.Add(new Label
                                              {
                                                  Text = "Date",
                                                  HeightRequest = 50,
                                                  HorizontalOptions = LayoutOptions.Start,
                                                  Style = App.Styles.SubtitleStyle
                                              }, 1, 2, 0, 1);

            #endregion 

            #region list components
            listEvents = new List<EventInfoObject>();

            foreach(var eventInfo in profileInformationEvents.Events)
            {
                listEvents.Add(new EventInfoObject
                               {
                                   EventName = eventInfo.Name,
                                   EventIntervalDate =
                                       String.Format("{0} - {1}", eventInfo.StartDate, eventInfo.EndDate),
                                   EventTime = String.Format("{0} - {1}", eventInfo.StartTime, eventInfo.EndTime),
                                   eventInfo = eventInfo,
                               });
            }

            var eventTemplate = new DataTemplate(typeof(TemplateListViewEvents));

            eventsListView = new ListView
            {
                ItemsSource = listEvents,
                ItemTemplate = eventTemplate
            };
            
            eventsListView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                var item = (EventInfoObject)e.SelectedItem;

                var itemPageProfile = new EventDetailInformation(item.eventInfo);               
                await Navigation.PushAsync(itemPageProfile);
                ((ListView)sender).SelectedItem = null;

            };
            #endregion

            #region main layout
            ScrollView scrollView  = new ScrollView{
                Content = new StackLayout{
                    Spacing = 2,
                    VerticalOptions = LayoutOptions.Start,
                    Children =
                    {
                        eventsListView
                    }
                }
            };

            Content = new StackLayout
                      {
                          VerticalOptions = LayoutOptions.Start,
                          Padding = 20,
                          Children =
                          {
                              searchView.Container,
                              gridEventHeaderTitle,
                              scrollView
                          }
                      };

            #endregion
        }


        //Method that to the search
        void OnSearchBarButtonPressed(object sender, EventArgs args)
        {
            //TODO: User custom SearchVIew or Page
            SearchBar searchBar = (SearchBar)sender;
            string searchText = searchBar.Text; // recovery the text of search bar

            if (!string.IsNullOrEmpty(searchText) || !string.IsNullOrWhiteSpace(searchText))
            {

                if (listEvents.Count == 0)
                {
                    noContactsMessage.Text = "There is no events.";
                }

                else
                {
                    List<EventInfoObject> list = (
                        from e in listEvents
                        where e.EventName.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
                        select e).ToList<EventInfoObject>();

                    if (list.Any())
                    {
                        eventsListView.ItemsSource = list;
                        noContactsMessage.Text = "";
                    }

                    else
                    {
                        eventsListView.ItemsSource = defaultList;
                    }
                }
            }
            else
            {
                eventsListView.ItemsSource = listEvents;
            }
        }

        /// <summary>
        /// When Category selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnSelectedIndexChanged(object sender, EventArgs args)
        {

        }
    }
}