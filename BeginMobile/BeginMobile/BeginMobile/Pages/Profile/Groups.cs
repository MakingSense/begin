using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BeginMobile.Interfaces;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Groups : BaseContentPage
    {
        private ListView _listViewGroup;
        private ProfileInformationGroups _groupInformation;
        private static Grid _gridMain;
        private ObservableCollection<Group> _groups;
        private readonly List<Group> _defaultGroups = new List<Group>();
        private ImageSource _imageSourceGroupByDefault;
        private readonly LoginUser currentUser;
        
        public Groups()
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = "Groups";
            currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            LoadDeafultImage();

            //Content = _gridMain;

            Init(currentUser);
        }

        private async Task Init(LoginUser currentUser)
        {
            _groupInformation = await BeginApplication.ProfileServices.GetGroups(currentUser.AuthToken, currentUser.User.UserName);

            _groups = _groupInformation != null ? _groupInformation.Groups : 
                new ObservableCollection<Group>(_defaultGroups);

            _listViewGroup = new ListView
            {
                ItemTemplate = new DataTemplate(() => new ProfileGroupItemCell(_imageSourceGroupByDefault)),
                ItemsSource = _groups,
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


            _gridMain = new Grid()
            {
                Padding = BeginApplication.Styles.ThicknessMainLayout,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition{Height = GridLength.Auto}
                }
            };
            //var relativeLayout = new RelativeLayout
            //{
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //};
            //relativeLayout.Children.Add(_listViewGroup,
            //    Constraint.Constant(0),
            //    Constraint.Constant(0),
            //    Constraint.RelativeToParent(parent => { return parent.Width; }),
            //    Constraint.RelativeToParent(parent => { return parent.Height; }));

           // _stackLayoutMain.Children.Add(relativeLayout);

            _gridMain.Children.Add(_listViewGroup, 0, 0);

           // Content = new ScrollView { Content = _stackLayoutMain };
            Content = _gridMain;
        }

        public List<Group> GetListGroups()
        {
            return _defaultGroups;
        }

        public async void LoadDeafultImage()
        {
            #if __ANDROID__
                        //var imageArray = await ImageResizer.GetResizeImage(BeginApplication.Styles.DefaultGroupIcon);
                        //this._imageSourceGroupByDefault = ImageSource.FromStream(() => new MemoryStream(imageArray));
            this._imageSourceGroupByDefault = BeginApplication.Styles.DefaultGroupIcon;
            #endif
            #if __IOS__
                                    this._imageSourceGroupByDefault = BeginApplication.Styles.DefaultGroupIcon;
            #endif
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //this.Content = null;
            _groups = null;
        }
    }
}