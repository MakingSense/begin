using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Utils
{
    public class Styles
    {
        public static Style LinkButton()
        {
            return new Style(typeof (Button))
                   {
                       Setters =
                       {
                           new Setter {Property = Button.BackgroundColorProperty, Value = Color.Transparent},
                           new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                           new Setter {Property = Button.HeightRequestProperty, Value = 42},
                           new Setter {Property = Button.FontSizeProperty, Value = 14},
                       }
                   };
        }

        public static Style DefaultButton()
        {
            return new Style(typeof (Button))
                   {
                       Setters =
                       {
                           new Setter
                           {
                               Property = Button.BackgroundColorProperty,
                               Value = Color.FromHex("77D065")
                           },
                           new Setter {Property = Button.FontProperty, Value = Color.White}
                       }
                   };

        }


        public static Style Title()
        {
            return new Style(typeof (Label))
                   {
                       Setters =
                       {
                           new Setter {Property = Label.TextColorProperty, Value = Color.FromHex("77D065")},
                           new Setter {Property = Label.FontSizeProperty, Value = 24}
                       }
                   };

        }



        public static Style SubTitle()
        {
            return new Style(typeof (Label))
                   {
                       Setters =
                       {
                           new Setter {Property = Label.TextColorProperty, Value = Color.FromHex("77D065")},
                           new Setter {Property = Label.FontSizeProperty, Value = 16}
                       }
                   };

        }
    }
}
