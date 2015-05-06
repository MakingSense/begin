using System.Collections.Generic;
using BeginMobile.Pages.Profile;
using BeginMobile.LocalizeResources.Resources;

namespace BeginMobile.MenuProfile
{
    public class MenuProfile: List<MenuItem>
    {
        public MenuProfile()
        {
            this.Add(new MenuItem()
            {
                Title = AppResources.LabelTitleProfile,
                //IconSource = "contacts.png"
                TargetType = typeof(ProfileMe)
            });

            this.Add(new MenuItem()
            {
                Title = AppResources.ToolBarProfileMeMyAct,
                IconSource = "Icon.png", 
                TargetType = typeof(MyActivity)
            });

            this.Add(new MenuItem()
            {
                Title = AppResources.ToolBarProfileMeInfo,
                //IconSource = "contacts.png"
                TargetType = typeof(Information)
            });

            this.Add(new MenuItem()
            {
                Title = AppResources.ToolBarProfileMeMessages,
                //IconSource = "contacts.png"
                TargetType = typeof(Messages)
            });

            this.Add(new MenuItem()
            {
                Title = AppResources.ToolBarProfileMeContacts,
                //IconSource = "contacts.png"
                TargetType = typeof(Contacts)
            });

            this.Add(new MenuItem()
            {
                Title = AppResources.ToolBarProfileMeGroups,
                //IconSource = "contacts.png"
                TargetType = typeof(Groups)
            });

            this.Add(new MenuItem()
            {
                Title = AppResources.ToolBarProfileMeShop,
                //IconSource = "contacts.png"
                TargetType = typeof(Shop)
            });

            this.Add(new MenuItem()
            {
                Title = AppResources.ToolBarProfileMeEvents,
                //IconSource = "contacts.png"
                TargetType = typeof(Events)
            });

        }
    }
}
