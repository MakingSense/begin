using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Utils
{
    public class Styles
    {
        private string fontfamily = "";
        private double fontSizeMedium = 0.0;
        private double fontSizeLarge = 0.0;
        private double fontSizeSmall = 0.0;

        private double textfontSizeMedium = 0.0;
        private double textfontSizeLarge = 0.0;
        private double textFontSizeSmall = 0.0;

        private Setter buttonFontSize = new Setter();
        private Setter titleFontSize = new Setter();
        private Setter subTitleFontSize = new Setter();
        private Setter textBodyFontSize = new Setter();

        public Styles()
        {
            fontfamily = Device.OnPlatform(
                   iOS: "MarkerFelt-Thin",
                   Android: "Droid Sans Mono",
                   WinPhone: "Comic Sans MS"); //

            fontSizeMedium = Device.OnPlatform(
                                 Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                                 Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                                 Device.GetNamedSize(NamedSize.Medium, typeof(Button)));

            fontSizeLarge = Device.OnPlatform(
                          Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                          Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                          Device.GetNamedSize(NamedSize.Large, typeof(Button)));

            fontSizeSmall = Device.OnPlatform(
                  Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                  Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                  Device.GetNamedSize(NamedSize.Small, typeof(Button)));

            textfontSizeMedium = Device.OnPlatform(
      Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
      Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
      Device.GetNamedSize(NamedSize.Medium, typeof(Label)));

            textfontSizeLarge = Device.OnPlatform(
      Device.GetNamedSize(NamedSize.Large, typeof(Label)),
      Device.GetNamedSize(NamedSize.Large, typeof(Label)),
      Device.GetNamedSize(NamedSize.Large, typeof(Label)));

            textFontSizeSmall = Device.OnPlatform(
      Device.GetNamedSize(NamedSize.Small, typeof(Label)),
      Device.GetNamedSize(NamedSize.Small, typeof(Label)),
      Device.GetNamedSize(NamedSize.Small, typeof(Label)));

            var buttonFontSize = new Setter();
            var titleFontSize = new Setter();
            var subTitleFontSize = new Setter();
            var textBodyFontSize = new Setter();


            if (Device.Idiom == TargetIdiom.Tablet)
            {
                buttonFontSize.Property = Button.FontSizeProperty;
                buttonFontSize.Value = fontSizeLarge;
                titleFontSize.Property = Label.FontSizeProperty;
                titleFontSize.Value = textfontSizeLarge;
                subTitleFontSize.Property = Label.FontSizeProperty;
                subTitleFontSize.Value = textfontSizeMedium;
                textBodyFontSize.Property = Label.FontSizeProperty;
                textBodyFontSize.Value = 15;

            } if (Device.Idiom == TargetIdiom.Phone)
            {
                buttonFontSize.Property = Button.FontSizeProperty;
                buttonFontSize.Value = fontSizeMedium;
                titleFontSize.Property = Label.FontSizeProperty;
                titleFontSize.Value = textfontSizeMedium;
                subTitleFontSize.Property = Label.FontSizeProperty;
                subTitleFontSize.Value = textFontSizeSmall;
                textBodyFontSize.Property = Label.FontSizeProperty;
                textBodyFontSize.Value = textFontSizeSmall;
            }
        }
        public string FontFamily
        {
            get
            {
                return fontfamily;
            }
            set { fontfamily = value; }
        }

        public Style LinkButton
        {
            get
            {
                var style = new Style(typeof(Button))
                {
                    Setters =
                       {
                           new Setter {Property = Button.BackgroundColorProperty, Value = Color.Transparent},
                           new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                           new Setter {Property = Button.FontProperty, Value = fontfamily},
                       }
                };
                style.Setters.Add(buttonFontSize);

                return style;
            }
        }

        public Style DefaultButton
        {
            get
            {
                var style = new Style(typeof(Button))
                {
                    Setters =
                       {
                           new Setter {Property = Button.BackgroundColorProperty, Value = Color.FromHex("77D065")},
                           new Setter {Property = Button.BorderRadiusProperty, Value = 0},                      
                           new Setter {Property = Button.FontProperty, Value = Color.White},
                       }
                };
                style.Setters.Add(buttonFontSize);

                return style;
            }
        }

        public Style TitleStyle
        {
            get
            {
                Device.Styles.TitleStyle.Setters.Add(titleFontSize);
                return Device.Styles.TitleStyle;
            }
        }

        public Style SubtitleStyle
        {
            get
            {
                Device.Styles.SubtitleStyle.Setters.Add(subTitleFontSize);
                Device.Styles.SubtitleStyle.Setters.Add(new Setter { Property = Label.FontProperty, Value = Color.FromHex("77D065") });// TODO: Delete
                return Device.Styles.SubtitleStyle;
            }
        }


        public Style BodyStyle
        {
            get
            {
                Device.Styles.BodyStyle.Setters.Add(textBodyFontSize);

                return Device.Styles.BodyStyle;
            }
        }

        public Style CaptionStyle
        {
            get
            {
                return Device.Styles.CaptionStyle;
            }
        }


        public Style ListItemDetailTextStyle
        {
            get
            {
                return Device.Styles.ListItemDetailTextStyle;
            }
        }

        public Style ListItemTextStyle
        {
            get
            {
                return Device.Styles.ListItemTextStyle;
            }
        }
    }
}