using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Services.DTO;

namespace BeginMobile.Pages.GroupPages
{
    public class GroupListPage : TabContent
    {
        private ListView _lViewGroup;
        private RelativeLayout _rLayout;

        public GroupListPage(string title, string iconImg): base(title, iconImg)
        {
            //_lViewGroup = new ListView { StyleId = "GroupList"};

            //_lViewGroup.ItemTemplate = new DataTemplate(typeof(GroupItemCell));
            
            //_lViewGroup.ItemSelected += async (sender, e) =>
            //{
            //    ((ListView)sender).SelectedItem = null; 
            //};

            //_rLayout = new RelativeLayout();
            //_rLayout.Children.Add(_lViewGroup,
            //    xConstraint: Constraint.Constant(0),
            //    yConstraint: Constraint.Constant(0),
            //    widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
            //    heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            //Content = new ScrollView() {Content = _rLayout};

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationGroups groupInformation = App.ProfileServices.GetGroups(currentUser.User.UserName, currentUser.AuthToken);

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
                var groupPage = new GroupItemPage();
                groupPage.BindingContext = groupItem;
                await Navigation.PushAsync(groupPage);

                // clears the 'selected' background
                ((ListView)sender).SelectedItem = null;
            };

            _rLayout = new RelativeLayout();
            _rLayout.Children.Add(_lViewGroup,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            Content = new ScrollView() { Content = _rLayout };
        }
    }
}
