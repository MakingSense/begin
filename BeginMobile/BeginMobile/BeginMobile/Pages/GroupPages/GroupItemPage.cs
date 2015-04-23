using System.Collections.Generic;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace BeginMobile.Pages.GroupPages
{
    class GroupItemPage : ContentPage
    {
        private Group _groupInformation;
        private const string Sections = "members";
        private const string GroupImage = "userdefault3.png";
        private LoginUser _currentUser;
        private Group _groupItem;
        public GroupItemPage(Group group)
        {
            this.SetBinding(TitleProperty, "Name", stringFormat: "Group - {0}");
            _groupItem = group;
            _currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            Init();
        }

        private async Task Init()
        {
            _groupInformation = await BeginApplication.ProfileServices.GetGroup(_currentUser.AuthToken, _groupItem.Id, Sections);

            if (_groupInformation == null)
            {
                _groupInformation = new Group();
            }

            var groupDetail = GetGroupModel();

            var circleGroupImage = new CircleImage
            {
                BorderColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                BorderThickness = Device.OnPlatform(2, 3, 3),
                HeightRequest = Device.OnPlatform(50, 100, 100),
                WidthRequest = Device.OnPlatform(50, 100, 100),

                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Source = GroupImage
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
            gridImage.Children.Add(circleGroupImage, 1, 0);
            gridImage.Children.Add(stackLayoutLinesRight, 2, 0);

            var labelStatusGroup = new Label
            {
                YAlign = TextAlignment.End,
                Style = BeginApplication.Styles.ListItemTextStyle,
                FontAttributes = FontAttributes.Bold,
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

            gridMain.Children.Add(gridImage, 0, 0);
            gridMain.Children.Add(stackLayoutTitle, 0, 1);
            gridMain.Children.Add(labelDescription, 0, 2);

            if (_groupInformation.Members != null && _groupInformation.Members.Count > 0)
            {
                var labelTitleMember = new Label
                {
                    YAlign = TextAlignment.End,
                    Style = BeginApplication.Styles.LabelLargeTextTitle,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start,
                    Text = "Members:"
                };

                var stackLayoutMembers = new StackLayout
                {
                    Spacing = 2,
                    Padding = BeginApplication.Styles.LayoutThickness,
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
            return new BoxView { Color = BeginApplication.Styles.ColorLine, WidthRequest = 100, HeightRequest = 2 };
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

                ((ListView)sender).SelectedItem = null;
            };

            return listViewMembers;
        }
    }
}