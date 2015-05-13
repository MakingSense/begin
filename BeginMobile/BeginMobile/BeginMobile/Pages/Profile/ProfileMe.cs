using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.ContactPages;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ProfileMe : ContentPage
    {
        private Grid _commonGridDetailLayout;
        private Grid _commonGridMenuButtons;
        private Grid _commonGridResults;
        private ListView _commonListView;
        private Grid _commonMainGrid;
        private ScrollView _commonMainScrollView;
        private ImageSource _imageSourceGroupByDefault;

        private readonly LoginUser _currentUser;
        private BoxView _boxViewButtonSelectedInfo;
        private BoxView _boxViewButtonSelectedActivities;
        private BoxView _boxViewButtonSelectedOthers;
        //private BoxView _boxViewButtonSelectedContacts;

        private Button _buttonActivities;
        private Button _buttonInformation;
        private Button _buttonOthers;
     //   private Button _buttonContacts;
        private Button _buttonGroups;
        private Button _buttonShops;
        private Button _buttonEvents;

        private readonly Information _information = new Information();
        private readonly ContactPage _contacts = new ContactPage(String.Empty, String.Empty);
        private readonly MyActivity _activity = new MyActivity();
        private readonly GroupListPage _groups = new GroupListPage(String.Empty, String.Empty);
        private readonly Shop _shops = new Shop();
        private readonly Events _events = new Events();

        private ViewExposure _viewExposure = new ViewExposure();

        public ProfileMe(LoginUser currenLoginUser)
        {
            //loads the navidator Image
            LoadDeafultImage();

            //set the styles to this Page
            Style = BeginApplication.Styles.PageStyle;
            Title = AppResources.LabelProfileMeTitle;
            _currentUser = currenLoginUser;

            //Initialize components           
            InitProfileDetails(_currentUser.User);
        }

        private void InitProfileDetails(User user)
        {
            InitProfileControlButtons();
            var userAvatar = BeginApplication.Styles.DefaultProfileUserIconName;
            var circleProfileImage = new CircleImage
                                     {
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
                               Text = "Web Developer", //user.Profession,
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

            //main grid layouts
            _commonMainScrollView = new ScrollView();

            _commonMainGrid = new Grid
                              {
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions = new RowDefinitionCollection
                                                   {
                                                       new RowDefinition {Height = GridLength.Auto},
                                                       new RowDefinition {Height = GridLength.Auto},
                                                       new RowDefinition {Height = GridLength.Auto}
                                                   },
                                  ColumnDefinitions =
                                  {
                                      new ColumnDefinition {Width = GridLength.Auto}
                                  }
                              };
            _commonGridResults = new Grid
                                 {
                                     VerticalOptions = LayoutOptions.FillAndExpand,
                                     HorizontalOptions = LayoutOptions.FillAndExpand,
                                     Padding = BeginApplication.Styles.LayoutThickness
                                 };
            _commonListView = new ListView {HasUnevenRows = true};

            _commonGridResults.Children.Add(_commonListView, 0, 0);
            //Init();

            _commonMainGrid.Children.Add(_commonGridDetailLayout, 0, 0);
            _commonMainGrid.Children.Add(_commonGridMenuButtons, 0, 1);
            _commonMainGrid.Children.Add(_commonGridResults, 0, 2);
            _commonMainScrollView.Content = _commonMainGrid;

            Content = _commonMainGrid;
        }

        public async void Init()
        {
            _commonGridResults.Children.Add(_activity.GetGridActivities, 0, 0);
        }

        /**
         * Initialize the control buttons that simulate the tabbed options
         **/

        private void InitProfileControlButtons()
        {
            _buttonActivities = new Button { Text = "Activities", Style = BeginApplication.Styles.LinkButton, TextColor = BeginApplication.Styles.TabSelectedTextColor };
            _buttonInformation = new Button
                                 {
                                     Text = "Information",
                                     Style = BeginApplication.Styles.LinkButton
                                 };
           // _buttonContacts = new Button {Text = "Contacts", Style = BeginApplication.Styles.LinkButton};
            _buttonOthers = new Button {Text = "...", Style = BeginApplication.Styles.LinkButton};

            _commonGridMenuButtons = new Grid
                                     {
                                         HorizontalOptions = LayoutOptions.Center,
                                         VerticalOptions = LayoutOptions.Center,
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
            //_boxViewButtonSelectedContacts = new BoxView
            //                                 {
            //                                     Style = BeginApplication.Styles.TabUnderLine,
            //                                     IsVisible = false
            //                                 };
            _boxViewButtonSelectedOthers = new BoxView
                                           {
                                               Style = BeginApplication.Styles.TabUnderLine,
                                               IsVisible = false
                                           };

            _commonGridMenuButtons.Children.Add(_buttonActivities, 0, 0);
            _commonGridMenuButtons.Children.Add(_boxViewButtonSelectedActivities, 0, 1);
            _commonGridMenuButtons.Children.Add(_buttonInformation, 1, 0);
            _commonGridMenuButtons.Children.Add(_boxViewButtonSelectedInfo, 1, 1);
            //_commonGridMenuButtons.Children.Add(_buttonContacts, 2, 0);
            //_commonGridMenuButtons.Children.Add(_boxViewButtonSelectedContacts, 2, 1);
            _commonGridMenuButtons.Children.Add(_buttonOthers, 3, 0);
            _commonGridMenuButtons.Children.Add(_boxViewButtonSelectedOthers, 3, 1);


            _buttonActivities.Clicked += ButtonActivityEventHandler;
            _buttonInformation.Clicked += ButtonInformationEventHandler;
           // _buttonContacts.Clicked += ButtonContactEventHandler;
            _buttonOthers.Clicked += ButtonOtherEventHadler;
        }

        /*
         * clear the common list view
         */

        private void ClearListViewAndHideDetailsGrid()
        {
            _commonGridResults.Children.Clear();
            _commonGridDetailLayout.IsVisible = false;
            _commonListView.ItemsSource = null;
            _commonListView.ItemTemplate = null;
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

        #region buttons control events

        private async void ButtonActivityEventHandler(object sender, EventArgs e)
        {
            ClearListViewAndHideDetailsGrid();

            //TODO: refactoring this part
            var thisButton = sender as Button;
            if (thisButton != null) thisButton.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _buttonInformation.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonOthers.TextColor = BeginApplication.Styles.DefaultColorButton;
            _boxViewButtonSelectedInfo.IsVisible = false;
            _boxViewButtonSelectedActivities.IsVisible = true;
            _boxViewButtonSelectedOthers.IsVisible = false;
         //   _boxViewButtonSelectedContacts.IsVisible = false;

            //_commonListView.ItemTemplate = new DataTemplate(typeof (Activities));
            //_commonListView.ItemsSource = await GetListActivities();
            //_commonListView.ItemSelected += (s, eventArgs) =>
            //                                {
            //                                    if (eventArgs.SelectedItem == null)
            //                                    {
            //                                        return;
            //                                    }
            //                                    ((ListView) s).SelectedItem = null;
            //                                };

            //_commonGridResults.Children.Add(_commonListView, 0, 0);

            _commonGridResults.Children.Add(_activity.GetGridActivities, 0, 0);
        }

        private void ButtonInformationEventHandler(object sender, EventArgs e)
        {
            ClearListViewAndHideDetailsGrid();

            var thisButton = sender as Button;
            if (thisButton != null) thisButton.TextColor = BeginApplication.Styles.TabSelectedTextColor;

            _buttonActivities.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonOthers.TextColor = BeginApplication.Styles.DefaultColorButton;
            _boxViewButtonSelectedInfo.IsVisible = true;
            _boxViewButtonSelectedActivities.IsVisible = false;
            _boxViewButtonSelectedOthers.IsVisible = false;
            //_boxViewButtonSelectedContacts.IsVisible = false;
            _commonGridResults.Children.Add(_information.GetGridInfo());
        }


        private async void ButtonContactEventHandler(object sender, EventArgs e)
        {
            ClearListViewAndHideDetailsGrid();
            var thisButton = sender as Button;
            if (thisButton != null) thisButton.TextColor = BeginApplication.Styles.TabSelectedTextColor;

            _buttonActivities.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonOthers.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonInformation.TextColor = BeginApplication.Styles.DefaultColorButton;
           // _boxViewButtonSelectedContacts.IsVisible = true;
            _boxViewButtonSelectedInfo.IsVisible = false;
            _boxViewButtonSelectedActivities.IsVisible = false;
            _boxViewButtonSelectedOthers.IsVisible = false;
            //TODO here Contacts logic options for load contacts 


            _commonGridResults.Children.Add(_contacts.GridContacts(), 0, 0);
        }

        private async void ButtonOtherEventHadler(object sender, EventArgs e)
        {
            ClearListViewAndHideDetailsGrid(); //clear the common list

            var thisButton = sender as Button;
            if (thisButton != null) thisButton.TextColor = BeginApplication.Styles.TabSelectedTextColor;
            _boxViewButtonSelectedInfo.IsVisible = false;
            _boxViewButtonSelectedActivities.IsVisible = false;
            _boxViewButtonSelectedOthers.IsVisible = true;
            //_boxViewButtonSelectedContacts.IsVisible = false;
            _buttonInformation.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonActivities.TextColor = BeginApplication.Styles.DefaultColorButton;

            var action = await DisplayActionSheet(null, OtherOptions.Cancel, null,OtherOptions.Contacts,
                OtherOptions.Groups, OtherOptions.Shops, OtherOptions.Events);

            switch (action)
            {                
                case OtherOptions.Contacts:
                   // _viewExposure.SetViewToExpose(_contacts.GridContacts(), _contacts.GridContacts(),"All contacts", "My Contacts");
                    //await Navigation.PushAsync(_viewExposure);
                    await Navigation.PushAsync(_contacts);
                    break;
                case OtherOptions.Groups:
                    await Navigation.PushAsync(_groups);
                    break;
                case OtherOptions.Shops:
                    await Navigation.PushAsync(_shops);
                    break;
                case OtherOptions.Events:
                    await Navigation.PushAsync(_events);
                    break;
                case OtherOptions.Cancel:
                    return;  
                    
                default:
                    return;
            }
        }

        #endregion

        #region private methods

        private async Task<ObservableCollection<ActivityViewModel>> GetListActivities()
        {
            //request the activities API
            var profileActivity =
                await BeginApplication.ProfileServices.GetActivities(_currentUser.AuthToken, _currentUser.User.UserName);

            var listActivityData = new ObservableCollection<ActivityViewModel>();

            if (profileActivity != null)
            {
                var listActivityViewModel = from activity in profileActivity.Activities
                    where activity.Component.Equals("activity", StringComparison.InvariantCultureIgnoreCase)
                    select new ActivityViewModel
                           {
                               Icon = BeginApplication.Styles.DefaultActivityIcon,
                               //TODO:change for activity avatar if this exist
                               NameSurname = profileActivity.NameSurname,
                               ActivityDescription = activity.Content,
                               ActivityType = activity.Type,
                               DateAndTime = activity.Date
                           };

                foreach (var activityViewModel in listActivityViewModel)
                {
                    listActivityData.Add(activityViewModel);
                }
            }
            return listActivityData;
        }


        //private void InitToolBar()
        //{
        //    var toolBarItemMyActivity = new ToolbarItem
        //    {
        //        Icon = "Icon.png",
        //        Text = "",//AppResources.ToolBarProfileMeMyAct,
        //        Order = ToolbarItemOrder.Primary,
        //        Command = new Command(() => Navigation.PushAsync(new MyActivity()))
        //    };

        //    var toolBarItemInformation = new ToolbarItem
        //    {
        //        Icon = "",
        //        Text = AppResources.ToolBarProfileMeInfo,
        //        Order = ToolbarItemOrder.Primary,
        //        Command = new Command(() => Navigation.PushAsync(new Information()))
        //    };

        //    var toolBarItemMessages = new ToolbarItem
        //    {
        //        Icon = "",
        //        Text = AppResources.ToolBarProfileMeMessages,
        //        Order = ToolbarItemOrder.Secondary,
        //        Command = new Command(() => Navigation.PushAsync(new Messages()))
        //    };

        //    var toolBarItemContacts = new ToolbarItem
        //    {
        //        Icon = "",
        //        Text = AppResources.ToolBarProfileMeContacts,
        //        Order = ToolbarItemOrder.Secondary,
        //        Command = new Command(() => Navigation.PushAsync(new Contacts()))
        //    };

        //    var toolBarItemGroups = new ToolbarItem
        //    {
        //        Icon = "",
        //        Text = AppResources.ToolBarProfileMeGroups,
        //        Order = ToolbarItemOrder.Secondary,
        //        Command = new Command(() => Navigation.PushAsync(new Groups()))
        //    };

        //    var toolBarItemShop = new ToolbarItem
        //    {
        //        Icon = "",
        //        Text = AppResources.ToolBarProfileMeShop,
        //        Order = ToolbarItemOrder.Primary,
        //        Command = new Command(() => Navigation.PushAsync(new Shop()))
        //    };

        //    var toolBarItemEvents = new ToolbarItem
        //    {
        //        Icon = "",
        //        Text = AppResources.ToolBarProfileMeEvents,
        //        Order = ToolbarItemOrder.Secondary,
        //        Command = new Command(() => Navigation.PushAsync(new Events()))
        //    };

        //    ToolbarItems.Add(toolBarItemMyActivity);
        //    ToolbarItems.Add(toolBarItemInformation);
        //    ToolbarItems.Add(toolBarItemMessages);
        //    ToolbarItems.Add(toolBarItemContacts);
        //    ToolbarItems.Add(toolBarItemGroups);
        //    ToolbarItems.Add(toolBarItemShop);
        //    ToolbarItems.Add(toolBarItemEvents);

        //}

        #endregion
    }


    public static class OtherOptions
    {
        public const string Activity = "Activity";
        public const string Information = "Information";
        public const string Contacts = "Contacts";
        public const string Groups = "Groups";
        public const string Shops = "Shops";
        public const string Events = "Events";
        public const string Cancel = "Cancel";
    }      
}