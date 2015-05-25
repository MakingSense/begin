using BeginMobile.Interfaces;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using BeginMobile.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.LocalizeResources.Resources;

namespace BeginMobile.Pages.Wall
{
    public class WallPage : TabContent
    {
        private ListView _listViewWall;
        private ProfileMeWall _profileShop;
        private LoginUser _currentUser;
        private StackLayout _stackLayoutMain;
        private ObservableCollection<BeginWallViewModel> _listWall;
        private const int DefaultLimit = 10;
        private bool _isLoading;
        private int _offset = DefaultLimit;
        private int _limit = 10;
        private Button _buttonAddMore;
        private Grid _gridMain;
        private StackLayout _stackLayoutLoadingIndicator;

        public const string TextAnd = "And";
        public const string TextGroup= " group";
        public const string TextGroupTopic = " group topic";
        public const string TextActivity = " activity";
        private bool _areLastItems;

        private ActivityIndicator _activityIndicatorLoading;

        private ImageSource _imageSourceWallByDefault;

        private string MasterTitle{ get; set; }

        public WallPage(string title, string iconImage)
            : base(title, iconImage)
        {
            _currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];
            //MasterTitle = AppResources.AppHomeChildNewsFeed;

            Title = AppResources.AppHomeChildNewsFeed;
            BackgroundColor = BeginApplication.Styles.ColorWhiteBackground;

            this._imageSourceWallByDefault = BeginApplication.Styles.DefaultWallIcon;
            //LoadDeafultImage();

            _areLastItems = false;

            _gridMain = new Grid()
            {
                Padding = BeginApplication.Styles.ThicknessMainLayout,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            _stackLayoutLoadingIndicator = CreateStackLayoutWithLoadingIndicator(ref _activityIndicatorLoading);

            _gridMain.Children.Add(_stackLayoutLoadingIndicator, 0, 1);

            //Section Toolbar items
            var toolBarEditPublicWall = new ToolbarItem()
            {
                Order = ToolbarItemOrder.Primary,
                Name = "Public Wall",
                Icon = BeginApplication.Styles.WriteIcon,
                Command = new Command(async () => ExecuteEditPublicWallCommand())
            };

            ToolbarItems.Add(toolBarEditPublicWall);
            //End Toolbar items

            Content = _gridMain;
            Init();
        }

        protected async Task ExecuteEditPublicWallCommand()
        {
            await DisplayAlert("Public Wall", "Edit wall", "Ok");
        }

        private ObservableCollection<BeginWallViewModel> ListBeginWallViewModel(List<BeginMobile.Services.DTO.WallActivityItem> oldListWall)
        {
            ObservableCollection<BeginWallViewModel> resultList = null;

            if (oldListWall != null)
            {
                resultList = new ObservableCollection<BeginWallViewModel>();

                foreach (var wall in oldListWall)
                {
                    var modelItem = GetBeginWallViewModel(wall);
                    resultList.Add(modelItem);
                }
            }
            else
            {
                resultList = new ObservableCollection<BeginWallViewModel>(new List<BeginWallViewModel>());
            }

            return resultList;
        }

        private async Task Init()
        {
            _activityIndicatorLoading.IsRunning = true;
            _activityIndicatorLoading.IsVisible = true;

            _profileShop = await BeginApplication.ProfileServices.GetWall(_currentUser.AuthToken, limit: _limit.ToString(), offset: _offset.ToString());
            _listWall = ListBeginWallViewModel(_profileShop.ListOfWall);
            //_listWall = HDListGetWall();

            _listViewWall = new ListView
            {
                StyleId = "WallList"
            };

            _listViewWall.HasUnevenRows = true;
            _listViewWall.ItemTemplate = new DataTemplate(() => new WallItemCell(_imageSourceWallByDefault));
            _listViewWall.ItemsSource = _listWall;
            _listViewWall.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                ((ListView)sender).SelectedItem = null;
            };

            _listViewWall.ItemAppearing += async (sender, e) =>
            {
                if (_isLoading == true || 
                    _listWall.Count == 0 ||
                    _listWall.Count < DefaultLimit ||
                    _areLastItems == true)
                    return;

                var appearingItem = (BeginWallViewModel) e.Item;
                var lastItem = _listWall[_listWall.Count - 1];

                if ((appearingItem.ItemId == lastItem.ItemId) &&
                    (appearingItem.Type == lastItem.Type) &&
                    appearingItem.PublicDate == lastItem.PublicDate)
                {
                    addLoadingIndicator(_stackLayoutLoadingIndicator);
                    await LoadItems();
                }
            };

            var relativeLayoutMain = new RelativeLayout() { VerticalOptions = LayoutOptions.FillAndExpand };
            relativeLayoutMain.Children.Add(_listViewWall,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            _activityIndicatorLoading.IsRunning = false;
            _activityIndicatorLoading.IsVisible = false;

            _gridMain.Children.Add(relativeLayoutMain, 0, 0);

            Content = _gridMain;

            removeLoadingIndicator(_stackLayoutLoadingIndicator);

        }

        private void removeLoadingIndicator(View loadingIndicator)
        {
            _gridMain.RowDefinitions[1].Height = GridLength.Auto;
            if (_gridMain.Children.Contains(loadingIndicator))
            {
                _gridMain.Children.Remove(loadingIndicator);
            }
        }

        
        private void addLoadingIndicator(View loadingIndicator)
        {
            _gridMain.RowDefinitions[1].Height = Device.OnPlatform<double>(33, 43, 43);
            if (!_gridMain.Children.Contains(loadingIndicator))
            {
                _gridMain.Children.Add(loadingIndicator, 0, 1);
            }
        }

        private async Task LoadItems()
        {
            _offset +=_limit;
            _isLoading = true;

            _activityIndicatorLoading.IsRunning = true;
            _activityIndicatorLoading.IsVisible = true;

            _profileShop = await BeginApplication.ProfileServices.GetWall(_currentUser.AuthToken, limit: _limit.ToString(), offset: _offset.ToString());

            if (_profileShop != null && _profileShop.ListOfWall.Count > 0)
            {
                Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                {
                    foreach (var beginWallViewModel in ListBeginWallViewModel(_profileShop.ListOfWall))
                    {
                        _listWall.Add(beginWallViewModel);
                    }

                    _activityIndicatorLoading.IsRunning = false;
                    _activityIndicatorLoading.IsVisible = false;
                    removeLoadingIndicator(_stackLayoutLoadingIndicator);

                    _isLoading = false;
                    return false;
                });
                
            }
            else
            {
                _activityIndicatorLoading.IsRunning = false;
                _activityIndicatorLoading.IsVisible = false;
                removeLoadingIndicator(_stackLayoutLoadingIndicator);

                _isLoading = false;
            }
        }

        public async void LoadDeafultImage()
        {
            #if __ANDROID__
                        var imageArray = await ImageResizer.GetResizeImage(BeginApplication.Styles.DefaultWallIcon);
                        this._imageSourceWallByDefault = ImageSource.FromStream(() => new MemoryStream(imageArray));
            #endif
            #if __IOS__
            this._imageSourceWallByDefault = BeginApplication.Styles.DefaultWallIcon;
            #endif
        }


        private ObservableCollection<BeginWallViewModel> HDListGetWall()
        {
            var result = new List<BeginWallViewModel>();

            string[] components =
            {
                WallParameters.Groups,
                WallParameters.Activity,
                WallParameters.Event,
                WallParameters.Profile
            };

            var random = new Random();

            for (var i = 0; i < 20; i++)
            {
                var beginWall = new BeginWallViewModel()
                {
                    ItemId = i.ToString(),
                    Component = components[random.Next(0, 4)],
                    Type = "Type " + i,
                    PublicDate = "10/10/15 18:" + i,
                    DisplayName = "User " + i,
                    Title = "My title " + i,
                    Description = "Description " + i,
                    Reason = "Reason " + i,
                    Date = "10/11/15 18:" + i,
                };

                result.Add(beginWall);
            }

            return new ObservableCollection<BeginWallViewModel>(result);
        }

        private BeginWallViewModel GetBeginWallViewModel(BeginMobile.Services.DTO.WallActivityItem wallItem)
        {
            var beginWall = new BeginWallViewModel()
            {
                ItemId = wallItem.ItemId,
                Component = wallItem.Component,
                Type = wallItem.Type,
                PublicDate = wallItem.Date,
                PublicDateShort = wallItem.Date
            };


            switch (wallItem.Type)
            {
                case WallParameters.CreatedGroup:
                    
                    beginWall.DisplayName = wallItem.User.DisplayName;
                    beginWall.ExtraText = "";
                    beginWall.DisplayNameTwo = "";
                    beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                    beginWall.Reason = WallParameters.DescCreatedGroup;
                    beginWall.Description = wallItem.Group.Name;
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.JoinedGroup:
                    beginWall.DisplayName = wallItem.User.DisplayName;
                    beginWall.ExtraText = "";
                    beginWall.DisplayNameTwo = "";
                    beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                    beginWall.Reason = WallParameters.DescJoinedGroup;
                    beginWall.Description = wallItem.Group.Name;
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.RtmediaUpdate:
                    beginWall.DisplayName = wallItem.User.DisplayName;
                    beginWall.ExtraText = "";
                    beginWall.DisplayNameTwo = "";
                    beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                    beginWall.Reason = WallParameters.DescRtmediaUpdate;
                    beginWall.Description = wallItem.Group.Name;
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.ActivityUpdate:

                    if(wallItem.Component == WallParameters.Groups){
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescActivityUpdate + TextGroup;
                        beginWall.Description = wallItem.Group.Name;
                        beginWall.Date = wallItem.Date;
                    }
                    else if (wallItem.Component == WallParameters.Activity)
                    {
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescActivityUpdate + TextActivity;
                        beginWall.Description = wallItem.Content;
                        beginWall.Date = wallItem.Date;
                    }
                    else
                    {
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescActivityUpdate;
                        beginWall.Description = null;
                        beginWall.Date = wallItem.Date;
                    }
                    break;
                case WallParameters.FriendshipCreated:
                case WallParameters.FriendshipAccepted:
                    beginWall.DisplayName = wallItem.User1.DisplayName;
                    beginWall.ExtraText = TextAnd;
                    beginWall.DisplayNameTwo = wallItem.User2.DisplayName;
                    beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                    beginWall.Reason = WallParameters.DescFriendshipAccepted;
                    beginWall.Description = null;
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.NewBooking:
                    if (wallItem.Component == WallParameters.Groups)
                    {
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescNewBooking + TextGroup;
                        beginWall.Description = wallItem.Group.Name;
                        beginWall.Date = wallItem.Date;
                    }
                    else if (wallItem.Component == WallParameters.Event)
                    {
                        beginWall.DisplayName = wallItem.Event.Owner.NameSurname;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescNewBooking;
                        beginWall.Description = wallItem.Event.Name;
                        beginWall.Date = wallItem.Event.StartDate + " - " + wallItem.Event.EndDate;
                    }
                    break;
                case WallParameters.NewEvent:

                    if(wallItem.Component ==  WallParameters.Groups){
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescNewEvent + TextGroup;
                        beginWall.Description = wallItem.Group.Name;
                        beginWall.Date = wallItem.Date;

                    } else if(wallItem.Component ==  WallParameters.Event){
                        beginWall.DisplayName = wallItem.Event.Owner.NameSurname;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescNewEvent;
                        beginWall.Description = wallItem.Event.Name;
                        beginWall.Date = wallItem.Event.StartDate + " -" +
                                         "" +
                                         " " + wallItem.Event.EndTime;
                    }
                    break;
                case WallParameters.NewMember:
                    beginWall.DisplayName = wallItem.User.DisplayName;
                    beginWall.ExtraText = "";
                    beginWall.DisplayNameTwo = "";
                    beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                    beginWall.Reason = WallParameters.DescNewMember;
                    beginWall.Description = null;
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.UpdatedProfile:
                    if (wallItem.Component == WallParameters.Profile)
                    {
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescUpdatedProfile;
                        beginWall.Description = null;
                        beginWall.Date = wallItem.Date;
                    }
                    break;
                case WallParameters.ActivityComment:
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescActivityComment;
                        beginWall.Description = wallItem.Content;
                        beginWall.Date = wallItem.Date;
                        break;
                case WallParameters.BbpTopicCreate:
                    if (wallItem.Component == WallParameters.Groups)
                    {
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescBbpTopicCreate + TextGroupTopic;
                        beginWall.Description = wallItem.Group.Name;
                        beginWall.Date = wallItem.Date;
                    }
                    break;
                case WallParameters.NewBlogPost:
                case WallParameters.NewBlogComment:
                case WallParameters.CbpReplyCreate:
                case WallParameters.Everything:
                default:
                    beginWall.DisplayName = "";
                    beginWall.ExtraText = "";
                    beginWall.DisplayNameTwo = "";
                    beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                    beginWall.Reason = "";
                    beginWall.Description = null;
                    beginWall.Date = "";
                    break;
            }
            return beginWall;
        }

        //protected override void OnAppearing ()
        //{
        //    base.OnAppearing ();
        //    var title = MasterTitle;

        //    MessagingCenter.Send (this, "masterTitle", title);
        //    MessagingCenter.Unsubscribe<WallPage, string>(this, "masterTitle");
        //}
    }


    public class WallParameters
    {
        public const string ActivityUpdate = "activity_update";
        public const string NewBlogPost = "new_blog_post";
        public const string NewBlogComment = "new_blog_comment";
        public const string CreatedGroup = "created_group";
        public const string JoinedGroup = "joined_group";
        public const string FriendshipAccepted = "friendship_accepted";
        public const string FriendshipCreated = "friendship_created";
        public const string NewMember = "new_member";
        public const string BbpTopicCreate = "bbp_topic_create";
        public const string CbpReplyCreate = "bbp_reply_create";
        public const string NewBooking = "new_booking";
        public const string NewEvent = "new_event";
        public const string RtmediaUpdate = "rtmedia_update";
        public const string UpdatedProfile = "updated_profile";
        public const string ActivityComment = "activity_comment";
        public const string Everything = "-1";
        

        public const string DescCreatedGroup = "Created the group";
        public const string DescActivityUpdate = "Update";
        public const string DescNewBlogPost = "Created the group";
        public const string DescNewBlogComment = "Created the group";
        public const string DescJoinedGroup = "Joined to the group";
        public const string DescFriendshipAccepted = "Now are friends";
        public const string DescFriendshipCreated = "Now are friends";
        public const string DescNewMember = "New member";
        public const string DescBbpTopicCreate = "Created";
        public const string DescCbpReplyCreate = "Created the group";
        public const string DescEverything = "";
        public const string DescNewBooking = "Create new booking";
        public const string DescNewEvent = "Create new event";
        public const string DescRtmediaUpdate = "Update";
        public const string DescUpdatedProfile = "Update profile";
        public const string DescActivityComment = "Comment to";

        public const string Activity = "activity";
        public const string Profile = "profile";
        public const string Groups = "groups";
        public const string Friends = "friends";
        public const string Event = "event";

        
    }
}
