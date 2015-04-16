using BeginMobile.Interfaces;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.Wall
{
    public class WallPage : TabContent
    {
        private ListView _listViewWall;
        private ProfileMeWall _profileShop;
        private LoginUser _currentUser;
        private StackLayout _stackLayoutMain;
        private ObservableCollection<BeginWallViewModel> _listWall;
        private bool _isLoading;
        private int _offset = 0;
        private int _limit = 10;
        private Button _buttonAddMore;
        private Grid _gridMain;


        public WallPage(string title, string iconImage)
            : base(title, iconImage)
        {
            _currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            _gridMain = new Grid()
            {
                Padding = new Thickness(10, 0, 10, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = 50 }
                }
            };

            _gridMain.Children.Add(CreateStackLayoutWithLoadingIndicator(), 0, 1);
            Content = _gridMain;
            Init();
        }

        private ObservableCollection<BeginWallViewModel> ListBeginWallViewModel(List<BeginMobile.Services.DTO.Wall> oldListWall)
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

            return resultList;
        }

        private async Task Init()
        {
            ActivityIndicatorLoading.IsRunning = true;
            ActivityIndicatorLoading.IsVisible = true;

            _profileShop = await BeginApplication.ProfileServices.GetWall(_currentUser.AuthToken, limit: _limit.ToString(), offset: _offset.ToString());
            _listWall = ListBeginWallViewModel(_profileShop.ListOfWall);

            

            _listViewWall = new ListView
            {
                StyleId = "WallList"
            };

            _listViewWall.HasUnevenRows = true;
            _listViewWall.ItemTemplate = new DataTemplate(() => new WallItemCell());
            _listViewWall.ItemsSource = _listWall;
            _listViewWall.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null) return;
                ((ListView)sender).SelectedItem = null;
            };

            _listViewWall.ItemAppearing += async (sender, e) =>
            {
                if (_isLoading || _listWall.Count == 0)
                    return;

                var appearingItem = (BeginWallViewModel) e.Item;
                var lastItem = _listWall[_listWall.Count - 1];

                if ((appearingItem.ItemId == lastItem.ItemId) &&
                    (appearingItem.Type == lastItem.Type))
                {
                    await LoadItems();
                }
            };

            var relativeLayoutMain = new RelativeLayout() { VerticalOptions = LayoutOptions.FillAndExpand };
            relativeLayoutMain.Children.Add(_listViewWall,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            ActivityIndicatorLoading.IsRunning = false;
            ActivityIndicatorLoading.IsVisible = false;

            _gridMain.Children.Add(relativeLayoutMain, 0, 0);

            Content = _gridMain;

        }

        private async Task LoadItems()
        {
            _offset +=_limit;

            _isLoading = true;

            ActivityIndicatorLoading.IsRunning = true;
            ActivityIndicatorLoading.IsVisible = true;

            _profileShop = await BeginApplication.ProfileServices.GetWall(_currentUser.AuthToken, limit: _limit.ToString(), offset: _offset.ToString());

            if (_profileShop != null && _profileShop.ListOfWall.Count > 0)
            {
                Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                {
                    foreach (var beginWallViewModel in ListBeginWallViewModel(_profileShop.ListOfWall))
                    {
                        _listWall.Add(beginWallViewModel);
                    }


                    ActivityIndicatorLoading.IsRunning = false;
                    ActivityIndicatorLoading.IsVisible = false;

                    _isLoading = false;
                    return false;
                });
                
            }
            else
            {
                ActivityIndicatorLoading.IsRunning = false;
                ActivityIndicatorLoading.IsVisible = false;
                _isLoading = false;
            }
        }

        private BeginWallViewModel GetBeginWallViewModel(BeginMobile.Services.DTO.Wall wallItem)
        {
            var beginWall = new BeginWallViewModel()
            {
                ItemId = wallItem.ItemId,
                Component = wallItem.Component,
                Type = wallItem.Type
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

                        beginWall.Reason = WallParameters.DescActivityUpdate + " group";
                        beginWall.Description = wallItem.Group.Name;
                        beginWall.Date = wallItem.Date;
                    }
                    else if (wallItem.Component == WallParameters.Activity)
                    {
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescActivityUpdate + " activity";
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
                    beginWall.ExtraText = "And";
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

                        beginWall.Reason = WallParameters.DescNewBooking + " group";
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

                        beginWall.Reason = WallParameters.DescNewEvent +" group";
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

                        beginWall.Reason = WallParameters.DescBbpTopicCreate + " group topic";
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

        public const string And = "And";
    }
}
