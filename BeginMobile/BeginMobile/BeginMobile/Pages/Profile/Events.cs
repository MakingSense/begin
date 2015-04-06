﻿using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.Services.DTO;
using BeginMobile.Utils;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Events : ContentPage
    {
        private readonly Label _labelNoEventsMessage;
        private readonly List<EventInfoObject> _listEvents;
        private readonly ListView _eventsListView;
        private readonly List<EventInfoObject> _defaultList = new List<EventInfoObject>();
        private readonly SearchView _searchView;
        private readonly LoginUser _currentUser;

        public Events()
        {
            Title = "Events";

            _searchView = new SearchView("All Categories");
            _searchView.SetPlaceholder("Search by event name");

            #region Call api
            _currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationEvents profileInformationEvents = App.ProfileServices.GetEvents(_currentUser.User.UserName, _currentUser.AuthToken);
            #endregion

            #region Search components

            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _searchView.Category.SelectedIndexChanged += SearchItemEventHandler;
            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            _labelNoEventsMessage = new Label();

            #endregion

            #region Subtitles layout

            var gridEventHeaderTitle = new Grid
                                       {
                                           HorizontalOptions = LayoutOptions.FillAndExpand
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

            #region List components
            _listEvents = new List<EventInfoObject>();

            foreach (var eventInfo in profileInformationEvents.Events)
            {
                _listEvents.Add(new EventInfoObject
                               {
                                   EventName = eventInfo.Name,
                                   EventIntervalDate =
                                       String.Format("{0} - {1}", eventInfo.StartDate, eventInfo.EndDate),
                                   EventTime = String.Format("{0} - {1}", eventInfo.StartTime, eventInfo.EndTime),
                                   EventInfo = eventInfo
                               });
            }

            var eventTemplate = new DataTemplate(typeof(TemplateListViewEvents));

            _eventsListView = new ListView
            {
                ItemsSource = _listEvents,
                ItemTemplate = eventTemplate
            };

            _eventsListView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                var item = (EventInfoObject)e.SelectedItem;

                var itemPageProfile = new EventDetailInformation(item.EventInfo) { BindingContext = item.EventInfo };

                await Navigation.PushAsync(itemPageProfile);
                ((ListView)sender).SelectedItem = null;

            };
            #endregion

            #region Main layout
            var scrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Spacing = 2,
                    VerticalOptions = LayoutOptions.Start,
                    Children =
                    {
                        _eventsListView
                    }
                }
            };

            Content = new StackLayout
                      {
                          VerticalOptions = LayoutOptions.Start,
                          Padding = 20,
                          Children =
                          {
                              _searchView.Container,
                              gridEventHeaderTitle,
                              scrollView
                          }
                      };

            #endregion
        }

        #region Events

        /// <summary>
        /// Common handler when an searchBar item has changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SearchItemEventHandler(object sender, EventArgs args)
        {
            string limit;
            string cat;

            var q = sender.GetType() == typeof (SearchBar) ? ((SearchBar) sender).Text : _searchView.SearchBar.Text;

            RetrieveLimitSelected(out limit);
            RetrieveCategorySelected(out cat);

            List<ProfileEvent> profileEventList =
                App.ProfileServices.GetEventsByParams(_currentUser.AuthToken, q, cat, limit);

            if (profileEventList.Any())
            {
                _eventsListView.ItemsSource = RetrieveEventInfoObjectList(profileEventList);
                _labelNoEventsMessage.Text = string.Empty;
            }

            else
            {
                _eventsListView.ItemsSource = _defaultList;
            }
        }

        #endregion

        #region Private methods
        private void RetrieveCategorySelected(out string cat)
        {
            var catSelectedIndex = _searchView.Category.SelectedIndex;
            var catLastIndex = _searchView.Category.Items.Count - 1;

            cat = catSelectedIndex == -1 || catSelectedIndex == catLastIndex
                ? null
                : _searchView.Category.Items[catSelectedIndex];
        }
        private void RetrieveLimitSelected(out string limit)
        {
            var limitSelectedIndex = _searchView.Limit.SelectedIndex;
            var limitLastIndex = _searchView.Limit.Items.Count - 1;

            limit = limitSelectedIndex == -1 || limitSelectedIndex == limitLastIndex
                ? null
                : _searchView.Limit.Items[limitSelectedIndex];
        }
        private static IEnumerable<EventInfoObject> RetrieveEventInfoObjectList(IEnumerable<ProfileEvent> profileEventList)
        {
            return profileEventList.Select(eventInfo => new EventInfoObject
                                                        {
                                                            EventName = eventInfo.Name,
                                                            EventIntervalDate =
                                                                String.Format("{0} - {1}", eventInfo.StartDate,
                                                                    eventInfo.EndDate),
                                                            EventTime =
                                                                String.Format("{0} - {1}", eventInfo.StartTime,
                                                                    eventInfo.EndTime),
                                                            EventInfo = eventInfo
                                                        });
        }

        #endregion

    }
}