using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class TermsAndConditions : ContentPage
    {
        public TermsAndConditions(bool isLoadByLogin)
        {
            Title = "Terms And Conditions";

            var labelBody = new Label
                            {
                                Text = "Terms And Conditions",
                                Style = BeginApplication.Styles.BodyStyle
                            };

            if (isLoadByLogin)
            {
                var buttonBack = new Button
                             {
                                 Text = "Go back",
                                 Style = BeginApplication.Styles.DefaultButton
                             };

                buttonBack.Clicked += (sender, e) =>
                                  {
                                      MessagingCenter.Send<ContentPage>(this, "Register");
                                  };

                Content = new StackLayout
                          {
                              Spacing = 20,
                              Padding = 50,
                              Children =
                              {
                                  labelBody,
                                  buttonBack
                              }
                          };
            }

            else
            {
                Content = new StackLayout
                          {
                              Spacing = 20,
                              Padding = 50,
                              Children =
                              {
                                  labelBody
                              }
                          };
            }
        }
    }
}