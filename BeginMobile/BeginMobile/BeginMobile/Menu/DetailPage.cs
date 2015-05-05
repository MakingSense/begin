using BeginMobile.LocalizeResources.Resources;
using Xamarin.Forms;

namespace BeginMobile.Menu
{
    public class DetailPage : ContentPage
        {
            public DetailPage()
            {
                Style = BeginApplication.Styles.PageStyle;
                Title = AppResources.LabelDetailPageHome.ToUpper();
                Icon = null;

                BackgroundColor = Color.FromHex("8A8A8A");

                var myHomeHeader = new Label
                {
                    Text = "Lorem",
                    HorizontalOptions = LayoutOptions.Center
                };

                Content = new StackLayout
                {
                    Children =
                                  {
                                      myHomeHeader,
                                      new Label
                                          {
                                              Text = ""
                                          }
                                  },
                };
            }
        }
	}
