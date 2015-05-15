			using BeginMobile.Pages;
			using BeginMobile.Pages.ContactPages;
			using BeginMobile.Pages.GroupPages;
			using BeginMobile.Pages.Profile;
			using System;
			using System.Collections.Generic;
			using System.Collections.ObjectModel;
			using System.Text;
			using Xamarin.Forms;
			using BeginMobile.LocalizeResources.Resources;
			using BeginMobile.Services.DTO;
			using BeginMobile.Accounts;

			namespace BeginMobile.Menu
			{
			    public class OptionsPage : TabContent
			    {
			        private const string DefaultIcon = "userprofile.png";
					public string MasterTitle{ get; set; }

			        public OptionsPage(string title, string iconImg)
			            : base(title, iconImg)
			        {

						MasterTitle = "Options";

						BackgroundColor = BeginApplication.Styles.MenuBackground;

						var currentUser = (LoginUser) Application.Current.Properties["LoginUser"];

						const bool isLoadByLogin = false;			

						var userAvatar = BeginApplication.Styles.DefaultProfileUserIconName;
						if (currentUser != null)
						{
							var userAvatarUrl = currentUser.User.Avatar;

							if (!string.IsNullOrEmpty(userAvatarUrl))
							{
								userAvatar = userAvatarUrl;
							}
						}

						if (currentUser == null) return;

						var listMenuData = new List<MenuItemViewModel>
						{
							new MenuItemViewModel
							{
								Icon = userAvatar,
								OptionName = currentUser.User.DisplayName,
								OptionDetail = currentUser.User.Email
							}
						};

						var dataTemplateListViewMenuIcon = new DataTemplate(typeof (MenuIconDataTemplate));

						var listViewMenuIcon = new ListView {
							VerticalOptions = LayoutOptions.Start,
							ItemsSource = listMenuData,
							ItemTemplate = dataTemplateListViewMenuIcon,
							BackgroundColor = BeginApplication.Styles.MenuBackground,
							HeightRequest = 180,
							HasUnevenRows = true
						};


						var dataTemplateMenuOptions = new DataTemplate(typeof (MenuDataTemplate));

						var listOptionsData = new List<MenuItemViewModel> {

						new MenuItemViewModel {
							Icon = DefaultIcon, 
							OptionName = MenuItemsNames.AppHomeContacts
							},

							new MenuItemViewModel {
								Icon = DefaultIcon,
								OptionName =    MenuItemsNames.AppHomeChildGroups
							},					

							new MenuItemViewModel {
								Icon = DefaultIcon,
								OptionName = MenuItemsNames.AppHomeChildEvents
									},

						new MenuItemViewModel {
							Icon = DefaultIcon,
							OptionName = MenuItemsNames.AppHomeServices
							},

						new MenuItemViewModel {
							Icon = DefaultIcon,
							OptionName = MenuItemsNames.AppHomeShops
							},	

							new MenuItemViewModel {
								Icon = "",
								OptionName = ""
							},

							new MenuItemViewModel {
								Icon = "",
								OptionName = ""
							},

							new MenuItemViewModel {
							Icon = string.Empty,
								OptionName =
									MenuItemsNames
										.Logout
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
							var selectedItemOptionName =
								((MenuItemViewModel) eventArgs.SelectedItem).OptionName;
							var profileMe = new ProfileMe(currentUser);
							var contentPageKnocks = new ContentPage
							{
								Title =
									AppResources
										.LabelMenuKnocks
									};

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

							case MenuItemsNames.AppHomeContacts:
						                        using (var contentContactPage = 
							new ContactPage(new Label { Text = MenuItemsNames.AppHomeContacts, Style = BeginApplication.Styles.StyleNavigationTitle, TextColor = Color.FromHex("FFFFFF") }.Text,
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

							case MenuItemsNames.AppHomeServices:
									await DisplayAlert("Services", "Goes to services", "Ok");
									break;

							case MenuItemsNames.AppHomeShops:
											var shops = new Shop();		
											await Navigation.PushAsync(shops);							
											break;

							case MenuItemsNames.Knocks:
								await Navigation.PushAsync(contentPageKnocks);				
								break;

							case MenuItemsNames.Profile:
								await Navigation.PushAsync(profileMe);
								break;

							case MenuItemsNames.Logout:
								BeginApplication.CurrentBeginApplication.Logout();
								break;

							case MenuItemsNames.ChangePassword:
								await Navigation.PushAsync(new ChangePasswordPage());
								break;

							case MenuItemsNames.About:
								await Navigation.PushAsync(new AboutUs());				
								break;

							case MenuItemsNames.Privacy:
								await Navigation.PushAsync(new Privacy());
							
								break;
							case MenuItemsNames.HelpCenter:
								await Navigation.PushAsync(new HelpCenter());						
								break;

							case MenuItemsNames.TermsAndConditions:
								await
								Navigation.PushAsync(
									new TermsAndConditions(isLoadByLogin));
							
								break;

							case MenuItemsNames.UpdateProfile:
								await Navigation.PushAsync(new UpdateProfilePage());
								break;
							}

							((ListView) sender).SelectedItem = null;

						};
						listViewMenuOptions.HasUnevenRows = true;

					var stackLayoutControls = new StackLayout {				
						VerticalOptions = LayoutOptions.FillAndExpand,
						Orientation = StackOrientation.Vertical,
						Children = {
							listViewMenuOptions
						}
					};

					StackLayout mainStackLayout = 

						new StackLayout {
							Spacing = 2,
					
							Padding = BeginApplication.Styles.LayoutThickness,
							Children = {
								//listViewMenuIcon,
								stackLayoutControls
							}
						};

						Content = mainStackLayout;

			        }

			protected override void OnAppearing ()
			{
				base.OnAppearing ();
				var title = MasterTitle;

				MessagingCenter.Send (this, "masterTitle", title);
				MessagingCenter.Unsubscribe<OptionsPage, string>(this, "masterTitle");
			}
		}
	}