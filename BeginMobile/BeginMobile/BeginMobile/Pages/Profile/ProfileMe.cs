using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class ProfileMe : ContentPage
    {
        private Grid _detailsGridLayout ;
        private Grid _gridMenuButtons;
        private Grid _gridResultContainer;
        private ScrollView _mainScrollView;
        private Button _buttonGroups;
        private Button buttonInformation;
        private Button buttonOthers;
        private ImageSource _imageSourceGroupByDefault;
        private ProfileInformationGroups _groupInformation;
        private LoginUser currentUser;
        private BoxView boxViewButtonSelectedInfo;
        private BoxView boxViewButtonSelectedGroups;
        private BoxView boxViewButtonSelectedOthers;

        private List<Group> _defaultGroups = new List<Group>();

        private ObservableCollection<Group> _groups;

        public ProfileMe(User user)
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = AppResources.LabelProfileMeTitle;
            currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];          
            //Toolbar menu item
            InitToolBar();

            //Content
            InitializeComponents(user);
        }

        private void InitializeComponents(User user)
        {
            //if (user != null)
            //{
            //    var userAvatarUrl = user.Avatar;

            //    if (!string.IsNullOrEmpty(userAvatarUrl))
            //    {
            //        userAvatar = userAvatarUrl;
            //    }
            //}
            
            
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
                Text = "Web Developer",//user.Profession,
                HorizontalOptions = LayoutOptions.Center,
                Style = BeginApplication.Styles.SubtitleStyle
            };

            var labelDirection = new Label
            {
                Text = "@"+user.UserName,
                HorizontalOptions = LayoutOptions.Center,
                Style = BeginApplication.Styles.TextBodyStyle
            };
            var labelRating = new Label
            {
                Text = "*****",
                HorizontalOptions = LayoutOptions.Center,
                Style = BeginApplication.Styles.TitleStyle
            };
            var test = Width/2;
            _detailsGridLayout = new Grid
                                 {
                                     MinimumHeightRequest = Width/2,
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
            _detailsGridLayout.Children.Add(circleProfileImage,0,0);
            _detailsGridLayout.Children.Add(labelName,0,1);
            _detailsGridLayout.Children.Add(labelJob,0,2);
            _detailsGridLayout.Children.Add(labelDirection,0,3);
            _detailsGridLayout.Children.Add(labelRating, 0, 4);


            // Buttons

            _buttonGroups = new Button { Text = "Groups", Style = BeginApplication.Styles.LinkButton};
            buttonInformation = new Button
            {
                Text = "Information",
                Style = BeginApplication.Styles.LinkButton,
                
            };
            buttonOthers = new Button { Text = "ooo", Style = BeginApplication.Styles.LinkButton};
            _gridMenuButtons = new Grid
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
                                         new ColumnDefinition {Width = GridLength.Auto}
                                     }
            };
            boxViewButtonSelectedInfo = new BoxView { Color = Color.Blue, WidthRequest = 100, HeightRequest = 3, IsVisible = false};
            boxViewButtonSelectedGroups = new BoxView { Color = Color.Blue, WidthRequest = 100, HeightRequest = 3, IsVisible = false };
            boxViewButtonSelectedOthers = new BoxView { Color = Color.Blue, WidthRequest = 100, HeightRequest = 3, IsVisible = false };

            _gridMenuButtons.Children.Add(buttonInformation, 0, 0);
            _gridMenuButtons.Children.Add(boxViewButtonSelectedInfo, 0, 1);
            _gridMenuButtons.Children.Add(_buttonGroups, 1, 0);
            _gridMenuButtons.Children.Add(boxViewButtonSelectedGroups, 1, 1);
            _gridMenuButtons.Children.Add(buttonOthers, 2, 0);
            _gridMenuButtons.Children.Add(boxViewButtonSelectedOthers, 2, 1);
            _gridResultContainer = new Grid();


            _buttonGroups.Clicked += GroupEventHandler;
            buttonInformation.Clicked += InformationEventHandler;
            buttonOthers.Clicked += OtherEventHadler;

            _mainScrollView = new ScrollView
                      {
                          Content = new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.StartAndExpand,
                                        Orientation = StackOrientation.Vertical,
                                        Children =
                                        {
                                            _detailsGridLayout,
                                            _gridMenuButtons,
                                            _gridResultContainer
                                        }
                                    }
                      };

            Content = _mainScrollView;
        }

        private async void GroupEventHandler(object sender, EventArgs e)
        {
            LoadDeafultImage();
            var thisButton = sender as Button;
            thisButton.TextColor = Color.Black;
            boxViewButtonSelectedInfo.IsVisible = false;
            boxViewButtonSelectedGroups.IsVisible = true;
            boxViewButtonSelectedOthers.IsVisible = false;

            buttonInformation.TextColor = BeginApplication.Styles.DefaultColorButton;
            buttonOthers.TextColor = BeginApplication.Styles.DefaultColorButton;


            _groupInformation = await BeginApplication.ProfileServices.GetGroups(currentUser.User.UserName,
                currentUser.AuthToken);

            _groups = _groupInformation != null ? _groupInformation.Groups :
                new ObservableCollection<Group>(_defaultGroups);

            _gridResultContainer = null;
            _mainScrollView = null;
            _gridResultContainer = new Grid();

            var _listViewGroup = new ListView
            {
                ItemTemplate = new DataTemplate(() => new ProfileGroupItemCell(_imageSourceGroupByDefault)),
                ItemsSource = _groups,
                HasUnevenRows = true
            };
            _listViewGroup.ItemSelected += LisViewGroupsItemSelected;
            _gridResultContainer.Children.Add(_listViewGroup);
            
            _mainScrollView = new ScrollView
                              {
                                  Content = new StackLayout
                                            {
                                                VerticalOptions = LayoutOptions.StartAndExpand,
                                                Orientation = StackOrientation.Vertical,
                                                Children =
                                                {
                                                    _gridMenuButtons,
                                                    _gridResultContainer
                                                }
                                            }
                              };
            Content = _mainScrollView;
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


        private void InformationEventHandler(object sender, EventArgs e)
        {
            var thisButton = sender as Button;
            thisButton.TextColor = Color.Black;

            _buttonGroups.TextColor = BeginApplication.Styles.DefaultColorButton;
            buttonOthers.TextColor = BeginApplication.Styles.DefaultColorButton;
            boxViewButtonSelectedInfo.IsVisible = true;
            boxViewButtonSelectedGroups.IsVisible = false;
            boxViewButtonSelectedOthers.IsVisible = false;

            _detailsGridLayout.IsVisible = false;
           
            _gridResultContainer = null;
            _mainScrollView = null;
            _gridResultContainer = new Grid();

            var information = new Information();
            if(information.GetGridInfo()!=null)
            {
                _gridResultContainer = information.GetGridInfo();
            }
            
            _mainScrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Orientation = StackOrientation.Vertical,
                    Children =
                                        {
                                            _gridMenuButtons,
                                            _gridResultContainer
                                        }
                }

            };
            Content = _mainScrollView;
        }

        private void OtherEventHadler(object sender, EventArgs e)
        {
            _gridResultContainer = null;
            _mainScrollView = null;
            _gridResultContainer = new Grid();

            var thisButton = sender as Button;
            thisButton.TextColor = Color.Black;
            boxViewButtonSelectedInfo.IsVisible = false;
            boxViewButtonSelectedGroups.IsVisible = false;
            boxViewButtonSelectedOthers.IsVisible = true;

            _mainScrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Orientation = StackOrientation.Vertical,
                    Children =
                                        {
                                            _gridMenuButtons,
                                            _gridResultContainer
                                        }
                }

            };
            Content = _mainScrollView;

            buttonInformation.TextColor = BeginApplication.Styles.DefaultColorButton;
            _buttonGroups.TextColor = BeginApplication.Styles.DefaultColorButton;
            var action = DisplayActionSheet("Options", "Cancel", "Destrucction", "Activities", "Shop", "Events",
                "Messages");

        }

        private void InitToolBar()
        {
            var toolBarItemMyActivity = new ToolbarItem
                                 {
                                     Icon = "Icon.png",
                                     Text = "",//AppResources.ToolBarProfileMeMyAct,
                                     Order = ToolbarItemOrder.Primary,
                                     Command = new Command(() => Navigation.PushAsync(new MyActivity()))
                                 };

            var toolBarItemInformation = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeInfo,
                Order = ToolbarItemOrder.Primary,
                Command = new Command(() => Navigation.PushAsync(new Information()))
            };

            var toolBarItemMessages = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeMessages,
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Messages()))
            };

            var toolBarItemContacts = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeContacts,
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Contacts()))
            };

            var toolBarItemGroups = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeGroups,
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Groups()))
            };

            var toolBarItemShop = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeShop,
                Order = ToolbarItemOrder.Primary,
                Command = new Command(() => Navigation.PushAsync(new Shop()))
            };

            var toolBarItemEvents = new ToolbarItem
            {
                Icon = "",
                Text = AppResources.ToolBarProfileMeEvents,
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Events()))
            };

            ToolbarItems.Add(toolBarItemMyActivity);
            ToolbarItems.Add(toolBarItemInformation);
            ToolbarItems.Add(toolBarItemMessages);
            ToolbarItems.Add(toolBarItemContacts);
            ToolbarItems.Add(toolBarItemGroups);
            ToolbarItems.Add(toolBarItemShop);
            ToolbarItems.Add(toolBarItemEvents);

        }

        private async void LisViewGroupsItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var groupItem = (Group)e.SelectedItem;
            var groupPage = new GroupItemPage(groupItem) { BindingContext = groupItem };
            await Navigation.PushAsync(groupPage);

            // clears the 'selected' background
            ((ListView)sender).SelectedItem = null;
        }
    }
}