using BeginMobile.Interfaces;
using BeginMobile.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace BeginMobile.Menu
{
    public class HomePage : MasterDetailPage
    {
        public HomePage(LoginUser loginUser)
        {
            Title = "Home";
            Icon = null;

            Master = new Menu(loginUser.User);
            Detail = new DetailPage();
        }

        
    }
}
