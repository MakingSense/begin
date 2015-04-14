using System.Collections.Generic;
using Xamarin.Forms;


namespace BeginMobile.MenuProfile
{
    public class MenuPage: ContentPage
    {
        public ListView ListViewMenu { get; set; }
        public MenuPage()
        {
            //Icon = "settings.png";
            Title = "Menu";
            BackgroundColor = Color.FromHex("333333");

            ListViewMenu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.FromHex("AAAAAA"),
                    Text = "MENU",
                }
            };

            //Layout
            var stackLayoutMain = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            stackLayoutMain.Children.Add(menuLabel);
            stackLayoutMain.Children.Add(ListViewMenu);

            //Content
            Content = stackLayoutMain;

        }
    }
}
