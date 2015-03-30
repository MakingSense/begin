using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Utils
{
    public class Styles
    {
        private string fontfamily = "";
        private double fontSizeForButtonMedium = 0.0;
        private double fontSizeButtonLarge = 0.0;
        private double fontSizeButtonSmall = 0.0;

        private double textfontSizeMedium = 0.0;
        private double textfontSizeLarge = 0.0;
        private double textFontSizeSmall = 0.0;

        private Setter buttonFontSize = null;
        private Setter titleFontSize = null;
        private Setter subTitleFontSize = null;
        private Setter textBodyFontSize = null;

        public Styles()
        {
            fontfamily = Device.OnPlatform(
                   iOS: "Helvetica",
                   Android: "Droid Sans Mono",
                   WinPhone: "Comic Sans MS"); //

            fontSizeForButtonMedium = Device.OnPlatform(
                                 Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                                 Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                                 Device.GetNamedSize(NamedSize.Medium, typeof(Button)));

            fontSizeButtonLarge = Device.OnPlatform(
                          Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                          Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                          Device.GetNamedSize(NamedSize.Large, typeof(Button)));

            fontSizeButtonSmall = Device.OnPlatform(
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

            buttonFontSize = new Setter();
            titleFontSize = new Setter();
            subTitleFontSize = new Setter();
            textBodyFontSize = new Setter();


            if (Device.Idiom == TargetIdiom.Tablet)
            {
                buttonFontSize.Property = Button.FontSizeProperty;
                buttonFontSize.Value = fontSizeButtonLarge;
                titleFontSize.Property = Label.FontSizeProperty;
                titleFontSize.Value = textfontSizeLarge;
                subTitleFontSize.Property = Label.FontSizeProperty;
                subTitleFontSize.Value = textfontSizeMedium;
                textBodyFontSize.Property = Label.FontSizeProperty;
                textBodyFontSize.Value = textFontSizeSmall;

            } if (Device.Idiom == TargetIdiom.Phone)
            {
                buttonFontSize.Property = Button.FontSizeProperty;
                buttonFontSize.Value = fontSizeForButtonMedium;
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
                var style = new Style(typeof (Button))
                            {
                                Setters =
                                {
                                    new Setter {Property = Button.BackgroundColorProperty, Value = Color.Transparent},
                                    new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                                    new Setter {Property = Button.FontFamilyProperty, Value = FontFamily},
                                    new Setter {Property = Button.FontSizeProperty, Value = fontSizeButtonSmall},
                                    new Setter
                                    {
                                        Property = Button.TextColorProperty,
                                        Value = Device.OnPlatform<Color>
                                            (iOS: Color.FromHex("354B60"),
                                                Android: Color.White,
                                                WinPhone: Color.White)
                                    },
                                }
                            };                

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
                           new Setter {Property = Button.BorderRadiusProperty, Value = 8},                      
                           new Setter {Property = Button.TextColorProperty, Value = Color.White},
                           new Setter {Property = Button.FontFamilyProperty, Value = FontFamily},
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
                Device.Styles.TitleStyle.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = FontFamily });                
                return Device.Styles.TitleStyle;
            }
        }

        public Style SubtitleStyle
        {
            get
            {
                Device.Styles.SubtitleStyle.Setters.Add(new Setter
                                                        {
                                                            Property = Label.TextColorProperty,
                                                            Value =
                                                                Device.OnPlatform<Color>(
                                                                    iOS: Color.FromHex("354B60"),
                                                                    Android: Color.FromHex("77D065"),
                                                                    WinPhone: Color.FromHex("77D065"))
                                                        });

                Device.Styles.SubtitleStyle.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = FontFamily });
                Device.Styles.SubtitleStyle.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = textfontSizeMedium });
                return Device.Styles.SubtitleStyle;
            }
        }

        public Style BodyStyle
        {
            get
            {
                Device.Styles.BodyStyle.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = FontFamily });
                Device.Styles.BodyStyle.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = Device.OnPlatform<double>(iOS: 7, Android: 12, WinPhone: 12) });
                return Device.Styles.BodyStyle;
            }
        }

        public Style CaptionStyle
        {
            get
            {
                Device.Styles.CaptionStyle.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = FontFamily });
                return Device.Styles.CaptionStyle;
            }
        }


        public Style ListItemDetailTextStyle
        {
            get
            {
                Device.Styles.ListItemDetailTextStyle.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = FontFamily });
                Device.Styles.ListItemDetailTextStyle.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = Device.OnPlatform<double>(iOS: 7, Android: 12, WinPhone: 12) });
                return Device.Styles.ListItemDetailTextStyle;
            }
        }

        public Style ListItemTextStyle
        {
            get
            {
                Device.Styles.ListItemTextStyle.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = FontFamily });
                Device.Styles.ListItemTextStyle.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = Device.OnPlatform<double>(iOS: 8, Android: 15, WinPhone: 15)});
                return Device.Styles.ListItemTextStyle;
            }
        }

        public Color BlackBackground
        {
            get
            {
                return Color.Black;
            }
        }

        public Color BrownBackground
        {
            get
            {
                return Color.Black;
            }
        }

        public Color LabelTextColor
        {
            get
            {
                return Device.OnPlatform<Color>
                    (iOS: Color.FromHex("354B60"),
                        Android: Color.FromHex("77D065"),
                        WinPhone: Color.FromHex("77D065"));
            }
        }

        public Color MenuBackground
        {
            get
            {
                return Device.OnPlatform<Color>
                    (iOS: Color.FromHex("ECECEC"),
                        Android: Color.FromHex("323232"),
                        WinPhone: Color.FromHex("323232"));
            }
        }
        public Style StyleNavigationTitle
        {
            get
            {
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    return new Style(typeof(Label)) { Setters = { new Setter { Property = Label.FontSizeProperty, Value = Device.OnPlatform<double>(iOS: 12, Android: 12, WinPhone: 12) } } };
                }
                else
                {
                    return new Style(typeof(Label)) { Setters = { new Setter { Property = Label.FontSizeProperty, Value = Device.OnPlatform<double>(iOS: 8, Android: 10, WinPhone: 10) } } };
                }
               
            }
        }
         public Thickness LayoutThickness
        {
            get
            {
                return Device.OnPlatform<Thickness>(
                    iOS: new Thickness(20, 0, 0, 0),
                    Android: new Thickness(20, 0, 0, 0),
                    WinPhone: new Thickness(20, 0, 0, 0));
            }
        }

         public Thickness ListDetailThickness
         {
             get
             {
                 return Device.OnPlatform( 
                    iOS: new Thickness(10, 5, 10, 5),
                    Android: new Thickness(10, 5, 10, 5),
                    WinPhone: new Thickness(10, 5, 10, 5));
                 
             }
         }

    }
}