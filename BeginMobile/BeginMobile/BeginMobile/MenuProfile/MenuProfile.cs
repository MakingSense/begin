using System.Collections.Generic;
using BeginMobile.Profile;

namespace BeginMobile.MenuProfile
{
    public class MenuProfile: List<MenuItem>
    {
        public MenuProfile()
        {
            this.Add(new MenuItem()
            {
                Title = "Profile",
                //IconSource = "contacts.png"
                TargetType = typeof(ProfileMe)
            });

            this.Add(new MenuItem()
            {
                Title = "My activity",
                //IconSource = "contacts.png"
                TargetType = typeof(MyActivity)
            });

            this.Add(new MenuItem()
            {
                Title = "Information",
                //IconSource = "contacts.png"
                TargetType = typeof(Information)
            });

            this.Add(new MenuItem()
            {
                Title = "Messages",
                //IconSource = "contacts.png"
                TargetType = typeof(Messages)
            });

            this.Add(new MenuItem()
            {
                Title = "Contacts",
                //IconSource = "contacts.png"
                TargetType = typeof(Contacts)
            });

            this.Add(new MenuItem()
            {
                Title = "Groups",
                //IconSource = "contacts.png"
                TargetType = typeof(Groups)
            });

            this.Add(new MenuItem()
            {
                Title = "Shop",
                //IconSource = "contacts.png"
                TargetType = typeof(Shop)
            });

            this.Add(new MenuItem()
            {
                Title = "Events",
                //IconSource = "contacts.png"
                TargetType = typeof(Events)
            });

        }
    }
}
