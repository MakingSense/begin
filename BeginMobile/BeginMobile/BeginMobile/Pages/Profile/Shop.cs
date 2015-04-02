using BeginMobile.Pages.ShopPages;
using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Shop : ContentPage
    {
        private ListView _lViewShops;
        private RelativeLayout _sLayoutMain;

        public Shop()
        {
            Title = "Shop";

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationShop profileShop = App.ProfileServices.GetShopInfo(currentUser.User.UserName, currentUser.AuthToken);

            _lViewShops = new ListView
                          {
                              ItemTemplate = new DataTemplate(typeof (ProfileShopItemCell)),
                              ItemsSource = profileShop.Shop,
                              HasUnevenRows = true
                          };

            _lViewShops.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                var groupItem = (ProfileShop)e.SelectedItem;
                var groupPage = new ShopItemPage { BindingContext = groupItem };
                await Navigation.PushAsync(groupPage);

                // clears the 'selected' background
                ((ListView)sender).SelectedItem = null;
            };

            _sLayoutMain = new RelativeLayout();
            _sLayoutMain.Children.Add(_lViewShops,
                Constraint.Constant(0), Constraint.Constant(0),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.RelativeToParent(parent => parent.Height));

            Content = new ScrollView { Content = _sLayoutMain };
        }
    }
}