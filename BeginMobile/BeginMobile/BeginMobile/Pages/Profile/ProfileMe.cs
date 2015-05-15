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
        private BoxView _boxViewButtonSelectedInfo;
        private BoxView _boxViewButtonSelectedActivities;
        private BoxView _boxViewButtonSelectedOthers;
        private Button _buttonActivities;
        private Button _buttonInformation;
        private Button _buttonOthers;
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
            InitProfileDetails(currenLoginUser.User);
        }

        private void InitProfileDetails(User user)
        {
            InitProfileControlButtons();
            var userAvatar = BeginApplication.Styles.DefaultProfileUserIconName;
            var circleProfileImage = new CircleImage
                                     {
                                         HorizontalOptions = LayoutOptions.Center,
                                         VerticalOptions = LayoutOptions.Center,
                                         Style = BeginApplication.Styles.CircleImageForDetails,
                                         Source = userAvatar
                                     };

            var labelTitle = new Label
                             {
                                 Text = AppResources.LabelTitleProfile,
                                 FontSize = 20,
                                 FontAttributes = FontAttributes.Bold,
                                 HorizontalOptions = LayoutOptions.Center,
                                 Style = BeginApplication.Styles.TitleStyle
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
                               Text = "Web Developer",
                               //user.Profession,//TODO: remove the harcode data and uncomment the user.Profession
                               HorizontalOptions = LayoutOptions.Center,
                               Style = BeginApplication.Styles.SubtitleStyle
                           };

            var labelDirection = new Label
                                 {
                                     Text = "@" + user.UserName,
                                     HorizontalOptions = LayoutOptions.Center,
                                     Style = BeginApplication.Styles.TextBodyStyle
                                 };
            var labelRating = new Label
                              {
                                  Text = "*****",
                                  HorizontalOptions = LayoutOptions.Center,
                                  Style = BeginApplication.Styles.TitleStyle
                              };

            _commonGridDetailLayout = new Grid
                                      {                                          
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
            _commonGridDetailLayout.Children.Add(circleProfileImage, 0, 0);
            _commonGridDetailLayout.Children.Add(labelName, 0, 1);
            _commonGridDetailLayout.Children.Add(labelJob, 0, 2);
            _commonGridDetailLayout.Children.Add(labelDirection, 0, 3);
            _commonGridDetailLayout.Children.Add(labelRating, 0, 4);
            _commonMainScrollView = new ScrollView();
            _commonMainGrid = new Grid();
            _commonGridResults = new Grid();
            Init();
            _commonMainGrid.Children.Add(_commonGridDetailLayout, 0, 0);
            _commonMainGrid.Children.Add(_commonGridMenuButtons, 0, 1);
            _commonMainGrid.Children.Add(_commonGridResults, 0, 2);

            _commonMainScrollView.Content = _commonMainGrid;

            Content = _commonMainScrollView;
        }

        public async void Init()
        {
            try
            {
                if (_activity.Content != null)
                    _commonGridResults.Children.Add(_activity.Content, 0, 0);
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
            _buttonActivities = new Button
                                {
                                    Text = TabsNames.Tab1Activity,
                                    Style = BeginApplication.Styles.LinkButton,
                                    TextColor = BeginApplication.Styles.TabSelectedTextColor
                                };
            _buttonInformation = new Button
                                 {
                                     Text = TabsNames.Tab2Information,
                                     Style = BeginApplication.Styles.LinkButton
                                 };
            _buttonOthers = new Button {Text = TabsNames.TabMore, Style = BeginApplication.Styles.LinkButton};

            _commonGridMenuButtons = new Grid
                                     {
                                         HorizontalOptions = LayoutOptions.Center,
                                         VerticalOptions = LayoutOptions.Start,
                                         RowDefinitions =
                                         {
                                             new RowDefinition {Height = GridLength.Auto},
                                             new RowDefinition {Height = GridLength.Auto}
                                         },
                                         ColumnDefinitions =
                                         {
                                             new ColumnDefinition {Width = GridLength.Auto},
                                             new ColumnDefinition {Width = GridLength.Auto},
                                             new ColumnDefinition {Width = GridLength.Auto},
                                             new ColumnDefinition {Width = GridLength.Auto},
                                         }
                                     };
            _boxViewButtonSelectedInfo = new BoxView
                                         {
                                             Style = BeginApplication.Styles.TabUnderLine,
                                             IsVisible = false
                                         };
            _boxViewButtonSelectedActivities = new BoxView
                                               {
                                                   Style = BeginApplication.Styles.TabUnderLine,
                                                   IsVisible = true,
                                               };

            _boxViewButtonSelectedOthers = new BoxView
                                           {
                                               Style = BeginApplication.Styles.TabUnderLine,
                                               IsVisible = false
                                           };

            _commonGridMenuButtons.Children.Add(_buttonActivities, 0, 0);
            _commonGridMenuButtons.Children.Add(_boxViewButtonSelectedActivities, 0, 1);
            _commonGridMenuButtons.Children.Add(_buttonInformation, 1, 0);
            _commonGridMenuButtons.Children.Add(_boxViewButtonSelectedInfo, 1, 1);
            _commonGridMenuButtons.Children.Add(_buttonOthers, 3, 0);
            _commonGridMenuButtons.Children.Add(_boxViewButtonSelectedOthers, 3, 1);
            _buttonActivities.Clicked += ButtonActivityEventHandler;
            _buttonInformation.Clicked += ButtonInformationEventHandler;
            _buttonOthers.Clicked += ButtonMoreEventHadler;
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

        private void ViewExposureSetProperties()
        {
            if (_activity == null || _information == null) return;
            ClearListViewAndHideDetailsGrid();
            _viewExposure.PageOne = _activity;
            _viewExposure.PageTwo = _information;
            _viewExposure.TabOneName = TabsNames.Tab1Activity;
            _viewExposure.TabTwoName = TabsNames.Tab2Information;
            _viewExposure.TabThreeName = TabsNames.TabMore;
        }

        private async void ButtonActivityEventHandler(object sender, EventArgs e)
        {
            if (_activity == null) return;
            ViewExposureSetProperties();
            _commonGridResults.Children.Add(_activity.Content, 0, 0);
            _viewExposure.SetViewToExpose(TabsNames.Tab1 = TabsNames.Tab1Activity);
            await Navigation.PushAsync(_viewExposure);
        }

        private async void ButtonInformationEventHandler(object sender, EventArgs e)
        {
            try
            {
                if (_information != null) _commonGridResults.Children.Add(_information.Content, 0, 0);
                ViewExposureSetProperties();
                _viewExposure.SetViewToExpose(TabsNames.Tab2 = TabsNames.Tab2Information);
                await Navigation.PushAsync(_viewExposure);
            }
            catch (Exception ex)
            {
                _log.Exception(ex);
                AppContextError.Send(typeof (ProfileMe).Name, "ButtonInformationEventHandler", ex, null,
                    ExceptionLevel.Application);
            }
        }

        private async void ButtonMoreEventHadler(object sender, EventArgs e)
        {
            ClearListViewAndHideDetailsGrid();
            var thisButton = sender as Button;
            if (thisButton != null) thisButton.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewButtonSelectedInfo.IsVisible = false;
            _boxViewButtonSelectedActivities.IsVisible = false;
            _boxViewButtonSelectedOthers.IsVisible = true;
            _buttonInformation.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonActivities.TextColor = BeginApplication.Styles.DefaultColorButton;

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