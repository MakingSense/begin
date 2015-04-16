using System;
using System.Collections.Generic;
using BeginMobile.Accounts;
using BeginMobile.Pages;
using BeginMobile.Pages.Profile;
using BeginMobile.Services.DTO;
using Xamarin.Forms;

namespace BeginMobile.Menu
{
    public class Menu : ContentPage
    {
        private const string ProfileMenuIcon = "userprofile.png";
        private const string KnocksMenuIcon = "padlock.png";

        private static string ProfileUserDefault
        {
            get
            {
                return Device.OS == TargetPlatform.iOS ? "Contact.png" : "userprofile.png";
            }
        }

        private readonly Action _onToggleRequest;

        public Menu(Action onToggleRequest)
        {

            BackgroundColor = BeginApplication.Styles.MenuBackground;
            var currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];

            const bool isLoadByLogin = false;
            _onToggleRequest = onToggleRequest;

            Title = "Menu";
            Icon = Device.OS == TargetPlatform.iOS ? "More.png" : null;
            var listMenuData = new List<MenuItemViewModel>
            {              
                new MenuItemViewModel { Icon = ProfileUserDefault, OptionName = currentUser.User.DisplayName, OptionDetail= currentUser.User.Email}
            };
            var dataTemplateListViewMenuIcon = new DataTemplate(typeof(MenuIconDataTemplate));
            var listViewMenuIcon = new ListView
                                    {
                                        VerticalOptions = LayoutOptions.Start,
                                        ItemsSource = listMenuData,
                                        ItemTemplate = dataTemplateListViewMenuIcon,
                                        BackgroundColor = BeginApplication.Styles.MenuBackground,
                                        HeightRequest = 180,
                                        HasUnevenRows = true
                                    };


            var dataTemplateMenuOptions = new DataTemplate(typeof(MenuDataTemplate));

            var listOptionsData = new List<MenuItemViewModel>
                                  {
                                      new MenuItemViewModel
                                      {
                                          Icon =
                                              ProfileMenuIcon,
                                          OptionName =
                                              MenuItemsNames
                                              .Profile
                                      },
                                      new MenuItemViewModel
                                      {
                                          Icon = KnocksMenuIcon,
                                          OptionName =
                                              MenuItemsNames
                                              .Knocks
                                      },
                                      new MenuItemViewModel
                                      {
                                          Icon = "",
                                          OptionName = ""
                                      },
                                      new MenuItemViewModel
                                      {
                                          Icon = "",
                                          OptionName =
                                              MenuItemsNames
                                              .Logout
                                      },
                                      new MenuItemViewModel
                                      {
                                          Icon = "",
                                          OptionName =
                                              MenuItemsNames
                                              .ChangePassword
                                      },
                                      new MenuItemViewModel
                                      {
                                          Icon = "",
                                          OptionName =
                                              MenuItemsNames
                                              .About
                                      },
                                      new MenuItemViewModel
                                      {
                                          Icon = "",
                                          OptionName =
                                              MenuItemsNames
                                              .Privacy
                                      },
                                      new MenuItemViewModel
                                      {
                                          Icon = "",
                                          OptionName =
                                              MenuItemsNames
                                              .HelpCenter
                                      },
                                      new MenuItemViewModel
                                      {
                                          Icon = "",
                                          OptionName =
                                              MenuItemsNames
                                              .TermsAndConditions
                                      },
                                      new MenuItemViewModel
                                      {
                                          Icon = "",
                                          OptionName =
                                              MenuItemsNames
                                              .UpdateProfile
                                      }
                                  };

            var listViewMenuOptions = new ListView
            {
                VerticalOptions = LayoutOptions.Start,
                ItemsSource = listOptionsData,
                ItemTemplate = dataTemplateMenuOptions,
                BackgroundColor = BeginApplication.Styles.MenuBackground,
            };

            listViewMenuOptions.ItemSelected += async (sender, eventArgs) =>
            {
                if (eventArgs.SelectedItem == null)
                {
                    return;
                }
                var selectedItemOptionName = ((MenuItemViewModel)eventArgs.SelectedItem).OptionName;
                var profileMe = new ProfileMe(currentUser.User);
                var contentPageKnocks = new ContentPage { Title = "Knocks" };

                switch (selectedItemOptionName)
                {
                    case MenuItemsNames.Knocks:
                       await Navigation.PushAsync(contentPageKnocks);
                        _onToggleRequest();
                        break;
                    case MenuItemsNames.Profile:
                        await Navigation.PushAsync(profileMe);
                        _onToggleRequest();
                        break;
                    case MenuItemsNames.Logout:
                        BeginApplication.CurrentBeginApplication.Logout();
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
                ((ListView)sender).SelectedItem = null;
                _onToggleRequest();
            };
            listViewMenuOptions.HasUnevenRows = true;            

            var stackLayoutControls = new StackLayout
            {

                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children =
                                             {                                                 
                                                 listViewMenuOptions
                                             }
            };

            StackLayout mainStackLayout = new StackLayout
            {
                Spacing = 2,
                Padding = BeginApplication.Styles.LayoutThickness,
                Children =
                                  {        
                                      listViewMenuIcon,
                                      stackLayoutControls
                                  }
            };

            Content = mainStackLayout;
        }
    }

    public class MenuDataTemplate : ViewCell
    {
        public MenuDataTemplate()
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
            var labelOptionName = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Style = BeginApplication.Styles.ListItemTextStyle,
                TextColor = BeginApplication.Styles.MenuOptionsColor
            };

            labelOptionName.SetBinding(Label.TextProperty, "OptionName");
         

            var stackLayoutOptions = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { labelOptionName }
            };

            return stackLayoutOptions;
        }
    }

    public class MenuIconDataTemplate : ViewCell
    {
        public MenuIconDataTemplate()
        {
            var icon = new Image
            {
                HeightRequest = Device.OnPlatform(50, 100, 100),
                WidthRequest = Device.OnPlatform(50, 100, 100),
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Start,
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
            var labelOptionName = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                YAlign = TextAlignment.Center,
                Style = BeginApplication.Styles.ListItemTextStyle,
            };
            var labelDescription = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                YAlign = TextAlignment.Center,
                Style = BeginApplication.Styles.ListItemDetailTextStyle
            };
            labelDescription.SetBinding(Label.TextProperty, "OptionDetail");
            labelOptionName.SetBinding(Label.TextProperty, "OptionName");

            var optionLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { labelOptionName, labelDescription }
            };
            return optionLayout;
        }
    }

    public class MenuItemViewModel
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
