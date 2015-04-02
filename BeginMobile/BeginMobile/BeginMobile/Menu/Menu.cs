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
            var listData = new List<ConfigurationMenuItems>
            {              
                new ConfigurationMenuItems { Icon = pUserDefault, OptionName = currentUser.User.DisplayName, OptionDetail= currentUser.User.Email}
            };
            var iconTemplate = new DataTemplate(typeof(MenuIconTemplate));
            var userImageListView = new ListView
            {
                VerticalOptions = LayoutOptions.Start,
                ItemsSource = listData,
                ItemTemplate = iconTemplate,
                BackgroundColor = App.Styles.MenuBackground,
                HeightRequest = 180,
            };
            userImageListView.HasUnevenRows = true;


            var cell = new DataTemplate(typeof(CustomMenuItemTemplateCell));

            var listButtonsData = new List<ConfigurationMenuItems>();
            listButtonsData.Add(new ConfigurationMenuItems { Icon = pProfileMenuIcon, OptionName = MenuItemsNames.Profile });
            listButtonsData.Add(new ConfigurationMenuItems { Icon = knocks, OptionName = MenuItemsNames.Knocks });
            listButtonsData.Add(new ConfigurationMenuItems { Icon = "", OptionName = "" });
            listButtonsData.Add(new ConfigurationMenuItems { Icon = "", OptionName = MenuItemsNames.Logout });
            listButtonsData.Add(new ConfigurationMenuItems { Icon = "", OptionName = MenuItemsNames.ChangePassword });
            listButtonsData.Add(new ConfigurationMenuItems { Icon = "", OptionName = MenuItemsNames.About });
            listButtonsData.Add(new ConfigurationMenuItems { Icon = "", OptionName = MenuItemsNames.Privacy });
            listButtonsData.Add(new ConfigurationMenuItems { Icon = "", OptionName = MenuItemsNames.HelpCenter });
            listButtonsData.Add(new ConfigurationMenuItems { Icon = "", OptionName = MenuItemsNames.TermsAndConditions });
            listButtonsData.Add(new ConfigurationMenuItems { Icon = "", OptionName = MenuItemsNames.UpdateProfile });

            var listViewOptionButtons = new ListView
            {
                VerticalOptions = LayoutOptions.Start,
                ItemsSource = listButtonsData,
                ItemTemplate = cell,
                BackgroundColor = App.Styles.MenuBackground,
            };

            listViewOptionButtons.ItemSelected += async (s, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                string item = ((ConfigurationMenuItems)e.SelectedItem).OptionName;
                var itemPageProfile = new ProfileMe(currentUser.User);
                var itemPageKnocks = new ContentPage { Title = "Knocks" };

                switch (item)
                {
                    case MenuItemsNames.Knocks:
                       await Navigation.PushAsync(itemPageKnocks);
                        _onToggleRequest();
                        break;
                    case MenuItemsNames.Profile:
                        await Navigation.PushAsync(itemPageProfile);
                        _onToggleRequest();
                        break;
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
                _onToggleRequest();
            };
            listViewOptionButtons.HasUnevenRows = true;            

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
                                      userImageListView,
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

    public class MenuIconTemplate : ViewCell
    {
        public MenuIconTemplate()
        {
            var icon = new Image
            {
                HeightRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),
                WidthRequest = Device.OnPlatform<int>(iOS: 50, Android: 100, WinPhone: 100),
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
                //BorderThickness = Device.OnPlatform<int>(iOS: 2, Android: 3, WinPhone: 3),
            };
           
            icon.SetBinding(Image.SourceProperty, new Binding("Icon"));

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
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemTextStyle,
            };
            var labelDescription = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                YAlign = TextAlignment.Center,
                Style = App.Styles.ListItemDetailTextStyle
            };
            labelDescription.SetBinding(Label.TextProperty, "OptionDetail");
            optionName.SetBinding(Label.TextProperty, "OptionName");

            var optionLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { optionName, labelDescription }
            };
            return optionLayout;
        }
    }

    public class ConfigurationMenuItems
    {
        public string Icon { get; set; }

        public string OptionName { get; set; }
        public string OptionDetail { get; set; }
    }

    public static class MenuItemsNames
    {
        public const string Profile = "Profile";
        public const string Knocks = "Knocks";
        public const string Logout = "Logout";
        public const string ChangePassword = "Change your password";
        public const string About = "About Us";
        public const string Privacy = "Privacy";
        public const string HelpCenter = "Help Center";
        public const string TermsAndConditions = "Terms And Conditions";
        public const string UpdateProfile = "Update Profile";
        
    }
}
