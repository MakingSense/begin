using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.Utils;
using BeginMobile.Utils.Extensions;

namespace BeginMobile.Pages.Profile
{
    public class Groups : ContentPage
    {
        private ListView _lViewGroup;
        private RelativeLayout _rLayout;
        private ProfileInformationGroups groupInformation;
        private Label noGroupsMessage;
        private List<Group> defaultList = new List<Group>();

        private Picker sectionsPicker;
        private SearchView searchView;
        private List<string> sections = new List<String> { "Members", "Activities", "All Sections" };
        public Groups()
        {
            Title = "Groups";

            searchView = new SearchView("All Categories");
            searchView.SetPlaceholder("Search by group name");
            sectionsPicker = new Picker
                             {
                                 Title = "Sections",
                                 VerticalOptions = LayoutOptions.CenterAndExpand
                             };

            foreach (var item in sections)
            {
                sectionsPicker.Items.Add(item);
            }

            searchView.Container.Children.Add(sectionsPicker);

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            groupInformation = App.ProfileServices.GetGroups(currentUser.User.UserName, currentUser.AuthToken);

            _lViewGroup = new ListView() { };

            _lViewGroup.ItemTemplate = new DataTemplate(typeof(ProfileGroupItemCell));
            _lViewGroup.ItemsSource = groupInformation.Groups;

            _lViewGroup.HasUnevenRows = true;

            _lViewGroup.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                var groupItem = (Group)e.SelectedItem;
                var groupPage = new GroupItemPage(groupItem);
                groupPage.BindingContext = groupItem;
                await Navigation.PushAsync(groupPage);

                // clears the 'selected' background
                ((ListView)sender).SelectedItem = null;
            };
           
            searchView.SearchBar.TextChanged += OnSearchBarButtonPressed;
            searchView.Category.SelectedIndexChanged += OnSelectedIndexChanged;
            noGroupsMessage = new Label();
       
            StackLayout mainLayout = new StackLayout
            {
                Padding = 10,
                Spacing = 2,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical
            };

            mainLayout.Children.Add(searchView.Container);
            mainLayout.Children.Add(new ScrollView()
                                    {
                                        Content = _lViewGroup
                                    });

            Content = mainLayout;

        }

        private void OnSearchBarButtonPressed(object sender, EventArgs args)
        {
            var groupsList = groupInformation.Groups;

            SearchBar searchBar = (SearchBar)sender;
            string searchText = searchBar.Text; // recovery the text of search bar

            if (!string.IsNullOrEmpty(searchText) || !string.IsNullOrWhiteSpace(searchText))
            {

                if (groupsList.Count == 0)
                {
                    noGroupsMessage.Text = "There is no groups";
                }

                else
                {
                    List<Group> list =
                        (from g in groupsList 
                            where g.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
                            select g).ToList<Group>();

                    if (list.Any())
                    {
                         _lViewGroup.ItemsSource = list;
                        noGroupsMessage.Text = "";
                    }

                    else
                    {
                         _lViewGroup.ItemsSource = defaultList;
                    }
                }
            }
            else
            {
                _lViewGroup.ItemsSource = groupInformation.Groups;
            }

        }
        private void OnSelectedIndexChanged(object sender, EventArgs args) { }
    }
}