using BeginMobile.Pages;
using BeginMobile.Pages.ContactPages;
using BeginMobile.Pages.GroupPages;
using BeginMobile.Pages.Profile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Menu
{
    public class OptionsPage : TabContent
    {
        private const string DefaultIcon = "userprofile.png";
        public OptionsPage(string title, string iconImg)
            : base(title, iconImg)
        {
            var listoptionsItems = new ObservableCollection<MenuItemViewModel>
                                       {
                                           new MenuItemViewModel
                                           {
                                               Icon = DefaultIcon,
                                               OptionName =
                                                   MenuItemsNames
                                                   .AppHomeChildGroups
                                           },
                                           new MenuItemViewModel
                                           {
                                               Icon = DefaultIcon,
                                               OptionName =
                                                   MenuItemsNames
                                                   .AppHomeChildFindContacts
                                           },
                                           new MenuItemViewModel
                                           {
                                               Icon = DefaultIcon,
                                               OptionName =
                                                   MenuItemsNames
                                                   .AppHomeChildEvents
                                           }
                                       };


            var dataTemplateMenuOptions = new DataTemplate(typeof(MenuDataTemplate));
            var listViewMainControls = new ListView
            {
                VerticalOptions = LayoutOptions.Start,
                ItemsSource = listoptionsItems,
                ItemTemplate = dataTemplateMenuOptions,
                HasUnevenRows = true
            };


            listViewMainControls.ItemTapped += async (sender, eventArgs) =>
            {
                if (eventArgs.Item == null)
                {
                    return;
                }
                var selectedItemOptionName =
                    ((MenuItemViewModel)eventArgs.Item).OptionName;

                switch (selectedItemOptionName)
                {
                    case MenuItemsNames.AppHomeChildGroups:
                        using (
                            var contentPageGroupListPage = new GroupListPage(new Label { Text = MenuItemsNames.AppHomeChildGroups, Style = BeginApplication.Styles.StyleNavigationTitle, TextColor = Color.FromHex("FFFFFF") }.Text,
                    "padlock.png"))
                        {
                            await
                                Navigation.PushAsync(contentPageGroupListPage);
                            break;
                        }
                    case MenuItemsNames.AppHomeChildFindContacts:
                        using (var contentContactPage = new ContactPage(new Label { Text = MenuItemsNames.AppHomeChildFindContacts, Style = BeginApplication.Styles.StyleNavigationTitle, TextColor = Color.FromHex("FFFFFF") }.Text,
                    "padlock.png"))
                            
                        {
                            await
                                Navigation.PushAsync(contentContactPage);
                            break;
                        }

                    case MenuItemsNames.AppHomeChildEvents:
                        using (var contentEvents = new Events())
                            
                    {
                        await
                            Navigation.PushAsync(contentEvents);
                        break;
                    }
                }
                ((ListView)sender).SelectedItem = null;
            };

            var gridMain = new Grid()
            {
                HorizontalOptions =  LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition(){Height = GridLength.Auto}
                }
            };

            gridMain.Children.Add(listViewMainControls);

            Content = gridMain;
        }
    }
}
