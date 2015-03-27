using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.Wall
{
    public class WallPage : TabContent
    {
        private ListView _lViewWall;
        private RelativeLayout _rLayoutMain;

        public WallPage(string title, string iconImg)
            : base(title, iconImg)
        {
            //Do something
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            ProfileMeWall profileShop = App.ProfileServices.GetWall(currentUser.AuthToken);
            var listWall = NewListWall(profileShop.ListOfWall);

            _lViewWall = new ListView
                         {
                             StyleId = "WallList"
                         };

            _lViewWall.HasUnevenRows = true;
            _lViewWall.ItemTemplate = new DataTemplate(typeof(WallItemCell));
            _lViewWall.ItemsSource = listWall;
            _lViewWall.ItemSelected += async (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };

            _rLayoutMain = new RelativeLayout();
            _rLayoutMain.Children.Add(_lViewWall,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            Content = new ScrollView() { Content = _rLayoutMain };

        }

        private List<BeginWallViewModel> NewListWall(List<BeginMobile.Services.DTO.Wall> oldListWall)
        {
            List<BeginWallViewModel> resultList = null; 

            if (oldListWall != null)
            {
                resultList = new List<BeginWallViewModel>();

                foreach (var wall in oldListWall)
                {
                    var modelItem = GetModel(wall);
                    resultList.Add(modelItem);
                }
            }

            return resultList;
        }

        private BeginWallViewModel GetModel(BeginMobile.Services.DTO.Wall wallItem)
        {
            var beginWall = new BeginWallViewModel();

            switch (wallItem.Type)
            {
                case WallParameters.CreatedGroup:
                    beginWall.Name = wallItem.User.DisplayName;
                    beginWall.ExtraText = "";
                    beginWall.Reason = WallParameters.DescCreatedGroup;
                    beginWall.Description = "Group " + new Random().Next(0, 100);
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.ActivityUpdate:
                    beginWall.Name = wallItem.User.DisplayName;
                    beginWall.ExtraText = "";
                    beginWall.Reason = WallParameters.DescActivityUpdate;
                    beginWall.Description = "";
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.FriendshipCreated:
                case WallParameters.FriendshipAccepted:
                    beginWall.Name = wallItem.User1.DisplayName;
                    beginWall.ExtraText = "and";
                    beginWall.Reason = wallItem.User2.DisplayName;
                    beginWall.Description = WallParameters.DescFriendshipAccepted;
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.NewBooking:
                    beginWall.Name = wallItem.Event.Owner.DisplayName;
                    beginWall.ExtraText = "";
                    beginWall.Reason = WallParameters.DescNewBooking;
                    beginWall.Description = wallItem.Event.Name;
                    beginWall.Date = wallItem.Date;
                    break;
                case WallParameters.NewBlogPost:
                case WallParameters.NewBlogComment:
                case WallParameters.JoinedGroup:
                case WallParameters.NewMember:
                case WallParameters.BbpTopicCreate:
                case WallParameters.CbpReplyCreate:
                case WallParameters.Everything:
                    beginWall.Name = "";
                    beginWall.ExtraText = "";
                    beginWall.Reason = "";
                    beginWall.Description = "";
                    beginWall.Date = "";
                    break;
                default:
                    beginWall.Name = "";
                    beginWall.ExtraText = "";
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
        public const string Everything = "-1";

        public const string DescCreatedGroup = "created the group";
        public const string DescActivityUpdate = "posted an update";
        public const string DescNewBlogPost = "created the group";
        public const string DescNewBlogComment = "created the group";
        public const string DescJoinedGroup = "created the group";
        public const string DescFriendshipAccepted = "now are friends";
        public const string DescFriendshipCreated = "now are friends";
        public const string DescNewMember = "created the group";
        public const string DescBbpTopicCreate = "created the group";
        public const string DescCbpReplyCreate = "created the group";
        public const string DescEverything = "";
        public const string DescNewBooking = "create new booking";

    }
}
