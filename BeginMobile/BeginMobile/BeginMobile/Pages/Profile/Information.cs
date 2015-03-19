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
        
        private ProfileInfo profileInfo;

        public Information()
        {
            var currentUser = (LoginUser) App.Current.Properties["LoginUser"];

            profileInfo = App.ProfileServices.GetInformation(currentUser.User.UserName, currentUser.AuthToken);

            if (profileInfo == null)
            {
                profileInfo = new ProfileInfo();
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
                        Text = profileInfo.FirstName,
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
                        Text = "Birthday: "+ profileInfo.BirthDay,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Gender: "+ profileInfo.Gender.ToString(),
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Address: "+ profileInfo.Address,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Country: "+ profileInfo.Country,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "State: "+ profileInfo.State,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "City: "+ profileInfo.City,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Phone: " + profileInfo.Phone.ToString(),
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Cell phone: " + profileInfo.CellPhone.ToString(),
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Email: " + profileInfo.Email,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Skype: " + profileInfo.Skype,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Others: " + profileInfo.Others,
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
                        Text = "Level: "+ profileInfo.EducationLevel,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Establishment: "+ profileInfo.Establishment,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Title: " + profileInfo.EducationTitle,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Category: " + profileInfo.EducationCategory,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Subcategory: "+ profileInfo.EducationSubcategory,
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
                        Text = "Company: " + profileInfo.Company,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Position: " + profileInfo.Position,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "City: " + profileInfo.CityWork,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Work description: "+ profileInfo.WorkDescription,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "I currently work here: "+ profileInfo.CurrentWork,
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Dates: " +  profileInfo.Dates,
                        FontAttributes = FontAttributes.Italic,
                    }
                },

            };
        }
        #endregion
    }
}
