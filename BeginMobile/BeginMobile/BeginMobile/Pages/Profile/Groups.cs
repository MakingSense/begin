using BeginMobile.Interfaces;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Groups : BaseContentPage
    {
        private ListView _listViewGroup;
        private StackLayout _stackLayoutMain;
        private ProfileInformationGroups _groupInformation;
        private Grid _gridMain;
        private ObservableCollection<Group> _groups;
        private List<Group> _defaultGroups = new List<Group>();

        public Groups()
        {
            Title = "Groups";
            var currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            _gridMain = new Grid()
            {
                Padding = BeginApplication.Styles.LayoutThickness,
                VerticalOptions = LayoutOptions.FillAndExpand,
               HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition(){Height = GridLength.Auto}
                }
            };

            //Content = _gridMain;

            Init(currentUser);
        }

        private async Task Init(LoginUser currentUser)
        {
            _groupInformation = await BeginApplication.ProfileServices.GetGroups(currentUser.User.UserName,
                currentUser.AuthToken);

            _groups = _groupInformation != null ? _groupInformation.Groups : 
                new ObservableCollection<Group>(_defaultGroups);

            _listViewGroup = new ListView
            {
                ItemTemplate = new DataTemplate(() => new ProfileGroupItemCell()),
                ItemsSource = _groups,
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

            var relativeLayout = new RelativeLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            relativeLayout.Children.Add(_listViewGroup,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(parent => { return parent.Width; }),
                Constraint.RelativeToParent(parent => { return parent.Height; }));

            //_stackLayoutMain.Children.Add(relativeLayout);

            _gridMain.Children.Add(relativeLayout, 0, 0);

            //Content = new ScrollView { Content = _stackLayoutMain };
            Content = _gridMain;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //this.Content = null;
            _groups = null;
        }
    }
}