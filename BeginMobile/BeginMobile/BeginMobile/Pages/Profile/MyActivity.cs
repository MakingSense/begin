using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.Wall;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class MyActivity : ContentPage
    {
        private readonly LoginUser _currentUser;
        private ProfileInformationActivities _profileActivity;
        private Grid _gridMainComponents;

        //For activity
        public const string TextAnd = "And";
        public const string TextGroup = " group";
        public const string TextGroupTopic = " group topic";
        public const string TextActivity = " activity";

        //Pagination
        private bool _isLoading;
        private int _offset = 0;
        private int _limit = DefaultLimit;
        private string _name;
        private string _sort;
        private const int DefaultLimit = 10;
        private bool _areLastItems;

        private ImageSource _imageSourceWallByDefault;

        private ObservableCollection<BeginWallViewModel> _listDataSource;

        public MyActivity()
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = AppResources.LabelActivityTitle;
            _currentUser = (LoginUser) BeginApplication.Current.Properties["LoginUser"];

            Init();
        }

        private async Task Init()
        {
            _profileActivity =
                await BeginApplication.ProfileServices.GetActivities(
                _currentUser.AuthToken, 
                _currentUser.User.UserName, 
                _limit.ToString(), 
                _offset.ToString());
            _listDataSource = ListBeginWallViewModel(_profileActivity);

            this._imageSourceWallByDefault = BeginApplication.Styles.DefaultWallIcon;

            /*if (_profileActivity != null)
            {
                var listActivityViewModel = from activity in _profileActivity.Activities
                    where activity.Component.Equals("activity", StringComparison.InvariantCultureIgnoreCase)
                    select new ActivityViewModel
                           {
                               Icon = BeginApplication.Styles.DefaultActivityIcon,
                               //TODO:change for activity avatar if this exist
                               NameSurname = _profileActivity.NameSurname,
                               ActivityDescription = activity.Content,
                               ActivityType = activity.Type,
                               DateAndTime = activity.Date
                           };

                foreach (var activityViewModel in listActivityViewModel)
                {
                    listDataSource.Add(activityViewModel);
                }
            }*/



            //var listViewTemplate = new DataTemplate(typeof(Activities));
            var listViewTemplate = new DataTemplate(() => new WallItemCell(_imageSourceWallByDefault));

            var listViewActivities = new ListView
                                     {
                                         ItemsSource = _listDataSource,
                                         ItemTemplate = listViewTemplate
                                     };

            listViewActivities.ItemSelected += (s, e) =>
                                               {
                                                   if (e.SelectedItem == null)
                                                   {
                                                       return;
                                                   }

                                                   ((ListView) s).SelectedItem = null;
                                               };

            listViewActivities.HasUnevenRows = true;


            _gridMainComponents = new Grid
            {
                Padding = BeginApplication.Styles.LayoutThickness,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      
                                  }
            };

            _gridMainComponents.Children.Add(listViewActivities, 0, 0);


            Content = _gridMainComponents;
        }

        #region "Set view model for activities"

        private ObservableCollection<BeginWallViewModel> ListBeginWallViewModel(ProfileInformationActivities profileActivity)
        {
            ObservableCollection<BeginWallViewModel> resultList = null;

            if (profileActivity != null && profileActivity.Activities != null)
            {
                resultList = new ObservableCollection<BeginWallViewModel>();

                foreach (var activity in profileActivity.Activities)
                {
                    var modelItem = GetBeginActivityViewModel(activity);
                    resultList.Add(modelItem);
                }
            }
            else
            {
                resultList = new ObservableCollection<BeginWallViewModel>(new List<BeginWallViewModel>());
            }

            return resultList;
        }

        private BeginWallViewModel GetBeginActivityViewModel(BeginMobile.Services.DTO.WallActivityItem wallItem)
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

                    if (wallItem.Component == WallParameters.Groups)
                    {
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

                    if (wallItem.Component == WallParameters.Groups)
                    {
                        beginWall.DisplayName = wallItem.User.DisplayName;
                        beginWall.ExtraText = "";
                        beginWall.DisplayNameTwo = "";
                        beginWall.Title = beginWall.DisplayName + " " + beginWall.ExtraText + " " + beginWall.DisplayNameTwo;

                        beginWall.Reason = WallParameters.DescNewEvent + TextGroup;
                        beginWall.Description = wallItem.Group.Name;
                        beginWall.Date = wallItem.Date;

                    }
                    else if (wallItem.Component == WallParameters.Event)
                    {
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
        #endregion

        public Grid GetGrid()
        {
            return _gridMainComponents;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.Content = null;
            _profileActivity = null;
        }
    }
}