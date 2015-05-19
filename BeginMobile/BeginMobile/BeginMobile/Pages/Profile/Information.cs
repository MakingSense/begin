using BeginMobile.Services.DTO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Information : ContentPage
    {
        private ProfileInfo _profileInfo;
        private readonly LoginUser _currentUser;
        private Grid _gridInfo;
        public Information()
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = "Information";
            _currentUser = (LoginUser) BeginApplication.Current.Properties["LoginUser"];
            Init();
        }

        private async Task Init()
        {
            _profileInfo =
                await
                    BeginApplication.ProfileServices.GetInformationDetail(_currentUser.AuthToken, 
                    _currentUser.User.UserName) ?? new ProfileInfo {Details = new UserDetails()};

            //Name and surname
            var boxViewBegNameSurname = new BoxView {Color = Color.White, WidthRequest = 100, HeightRequest = 2};
            var boxViewEndNameSurname = new BoxView {Color = Color.White, WidthRequest = 100, HeightRequest = 2};

            //About me
            var boxViewBegAboutMe = new BoxView {Color = Color.White, WidthRequest = 100, HeightRequest = 2};
            var boxViewEndAboutMe = new BoxView {Color = Color.White, WidthRequest = 100, HeightRequest = 2};

            //Education and profession
            var boxViewBegEduProf = new BoxView {Color = Color.White, WidthRequest = 100, HeightRequest = 2};
            var boxViewEndEduProf = new BoxView {Color = Color.White, WidthRequest = 100, HeightRequest = 2};

            //Work experience
            var boxViewBegWorkExp = new BoxView {Color = Color.White, WidthRequest = 100, HeightRequest = 2};
            var boxViewEndWorkExp = new BoxView {Color = Color.White, WidthRequest = 100, HeightRequest = 2};

            _gridInfo = new Grid
            {
                Padding = BeginApplication.Styles.LayoutThickness,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowDefinitions =
                                     {
                                         new RowDefinition {Height = GridLength.Auto},
                                         new RowDefinition {Height = GridLength.Auto},
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
            _gridInfo.Children.Add(new ProfileTitle("Name and surname"), 0, 0);
            _gridInfo.Children.Add(GetNameAndSurname(), 0, 1);
            _gridInfo.Children.Add(new ProfileTitle("About Me"), 0, 2);
            _gridInfo.Children.Add(GetInformationDetail(), 0, 3);
            _gridInfo.Children.Add(new ProfileTitle("Education and profession"), 0, 4);
            _gridInfo.Children.Add(GetEducationProffesion(), 0, 5);
            _gridInfo.Children.Add(new ProfileTitle("Work experience"), 0, 6);
            _gridInfo.Children.Add(GetWorkExperience(), 0, 7);


            //stackLayoutInfo = new StackLayout
            //                      {
            //                          Padding = BeginApplication.Styles.LayoutThickness,
            //                          Children =
            //                          {
            //                              //boxViewBegNameSurname,
            //                              new ProfileTitle("Name and surname"),
            //                              //boxViewEndNameSurname,
            //                              GetNameAndSurname(),
            //                              //boxViewBegAboutMe,
            //                              new ProfileTitle("About Me"),
            //                              //boxViewEndAboutMe,
            //                              GetInformationDetail(),
            //                              //boxViewBegEduProf,
            //                              new ProfileTitle("Education and profession"),
            //                              //boxViewEndEduProf,
            //                              GetEducationProffesion(),
            //                              //boxViewBegWorkExp,
            //                              new ProfileTitle("Work experience"),
            //                              //boxViewEndWorkExp,
            //                              GetWorkExperience()
            //                          }
            //                      };

            var scrollViewInfo = new ScrollView
                                 {
                                     VerticalOptions = LayoutOptions.Fill,
                                     HorizontalOptions = LayoutOptions.Fill,
                                     Content = _gridInfo
                                 };

            Content = scrollViewInfo;
        }

        public Grid GetGrid()
        {
            return _gridInfo;
        }

        //Information details

        #region Information

        public StackLayout GetNameAndSurname()
        {
            var stackLayoutNameAndSur = new StackLayout
                                        {
                                            Spacing = 2,
                                            Padding = new Thickness(10, 0, 0, 0),
                                            Children =
                                            {
                                                new Label
                                                {
                                                    Text = _profileInfo.NameSurname,
                                                    FontAttributes = FontAttributes.Bold,
                                                    Style = BeginApplication.Styles.TextBodyStyle
                                                }
                                            }
                                        };

            return stackLayoutNameAndSur;
        }

        public Grid GetInformationDetail()
        {
            var labelTextBirthDay = new Label
                                    {
                                        Text = "Birthday: ",
                                        Style = BeginApplication.Styles.SubtitleStyle
                                    };
            var labelTextGender = new Label
                                  {
                                      Text = "Gender: ",
                                      Style = BeginApplication.Styles.SubtitleStyle
                                  };
            var labelTextAddress = new Label
                                   {
                                       Text = "Address: ",
                                       Style = BeginApplication.Styles.SubtitleStyle
                                   };
            var labelTextCountry = new Label
                                   {
                                       Text = "Country: ",
                                       Style = BeginApplication.Styles.SubtitleStyle
                                   };
            var labelTextState = new Label
                                 {
                                     Text = "State: ",
                                     Style = BeginApplication.Styles.SubtitleStyle
                                 };
            var labelTextCity = new Label
                                {
                                    Text = "City: ",
                                    Style = BeginApplication.Styles.SubtitleStyle
                                };
            var labelTextPhone = new Label
                                 {
                                     Text = "Phone: ",
                                     Style = BeginApplication.Styles.SubtitleStyle
                                 };
            var labelTextCellPhone = new Label
                                     {
                                         Text = "Cell phone: ",
                                         Style = BeginApplication.Styles.SubtitleStyle
                                     };
            var labelTextEmail = new Label
                                 {
                                     Text = "Email: ",
                                     Style = BeginApplication.Styles.SubtitleStyle
                                 };
            var labelTextSkype = new Label
                                 {
                                     Text = "Skype: ",
                                     Style = BeginApplication.Styles.SubtitleStyle
                                 };
            var labelTextOthers = new Label
                                  {
                                      Text = "Others: ",
                                      Style = BeginApplication.Styles.SubtitleStyle
                                  };

            var labelBirthDay = new Label
                                {
                                    Text = _profileInfo.Details.Birthday,
                                    Style = BeginApplication.Styles.TextBodyStyle
                                };
            var labelGender = new Label
                              {
                                  Text = _profileInfo.Details.Gender,
                                  Style = BeginApplication.Styles.TextBodyStyle
                              };
            var labelAddress = new Label
                               {
                                   Text = _profileInfo.Details.Address,
                                   Style = BeginApplication.Styles.TextBodyStyle
                               };
            var labelCountry = new Label
                               {
                                   Text = _profileInfo.Details.Country,
                                   Style = BeginApplication.Styles.TextBodyStyle
                               };
            var labelState = new Label
                             {
                                 Text = _profileInfo.Details.State,
                                 Style = BeginApplication.Styles.TextBodyStyle
                             };
            var labelCity = new Label
                            {
                                Text = _profileInfo.Details.City,
                                Style = BeginApplication.Styles.TextBodyStyle
                            };
            var labelPhone = new Label
                             {
                                 Text = _profileInfo.Details.Phone,
                                 Style = BeginApplication.Styles.TextBodyStyle
                             };
            var labelCellPhone = new Label
                                 {
                                     Text = _profileInfo.CellPhone.ToString(),
                                     Style = BeginApplication.Styles.TextBodyStyle
                                 };
            var labelEmail = new Label
                             {
                                 Text = _profileInfo.Email,
                                 Style = BeginApplication.Styles.TextBodyStyle
                             };
            var labelSkype = new Label
                             {
                                 Text = _profileInfo.Skype,
                                 Style = BeginApplication.Styles.TextBodyStyle
                             };
            var labelOthers = new Label
                              {
                                  Text = _profileInfo.Others,
                                  Style = BeginApplication.Styles.TextBodyStyle
                              };

            var gridAboutMe = new Grid
                              {
                                  HorizontalOptions = LayoutOptions.Start,
                                  VerticalOptions = LayoutOptions.Start,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  },
                                  ColumnDefinitions =
                                  {
                                      new ColumnDefinition {Width = GridLength.Auto},
                                      new ColumnDefinition {Width = GridLength.Auto}
                                  }
                              };
            gridAboutMe.Children.Add(labelTextBirthDay, 0, 0);
            gridAboutMe.Children.Add(labelBirthDay, 1, 0);
            gridAboutMe.Children.Add(labelTextGender, 0, 1);
            gridAboutMe.Children.Add(labelGender, 1, 1);
            gridAboutMe.Children.Add(labelTextAddress, 0, 2);
            gridAboutMe.Children.Add(labelAddress, 1, 2);
            gridAboutMe.Children.Add(labelTextCountry, 0, 3);
            gridAboutMe.Children.Add(labelCountry, 1, 3);
            gridAboutMe.Children.Add(labelTextState, 0, 4);
            gridAboutMe.Children.Add(labelState, 1, 4);
            gridAboutMe.Children.Add(labelTextCity, 0, 5);
            gridAboutMe.Children.Add(labelCity, 1, 5);
            gridAboutMe.Children.Add(labelTextPhone, 0, 6);
            gridAboutMe.Children.Add(labelPhone, 1, 6);
            gridAboutMe.Children.Add(labelTextCellPhone, 0, 7);
            gridAboutMe.Children.Add(labelCellPhone, 1, 7);
            gridAboutMe.Children.Add(labelTextEmail, 0, 8);
            gridAboutMe.Children.Add(labelEmail, 1, 8);
            gridAboutMe.Children.Add(labelTextSkype, 0, 9);
            gridAboutMe.Children.Add(labelSkype, 1, 9);
            gridAboutMe.Children.Add(labelTextOthers, 0, 10);
            gridAboutMe.Children.Add(labelOthers, 1, 10);

            return gridAboutMe;
        }

        public Grid GetEducationProffesion()
        {
            var labelTextLevel = new Label
                                 {
                                     Text = "Level: ",
                                     Style = BeginApplication.Styles.SubtitleStyle
                                 };
            var labelTextEstablishment = new Label
                                         {
                                             Text = "Establishment: ",
                                             Style = BeginApplication.Styles.SubtitleStyle
                                         };
            var labelTextTitle = new Label
                                 {
                                     Text = "Title: ",
                                     Style = BeginApplication.Styles.SubtitleStyle
                                 };
            var labelTextCategory = new Label
                                    {
                                        Text = "Category: ",
                                        Style = BeginApplication.Styles.SubtitleStyle
                                    };
            var labelTextSubcategory = new Label
                                       {
                                           Text = "Subcategory: ",
                                           Style = BeginApplication.Styles.SubtitleStyle
                                       };


            var labelLevel = new Label
                             {
                                 Text = _profileInfo.EducationLevel,
                                 Style = BeginApplication.Styles.TextBodyStyle
                             };
            var labelEstablishment = new Label
                                     {
                                         Text = _profileInfo.Establishment,
                                         Style = BeginApplication.Styles.TextBodyStyle
                                     };

            var labelTitle = new Label
                             {
                                 Text = _profileInfo.EducationTitle,
                                 Style = BeginApplication.Styles.TextBodyStyle
                             };
            var labelCategory = new Label
                                {
                                    Text = _profileInfo.EducationCategory,
                                    Style = BeginApplication.Styles.TextBodyStyle
                                };
            var labelSubcategory = new Label
                                   {
                                       Text = _profileInfo.EducationSubcategory,
                                       Style = BeginApplication.Styles.TextBodyStyle
                                   };

            var gridEducationProffesion = new Grid
                                          {
                                              HorizontalOptions = LayoutOptions.Start,
                                              VerticalOptions = LayoutOptions.Start,
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
                                                  new ColumnDefinition {Width = GridLength.Auto},
                                                  new ColumnDefinition {Width = GridLength.Auto}
                                              }
                                          };
            gridEducationProffesion.Children.Add(labelTextLevel, 0, 0);
            gridEducationProffesion.Children.Add(labelLevel, 1, 0);
            gridEducationProffesion.Children.Add(labelTextEstablishment, 0, 1);
            gridEducationProffesion.Children.Add(labelEstablishment, 1, 1);
            gridEducationProffesion.Children.Add(labelTextTitle, 0, 2);
            gridEducationProffesion.Children.Add(labelTitle, 1, 2);
            gridEducationProffesion.Children.Add(labelTextCategory, 0, 3);
            gridEducationProffesion.Children.Add(labelCategory, 1, 3);
            gridEducationProffesion.Children.Add(labelTextSubcategory, 0, 4);
            gridEducationProffesion.Children.Add(labelSubcategory, 1, 4);
            return gridEducationProffesion;
        }

        public Grid GetWorkExperience()
        {
            var labelTextCompany = new Label
                                   {
                                       Text = "Company: ",
                                       Style = BeginApplication.Styles.SubtitleStyle
                                   };
            var labelTextPosition = new Label
                                    {
                                        Text = "Position: ",
                                        Style = BeginApplication.Styles.SubtitleStyle
                                    };
            var labelTextCity = new Label
                                {
                                    Text = "City: ",
                                    Style = BeginApplication.Styles.SubtitleStyle
                                };
            var labelTextWorkDescription = new Label
                                           {
                                               Text = "Work description: ",
                                               Style = BeginApplication.Styles.SubtitleStyle
                                           };
            var labelTextCurrentlyWork = new Label
                                         {
                                             Text = "I currently work here: ",
                                             Style = BeginApplication.Styles.SubtitleStyle
                                         };
            var labelTextDates = new Label
                                 {
                                     Text = "Dates: ",
                                     Style = BeginApplication.Styles.SubtitleStyle
                                 };

            var labelCompany = new Label
                               {
                                   Text = _profileInfo.Company,
                                   Style = BeginApplication.Styles.TextBodyStyle
                               };
            var labelPosition = new Label
                                {
                                    Text = _profileInfo.Position,
                                    Style = BeginApplication.Styles.TextBodyStyle
                                };
            var labelCity = new Label
                            {
                                Text = _profileInfo.CityWork,
                                Style = BeginApplication.Styles.TextBodyStyle
                            };
            var labelWorkDescription = new Label
                                       {
                                           Text = _profileInfo.WorkDescription,
                                           Style = BeginApplication.Styles.TextBodyStyle
                                       };
            var labelCurrentlyWork = new Label
                                     {
                                         Text = _profileInfo.CurrentWork,
                                         Style = BeginApplication.Styles.TextBodyStyle
                                     };
            var labelDates = new Label
                             {
                                 Text = _profileInfo.Dates,
                                 Style = BeginApplication.Styles.TextBodyStyle
                             };

            var gridWorkExperience = new Grid
                                     {
                                         HorizontalOptions = LayoutOptions.Start,
                                         VerticalOptions = LayoutOptions.Start,
                                         RowDefinitions =
                                         {
                                             new RowDefinition {Height = GridLength.Auto},
                                             new RowDefinition {Height = GridLength.Auto},
                                             new RowDefinition {Height = GridLength.Auto},
                                             new RowDefinition {Height = GridLength.Auto},
                                             new RowDefinition {Height = GridLength.Auto},
                                             new RowDefinition {Height = GridLength.Auto}
                                         },
                                         ColumnDefinitions =
                                         {
                                             new ColumnDefinition {Width = GridLength.Auto},
                                             new ColumnDefinition {Width = GridLength.Auto}
                                         }
                                     };
            gridWorkExperience.Children.Add(labelTextCompany, 0, 0);
            gridWorkExperience.Children.Add(labelCompany, 1, 0);
            gridWorkExperience.Children.Add(labelTextPosition, 0, 1);
            gridWorkExperience.Children.Add(labelPosition, 1, 1);
            gridWorkExperience.Children.Add(labelTextCity, 0, 2);
            gridWorkExperience.Children.Add(labelCity, 1, 2);
            gridWorkExperience.Children.Add(labelTextWorkDescription, 0, 3);
            gridWorkExperience.Children.Add(labelWorkDescription, 1, 3);
            gridWorkExperience.Children.Add(labelTextCurrentlyWork, 0, 4);
            gridWorkExperience.Children.Add(labelCurrentlyWork, 1, 4);
            gridWorkExperience.Children.Add(labelTextDates, 0, 5);
            gridWorkExperience.Children.Add(labelDates, 1, 5);
            return gridWorkExperience;
        }

        #endregion

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.Content = null;
        }
    }
}