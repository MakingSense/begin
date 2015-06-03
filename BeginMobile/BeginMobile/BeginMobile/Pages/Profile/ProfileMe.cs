using System;
using System.Collections.Generic;
using System.Linq;
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
        private Grid _commonMainGrid;
        private ScrollView _commonMainScrollView;
        private BoxView _boxViewTabSelectedInformation;
        private BoxView _boxViewTabSelectedActivities;
        private BoxView _boxViewTabSelectedMore;
        private BoxView _boxViewTabInactiveInformation;
        private BoxView _boxViewTabInactiveActivities;
        private BoxView _boxViewTabInactiveMore;
        private Label _tabActivities;
        private Label _tabInformation;
        private Label _tabMore;
        private Information _information;
        private MyActivity _activity;
        private Shop _shops;
        //private ContactPage _allContacts;
        private Contacts _myContacts;
        //private GroupListPage _allGroups;
        private Groups _myGroups;
        private Events _myEvents;
        private TabViewExposure _tabViewExposure;
        private readonly ViewExposure _viewExposure = new ViewExposure();
        private readonly ILoggingService _log = Logger.Current;
        private ImageSource _imageSourceGroupByDefault;
        private const int GotPuntuation = 3;
        private const int RankingGridRow = 0;
        private string _tabSelected = TabsNames.Tab1Activity;
        private readonly LoginUser _currentUser;

        public ProfileMe(LoginUser currenLoginUser)
        {
            LoadDeafultImage();
            _currentUser = currenLoginUser;
            Style = BeginApplication.Styles.PageStyle;
            Title = AppResources.LabelProfileMeTitle;

            _information = new Information(_currentUser);
            //_allContacts = new ContactPage(string.Empty, string.Empty);
            _myContacts = new Contacts();
            _activity = new MyActivity();
            //_allGroups = new GroupListPage(String.Empty, String.Empty);
            _myGroups = new Groups();
            _shops = new Shop();
            _myEvents = new Events();
            _tabViewExposure = new TabViewExposure();
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
                                         VerticalOptions = LayoutOptions.End,
                                         Style = BeginApplication.Styles.CircleImageForDetails,
                                         Source = userAvatar
                                     };

            var labelName = new Label
                            {
                                XAlign = TextAlignment.Center,
                                TextColor = Color.White,
                                Text = user.NameSurname,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.Center,
                                //Style = BeginApplication.Styles.TitleStyle,
                                FontSize = BeginApplication.Styles.TextFontSize18,
                                WidthRequest = 160
                            };

            var labelJob = new Label
                           {
                               Text =
                                   !string.IsNullOrEmpty(BeginApplication.SelectedUserProfession)
                                       ? BeginApplication.SelectedUserProfession
                                       : "Web Designer",
                               //user.Profession,//TODO: remove the harcode data and uncomment the user.Profession                               
                               HorizontalOptions = LayoutOptions.Center,
                               FontSize = BeginApplication.Styles.TextFontSize16,
                               TextColor = BeginApplication.Styles.ColorWhite,
                           };

            var labelDirection = new Label
                                 {
                                     TextColor = Color.White,
                                     Text = "@" + user.UserName,
                                     HorizontalOptions = LayoutOptions.Center,
                                     Style = BeginApplication.Styles.SubtitleStyle,
                                 };

            var gridRakingImage = new Grid
                                  {
                                      HorizontalOptions = LayoutOptions.Center,
                                      VerticalOptions = LayoutOptions.Center,
                                      RowDefinitions = new RowDefinitionCollection
                                                       {
                                                           new RowDefinition {Height = GridLength.Auto},
                                                       }
                                  };
            var imageRankingDefault = new Image
                                      {
                                          Source = ImageSource.FromFile(BeginApplication.Styles.RankingDefaultIcon),
                                          WidthRequest = 12,
                                          HeightRequest = 12
                                      };
            gridRakingImage.Children.Add(imageRankingDefault, 0, 0);
            gridRakingImage.Children.Add(imageRankingDefault, 1, 0);
            gridRakingImage.Children.Add(imageRankingDefault, 2, 0);
            gridRakingImage.Children.Add(imageRankingDefault, 3, 0);
            gridRakingImage.Children.Add(imageRankingDefault, 4, 0);

            for (var indexer = 0; indexer <= GotPuntuation; indexer ++)
            {
                var image = new Image
                            {
                                Source = ImageSource.FromFile(BeginApplication.Styles.RankingAddIcon),
                                WidthRequest = 12,
                                HeightRequest = 12
                            };
                gridRakingImage.Children.Add(image, indexer, RankingGridRow);
            }

            _commonGridDetailLayout = new Grid
                                      {
                                          HeightRequest = BeginApplication.Styles.ProfileDetailsHeight,
                                          BackgroundColor = Color.Transparent,
                                          HorizontalOptions = LayoutOptions.Center,
                                          VerticalOptions = LayoutOptions.Center,
                                          RowDefinitions =
                                          {
                                              //new RowDefinition {Height = new GridLength(, GridUnitType.Absolute)},
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
            //_commonGridDetailLayout.Children.Add(labelDirection, 0, 3);
            _commonGridDetailLayout.Children.Add(gridRakingImage, 0, 3);
            _commonMainScrollView = new ScrollView();
            _commonMainGrid = new Grid
                              {
                                  BackgroundColor = BeginApplication.Styles.ColorWhite,
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
                                      //new RowDefinition {Height = GridLength.Auto}
                                  }
                              };

            var imageBanner = new Image
                              {
                                  Source = ImageSource.FromFile(BeginApplication.Styles.DefaultProfileMeBannerImage),
                                  Aspect = Aspect.Fill,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                              };
            GridResults = new Grid
                          {
                              BackgroundColor = BeginApplication.Styles.ColorWhite,
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
                                  HeightRequest = 40
                              };


            var stackLayoutDetails = new StackLayout
                                     {
                                         BackgroundColor = BeginApplication.Styles.DefaultProfileMeBannerColor,
                                         Padding = BeginApplication.Styles.ProfileDetailsPadding,
                                         HeightRequest = 250,
                                         VerticalOptions = LayoutOptions.FillAndExpand,
                                         HorizontalOptions = LayoutOptions.FillAndExpand,
                                         Children =
                                         {
                                             _commonGridDetailLayout
                                         }
                                     };
            // _commonMainGrid.Children.Add(_newPublication.Container, 0, 0);
            //_commonMainGrid.Children.Add(imageBanner, 0, 1);
            _commonMainGrid.Children.Add(stackLayoutDetails, 0, 0);
            //_commonMainGrid.Children.Add(boxBlacknew, 0, 1);
            _commonMainGrid.Children.Add(_commonGridMenuButtons, 0, 1);
            _commonMainGrid.Children.Add(GridResults, 0, 2);

            _commonMainScrollView.Content = _commonMainGrid;
            ToolbarItems.Add(new ToolbarItem("Publication", BeginApplication.Styles.WriteIcon, async () =>
                                                                                                     {
                                                                                                         await
                                                                                                             Navigation
                                                                                                                 .PushAsync
                                                                                                                 (new NewPublication
                                                                                                                     (_currentUser));
                                                                                                     }));
            _commonMainScrollView.Scrolled += ScrollViewScrolled;
            Content = _commonMainScrollView;
        }

        public Grid GridResults { get; set; }

        public async void Init(ContentPage activityPage)
        {
            try
            {
                if (activityPage != null && activityPage.Content != null)
                    GridResults.Children.Add(activityPage.Content, 0, 0);
            }
            catch (Exception e)
            {
                _log.Exception(e);
                AppContextError.Send(typeof (ProfileMe).Name, "InitialActivitiesContent", e, null,
                    ExceptionLevel.Application);
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
                                 FontSize = BeginApplication.Styles.TextFontSize14,
                             };
            _tabInformation = new Label
                              {
                                  Text = TabsNames.Tab2Information,
                                  XAlign = TextAlignment.Center,
                                  FontSize = BeginApplication.Styles.TextFontSize14,
                              };
            _tabMore = new Label
                       {
                           Text = TabsNames.TabMore,
                           XAlign = TextAlignment.Center,
                           FontSize = BeginApplication.Styles.TextFontSize14,
                       };

            _tabActivities.GestureRecognizers.Add(tapGestureRecognizerTabOne);
            _tabInformation.GestureRecognizers.Add(tapGestureRecognizerTabTwo);
            _tabMore.GestureRecognizers.Add(tapGestureRecognizerTabThree);

            _commonGridMenuButtons = new Grid
                                     {
                                         BackgroundColor = BeginApplication.Styles.ColorWhite,
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
                                         ColumnSpacing = 0
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

            _boxViewTabInactiveActivities = new BoxView
                                            {
                                                Style = BeginApplication.Styles.TabUnderLineInactive,
                                                IsVisible = false,
                                            };

            _boxViewTabInactiveInformation = new BoxView
                                             {
                                                 Style = BeginApplication.Styles.TabUnderLineInactive,
                                                 IsVisible = true
                                             };

            _boxViewTabInactiveMore = new BoxView
                                      {
                                          Style = BeginApplication.Styles.TabUnderLineInactive,
                                          IsVisible = true
                                      };

            _commonGridMenuButtons.Children.Add(_tabActivities, 0, 0);
            _commonGridMenuButtons.Children.Add(_boxViewTabSelectedActivities, 0, 1);
            _commonGridMenuButtons.Children.Add(_boxViewTabInactiveActivities, 0, 1);
            _commonGridMenuButtons.Children.Add(_tabInformation, 1, 0);
            _commonGridMenuButtons.Children.Add(_boxViewTabSelectedInformation, 1, 1);
            _commonGridMenuButtons.Children.Add(_boxViewTabInactiveInformation, 1, 1);
            _commonGridMenuButtons.Children.Add(_tabMore, 2, 0);
            _commonGridMenuButtons.Children.Add(_boxViewTabSelectedMore, 2, 1);
            _commonGridMenuButtons.Children.Add(_boxViewTabInactiveMore, 2, 1);
        }

        /*
         * clear the common list view
         */

        private void ClearListViewAndHideDetailsGrid()
        {
            try
            {
                if (GridResults.Children != null)
                {
                    GridResults.Children.Clear();                    
                }                
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

        private async void ScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            try
            {
                ClearListViewAndHideDetailsGrid();
                var scrollView = sender as ScrollView;
                if (scrollView == null) return;
                if (!scrollView.Orientation.Equals(ScrollOrientation.Vertical)) return;
                if (string.IsNullOrEmpty(_tabSelected)) return;
                switch (_tabSelected)
                {
                    case TabsNames.Tab1Activity:
                        ViewExposureSetProperties();
                        _viewExposure.SetViewToExpose(TabsNames.Tab1 = TabsNames.Tab1Activity);
                        await Navigation.PushAsync(_viewExposure);
                        break;
                    case TabsNames.Tab2Information:
                        ViewExposureSetProperties();
                        _viewExposure.SetViewToExpose(TabsNames.Tab2 = TabsNames.Tab2Information);
                        await Navigation.PushAsync(_viewExposure);
                        break;
                }
            }
            catch (Exception ex)
            {
                _log.Exception(ex);
                AppContextError.Send(typeof (ProfileMe).Name, "ScrollViewScrolled", ex, null,
                    ExceptionLevel.Application);
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
            if (thisSender != null) ActivitySelected();
            ClearListViewAndHideDetailsGrid();
            GridResults.Children.Add(_activity.Content, 0, 0);
        }

        private async void EventHandlerTabInformation(object sender, EventArgs e)
        {
            try
            {
                if (_information == null) return;

                var thisSender = sender as Label;
                if (thisSender != null) InformationSelected();
                ClearListViewAndHideDetailsGrid();

                GridResults.Children.Add(_information.Content, 0, 0);
            }
            catch (Exception ex)
            {
                _log.Exception(ex);
                AppContextError.Send(typeof (ProfileMe).Name, "ButtonInformationEventHandler", ex, null,
                    ExceptionLevel.Application);
            }
        }

        private void ActivitySelected()
        {
            _tabActivities.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewTabSelectedActivities.IsVisible = true;
            _boxViewTabInactiveActivities.IsVisible = false;
            _boxViewTabSelectedInformation.IsVisible = false;
            _boxViewTabInactiveInformation.IsVisible = true;
            _boxViewTabSelectedMore.IsVisible = false;
            _boxViewTabInactiveMore.IsVisible = true;

            _tabInformation.TextColor = BeginApplication.Styles.DefaultColorButton;
            _tabMore.TextColor = BeginApplication.Styles.DefaultColorButton;

            _tabSelected = TabsNames.Tab1Activity;
        }

        private void InformationSelected()
        {
            _tabInformation.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewTabSelectedInformation.IsVisible = true;
            _boxViewTabInactiveInformation.IsVisible = false;
            _boxViewTabSelectedActivities.IsVisible = false;
            _boxViewTabInactiveActivities.IsVisible = true;
            _boxViewTabSelectedMore.IsVisible = false;
            _boxViewTabInactiveMore.IsVisible = true;
            _tabActivities.TextColor = BeginApplication.Styles.DefaultColorButton;
            _tabMore.TextColor = BeginApplication.Styles.DefaultColorButton;

            _tabSelected = TabsNames.Tab2Information;
        }

        private async void EventHadlerTabMore(object sender, EventArgs e)
        {
            //ClearListViewAndHideDetailsGrid();
            var thisSender = sender as Label;
            if (thisSender != null) thisSender.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewTabSelectedInformation.IsVisible = false;
            _boxViewTabInactiveInformation.IsVisible = true;
            _boxViewTabSelectedActivities.IsVisible = false;
            _boxViewTabInactiveActivities.IsVisible = true;
            _boxViewTabSelectedMore.IsVisible = true;
            _boxViewTabInactiveMore.IsVisible = false;
            _tabInformation.TextColor = BeginApplication.Styles.DefaultColorButton;
            _tabActivities.TextColor = BeginApplication.Styles.DefaultColorButton;

            var action = await DisplayActionSheet(null, MoreOptionsNames.Cancel, null, MoreOptionsNames.Contacts,
                MoreOptionsNames.Groups, MoreOptionsNames.Shops, MoreOptionsNames.Events);

            switch (action)
            {
                case MoreOptionsNames.Contacts:
                    _tabViewExposure.PageOne = _myContacts;
                    _tabViewExposure.PageTwo = _myContacts;
                    _tabViewExposure.TabOneName = TabsNames.Tab1Contacts;
                    _tabViewExposure.TabTwoName = TabsNames.Tab2Contacts;
                    _tabViewExposure.ToolbarItemTabOne = _myContacts.ToolbarItem;
                    _tabViewExposure.ToolbarItemTabTwo = _myContacts.ToolbarItem;
                    _tabViewExposure.SetInitialProperties(TabsNames.Tab1 = TabsNames.Tab1Contacts);
                    await Navigation.PushAsync(_tabViewExposure);
                    break;
                case MoreOptionsNames.Groups:
                    _tabViewExposure.PageOne = _myGroups;
                    _tabViewExposure.PageTwo = _myGroups;
                    _tabViewExposure.TabOneName = TabsNames.Tab1Groups;
                    _tabViewExposure.TabTwoName = TabsNames.Tab2Groups;
                    //_tabViewExposure.ToolbarItemTabOne = _myGroups.ToolbarItem;
                    _tabViewExposure.SetInitialProperties(TabsNames.Tab1 = TabsNames.Tab1Groups); //set selected item   
                    await Navigation.PushAsync(_tabViewExposure);
                    break;
                case MoreOptionsNames.Services:
                    await Navigation.PushAsync(_myGroups); //TODO replace for services page
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
                    if (string.IsNullOrEmpty(_tabSelected)) return;
                    switch (_tabSelected)
                    {
                        case TabsNames.Tab1Activity:
                            ActivitySelected();
                            break;
                        case TabsNames.Tab2Information:
                            InformationSelected();
                            break;
                    }
                    return;
                default:
                    return;
            }
        }

        #endregion
    }

    public class NewPublication : ContentPage
    {
        private readonly Editor _publicationEditor;
        private const string DefaultEditorText = "What is happening?";
        private LoginUser _currentUser;

        public NewPublication(LoginUser currentUser)
        {
            _currentUser = currentUser;
            Title = "New Publication";
            BackgroundColor = BeginApplication.Styles.ColorWhite;

            var image = new CircleImage
                        {
                            Source = BeginApplication.Styles.DefaultContactIcon,
                            Style = BeginApplication.Styles.CircleImageCommon,
                        };

            _publicationEditor = new Editor
                                 {
                                     Text = DefaultEditorText,
                                     BackgroundColor = BeginApplication.Styles.ColorWhiteBackground,
                                     HorizontalOptions = LayoutOptions.FillAndExpand,
                                 };
            _publicationEditor.Focused += FocusedEventHandler;
            _publicationEditor.Unfocused += UnfocusedEventHandler;
            _publicationEditor.TextChanged += TextChangedEventHandler;
            var mainGrid = new Grid
                           {
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               Padding = BeginApplication.Styles.ThicknessInternalLayout,
                               RowDefinitions = new RowDefinitionCollection
                                                {
                                                    new RowDefinition {Height = new GridLength(20, GridUnitType.Star)}
                                                },
                               ColumnDefinitions = new ColumnDefinitionCollection
                                                   {
                                                       new ColumnDefinition {Width = GridLength.Auto},
                                                       new ColumnDefinition {Width = GridLength.Auto}
                                                   }
                           };

            ToolbarItems.Add(new ToolbarItem("SendPublication", BeginApplication.Styles.PublicationsSendIcon,
                async () => { SendPublication(); }));
            mainGrid.Children.Add(image, 0, 0);
            mainGrid.Children.Add(_publicationEditor, 1, 0);

            Content = mainGrid;
        }

        public async void TextChangedEventHandler(object sender, TextChangedEventArgs e)
        {
        }

        public void UnfocusedEventHandler(object sender, FocusEventArgs e)
        {
            var editor = sender as Editor;
            if (editor == null) return;
            if (editor.Text.StartsWith(" "))
            {
                editor.Text.Trim();
            }
            if (string.IsNullOrEmpty(editor.Text))
            {
                editor.Text = DefaultEditorText;
            }

            //else
            //{
            //    if (editor.Text.Length <= 0) return;
            //    if (editor.Text.StartsWith("@"))
            //    {
            //        var label = new Label ();
            //        var userName = (editor.Text.Split(' '));
            //        label.Text = userName[0];
            //        label.FontAttributes = FontAttributes.Bold;
            //        var message = editor.Text.Substring(label.Text.Length, editor.Text.Length-1);
            //        editor.Text = string.Format("{0} {1}", label.Text, message);
            //    }                
            //}
        }

        public void FocusedEventHandler(object sender, FocusEventArgs e)
        {
            var editor = sender as Editor;
            if (editor != null) editor.Text = string.Empty;
        }

        public async void SendPublication()
        {
            if (!string.IsNullOrEmpty(_publicationEditor.Text))
            {
                await DisplayAlert("Info", "Sent Successfuly", "ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Validation Error", "Message is Empty", "re-try");
            }
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