using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class MyActivity : ContentPage
    {
        private readonly LoginUser _currentUser;
        private ProfileInformationActivities _profileActivity;

        public MyActivity()
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = AppResources.LabelActivityTitle;
            _currentUser = (LoginUser) BeginApplication.Current.Properties["LoginUser"];

            Init();
        }

        private async Task Init()
        {
            _profileActivity =
                await BeginApplication.ProfileServices.GetActivities(_currentUser.User.UserName, _currentUser.AuthToken);
            var listDataSource = new ObservableCollection<ActivityViewModel>();

            if (_profileActivity != null)
            {
                var listActivityViewModel = from activity in _profileActivity.Activities
                    where activity.Component.Equals("activity", StringComparison.InvariantCultureIgnoreCase)
                    select new ActivityViewModel
                           {
                               Icon = BeginApplication.Styles.DefaultActivityIcon,
                               //TODO:change for activity avatar if this exist
                               NameSurname = _profileActivity.NameSurname,
                               ActivityDescription = activity.Content,
                               ActivityType = activity.Type,
                               DateAndTime = activity.Date
                           };

                foreach (var activityViewModel in listActivityViewModel)
                {
                    listDataSource.Add(activityViewModel);
                }
            }

            var listViewTemplate = new DataTemplate(typeof (Activities));
            var listViewActivities = new ListView
                                     {
                                         ItemsSource = listDataSource,
                                         ItemTemplate = listViewTemplate
                                     };

            listViewActivities.ItemSelected += (s, e) =>
                                               {
                                                   if (e.SelectedItem == null)
                                                   {
                                                       return;
                                                   }

                                                   ((ListView) s).SelectedItem = null;
                                               };

            listViewActivities.HasUnevenRows = true;


            var gridMainComponents = new Grid
            {
                Padding = BeginApplication.Styles.LayoutThickness,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      
                                  }
            };

            gridMainComponents.Children.Add(listViewActivities, 0, 0);


            Content = gridMainComponents;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.Content = null;
            _profileActivity = null;
        }
    }
}