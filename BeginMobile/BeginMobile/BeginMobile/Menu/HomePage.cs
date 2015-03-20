using BeginMobile.Interfaces;
using BeginMobile.Pages;
using BeginMobile.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Menu
{
    public class HomePage : MasterDetailPage
    {
        public HomePage(LoginUser loginUser)
        {
            Title = "Home";
            Icon = null;

            Master = new Menu(OnToggleRequest);
            //Master = new Menu(null);

            Detail = new AppHome();
            //Detail = new DetailPage();
        }

        private void OnToggleRequest()
        {
            IsPresented = !IsPresented;
        }
        
    }
}
