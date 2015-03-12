using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BeginMobile.Menu
{
    public class HomePage : MasterDetailPage
    {
        public HomePage()
        {
            Title = "Home";
            Master = new Menu();
            Detail = new DetailPage();
        }

    }
}
