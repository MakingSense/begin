using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Groups: ContentPage
    {
        public Groups()
        {

            Title = "Groups";
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];

            var groupInformation = App.ProfileServices.GetGroups(currentUser.User.UserName, currentUser.AuthToken);

            var test = "";
            //Icon = "";
        }
    }
}
