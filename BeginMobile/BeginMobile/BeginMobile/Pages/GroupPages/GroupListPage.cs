using BeginMobile.Services.DTO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    public class GroupListPage : TabContent
    {
        private ListView _listViewGroup;
        private RelativeLayout _relativeLayout;
        private ProfileInformationGroups _groupInformation;

        public GroupListPage(string title, string iconImg)
            : base(title, iconImg)
        {
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            Init(currentUser);
        }

        private async Task Init(LoginUser currentUser)
        {
            _groupInformation = await App.ProfileServices.GetGroups(currentUser.User.UserName,
                currentUser.AuthToken);

            _listViewGroup = new ListView
            {
                ItemTemplate = new DataTemplate(() => new ProfileGroupItemCell()),
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

            _relativeLayout = new RelativeLayout();
            _relativeLayout.Children.Add(_listViewGroup, Constraint.Constant(0), Constraint.Constant(0), Constraint.RelativeToParent(parent => { return parent.Width; }), Constraint.RelativeToParent(parent => { return parent.Height; }));

            Content = new ScrollView { Content = _relativeLayout };
        }
    }
}
