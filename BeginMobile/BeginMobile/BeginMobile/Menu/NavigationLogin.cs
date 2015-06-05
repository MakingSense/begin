using BeginMobile.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Menu
{
    public class NavigationLogin : NavigationPage
    {
        public NavigationLogin(Page page)
            : base(page)
        {
            BarTextColor = Color.Black;
            BarBackgroundColor = Color.White;
            BackgroundColor = Color.White;
        }
    }
}
