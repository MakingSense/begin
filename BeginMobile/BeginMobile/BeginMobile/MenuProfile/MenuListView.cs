using Xamarin.Forms;

namespace BeginMobile.MenuProfile
{
    public class MenuListView: ListView
    {
        public MenuListView()
        {
            var dataMenuProfile = new BeginMobile.MenuProfile.MenuProfile();

            ItemsSource = dataMenuProfile;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;

            var imageCell = new DataTemplate(typeof (ImageCell));
            imageCell.SetBinding(TextCell.TextProperty, "Title");
            //cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");

            ItemTemplate = imageCell;
            SelectedItem = dataMenuProfile[0];

           
        }
    }
}
