using Xamarin.Forms;

namespace BeginMobile.MenuProfile
{
    public class MenuListView: ListView
    {
        public MenuListView()
        {
            var data = new BeginMobile.MenuProfile.MenuProfile();

            ItemsSource = data;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;

            var cell = new DataTemplate(typeof (ImageCell));
            cell.SetBinding(TextCell.TextProperty, "Title");
            //cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;
            SelectedItem = data[0];

           
        }
    }
}
