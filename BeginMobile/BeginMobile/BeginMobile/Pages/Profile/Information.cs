using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;

namespace BeginMobile.Pages.Profile
{
    public class Information: ContentPage
    {
        
        private ProfileInfoUser profileInfoUser;

        public Information()
        {
            var currentUser = (LoginUser) App.Current.Properties["LoginUser"];
            
            ProfileManager profileManager = new ProfileManager();

            profileInfoUser = profileManager.GetProfileInformation(currentUser.User.UserName, currentUser.AuthToken);

            if (profileInfoUser == null)
            {
                profileInfoUser = new ProfileInfoUser();
            }
           
            
            Title = "Information";
            //Icon = "";

            //Name and surname
            var boxViewBegNameSurname = new BoxView() { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };
            var boxViewEndNameSurname = new BoxView() { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };

            //About me
            var boxViewBegAboutMe = new BoxView() { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };
            var boxViewEndAboutMe = new BoxView() { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };

            //Education and profession
            var boxViewBegEduProf = new BoxView() { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };
            var boxViewEndEduProf = new BoxView() { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };

            //Work experience
            var boxViewBegWorkExp = new BoxView() { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };
            var boxViewEndWorkExp = new BoxView() { Color = Color.White, WidthRequest = 100, HeightRequest = 2 };

            var stackInfo = new StackLayout()
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

            var sclViewInfo = new ScrollView()
            {
                Content = stackInfo
            };

            Content = sclViewInfo;
        }


        //Information details
        #region Information
        public StackLayout GetNameAndSurname()
        {
            var subStackNameAndSur = new StackLayout()
            {
                Spacing = 2,
                Padding = new Thickness(10, 0, 0, 0),
                Children =
                {
                    new Label()
                    {
                        Text = profileInfoUser.FirstName,
                        FontAttributes = FontAttributes.Italic,
                    }
                },

            };

            return subStackNameAndSur;
        }

        public StackLayout GetInformationDetail()
        {
            var subAboutMe = new StackLayout()
            {
                Spacing = 2,
                Padding = new Thickness(10, 0, 0, 0),
                Children = 
                {
                    new Label(){
                        Text = "Birthday: "+ profileInfoUser.BirthDay,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Gender: "+ profileInfoUser.Gender.ToString(),
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Address: "+ profileInfoUser.Address,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Country: "+ profileInfoUser.Country,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "State: "+ profileInfoUser.State,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "City: "+ profileInfoUser.City,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Phone: " + profileInfoUser.Phone.ToString(),
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Cell phone: " + profileInfoUser.CellPhone.ToString(),
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Email: " + profileInfoUser.Email,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Skype: " + profileInfoUser.Skype,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Others: " + profileInfoUser.Others,
                        FontAttributes = FontAttributes.Italic,
                    }
                }
            };

            return subAboutMe;
        }

        public StackLayout GetEducationProffesion()
        {
            return new StackLayout()
            {
                Spacing = 2,
                Padding = new Thickness(10, 0, 0, 0),
                Children =
                {
                    new Label()
                    {
                        Text = "Level: "+ profileInfoUser.EducationLevel,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Establishment: "+ profileInfoUser.Establishment,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Title: " + profileInfoUser.EducationTitle,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Category: " + profileInfoUser.EducationCategory,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Subcategory: "+ profileInfoUser.EducationSubcategory,
                        FontAttributes = FontAttributes.Italic,
                    }
                },

            };
        }

        public StackLayout GetWorkExperience()
        {
            return new StackLayout()
            {
                Spacing = 2,
                Padding = new Thickness(10, 0, 0, 0),
                Children =
                {
                    new Label()
                    {
                        Text = "Company: " + profileInfoUser.Company,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Position: " + profileInfoUser.Position,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "City: " + profileInfoUser.CityWork,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Work description: "+ profileInfoUser.WorkDescription,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "I currently work here: "+ profileInfoUser.CurrentWork,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Dates: " +  profileInfoUser.Dates,
                        FontAttributes = FontAttributes.Italic,
                    }
                },

            };
        }
        #endregion
    }
}
