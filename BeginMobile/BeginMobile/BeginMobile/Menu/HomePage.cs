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

            if (Device.OS != TargetPlatform.iOS)
                Icon = null;

            else
            {
                Icon = new FileImageSource();
                Icon.File = "Icon-57.png";
            }
            
            Master = new Menu(OnToggleRequest);
            Detail = new AppHome();
        }

        private void OnToggleRequest()
        {
            IsPresented = !IsPresented;
        }
    }
}
