using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Services.DTO;

namespace BeginMobile.Pages.GroupPages
{
    public class GroupListPage : TabContent
    {
        private ListView lViewGroup;
        private RelativeLayout rLayout;

        public GroupListPage(string title, string iconImg): base(title, iconImg)
        {
            lViewGroup = new ListView { StyleId = "GroupList"};

            lViewGroup.ItemTemplate = new DataTemplate(typeof(GroupItemCell));

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

            rLayout = new RelativeLayout();
            rLayout.Children.Add(lViewGroup,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            Content = new ScrollView() {Content = rLayout};
            //Content = rLayout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var listGroup = Group.ListGroup;
            lViewGroup.ItemsSource = listGroup;
        }
    }
}
