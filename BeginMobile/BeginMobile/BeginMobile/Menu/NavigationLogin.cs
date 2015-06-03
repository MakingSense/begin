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

            /*this.PropertyChanged += async (o, s) =>
            {
                var navigationPage = CurrentPage as LoginMenu;
                if (!(navigationPage != null))
                {
                    BarTextColor = Color.Black;
                }
                else
                {
                    BarTextColor = Color.White;
                }
            };
            */
            
        }
    }
}
