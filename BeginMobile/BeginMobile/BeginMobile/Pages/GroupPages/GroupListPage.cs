using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    public class GroupListPage : TabContent
    {
        private readonly ListView _lViewGroup;
        private readonly RelativeLayout _rLayout;

        public GroupListPage(string title, string iconImg)
            : base(title, iconImg)
        {

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationGroups groupInformation = App.ProfileServices.GetGroups(currentUser.User.UserName, currentUser.AuthToken);

            _lViewGroup = new ListView
                          {
                              ItemTemplate = new DataTemplate(typeof (ProfileGroupItemCell)),
                              ItemsSource = groupInformation.Groups,
                              HasUnevenRows = true
                          };

            _lViewGroup.ItemSelected += async (sender, e) =>
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

            _rLayout = new RelativeLayout();
            _rLayout.Children.Add(_lViewGroup, Constraint.Constant(0), Constraint.Constant(0), Constraint.RelativeToParent(parent => { return parent.Width; }), Constraint.RelativeToParent(parent => { return parent.Height; }));

            Content = new ScrollView { Content = _rLayout };
        }
    }
}
