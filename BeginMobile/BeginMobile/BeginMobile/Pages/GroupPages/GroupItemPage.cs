using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Pages.GroupPages
{
    class GroupItemPage : ContentPage
    {
        private Group _groupInformation;
        private const string Sections = "members";
        private const string GroupImage = "userdefault3.png";
        public GroupItemPage(Group group)
        {
            this.SetBinding(TitleProperty, "Name", stringFormat:"Group - {0}");

            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            _groupInformation = App.ProfileServices.GetGroup(currentUser.AuthToken, group.Id, Sections);

            if (_groupInformation == null)
            {
                return;
            }

            var groupDetail = GetGroupModel();

            var groupImg = new CircleImage()
            {
                BorderColor = Device.OnPlatform<Color>(iOS: Color.Black, Android: Color.White, WinPhone: Color.White),
                BorderThickness = Device.OnPlatform<int>(iOS: 2, Android: 3, WinPhone: 3),
                HeightRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),
                WidthRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),

                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Source = GroupImage
            };

            var gridMain = new Grid()
            {
                Padding = new Thickness(10, 5, 10, 5),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition{Height= GridLength.Auto},
                    new RowDefinition{Height= GridLength.Auto},
                    new RowDefinition{Height= GridLength.Auto},
                    new RowDefinition{Height= GridLength.Auto},
                }
                
            };

            var gridImage = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            var stackLayoutLinesLeft = new StackLayout()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    BoxViewLine(),
                    BoxViewLine(),
                }
            };

            var stackLayoutLinesRight = new StackLayout()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    BoxViewLine(),
                    BoxViewLine(),
                }
            };

            gridImage.Children.Add(stackLayoutLinesLeft, 0, 0);
            gridImage.Children.Add(groupImg, 1, 0);
            gridImage.Children.Add(stackLayoutLinesRight, 2,0);

            var lblStatusGroup = new Label()
            {
                YAlign = TextAlignment.End,
                Style = App.Styles.ListItemTextStyle,
                FontAttributes = FontAttributes.Bold,
                Text = groupDetail.StatusGroup,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var lblDateText = new Label()
            {
                YAlign = TextAlignment.End,
                Style = App.Styles.LabelTextDate,
                Text = groupDetail.TextActiveDate,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var lblDescription = new Label()
            {
                YAlign = TextAlignment.Center,
                XAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle,
                Text = groupDetail.Description,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var stackLayoutTitle = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Children =
                {
                    lblStatusGroup, lblDateText
                }
            };

            gridMain.Children.Add(gridImage, 0, 0);
            gridMain.Children.Add(stackLayoutTitle, 0, 1);
            gridMain.Children.Add(lblDescription, 0, 2);

            if (_groupInformation.Members != null && _groupInformation.Members.Count > 0)
            {
                var lblTitleMember = new Label()
                {
                    YAlign = TextAlignment.End,
                    Style = App.Styles.LabelLargeTextTitle,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start,
                    Text = "Members:"
                };

                var stackLayoutMembers = new StackLayout()
                {

                    Children =
                    {
                        lblTitleMember, GetListViewMembers(_groupInformation.Members)
                    }
                };

                gridMain.Children.Add(stackLayoutMembers, 0, 3);
            }

            Content = new ScrollView()
            {
                Content = gridMain
            };
        }

        private GroupViewModel GetGroupModel()
        {
            var groupViewModel = new GroupViewModel()
            {
                Name = _groupInformation.Name,
                StatusGroup = _groupInformation.Status,
                Description = _groupInformation.Description,
                TextDate = _groupInformation.DateCreated,
                TextActiveDate = _groupInformation.DateCreated
            };

            return groupViewModel;
        }

        private BoxView BoxViewLine()
        {
            return new BoxView() { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };
        }

        private ListView GetListViewMembers(List<User> members)
        {
            var listViewMembers = new ListView() { };

            listViewMembers.ItemTemplate = new DataTemplate(typeof(MemberItemCell));
            listViewMembers.ItemsSource = members;
            listViewMembers.HasUnevenRows = true;
            listViewMembers.ItemSelected += async (sender, e) =>
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
