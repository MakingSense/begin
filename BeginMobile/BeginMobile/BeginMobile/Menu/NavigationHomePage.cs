using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Menu
{
    class NavigationHomePage: NavigationPage
    {
        public NavigationHomePage(Page page) : base(page)
        {
            BarTextColor = Color.White;
            BarBackgroundColor = Color.FromRgb(60, 186, 133);
        }
    }
}
