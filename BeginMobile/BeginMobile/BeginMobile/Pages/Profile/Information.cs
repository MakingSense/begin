using BeginMobile.Services.DTO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Information : ContentPage
    {
        private ProfileInfo _profileInfo;
        private LoginUser _currentUser;

        public Information()
        {
            Title = "Information";
            _currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            Init();
        }

        private async Task Init()
        {
            _profileInfo = await App.ProfileServices.GetInformationDetail(_currentUser.User.UserName, _currentUser.AuthToken) ??
                           new ProfileInfo { Details = new UserDetails() };

            //Name and surname
            var boxViewBegNameSurname = new BoxView { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };
            var boxViewEndNameSurname = new BoxView { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };

            //About me
            var boxViewBegAboutMe = new BoxView { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };
            var boxViewEndAboutMe = new BoxView { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };

            //Education and profession
            var boxViewBegEduProf = new BoxView { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };
            var boxViewEndEduProf = new BoxView { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };

            //Work experience
            var boxViewBegWorkExp = new BoxView { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };
            var boxViewEndWorkExp = new BoxView { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };

            var stackLayoutInfo = new StackLayout
            {
                Spacing = 0,
                Padding = 1,
                Children =
                                {
                                    boxViewBegNameSurname,
                                    new ProfileTitle("Name and surname"),
                                    boxViewEndNameSurname,
                                    GetNameAndSurname(),
                                    boxViewBegAboutMe,
                                    new ProfileTitle("AboutMe"),
                                    boxViewEndAboutMe,
                                    GetInformationDetail(),
                                    boxViewBegEduProf,
                                    new ProfileTitle("Education and profession"),
                                    boxViewEndEduProf,
                                    GetEducationProffesion(),
                                    boxViewBegWorkExp,
                                    new ProfileTitle("Work experience"),
                                    boxViewEndWorkExp,
                                    GetWorkExperience()
                                }
            };

            var scrollViewInfo = new ScrollView
            {
                Content = stackLayoutInfo
            };

            Content = scrollViewInfo;
        }

        //Information details
        #region Information
        private StackLayout GetNameAndSurname()
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
                                                 FontAttributes = FontAttributes.Italic
                                             }
                                         }
                                     };

            return stackLayoutNameAndSur;
        }

        public StackLayout GetInformationDetail()
        {
            var subAboutMe = new StackLayout
                             {
                                 Spacing = 2,
                                 Padding = new Thickness(10, 0, 0, 0),
                                 Children =
                                 {
                                     new Label
                                     {
                                         Text = "Birthday: " + _profileInfo.Details.Birthday,
                                         FontAttributes = FontAttributes.Italic
                                     },
                                     new Label
                                     {
                                         Text = "Gender: " + _profileInfo.Details.Gender,
                                         FontAttributes = FontAttributes.Italic
                                     },
                                     new Label
                                     {
                                         Text = "Address: " + _profileInfo.Details.Address,
                                         FontAttributes = FontAttributes.Italic
                                     },
                                     new Label
                                     {
                                         Text = "Country: " + _profileInfo.Details.Country,
                                         FontAttributes = FontAttributes.Italic
                                     },
                                     new Label
                                     {
                                         Text = "State: " + _profileInfo.Details.State,
                                         FontAttributes = FontAttributes.Italic
                                     },
                                     new Label
                                     {
                                         Text = "City: " + _profileInfo.Details.City,
                                         FontAttributes = FontAttributes.Italic
                                     },
                                     new Label
                                     {
                                         Text = "Phone: " + _profileInfo.Details.Phone,
                                         FontAttributes = FontAttributes.Italic
                                     },
                                     new Label
                                     {
                                         Text = "Cell phone: " + _profileInfo.CellPhone,
                                         FontAttributes = FontAttributes.Italic
                                     },
                                     new Label
                                     {
                                         Text = "Email: " + _profileInfo.Email,
                                         FontAttributes = FontAttributes.Italic
                                     },
                                     new Label
                                     {
                                         Text = "Skype: " + _profileInfo.Skype,
                                         FontAttributes = FontAttributes.Italic
                                     },
                                     new Label
                                     {
                                         Text = "Others: " + _profileInfo.Others,
                                         FontAttributes = FontAttributes.Italic
                                     }
                                 }
                             };

            return subAboutMe;
        }

        public StackLayout GetEducationProffesion()
        {
            return new StackLayout
                   {
                       Spacing = 2,
                       Padding = new Thickness(10, 0, 0, 0),
                       Children =
                       {
                           new Label
                           {
                               Text = "Level: " + _profileInfo.EducationLevel,
                               FontAttributes = FontAttributes.Italic
                           },
                           new Label
                           {
                               Text = "Establishment: " + _profileInfo.Establishment,
                               FontAttributes = FontAttributes.Italic
                           },
                           new Label
                           {
                               Text = "Title: " + _profileInfo.EducationTitle,
                               FontAttributes = FontAttributes.Italic
                           },
                           new Label
                           {
                               Text = "Category: " + _profileInfo.EducationCategory,
                               FontAttributes = FontAttributes.Italic
                           },
                           new Label
                           {
                               Text = "Subcategory: " + _profileInfo.EducationSubcategory,
                               FontAttributes = FontAttributes.Italic
                           }
                       }
                   };
        }

        public StackLayout GetWorkExperience()
        {
            return new StackLayout
                   {
                       Spacing = 2,
                       Padding = new Thickness(10, 0, 0, 0),
                       Children =
                       {
                           new Label
                           {
                               Text = "Company: " + _profileInfo.Company,
                               FontAttributes = FontAttributes.Italic
                           },
                           new Label
                           {
                               Text = "Position: " + _profileInfo.Position,
                               FontAttributes = FontAttributes.Italic
                           },
                           new Label
                           {
                               Text = "City: " + _profileInfo.CityWork,
                               FontAttributes = FontAttributes.Italic
                           },
                           new Label
                           {
                               Text = "Work description: " + _profileInfo.WorkDescription,
                               FontAttributes = FontAttributes.Italic
                           },
                           new Label
                           {
                               Text = "I currently work here: " + _profileInfo.CurrentWork,
                               FontAttributes = FontAttributes.Italic
                           },
                           new Label
                           {
                               Text = "Dates: " + _profileInfo.Dates,
                               FontAttributes = FontAttributes.Italic
                           }
                       }
                   };
        }
        #endregion
    }
}