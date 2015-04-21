using BeginMobile.LocalizeResources.Resources;
using Xamarin.Forms;

namespace BeginMobile.Pages
{
    public class TermsAndConditions : ContentPage
    {
        public TermsAndConditions(bool isLoadByLogin)
        {
            Title = AppResources.TermsAndConditionsTitle;

            var labelBody = new Label
                            {
                                Text = AppResources.TermsAndConditionsLabelTermsAndConditions,
                                Style = BeginApplication.Styles.BodyStyle
                            };

            if (isLoadByLogin)
            {
                var buttonBack = new Button
                             {
                                 Text = AppResources.TermsAndConditionsButtonGoBack,
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