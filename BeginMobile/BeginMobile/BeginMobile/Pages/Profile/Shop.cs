using System.Collections.Generic;
using System.Collections.ObjectModel;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.ShopPages;
using BeginMobile.Services.DTO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Shop : ContentPage
    {
        private ListView _listViewShops;
        private RelativeLayout _relativeLayoutMain;
        private ProfileInformationShop _profileShop;
        private readonly LoginUser _currentUser;
        public Shop()
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = AppResources.LabelShopTitle;
            
            _currentUser = (LoginUser)Application.Current.Properties["LoginUser"];
            Init();
        }

        private async Task Init()
        {
            _profileShop = await BeginApplication.ProfileServices.GetShopInfo(_currentUser.AuthToken, _currentUser.User.UserName);

            _listViewShops = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(ProfileShopItemCell)),
                ItemsSource = _profileShop.Shop,
                HasUnevenRows = true
            };

            _listViewShops.ItemSelected += async (sender, e) =>
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

            _relativeLayoutMain = new RelativeLayout();
            _relativeLayoutMain.Children.Add(_listViewShops,
                Constraint.Constant(0), Constraint.Constant(0),
                Constraint.RelativeToParent(parent => parent.Width),
                Constraint.RelativeToParent(parent => parent.Height));
//#if __ANDROID__ || __IOS__
//            ToolbarItems.Add(new ToolbarItem("Filter", BeginApplication.Styles.FilterIcon, async () =>
//            {
//                _searchView.Container.IsVisible = true;
//            }));
//#endif

            Content = new ScrollView { Content = _relativeLayoutMain };
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //this.Content = null;
            _profileShop = null;
        }
    }
}