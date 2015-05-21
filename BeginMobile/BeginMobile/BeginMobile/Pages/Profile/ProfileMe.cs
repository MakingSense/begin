using System;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.ContactPages;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Logging;
using BeginMobile.Services.Utils;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ProfileMe : ContentPage
    {
        private Grid _commonGridDetailLayout;
        private Grid _commonGridMenuButtons;
        private Grid _commonGridResults;
        private Grid _commonMainGrid;
        private ScrollView _commonMainScrollView;
        private BoxView _boxViewTabSelectedInformation;
        private BoxView _boxViewTabSelectedActivities;
        private BoxView _boxViewTabSelectedMore;
        private Label _tabActivities;
        private Label _tabInformation;
        private Label _tabMore;
        private readonly Information _information;
        private readonly MyActivity _activity;
        private readonly Shop _shops;
        private readonly ContactPage _allContacts;
        private readonly Contacts _requestContacts;
        private readonly GroupListPage _allGroups;
        private readonly Groups _myGroups;
        private readonly Events _myEvents;
        private readonly TabViewExposure _tabViewExposure;
        private readonly ViewExposure _viewExposure = new ViewExposure();
        private readonly ILoggingService _log = Logger.Current;
        private ImageSource _imageSourceGroupByDefault;
        private const int RankingQuantity = 5;
        private const int RankingGridRow = 0;
        private readonly Write _newPublication;
        private string _tabSelected = "";

        public ProfileMe(LoginUser currenLoginUser)
        {
            LoadDeafultImage();
            Style = BeginApplication.Styles.PageStyle;
            Title = AppResources.LabelProfileMeTitle;

            _information = new Information();
            _allContacts = new ContactPage(String.Empty, String.Empty);
            _requestContacts = new Contacts();
            _activity = new MyActivity();
            _allGroups = new GroupListPage(String.Empty, String.Empty);
            _myGroups = new Groups();
            _shops = new Shop();
            _myEvents = new Events();
            _tabViewExposure = new TabViewExposure();
            _newPublication = new Write(currenLoginUser);

            InitProfileDetails(currenLoginUser.User);
        }

        private void InitProfileDetails(User user)
        {
            Style = BeginApplication.Styles.PageStyle;
            InitProfileControlButtons();
            var userAvatar = BeginApplication.Styles.DefaultProfileUserIconName;
            var circleProfileImage = new CircleImage
                                     {
                                         HorizontalOptions = LayoutOptions.Center,
                                         VerticalOptions = LayoutOptions.Center,
                                         Style = BeginApplication.Styles.CircleImageForDetails,
                                         Source = userAvatar
                                     };

            var labelName = new Label
                            {
                                Text = user.DisplayName,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.Center,
                                Style = BeginApplication.Styles.TitleStyle
                            };

            var labelJob = new Label
                           {
                               Text =
                                   !string.IsNullOrEmpty(BeginApplication.SelectedUserProfession)
                                       ? BeginApplication.SelectedUserProfession
                                       : "Web Designer",
                               //user.Profession,//TODO: remove the harcode data and uncomment the user.Profession
                               HorizontalOptions = LayoutOptions.Center,
                               Style = BeginApplication.Styles.SubtitleStyle
                           };

            var labelDirection = new Label
                                 {
                                     Text = "@" + user.UserName,
                                     HorizontalOptions = LayoutOptions.Center,
                                     Style = BeginApplication.Styles.SubtitleStyle
                                 };

            var gridRakingImage = new Grid
                                  {
                                      HorizontalOptions = LayoutOptions.Center,
                                      VerticalOptions = LayoutOptions.Center,
                                      RowDefinitions = new RowDefinitionCollection
                                                       {
                                                           new RowDefinition {Height = GridLength.Auto}
                                                       }
                                  };
            for (var indexer = 1; indexer <= RankingQuantity; indexer++)
            {
                var image = new Image
                            {
                                Source = ImageSource.FromFile(BeginApplication.Styles.RankingIcon),
                                WidthRequest = 30,
                                HeightRequest = 30
                            };
                gridRakingImage.Children.Add(image, indexer, RankingGridRow);
            }

            _commonGridDetailLayout = new Grid
                                      {
                                          BackgroundColor = Color.Transparent,
                                          HorizontalOptions = LayoutOptions.Center,
                                          VerticalOptions = LayoutOptions.Center,
                                          RowDefinitions =
                                          {
                                              new RowDefinition {Height = GridLength.Auto},
                                              new RowDefinition {Height = GridLength.Auto},
                                              new RowDefinition {Height = GridLength.Auto},
                                              new RowDefinition {Height = GridLength.Auto},
                                              new RowDefinition {Height = GridLength.Auto}
                                          },
                                          ColumnDefinitions =
                                          {
                                              new ColumnDefinition {Width = GridLength.Auto}
                                          }
                                      };
            _commonGridDetailLayout.Children.Add(circleProfileImage, 0, 1);
            _commonGridDetailLayout.Children.Add(labelName, 0, 2);
            _commonGridDetailLayout.Children.Add(labelJob, 0, 3);
            _commonGridDetailLayout.Children.Add(labelDirection, 0, 4);
            _commonGridDetailLayout.Children.Add(gridRakingImage, 0, 5);
            _commonMainScrollView = new ScrollView();
            _commonMainGrid = new Grid
                              {
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.Start,
                                  RowSpacing = 0,
                                  ColumnSpacing = 0,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  }
                              };

            var imageBanner = new Image
                              {
                                  Source = ImageSource.FromFile(BeginApplication.Styles.DefaultProfileMeBannerImage),
                                  Aspect = Aspect.Fill,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                              };
            _commonGridResults = new Grid
                                 {
                                     HorizontalOptions = LayoutOptions.FillAndExpand,
                                     VerticalOptions = LayoutOptions.Start,
                                     RowDefinitions =
                                     {
                                         new RowDefinition {Height = GridLength.Auto}
                                     }
                                 };
            var boxBlacknew = new BoxView
                              {
                                  BackgroundColor = Color.FromHex("000000"),
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.Start,
                                  HeightRequest = 50,
                              };

            _commonMainGrid.Children.Add(_newPublication.Container, 0, 0);
            _commonMainGrid.Children.Add(imageBanner, 0, 1);
            _commonMainGrid.Children.Add(_commonGridDetailLayout, 0, 1);
            _commonMainGrid.Children.Add(boxBlacknew, 0, 2);
            _commonMainGrid.Children.Add(_commonGridMenuButtons, 0, 3);
            _commonMainGrid.Children.Add(_commonGridResults, 0, 4);

            _commonMainScrollView.Content = _commonMainGrid;
            ToolbarItems.Add(new ToolbarItem("Publication", BeginApplication.Styles.WriteIcon, async () =>
                                                                                                     {
                                                                                                         _newPublication
                                                                                                             .Container
                                                                                                             .IsVisible
                                                                                                             = true;
                                                                                                     }));
            //_commonMainScrollView.Focused += ;

            _commonMainScrollView.Scrolled += ScrollViewScrolled;
            //var iScrollPosition = 

            Content = _commonMainScrollView;
            Init();
        }


        public Grid GridResults
        {
            get { return _commonGridResults; }
            set { _commonGridResults = value; }
        }

        public async void Init()
        {
            try
            {
                var activity = new MyActivity();
                if (activity.Content != null)
                    _commonGridResults.Children.Add(activity.Content, 0, 0);
            }
            catch (Exception e)
            {
                _log.Exception(e);
                AppContextError.Send(typeof (ProfileMe).Name, "Init", e, null, ExceptionLevel.Application);
            }
        }

        /**
         * Initialize the control buttons that simulate the tabbed options
         **/

        private void InitProfileControlButtons()
        {
            var tapGestureRecognizerTabOne = new TapGestureRecognizer
                                             {
                                                 NumberOfTapsRequired = 1
                                             };
            var tapGestureRecognizerTabTwo = new TapGestureRecognizer
                                             {
                                                 NumberOfTapsRequired = 1
                                             };
            var tapGestureRecognizerTabThree = new TapGestureRecognizer
                                               {
                                                   NumberOfTapsRequired = 1
                                               };

            tapGestureRecognizerTabOne.Tapped += EventHandlerTabActivity;
            tapGestureRecognizerTabTwo.Tapped += EventHandlerTabInformation;
            tapGestureRecognizerTabThree.Tapped += EventHadlerTabMore;

            _tabActivities = new Label
                             {
                                 Text = TabsNames.Tab1Activity,
                                 XAlign = TextAlignment.Center,
                                 FontSize = BeginApplication.Styles.TextFontSizeLarge
                             };
            _tabInformation = new Label
                              {
                                  Text = TabsNames.Tab2Information,
                                  XAlign = TextAlignment.Center,
                                  FontSize = BeginApplication.Styles.TextFontSizeLarge,
                              };
            _tabMore = new Label
                       {
                           Text = TabsNames.TabMore,
                           XAlign = TextAlignment.Center,
                           FontSize = BeginApplication.Styles.TextFontSizeLarge,
                       };

            _tabActivities.GestureRecognizers.Add(tapGestureRecognizerTabOne);
            _tabInformation.GestureRecognizers.Add(tapGestureRecognizerTabTwo);
            _tabMore.GestureRecognizers.Add(tapGestureRecognizerTabThree);

            _commonGridMenuButtons = new Grid
                                     {
                                         HorizontalOptions = LayoutOptions.FillAndExpand,
                                         VerticalOptions = LayoutOptions.Start,
                                         RowDefinitions =
                                         {
                                             new RowDefinition {Height = new GridLength(10, GridUnitType.Auto)},
                                             new RowDefinition {Height = GridLength.Auto},
                                             new RowDefinition {Height = new GridLength(0.3, GridUnitType.Star)}
                                         },
                                         //ColumnDefinitions =
                                         //{
                                         //    new ColumnDefinition {Width =  new GridLength(5, GridUnitType.Star)},
                                         //    new ColumnDefinition {Width =  new GridLength(5, GridUnitType.Star)},
                                         //    new ColumnDefinition {Width =  new GridLength(5, GridUnitType.Star)},
                                         //}
                                     };
            _boxViewTabSelectedInformation = new BoxView
                                             {
                                                 Style = BeginApplication.Styles.TabUnderLine,
                                                 IsVisible = false
                                             };
            _boxViewTabSelectedActivities = new BoxView
                                            {
                                                Style = BeginApplication.Styles.TabUnderLine,
                                                IsVisible = true,
                                            };

            _boxViewTabSelectedMore = new BoxView
                                      {
                                          Style = BeginApplication.Styles.TabUnderLine,
                                          IsVisible = false
                                      };

            _commonGridMenuButtons.Children.Add(_tabActivities, 0, 0);
            _commonGridMenuButtons.Children.Add(_boxViewTabSelectedActivities, 0, 1);
            _commonGridMenuButtons.Children.Add(_tabInformation, 1, 0);
            _commonGridMenuButtons.Children.Add(_boxViewTabSelectedInformation, 1, 1);
            _commonGridMenuButtons.Children.Add(_tabMore, 2, 0);
            _commonGridMenuButtons.Children.Add(_boxViewTabSelectedMore, 2, 1);
        }

        /*
         * clear the common list view
         */

        private void ClearListViewAndHideDetailsGrid()
        {
            try
            {
                _commonGridResults.Children.Clear();
            }
            catch (Exception ex)
            {
                _log.Exception(ex);
                AppContextError.Send(typeof (ProfileMe).Name, "ClearListViewAndHideDetailsGrid", ex, null,
                    ExceptionLevel.Application);
            }
        }

        public async void LoadDeafultImage()
        {
#if __ANDROID__
    //var imageArray = await ImageResizer.GetResizeImage(BeginApplication.Styles.DefaultGroupIcon);
    //this._imageSourceGroupByDefault = ImageSource.FromStream(() => new MemoryStream(imageArray));
            this._imageSourceGroupByDefault = BeginApplication.Styles.DefaultGroupIcon;
#endif
#if __IOS__
#endif
        }

        #region buttons control events

        private async void ScrollViewScrolledToAsync(object sender, ScrolledEventArgs e)
        {
            if (!string.IsNullOrEmpty(_tabSelected))
            {
                ViewExposureSetProperties();
                switch (_tabSelected)
                {
                    case TabsNames.Tab1Activity:
                        _viewExposure.SetViewToExpose(TabsNames.Tab1 = TabsNames.Tab1Activity);
                        await Navigation.PushAsync(_viewExposure);
                        break;
                    case TabsNames.Tab2Information:
                        _viewExposure.SetViewToExpose(TabsNames.Tab2 = TabsNames.Tab2Information);
                        await Navigation.PushAsync(_viewExposure);
                        break;
                }
            }
        }

        private async void ScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            var scrollView = sender as ScrollView;
            if (scrollView != null)
            {
                await scrollView.ScrollToAsync(scrollView.ScrollX, scrollView.ScrollY, true);
            }
            if (!string.IsNullOrEmpty(_tabSelected))
            {
                ViewExposureSetProperties();
                switch (_tabSelected)
                {
                    case TabsNames.Tab1Activity:
                        _viewExposure.SetViewToExpose(TabsNames.Tab1 = TabsNames.Tab1Activity);
                        await Navigation.PushAsync(_viewExposure);
                        break;
                    case TabsNames.Tab2Information:
                        _viewExposure.SetViewToExpose(TabsNames.Tab2 = TabsNames.Tab2Information);
                        await Navigation.PushAsync(_viewExposure);
                        break;
                }
            }
        }

        private void ViewExposureSetProperties()
        {
            if (_activity == null || _information == null) return;
            _viewExposure.PageOne = _activity;
            _viewExposure.PageTwo = _information;
            _viewExposure.TabOneName = TabsNames.Tab1Activity;
            _viewExposure.TabTwoName = TabsNames.Tab2Information;
            _viewExposure.TabThreeName = TabsNames.TabMore;
        }

        private async void EventHandlerTabActivity(object sender, EventArgs e)
        {
            if (_activity == null) return;
            var thisSender = sender as Label;
            if (thisSender != null) thisSender.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewTabSelectedInformation.IsVisible = false;
            _boxViewTabSelectedActivities.IsVisible = true;
            _boxViewTabSelectedMore.IsVisible = false;
            _tabInformation.TextColor = BeginApplication.Styles.DefaultColorButton;
            _tabMore.TextColor = BeginApplication.Styles.DefaultColorButton;

            _tabSelected = TabsNames.Tab1Activity;
            ClearListViewAndHideDetailsGrid();
            _commonGridResults.Children.Add(_activity.Content, 0, 0);
            //ViewExposureSetProperties();            
            //_viewExposure.SetViewToExpose(TabsNames.Tab1 = TabsNames.Tab1Activity);
            //await Navigation.PushAsync(_viewExposure);
        }

        private async void EventHandlerTabInformation(object sender, EventArgs e)
        {
            try
            {
                if (_information == null) return;

                var thisSender = sender as Label;
                if (thisSender != null) thisSender.TextColor = BeginApplication.Styles.TabSelectedTextColor;
                _boxViewTabSelectedInformation.IsVisible = true;
                _boxViewTabSelectedActivities.IsVisible = false;
                _boxViewTabSelectedMore.IsVisible = false;
                _tabActivities.TextColor = BeginApplication.Styles.DefaultColorButton;
                _tabMore.TextColor = BeginApplication.Styles.DefaultColorButton;

                _tabSelected = TabsNames.Tab2Information;
                ClearListViewAndHideDetailsGrid();

                _commonGridResults.Children.Add(_information.Content, 0, 0);
                //ViewExposureSetProperties();
                //_viewExposure.SetViewToExpose(TabsNames.Tab2 = TabsNames.Tab2Information);
                //await Navigation.PushAsync(_viewExposure);
            }
            catch (Exception ex)
            {
                _log.Exception(ex);
                AppContextError.Send(typeof (ProfileMe).Name, "ButtonInformationEventHandler", ex, null,
                    ExceptionLevel.Application);
            }
        }

        private async void EventHadlerTabMore(object sender, EventArgs e)
        {
            ClearListViewAndHideDetailsGrid();
            var thisSender = sender as Label;
            if (thisSender != null) thisSender.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewTabSelectedInformation.IsVisible = false;
            _boxViewTabSelectedActivities.IsVisible = false;
            _boxViewTabSelectedMore.IsVisible = true;
            _tabInformation.TextColor = BeginApplication.Styles.DefaultColorButton;
            _tabActivities.TextColor = BeginApplication.Styles.DefaultColorButton;

            var action = await DisplayActionSheet(null, MoreOptionsNames.Cancel, null, MoreOptionsNames.Contacts,
                MoreOptionsNames.Groups, MoreOptionsNames.Shops, MoreOptionsNames.Events);

            switch (action)
            {
                case MoreOptionsNames.Contacts:
                    _tabViewExposure.PageOne = _allContacts;
                    _tabViewExposure.PageTwo = _requestContacts;
                    _tabViewExposure.TabOneName = TabsNames.Tab1Contacts;
                    _tabViewExposure.TabTwoName = TabsNames.Tab2Contacts;
                    _tabViewExposure.ToolbarItemTabOne = _allContacts.ToolbarItem;
                    _tabViewExposure.ToolbarItemTabTwo = _requestContacts.ToolbarItem;
                    _tabViewExposure.SetInitialProperties(TabsNames.Tab1 = TabsNames.Tab1Contacts);
                    await Navigation.PushAsync(_tabViewExposure);
                    break;
                case MoreOptionsNames.Groups:
                    _tabViewExposure.PageOne = _allGroups;
                    _tabViewExposure.PageTwo = _myGroups;
                    _tabViewExposure.TabOneName = TabsNames.Tab1Groups;
                    _tabViewExposure.TabTwoName = TabsNames.Tab2Groups;
                    _tabViewExposure.ToolbarItemTabOne = _allGroups.ToolbarItem;
                    _tabViewExposure.SetInitialProperties(TabsNames.Tab1 = TabsNames.Tab1Groups); //set selected item   
                    await Navigation.PushAsync(_tabViewExposure);
                    break;
                case MoreOptionsNames.Services:
                    await Navigation.PushAsync(_allGroups); //TODO replace for services page
                    break;
                case MoreOptionsNames.Shops:
                    await Navigation.PushAsync(_shops);
                    break;
                case MoreOptionsNames.Events:
                    _tabViewExposure.PageOne = _myEvents;
                    _tabViewExposure.PageTwo = _myEvents;
                    _tabViewExposure.TabOneName = TabsNames.Tab1Events;
                    _tabViewExposure.TabTwoName = TabsNames.Tab2Events;
                    _tabViewExposure.ToolbarItemTabOne = _myEvents.ToolbarItem;
                    _tabViewExposure.ToolbarItemTabTwo = _myEvents.ToolbarItem;
                    _tabViewExposure.SetInitialProperties(TabsNames.Tab1 = TabsNames.Tab1Events); //set selected item   
                    await Navigation.PushAsync(_tabViewExposure);
                    break;
                case MoreOptionsNames.Cancel:
                    return;
                default:
                    return;
            }
        }

        #endregion
    }

    public class Write
    {
        public Write(LoginUser loginUser)
        {
            ButtonCloseSearch = new Button
                                {
                                    Text = "X",
                                    HeightRequest = 40,
                                    WidthRequest = 40,
                                    HorizontalOptions = LayoutOptions.End
                                };
            ButtonCloseSearch.Clicked += CloseSearchEventHandler;
            Editor = new Editor
                     {
                         BackgroundColor = BeginApplication.Styles.ColorWhiteBackground,
                         HorizontalOptions = LayoutOptions.FillAndExpand,
                         HeightRequest = 100
                     };
            Container = new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Orientation = StackOrientation.Vertical,
                            Padding = new Thickness(20, 10, 20, 10),
                            BackgroundColor = BeginApplication.Styles.ApplicationGreenColor,
                            IsVisible = false
                        };
            Container.Children.Add(ButtonCloseSearch);
            Container.Children.Add(Editor);
        }

        public Button ButtonCloseSearch { get; set; }
        public Editor Editor { get; set; }
        public StackLayout Container { get; set; }

        private void CloseSearchEventHandler(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;
            Container.IsVisible = false;
        }
    }

    public static class MoreOptionsNames
    {
        //TODO add to resources
        public const string Contacts = "Contacts";
        public const string Groups = "Groups";
        public const string Services = "Services";
        public const string Shops = "Shops";
        public const string Events = "Events";
        public const string Cancel = "Cancel";
    }
}