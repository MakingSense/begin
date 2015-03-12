using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using BeginMobile.Pages;
using BeginMobile.Accounts;
using BeginMobile.MenuProfile;
using BeginMobile.Utils;

namespace BeginMobile.Menu
{
    public class Menu : ContentPage
    {
        private const string DefaultUri = "http://www.americanpresidents.org/images/01_150.gif";

        public Menu()
        {
            Title = "Menu";

            var userImage = new ImageCell
            {
                ImageSource =
                    ImageSource.FromUri(new Uri(DefaultUri)),
                Text = "Juan Perez",
                Detail = "juan.perez@gmail.com",
            };

            var userInfoTableView = new TableView
            {

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
                                       new ConfigurationMenuItems {OptionName = Items.Profile.ToString(), Icon = DefaultUri},
                                      
                                       new ConfigurationMenuItems {OptionName = Items.Knocks.ToString(), Icon = DefaultUri}
                                   };


            var cell = new DataTemplate(typeof(CustomMenuItemTemplateCell));

            var menu = new ListView
            {
                ItemsSource = menuItemList,
                ItemTemplate = cell
                //ItemsSource = menuItems,
            };



            menu.ItemSelected += async (sender, e) =>
            {
                var item = (ConfigurationMenuItems)e.SelectedItem;
                var itemPageProfile = new MasterDetailProfile();
                var itemPageKnocks = new ContentPage { Title = "Knocks" };

                if (item.OptionName.Equals(Items.Profile.ToString()))
                {
                    await Navigation.PushAsync(itemPageProfile);
                }
                else if (item.OptionName.Equals(Items.Knocks.ToString()))
                {
                    await Navigation.PushAsync(itemPageKnocks);
                }

            };

            var controlButtonStyle = CustomizedButtonStyle.GetControlButtonStyle();

            //controls buttons
            var buttonLogout = new Button
            {
                Text = "Logout",
                Style = controlButtonStyle
            };

            var buttonAbout = new Button
            {
                Text = "About",
                Style = controlButtonStyle
            };
            var buttonPrivacy = new Button
            {
                Text = "Privacy",
                Style = controlButtonStyle
            };
            var buttonSupport = new Button
            {
                Text = "Help Center",
                Style = controlButtonStyle
            };
            var buttonTermsAndConditions = new Button
            {
                Text = "Terms And Conditions",
                Style = controlButtonStyle
            };

            var controlsLayout = new StackLayout
            {

                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Children =
                                             {
                                                 buttonLogout,
                                                 buttonAbout,
                                                 buttonPrivacy,
                                                 buttonSupport,
                                                 buttonTermsAndConditions
                                             }
            };

            buttonLogout.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new Login());
                //await Navigation.PopToRootAsync();
            };
            buttonAbout.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new AboutUs());
            };
            buttonPrivacy.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new Privacy());
            };
            buttonSupport.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new HelpCenter());
            };
            buttonTermsAndConditions.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new TermsAndConditions());
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                                  {
                                      userInfoTableView,
                                      menu,
                                      controlsLayout
                                  }
            };


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
                HorizontalOptions = LayoutOptions.FillAndExpand
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
}
