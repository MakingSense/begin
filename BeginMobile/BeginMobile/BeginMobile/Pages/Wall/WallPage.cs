using BeginMobile.Interfaces;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using System;
using System.Collections.Generic;
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
        private List<BeginWallViewModel> _listWall;
        private bool _isLoading;

        public WallPage(string title, string iconImage)
            : base(title, iconImage)
        {
            _currentUser = (LoginUser)App.Current.Properties["LoginUser"];

            _stackLayoutMain = new StackLayout()
            {
                Spacing = 2,
                Padding = App.Styles.LayoutThickness,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions =LayoutOptions.FillAndExpand
            };

            _stackLayoutMain.Children.Add(CreateStackLayoutWithLoadingIndicator());
    
            Content = _stackLayoutMain;

            Init();
        }

        private List<BeginWallViewModel> ListBeginWallViewModel(List<BeginMobile.Services.DTO.Wall> oldListWall)
        {
            List<BeginWallViewModel> resultList = null; 

            if (oldListWall != null)
            {
                resultList = new List<BeginWallViewModel>();

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

            _profileShop = await App.ProfileServices.GetWall(_currentUser.AuthToken);
            _listWall = ListBeginWallViewModel(_profileShop.ListOfWall);
            //_listWall = new List<BeginWallViewModel>();

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

            /*_listViewWall.ItemAppearing += async (sender, e) =>
            {
                if (_isLoading || _listWall.Count == 0)
                    return;

                if (e.Item.ToString() == _listWall[_listWall.Count - 1].ToString())
                {
                    LoadItems();
                }
            };*/

            var relativeLayoutMain = new RelativeLayout() { VerticalOptions = LayoutOptions.FillAndExpand };
            relativeLayoutMain.Children.Add(_listViewWall,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            _stackLayoutMain.Children.Add(relativeLayoutMain);
            Content = _stackLayoutMain;

            //LoadItems();

            ActivityIndicatorLoading.IsRunning = false;
            ActivityIndicatorLoading.IsVisible = false;
        }

        /*private async Task LoadItems()
        {
            _isLoading = true;

            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                _listWall.AddRange(ListBeginWallViewModel(_profileShop.ListOfWall));
                _isLoading = false;
                return false;
            });
        }*/

        private BeginWallViewModel GetBeginWallViewModel(BeginMobile.Services.DTO.Wall wallItem)
        {
            var beginWall = new BeginWallViewModel();

            switch (wallItem.Type)
            {
                case WallParameters.CreatedGroup:
                    beginWall.DisplayName = wallItem.User.DisplayName;
                    beginWall.ExtraText = "";
                    beginWall.DisplayNameTwo = "";
                    beginWall.Reason = WallParameters.DescCreatedGroup;
                    beginWall.Description = wallItem.Group.Name;
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.JoinedGroup:
                    beginWall.DisplayName = wallItem.User.DisplayName;
                    beginWall.ExtraText = "";
                    beginWall.DisplayNameTwo = "";
                    beginWall.Reason = WallParameters.DescJoinedGroup;
                    beginWall.Description = wallItem.Group.Name;
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.RtmediaUpdate:
                    beginWall.DisplayName = wallItem.User.DisplayName;
                    beginWall.ExtraText = "";
                    beginWall.DisplayNameTwo = "";
                    beginWall.Reason = WallParameters.DescRtmediaUpdate;
                    beginWall.Description = wallItem.Group.Name;
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.ActivityUpdate:

                    if(wallItem.Component == WallParameters.Groups){
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Reason = WallParameters.DescActivityUpdate + " group";
                        beginWall.Description = wallItem.Group.Name;
                        beginWall.Date = wallItem.Date;
                    }
                    else if (wallItem.Component == WallParameters.Activity)
                    {
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Reason = WallParameters.DescActivityUpdate + " activity";
                        beginWall.Description = wallItem.Content;
                        beginWall.Date = wallItem.Date;
                    }
                    else
                    {
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Reason = WallParameters.DescActivityUpdate;
                        beginWall.Description = "";
                        beginWall.Date = wallItem.Date;
                    }
                    break;
                case WallParameters.FriendshipCreated:
                case WallParameters.FriendshipAccepted:
                    beginWall.DisplayName = wallItem.User1.DisplayName;
                    beginWall.ExtraText = "and";
                    beginWall.DisplayNameTwo = wallItem.User2.DisplayName;
                    beginWall.Reason = "";
                    beginWall.Description = WallParameters.DescFriendshipAccepted;
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.NewBooking:
                    if (wallItem.Component == WallParameters.Groups)
                    {
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Reason = WallParameters.DescNewBooking + " group";
                        beginWall.Description = wallItem.Group.Name;
                        beginWall.Date = wallItem.Date;
                    }
                    else if (wallItem.Component == WallParameters.Event)
                    {
                        beginWall.DisplayName = wallItem.Event.Owner.NameSurname;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
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
                        beginWall.Reason = WallParameters.DescNewEvent +" group";
                        beginWall.Description = wallItem.Group.Name;
                        beginWall.Date = wallItem.Date;

                    } else if(wallItem.Component ==  WallParameters.Event){
                        beginWall.DisplayName = wallItem.Event.Owner.NameSurname;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
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
                    beginWall.Reason = WallParameters.DescNewMember;
                    beginWall.Description = "";
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.UpdatedProfile:
                    if (wallItem.Component == WallParameters.Profile)
                    {
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Reason = WallParameters.DescUpdatedProfile;
                        beginWall.Description = "";
                        beginWall.Date = wallItem.Date;
                    }
                    break;
                case WallParameters.ActivityComment:
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
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
                    beginWall.Reason = "";
                    beginWall.Description = "";
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
        

        public const string DescCreatedGroup = "created the group";
        public const string DescActivityUpdate = "update";
        public const string DescNewBlogPost = "created the group";
        public const string DescNewBlogComment = "created the group";
        public const string DescJoinedGroup = "joined to the group";
        public const string DescFriendshipAccepted = "now are friends";
        public const string DescFriendshipCreated = "now are friends";
        public const string DescNewMember = "new member";
        public const string DescBbpTopicCreate = "created";
        public const string DescCbpReplyCreate = "created the group";
        public const string DescEverything = "";
        public const string DescNewBooking = "create new booking";
        public const string DescNewEvent = "create new event";
        public const string DescRtmediaUpdate = "update";
        public const string DescUpdatedProfile = "update profile";
        public const string DescActivityComment = "comment to";

        public const string Activity = "activity";
        public const string Profile = "profile";
        public const string Groups = "groups";
        public const string Friends = "friends";
        public const string Event = "event";
    }
}
