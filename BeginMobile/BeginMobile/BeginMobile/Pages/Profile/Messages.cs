using BeginMobile.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Messages: ContentPage
    {
        public Messages()
        {
            Title = "Messages";
            //Icon = "";

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationMessages profileShop = App.ProfileServices.GetMessagesInfo(currentUser.User.UserName, currentUser.AuthToken);

            /*_lViewShops = new ListView()
            {
                RowHeight = 40,
            };
            _lViewShops.ItemTemplate = new DataTemplate(typeof(ProfileShopItemCell));
            _lViewShops.ItemsSource = profileShop.Shop;

            _lViewShops.HasUnevenRows = true;

            _lViewShops.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                var groupItem = (ProfileShop)e.SelectedItem;
                var groupPage = new ShopItemPage();
                groupPage.BindingContext = groupItem;
                await Navigation.PushAsync(groupPage);

                // clears the 'selected' background
                ((ListView)sender).SelectedItem = null;
            };

            _sLayoutMain = new RelativeLayout();
            _sLayoutMain.Children.Add(_lViewShops,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            Content = new ScrollView() { Content = _sLayoutMain };*/

        }
    }
}
