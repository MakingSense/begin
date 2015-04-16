using BeginMobile.Services.DTO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    public class GroupListPage : TabContent
    {
        private ListView _listViewGroup;
        private StackLayout _stackLayoutMain;
        private ProfileInformationGroups _groupInformation;

        public GroupListPage(string title, string iconImg)
            : base(title, iconImg)
        {
            var currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            _stackLayoutMain = new StackLayout()
            {
                Spacing = 2
            };

            _stackLayoutMain.Children.Add(CreateStackLayoutWithLoadingIndicator());
            Content = _stackLayoutMain;

            Init(currentUser);
        }

        private async Task Init(LoginUser currentUser)
        {
            _groupInformation = await BeginApplication.ProfileServices.GetGroups(currentUser.User.UserName,
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

            var relativeLayout = new RelativeLayout();
            relativeLayout.Children.Add(_listViewGroup, 
                Constraint.Constant(0), 
                Constraint.Constant(0), 
                Constraint.RelativeToParent(parent => { return parent.Width; }), 
                Constraint.RelativeToParent(parent => { return parent.Height; }));

            _stackLayoutMain.Children.Add(relativeLayout);

            //Content = new ScrollView { Content = _stackLayoutMain };
            Content = _stackLayoutMain;
        }
    }
}
