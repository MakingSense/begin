using BeginMobile.Pages.ShopPages;
using BeginMobile.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Shop: ContentPage
    {
        private ListView _lViewShops;
        private StackLayout _sLayoutMain;

        public Shop()
        {
            Title = "Shop";
            //Icon = "";

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileInformationShop profileShop = App.ProfileServices.GetShopInfo(currentUser.User.UserName, currentUser.AuthToken);

            _lViewShops = new ListView()
            {
                RowHeight = 40,
            };
            _lViewShops.ItemTemplate = new DataTemplate(typeof(ProfileShopItemCell));
            _lViewShops.ItemsSource = profileShop.Shop;

            _lViewShops.HasUnevenRows = true;

            _sLayoutMain = new StackLayout();
            _sLayoutMain.Children.Add(_lViewShops);

            Content = new ScrollView() { Content = _sLayoutMain };
        }
    }
}
