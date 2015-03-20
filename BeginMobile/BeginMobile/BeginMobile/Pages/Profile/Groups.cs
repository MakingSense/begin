using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Groups: ContentPage
    {
        private ListView _lViewGroup;
        private RelativeLayout _rLayout;
        public Groups()
        {
            Title = "Groups";
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationGroups groupInformation = App.ProfileServices.GetGroups(currentUser.User.UserName, currentUser.AuthToken);

            _lViewGroup = new ListView()
            {
                RowHeight = 40,
            };
            _lViewGroup.ItemTemplate = new DataTemplate(typeof(ProfileGroupItemCell));
            _lViewGroup.ItemsSource = groupInformation.Groups;

            _lViewGroup.HasUnevenRows = true;
            
            /*lViewGroup.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return; 
                }

                var groupItem = (Group)e.SelectedItem;
                var groupPage = new GroupItemPage();
                groupPage.BindingContext = groupItem;
                await Navigation.PushAsync(groupPage);

                // clears the 'selected' background
                ((ListView)sender).SelectedItem = null; 
            };*/

            _rLayout = new RelativeLayout();
            _rLayout.Children.Add(_lViewGroup,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            Content = new ScrollView() {Content = _rLayout};
        }

    }
}
