using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Abstractions;
using BeginMobile.Pages;
using BeginMobile.Accounts;
using BeginMobile.MenuProfile;
using BeginMobile.Utils;
using BeginMobile.Pages.Profile;
using BeginMobile.Services.DTO;
using BeginMobile.Interfaces;

namespace BeginMobile.Menu
{
    public class Menu : ContentPage
    {
        private const string DefaultUri = "http://www.americanpresidents.org/images/01_150.gif";

        private const string pProfileMenuIcon = "userprofile.png";
        private const string knocks = "padlock.png";

        private string pUserDefault
        {
            get
            {
                return Device.OS == TargetPlatform.iOS ? "Contact.png" : "userprofile.png";
            }
        }

        private readonly Action _onToggleRequest;

        public Menu(Action onToggleRequest)
        {

            BackgroundColor = App.Styles.MenuBackground;
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];

            bool isLoadByLogin = false;
            _onToggleRequest = onToggleRequest;

            Title = "Menu";
            Icon = Device.OS == TargetPlatform.iOS ? "More.png" : null;

            var userImage = new ImageCell
            {
                ImageSource =
                    ImageSource.FromFile(pUserDefault),
                Text = currentUser.User.DisplayName,
                Detail = currentUser.User.Email

            };

            var userInfoTableView = new TableView
            {
                BackgroundColor = App.Styles.MenuBackground,
                HeightRequest = 180,
                Root = new TableRoot
                                                       {
                                                           new TableSection
                                                               {
                                                                   userImage
                                                               },
                                                       }
            };

            var menuItemList = new List<ConfigurationMenuItems>
                                   {                                       
                                       new ConfigurationMenuItems {OptionName = Items.Profile.ToString(), Icon = pProfileMenuIcon},
                                      
                                       new ConfigurationMenuItems {OptionName = Items.Knocks.ToString(), Icon = knocks}
                                   };


            var cell = new DataTemplate(typeof(CustomMenuItemTemplateCell));

            var menu = new ListView
            {
                BackgroundColor = App.Styles.MenuBackground,
                HeightRequest = 150,
                ItemsSource = menuItemList,
                ItemTemplate = cell,

                //ItemsSource = menuItems,
            };



            menu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                var item = (ConfigurationMenuItems)e.SelectedItem;
                var itemPageProfile = new ProfileMe(currentUser.User);
                var itemPageKnocks = new ContentPage { Title = "Knocks" };

                if (item.OptionName.Equals(Items.Profile.ToString()))
                {
                    await Navigation.PushAsync(itemPageProfile);
                }
                else if (item.OptionName.Equals(Items.Knocks.ToString()))
                {
                    await Navigation.PushAsync(itemPageKnocks);
                }

                ((ListView)sender).SelectedItem = null;
                _onToggleRequest();
            };


            var listButtonsData = new List<ConfigurationMenuItems>();
            listButtonsData.Add(new ConfigurationMenuItems{OptionName=MenuItemsNames.Logout});
            listButtonsData.Add(new ConfigurationMenuItems{Icon= "", OptionName=MenuItemsNames.ChangePassword});
            listButtonsData.Add(new ConfigurationMenuItems{Icon= "", OptionName=MenuItemsNames.About});
            listButtonsData.Add(new ConfigurationMenuItems{Icon= "", OptionName=MenuItemsNames.Privacy});
            listButtonsData.Add(new ConfigurationMenuItems{Icon= "", OptionName=MenuItemsNames.HelpCenter});
            listButtonsData.Add(new ConfigurationMenuItems{Icon= "", OptionName=MenuItemsNames.TermsAndConditions});
            listButtonsData.Add(new ConfigurationMenuItems {Icon = "", OptionName = MenuItemsNames.UpdateProfile });

            var listViewOptionButtons = new ListView
            {
                ItemsSource = listButtonsData,
                ItemTemplate = cell
            };

            listViewOptionButtons.ItemSelected += async (s, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                string item = ((ConfigurationMenuItems)e.SelectedItem).OptionName;

                switch (item)
                {
                    case MenuItemsNames.Logout:
                        App.Current.Logout();
                        break;
                    case MenuItemsNames.ChangePassword:
                        await Navigation.PushAsync(new ChangePasswordPage());
                        _onToggleRequest();
                        break;
                    case MenuItemsNames.About:
                        await Navigation.PushAsync(new AboutUs());
                        _onToggleRequest();
                        break;
                    case MenuItemsNames.Privacy:
                        await Navigation.PushAsync(new Privacy());
                        _onToggleRequest();
                        break;
                    case MenuItemsNames.HelpCenter:
                        await Navigation.PushAsync(new HelpCenter());
                        _onToggleRequest();
                        break;
                    case MenuItemsNames.TermsAndConditions:
                        await Navigation.PushAsync(new TermsAndConditions(isLoadByLogin));
                        _onToggleRequest();
                        break;
                    case MenuItemsNames.UpdateProfile:
                        await Navigation.PushAsync(new UpdateProfilePage());
                        _onToggleRequest();
                        break;
                }
                ((ListView)s).SelectedItem = null;
            };
            listViewOptionButtons.HasUnevenRows = true;
            

            //var controlButtonStyle = App.Styles.LinkButton;

            ////controls buttons
            //var buttonLogout = new Button
            //{
            //    Text = "Logout",
            //    Style = controlButtonStyle
            //};

            //var buttonChangePassword = new Button
            //{
            //    Text = "Change your password",
            //    Style = controlButtonStyle
            //};

            //var buttonAbout = new Button
            //{
            //    Text = "About",
            //    Style = controlButtonStyle
            //};
            //var buttonPrivacy = new Button
            //{
            //    Text = "Privacy",
            //    Style = controlButtonStyle
            //};
            //var buttonSupport = new Button
            //{
            //    Text = "Help Center",
            //    Style = controlButtonStyle
            //};
            //var buttonTermsAndConditions = new Button
            //{
            //    Text = "Terms And Conditions",
            //    Style = controlButtonStyle
            //};

            //var buttonUpdateProfile = new Button
            //{
            //    Text = "Update profile",
            //    Style = controlButtonStyle
            //};

            //buttonLogout.Clicked += async (s, e) =>
            //{
            //    //await Navigation.PushAsync(new Login());
            //    App.Current.Logout();
            //};

            //buttonChangePassword.Clicked += async (s, e) =>
            //{
            //    await Navigation.PushAsync(new ChangePasswordPage());
            //    _onToggleRequest();
            //};

            //buttonAbout.Clicked += async (s, e) =>
            //{
            //    await Navigation.PushAsync(new AboutUs());
            //    _onToggleRequest();
            //};
            //buttonPrivacy.Clicked += async (s, e) =>
            //{
            //    await Navigation.PushAsync(new Privacy());
            //    _onToggleRequest();
            //};
            //buttonSupport.Clicked += async (s, e) =>
            //{
            //    await Navigation.PushAsync(new HelpCenter());
            //    _onToggleRequest();
            //};
            //buttonTermsAndConditions.Clicked += async (s, e) =>
            //{
            //    await Navigation.PushAsync(new TermsAndConditions(isLoadByLogin));
            //    _onToggleRequest();
            //};

            //buttonUpdateProfile.Clicked += async (s, e) =>
            //{
            //    await Navigation.PushAsync(new UpdateProfilePage());
            //    _onToggleRequest();
            //};


            var stackLayoutControls = new StackLayout
            {

                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children =
                                             {
                                                 listViewOptionButtons
                                             }
            };

            StackLayout mainStackLayout = new StackLayout
            {
                Spacing = 2,
                Children =
                                  {
                                      userInfoTableView,
                                      menu,
                                      stackLayoutControls
                                  }
            };

            Content = mainStackLayout;
        }
    }

    public class CustomMenuItemTemplateCell : ViewCell
    {
        public CustomMenuItemTemplateCell()
        {
            var icon = new Image
            {
                HorizontalOptions = LayoutOptions.Start
            };
            icon.SetBinding(Image.SourceProperty, new Binding("Icon"));
            icon.WidthRequest = icon.HeightRequest = 40;

            var optionLayout = CreateOptionLayout();
            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                               {
                                   icon,
                                   optionLayout
                               }
            };
        }

        public static StackLayout CreateOptionLayout()
        {
            var optionName = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Style = App.Styles.ListItemTextStyle,
                TextColor = App.Styles.MenuOptionsColor
            };
            optionName.SetBinding(Label.TextProperty, "OptionName");
            var optionLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { optionName }
            };
            return optionLayout;
        }
    }

    public class ConfigurationMenuItems
    {
        public string Icon { get; set; }

        public string OptionName { get; set; }
    }
    public enum Items
    {
        Profile,
        Knocks
    }

    public static class MenuItemsNames
    {
        public const string Logout = "Logout";
        public const string ChangePassword = "Change your password";
        public const string About = "About Us";
        public const string Privacy = "Privacy";
        public const string HelpCenter = "Help Center";
        public const string TermsAndConditions = "Terms And Conditions";
        public const string UpdateProfile = "Update Profile";
    }
}
