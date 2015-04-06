﻿using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using BeginMobile.Utils;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Groups : ContentPage
    {
        private readonly ListView _listViewGroup;
        private readonly ProfileInformationGroups _groupInformation;
        private readonly Label _labelNoGroupsMessage;
        private readonly List<Group> _defaultList = new List<Group>();

        private readonly Picker _sectionsPicker;
        private readonly SearchView _searchView;
        private readonly List<string> _sectionsList = new List<String> { "Members", "Activities", "All Sections" };

        private readonly LoginUser _currentUser;
        public Groups()
        {
            Title = "Groups";

            _searchView = new SearchView("All Categories");
            _searchView.SetPlaceholder("Search by group name");
            _sectionsPicker = new Picker
                             {
                                 Title = "Sections",
                                 VerticalOptions = LayoutOptions.CenterAndExpand
                             };

            foreach (var item in _sectionsList)
            {
                _sectionsPicker.Items.Add(item);
            }

            _searchView.Container.Children.Add(_sectionsPicker);

            _currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            _groupInformation = App.ProfileServices.GetGroups(_currentUser.User.UserName, _currentUser.AuthToken);

            _listViewGroup = new ListView
                          {
                              ItemTemplate = new DataTemplate(typeof (ProfileGroupItemCell)),
                              ItemsSource = _groupInformation.Groups,
                              HasUnevenRows = true
                          };

            _listViewGroup.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                var groupItem = (Group)e.SelectedItem;
                var groupPage = new GroupItemPage(groupItem) { BindingContext = groupItem };
                await Navigation.PushAsync(groupPage);

                // clears the 'selected' background
                ((ListView)sender).SelectedItem = null;
            };

            #region Search components

            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _searchView.Category.SelectedIndexChanged += SearchItemEventHandler;
            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            _sectionsPicker.SelectedIndexChanged += SearchItemEventHandler;

            _labelNoGroupsMessage = new Label();

            #endregion

            var mainLayout = new StackLayout
            {
                Padding = 10,
                Spacing = 2,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical
            };

            mainLayout.Children.Add(_searchView.Container);
            mainLayout.Children.Add(new ScrollView
                                    {
                                        Content = _listViewGroup
                                    });

            Content = mainLayout;
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
            string sections;

            var q = sender.GetType() == typeof (SearchBar) ? ((SearchBar) sender).Text : _searchView.SearchBar.Text;

            RetrieveLimitSelected(out limit);
            RetrieveCategorySelected(out cat);
            RetrieveSectionSelected(out sections);
            
            List<Group> groupsList =
                App.ProfileServices.GetGroupsByParams(_currentUser.AuthToken, q, cat, limit, sections);

            if (groupsList.Any())
            {
                _listViewGroup.ItemsSource = groupsList;
                _labelNoGroupsMessage.Text = string.Empty;
            }

            else
            {
                _listViewGroup.ItemsSource = _defaultList;
            }
        }
        private void RetrieveSectionSelected(out string sections)
        {
            var sectionSelectedIndex = _sectionsPicker.SelectedIndex;
            var sectionLastIndex = _sectionsPicker.Items.Count - 1;

            if (sectionSelectedIndex == -1 || sectionSelectedIndex == sectionLastIndex)
            {
                sections = null;
            }

            else
            {
                sections = _sectionsPicker.Items[sectionSelectedIndex];
            }
        }
        private void RetrieveCategorySelected(out string cat)
        {
            var catSelectedIndex = _searchView.Category.SelectedIndex;
            var catLastIndex = _searchView.Category.Items.Count - 1;

            if (catSelectedIndex == -1 || catSelectedIndex == catLastIndex)
            {
                cat = null;
            }

            else
            {
                cat = _searchView.Category.Items[catSelectedIndex];
            }
        }
        private void RetrieveLimitSelected(out string limit)
        {
            var limitSelectedIndex = _searchView.Limit.SelectedIndex;
            var limitLastIndex = _searchView.Limit.Items.Count - 1;

            if (limitSelectedIndex == -1 || limitSelectedIndex == limitLastIndex)
            {
                limit = null;
            }

            else
            {
                limit = _searchView.Limit.Items[limitSelectedIndex];
            }
        }

        #endregion
    }
}