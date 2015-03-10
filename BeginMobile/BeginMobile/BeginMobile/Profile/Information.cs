using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
//using XamarinFormProfileApp.HttpClientCustom;

namespace BeginMobile.Profile
{
    public class Information: ContentPage
    {
        public Information()
        {
            //var listTest = new ManageEarthquake().GetEarthquakes();

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
                        Text = "James Eduardo",
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
                        Text = "Birthday: 03-jan-85",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Gender: M",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Address: 25 may avenue",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Country: Usa",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "State: Nebrasca",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "City: Griwell",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Phone: 591-256365",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Cell phone: 591-6993152",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Email: name@gmail.com",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Skype: nameuser",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label(){
                        Text = "Others: ",
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
                        Text = "Level: ",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Establishment: ",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Title: ",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Category: ",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Subcategory: ",
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
                        Text = "Company: ",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Position: ",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "City: ",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Work description: ",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "I currently work here: ",
                        FontAttributes = FontAttributes.Italic,
                    },
                    new Label()
                    {
                        Text = "Dates: ",
                        FontAttributes = FontAttributes.Italic,
                    }
                },

            };
        }
        #endregion
    }
}
