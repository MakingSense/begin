using System.Collections.Generic;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace BeginMobile.Pages.GroupPages
{
    internal class GroupItemPage : ContentPage
    {
        private Group _groupInformation;
        private const string Sections = "members";
        private LoginUser _currentUser;
        private Group _groupItem;

        public GroupItemPage(Group group)
        {
            Style = BeginApplication.Styles.PageStyle;
            this.SetBinding(TitleProperty, "Name", stringFormat: "Group - {0}");
            _groupItem = group;
            _currentUser = (LoginUser) BeginApplication.Current.Properties["LoginUser"];

            Init();
        }

        private async Task Init()
        {
            _groupInformation =
                await BeginApplication.ProfileServices.GetGroup(_currentUser.AuthToken, _groupItem.Id, Sections) ??
                new Group();

            var groupDetail = GetGroupModel();

            ImageSource imageResourceGroup = Device.OS == TargetPlatform.iOS
                ? BeginApplication.Styles.DefaultGroupIcon
                : BeginApplication.Styles.DefaultGroupIcon;

                        var groupBannerImage = new Image
                                   {
                                       Style = BeginApplication.Styles.SquareImageStyle,
                                       Source = imageResourceGroup,
                                       //TODO:change for group avatar if this exist
                                   };

            var gridMain = new Grid
                           {
                               Padding = new Thickness(10, 5, 10, 5),
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               RowDefinitions =
                               {
                                   new RowDefinition {Height = GridLength.Auto},
                                   new RowDefinition {Height = GridLength.Auto},
                                   new RowDefinition {Height = GridLength.Auto},
                                   new RowDefinition {Height = GridLength.Auto}
                               }
                           };

            var gridImage = new Grid
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand
                            };

            var stackLayoutLinesLeft = new StackLayout
                                       {
                                           VerticalOptions = LayoutOptions.CenterAndExpand,
                                           HorizontalOptions = LayoutOptions.FillAndExpand,
                                           Children =
                                           {
                                               BoxViewLine(),
                                               BoxViewLine()
                                           }
                                       };

            var stackLayoutLinesRight = new StackLayout
                                        {
                                            VerticalOptions = LayoutOptions.CenterAndExpand,
                                            HorizontalOptions = LayoutOptions.FillAndExpand,
                                            Children =
                                            {
                                                BoxViewLine(),
                                                BoxViewLine()
                                            }
                                        };

            gridImage.Children.Add(stackLayoutLinesLeft, 0, 0);
            gridImage.Children.Add(groupBannerImage, 1, 0);
            gridImage.Children.Add(stackLayoutLinesRight, 2, 0);

            var labelStatusGroup = new Label
                                   {
                                       YAlign = TextAlignment.End,
                                       Style = BeginApplication.Styles.ListItemTextStyle,
                                      
                                       Text = groupDetail.StatusGroup,
                                       HorizontalOptions = LayoutOptions.FillAndExpand
                                   };

            var labelDateText = new Label
                                {
                                    YAlign = TextAlignment.End,
                                    Style = BeginApplication.Styles.LabelTextDate,
                                    Text = groupDetail.TextActiveDate,
                                    HorizontalOptions = LayoutOptions.FillAndExpand
                                };

            var labelDescription = new Label
                                   {
                                       YAlign = TextAlignment.Center,
                                       XAlign = TextAlignment.Center,
                                       Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                       Text = groupDetail.Description,
                                       HorizontalOptions = LayoutOptions.FillAndExpand
                                   };

            var stackLayoutTitle = new StackLayout
                                   {
                                       Orientation = StackOrientation.Horizontal,
                                       HorizontalOptions = LayoutOptions.CenterAndExpand,
                                       VerticalOptions = LayoutOptions.Start,
                                       Children =
                                       {
                                           labelStatusGroup,
                                           labelDateText
                                       }
                                   };

            gridMain.Children.Add(groupBannerImage, 0, 0);
            gridMain.Children.Add(stackLayoutTitle, 0, 1);
            gridMain.Children.Add(labelDescription, 0, 2);

            if (_groupInformation.Members != null && _groupInformation.Members.Count > 0)
            {
                var labelTitleMember = new Label
                                       {
                                           YAlign = TextAlignment.End,
                                           Style = BeginApplication.Styles.LabelLargeTextTitle,                                          
                                           HorizontalOptions = LayoutOptions.Start,
                                           Text = "Members:"
                                       };

                var stackLayoutMembers = new StackLayout
                                         {
                                             Spacing = 2,
                                             Padding = BeginApplication.Styles.ThicknessMainLayout,
                                             Children =
                                             {
                                                 labelTitleMember,
                                                 GetListViewMembers(_groupInformation.Members)
                                             }
                                         };

                gridMain.Children.Add(stackLayoutMembers, 0, 3);
            }

            Content = new ScrollView
                      {
                          Content = gridMain
                      };
        }

        private GroupViewModel GetGroupModel()
        {
            var groupViewModel = new GroupViewModel
                                 {
                                     Name = _groupInformation.Name,
                                     StatusGroup = _groupInformation.Status,
                                     Description = _groupInformation.Description,
                                     TextDate = _groupInformation.DateCreated,
                                     TextActiveDate = _groupInformation.DateCreated
                                 };

            return groupViewModel;
        }

        private static BoxView BoxViewLine()
        {
            return new BoxView {Color = BeginApplication.Styles.ColorLine, WidthRequest = 100, HeightRequest = 2};
        }

        private static ListView GetListViewMembers(IEnumerable<User> members)
        {
            var listViewMembers = new ListView
                                  {
                                      ItemTemplate = new DataTemplate(typeof (MemberItemCell)),
                                      ItemsSource = members,
                                      HasUnevenRows = true
                                  };

            listViewMembers.ItemSelected += (sender, e) =>
                                            {
                                                if (e.SelectedItem == null)
                                                {
                                                    return;
                                                }

                                                ((ListView) sender).SelectedItem = null;
                                            };

            return listViewMembers;
        }
    }
}