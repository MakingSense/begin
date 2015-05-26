using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Utils
{
    public class Styles
    {
        private readonly double _fontSizeButtonSmall;
        private readonly double _textfontSizeMedium;
        private readonly double _textfontSizeLarge;
        private readonly double _textFontSizeSmall;
        private readonly Setter _buttonFontSize;
        private readonly Setter _titleFontSize;
        private readonly Setter titleFontSize;
        private readonly Setter subTitleFontSize;
        private readonly Setter textBodyFontSize;

        public Styles()
        {
            FontFamily = Device.OnPlatform("Helvetica", "Roboto", "Helvetica");

            var fontSizeForButtonMedium = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Medium, typeof (Button)),
                Device.GetNamedSize(NamedSize.Medium, typeof (Button)),
                Device.GetNamedSize(NamedSize.Medium, typeof (Button)));

            var fontSizeButtonLarge = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Large, typeof (Button)),
                Device.GetNamedSize(NamedSize.Large, typeof (Button)),
                Device.GetNamedSize(NamedSize.Large, typeof (Button)));

            _fontSizeButtonSmall = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Small, typeof (Button)),
                Device.GetNamedSize(NamedSize.Small, typeof (Button)),
                Device.GetNamedSize(NamedSize.Small, typeof (Button)));

            _textfontSizeMedium = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Medium, typeof (Label)),
                Device.GetNamedSize(NamedSize.Medium, typeof (Label)),
                Device.GetNamedSize(NamedSize.Medium, typeof (Label)));

            _textfontSizeLarge = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Large, typeof (Label)),
                Device.GetNamedSize(NamedSize.Large, typeof (Label)),
                Device.GetNamedSize(NamedSize.Large, typeof (Label)));

            _textFontSizeSmall = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Small, typeof (Label)),
                Device.GetNamedSize(NamedSize.Small, typeof (Label)),
                Device.GetNamedSize(NamedSize.Small, typeof (Label)));

            _buttonFontSize = new Setter();
            _titleFontSize = new Setter();
            titleFontSize = new Setter();
            titleFontSize = new Setter();
            subTitleFontSize = new Setter();
            textBodyFontSize = new Setter();

            _buttonFontSize.Property = Button.FontSizeProperty;
            _buttonFontSize.Value = fontSizeButtonLarge;
            _titleFontSize.Property = Label.FontSizeProperty;
            _titleFontSize.Value = _textfontSizeLarge;
            titleFontSize.Property = Label.FontSizeProperty;
            titleFontSize.Value = _textfontSizeLarge;
            subTitleFontSize.Property = Label.FontSizeProperty;
            subTitleFontSize.Value = _textfontSizeMedium;
            textBodyFontSize.Property = Label.FontSizeProperty;
            textBodyFontSize.Value = _textFontSizeSmall;


            //if (Device.Idiom == TargetIdiom.Tablet)
            //{
            //    _buttonFontSize.Property = Button.FontSizeProperty;
            //    _buttonFontSize.Value = fontSizeButtonLarge;
            //    _titleFontSize.Property = Label.FontSizeProperty;
            //    _titleFontSize.Value = _textfontSizeLarge;
            //    titleFontSize.Property = Label.FontSizeProperty;
            //    titleFontSize.Value = _textfontSizeLarge;
            //    subTitleFontSize.Property = Label.FontSizeProperty;
            //    subTitleFontSize.Value = _textfontSizeMedium;
            //    textBodyFontSize.Property = Label.FontSizeProperty;
            //    textBodyFontSize.Value = _textFontSizeSmall;
            //}
            //if (Device.Idiom == TargetIdiom.Phone)
            //{
            //    _buttonFontSize.Property = Button.FontSizeProperty;
            //    _buttonFontSize.Value = fontSizeForButtonMedium;
            //    _titleFontSize.Property = Label.FontSizeProperty;
            //    _titleFontSize.Value = _textfontSizeMedium;
            //    titleFontSize.Property = Label.FontSizeProperty;
            //    titleFontSize.Value = _textfontSizeMedium;
            //    subTitleFontSize.Property = Label.FontSizeProperty;
            //    subTitleFontSize.Value = _textFontSizeSmall;
            //    textBodyFontSize.Property = Label.FontSizeProperty;
            //    textBodyFontSize.Value = _textFontSizeSmall;
            //}
        }

        public Page CurrentPage { get; set; }
        public string FontFamily { get; set; }
        public double SubtitleFontSize { get { return _textfontSizeMedium; } }

        public Style LinkButton
        {
            get
            {
                var style = new Style(typeof (Button))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value = Color.Transparent
                                    },
                                    new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                                    new Setter {Property = Button.FontFamilyProperty, Value = FontFamily},
                                    new Setter {Property = Button.FontSizeProperty, Value = _fontSizeButtonSmall},
                                    new Setter
                                    {
                                        Property = Button.TextColorProperty,
                                        Value = Device.OnPlatform
                                            (Color.FromHex("646567"), Color.FromHex("646567"), Color.FromHex("646567"))
                                    },
                                }
                            };
                style.Setters.Add(_buttonFontSize);
                return style;
            }
        }

        public Style LinkLabelButton
        {
            get
            {
                var style = new Style(typeof (Button))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value = Color.Transparent
                                    },
                                    new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                                    new Setter {Property = Button.FontFamilyProperty, Value = FontFamily},
                                    new Setter {Property = Button.FontSizeProperty, Value = _textFontSizeSmall},
                                    new Setter
                                    {
                                        Property = Button.TextColorProperty,
                                        Value = Device.OnPlatform
                                            (Color.FromHex("646567"), Color.FromHex("646567"), Color.FromHex("646567"))
                                    },
                                }
                            };

                return style;
            }
        }
        public Style InitialPageTitleStyle
        {
            get
            {
                Device.Styles.TitleStyle.Setters.Add(_titleFontSize);
                Device.Styles.TitleStyle.Setters.Add(new Setter
                {
                    Property = Label.FontFamilyProperty,
                    Value = FontFamily
                });
                Device.Styles.TitleStyle.Setters.Add(new Setter
                {
                    Property = Label.TextColorProperty,
                    Value = Device.OnPlatform(Color.FromHex("444444"),
                        Color.FromHex("444444"), Color.FromHex("444444"))
                });
                Device.Styles.TitleStyle.Setters.Add(new Setter
                {
                    Property = Label.FontAttributesProperty,
                    Value = FontAttributes.Bold
                });              
                return Device.Styles.TitleStyle;
            }
        }
        public Style UploadLinkLabelButton
        {
            get
            {
                var style = new Style(typeof (Button))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value = Color.Transparent
                                    },
                                    new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                                    new Setter {Property = Button.FontFamilyProperty, Value = FontFamily},
                                    new Setter {Property = Button.FontSizeProperty, Value = 32},
                                    //new Setter {Property = Button.FontAttributesProperty = FontAttributes.Bold},
                                    new Setter
                                    {
                                        Property = Button.TextColorProperty,
                                        Value = Device.OnPlatform
                                            (Color.FromHex("43BB88"), Color.FromHex("43BB88"), Color.FromHex("646567"))
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
                var style = new Style(typeof (Button))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value = Device.OnPlatform(Color.FromHex("43BB88"),
                                            Color.FromHex("43BB88"), Color.FromHex("43BB88")),
                                    },
                                    new Setter {Property = Button.BorderRadiusProperty, Value = 25},
                                    new Setter
                                    {
                                        Property = Button.TextColorProperty,
                                        Value = Device.OnPlatform(Color.FromHex("444444"),
                                            Color.FromHex("444444"), Color.FromHex("444444"))
                                    },
                                    new Setter {Property = Button.FontFamilyProperty, Value = FontFamily},
                                }
                            };
                style.Setters.Add(_buttonFontSize);

                return style;
            }
        }

        public Style TitleStyle
        {
            get
            {
                Device.Styles.TitleStyle.Setters.Add(new Setter
                                                     {
                                                         Property = Label.FontFamilyProperty,
                                                         Value = FontFamily
                                                     });
                Device.Styles.TitleStyle.Setters.Add(new Setter
                                                     {
                                                         Property = Label.TextColorProperty,
                                                         Value = Device.OnPlatform(Color.FromHex("646566"),
                                                             Color.FromHex("646566"), Color.FromHex("646566"))
                                                     });
                Device.Styles.TitleStyle.Setters.Add(new Setter
                                                     {
                                                         Property = Label.FontAttributesProperty,
                                                         Value = FontAttributes.Bold
                                                     });
                Device.Styles.TitleStyle.Setters.Add(titleFontSize);
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
                                                                Device.OnPlatform(Color.FromHex("646566"),
                                                                    Color.FromHex("646566"), Color.FromHex("646566"))
                                                        });

                Device.Styles.SubtitleStyle.Setters.Add(new Setter
                                                        {
                                                            Property = Label.FontFamilyProperty,
                                                            Value = FontFamily
                                                        });
                Device.Styles.SubtitleStyle.Setters.Add(new Setter
                                                        {
                                                            Property = Label.FontSizeProperty,
                                                            Value = _textfontSizeMedium
                                                        });
                Device.Styles.SubtitleStyle.Setters.Add(new Setter
                                                        {
                                                            Property = Label.FontAttributesProperty,
                                                            Value = FontAttributes.None
                                                        });
                Device.Styles.SubtitleStyle.Setters.Add(subTitleFontSize);
                return Device.Styles.SubtitleStyle;
            }
        }

        public Style TextBodyStyle
        {
            get
            {
                Device.Styles.BodyStyle.Setters.Add(new Setter {Property = Label.FontFamilyProperty, Value = FontFamily});
                Device.Styles.BodyStyle.Setters.Add(new Setter
                                                    {
                                                        Property = Label.TextColorProperty,
                                                        Value =
                                                            Device.OnPlatform(Color.FromHex("646566"),
                                                                Color.FromHex("646566"), Color.FromHex("646566"))
                                                    });
                Device.Styles.BodyStyle.Setters.Add(new Setter
                                                    {
                                                        Property = Label.FontSizeProperty,
                                                        Value =
                                                            Device.OnPlatform<double>(12, 12, 12)
                                                    });
                return Device.Styles.BodyStyle;
            }
        }

        public Style CaptionStyle
        {
            get
            {
                Device.Styles.CaptionStyle.Setters.Add(new Setter
                                                       {
                                                           Property = Label.FontFamilyProperty,
                                                           Value = FontFamily
                                                       });
                Device.Styles.CaptionStyle.Setters.Add(new Setter
                                                       {
                                                           Property = Label.TextColorProperty,
                                                           Value =
                                                               Device.OnPlatform(Color.FromHex("646566"),
                                                                   Color.FromHex("646566"), Color.FromHex("646566"))
                                                       });
                return Device.Styles.CaptionStyle;
            }
        }


        public Style ListItemDetailTextStyle
        {
            get
            {
                Device.Styles.ListItemDetailTextStyle.Setters.Add(new Setter
                                                                  {
                                                                      Property = Label.FontFamilyProperty,
                                                                      Value = FontFamily
                                                                  });
                Device.Styles.ListItemDetailTextStyle.Setters.Add(new Setter
                                                                  {
                                                                      Property = Label.FontSizeProperty,
                                                                      Value =
                                                                          Device.OnPlatform<double>(7, 12, 12)
                                                                  });
                Device.Styles.ListItemDetailTextStyle.Setters.Add(new Setter
                                                                  {
                                                                      Property = Label.TextColorProperty,
                                                                      Value =
                                                                          Device.OnPlatform(Color.FromHex("646566"),
                                                                              Color.FromHex("646566"),
                                                                              Color.FromHex("646566"))
                                                                  });
                return Device.Styles.ListItemDetailTextStyle;
            }
        }

        public Style ListItemTextStyle
        {
            get
            {
                Device.Styles.ListItemTextStyle.Setters.Add(new Setter
                                                            {
                                                                Property = Label.FontFamilyProperty,
                                                                Value = FontFamily
                                                            });
                Device.Styles.ListItemTextStyle.Setters.Add(new Setter
                                                            {
                                                                Property = Label.FontSizeProperty,
                                                                Value =
                                                                    Device.OnPlatform<double>(8, 15, 15)
                                                            });
                Device.Styles.ListItemTextStyle.Setters.Add(new Setter
                                                            {
                                                                Property = Label.TextColorProperty,
                                                                Value =
                                                                    Device.OnPlatform(Color.FromHex("646566"),
                                                                        Color.FromHex("646566"), Color.FromHex("646566"))
                                                            });
                return Device.Styles.ListItemTextStyle;
            }
        }

        public Color BlackBackground
        {
            get { return Color.Black; }
        }

        public Color BrownBackground
        {
            get { return Color.Black; }
        }

        public Color LabelTextColor
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("646566"), Color.FromHex("646566"), Color.FromHex("646566"));
            }
        }

        public Color MenuBackground
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("222222"),
                        Color.FromHex("222222"), Color.FromHex("222222"));
            }
        }

        public Style SearchContainer
        {
            get
            {
                var style = new Style(typeof (StackLayout))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value = Device.OnPlatform
                                            (Color.FromHex("43BB88"), Color.FromHex("43BB88"), Color.FromHex("43BB88")),
                                    },
                                    new Setter
                                    {
                                        Property = Layout.PaddingProperty,
                                        Value = Device.Idiom == TargetIdiom.Phone
                                            ? Device.OnPlatform(5, 5, 5)
                                            : Device.OnPlatform(10, 10, 10)
                                    }
                                }
                            };
                return style;
            }
        }

        public Style StyleNavigationTitle
        {
            get
            {
                return Device.Idiom == TargetIdiom.Tablet
                    ? new Style(typeof (Label))
                      {
                          Setters =
                          {
                              new Setter
                              {
                                  Property = Label.FontSizeProperty,
                                  Value =
                                      Device.OnPlatform<double>(12, 12, 12)
                              }
                          }
                      }
                    : new Style(typeof (Label))
                      {
                          Setters =
                          {
                              new Setter
                              {
                                  Property = Label.FontSizeProperty,
                                  Value =
                                      Device.OnPlatform<double>(8, 10, 10)
                              }
                          }
                      };

            }
        }

        public Thickness ThicknessMainLayout
        {
            get
            {
                return new Thickness(0, 10, 0, 50);
            }
        }
        public Thickness ThicknessInsideListView
        {
            get
            {
                return new Thickness(15, 10, 15, 10);

            }
        }
        public Thickness ThicknessBetweenImageAndDetails
        {
            get
            {
                return Device.OnPlatform(new Thickness(5, 0, 0, 0), new Thickness(5, 0, 0, 0),
                    new Thickness(5, 0, 0, 0));
            }
        }

        public Thickness InitialPagesThickness
        {
            get
            {
                return Device.Idiom == TargetIdiom.Phone
                    ? new Thickness(20, 5, 20, 5)
                    : new Thickness(50, 5, 50, 5);

            }
        }

        public Thickness ListDetailThickness
        {
            get
            {
                return Device.OnPlatform(new Thickness(10, 5, 10, 5), new Thickness(10, 5, 10, 5),
                    new Thickness(10, 5, 10, 5));
            }
        }

        public Style LabelTextDate
        {
            get
            {
                return new Style(typeof (Label))
                       {
                           Setters =
                           {
                               new Setter {Property = Label.FontFamilyProperty, Value = FontFamily},
                               new Setter
                               {
                                   Property = Label.FontSizeProperty,
                                   Value = Device.OnPlatform<double>(7, 12, 12)
                               }
                           }
                       };
            }
        }

        public Style LabelLargeTextTitle
        {
            get
            {
                return new Style(typeof (Label))
                       {
                           Setters =
                           {
                               new Setter {Property = Label.FontFamilyProperty, Value = FontFamily},
                               new Setter
                               {
                                   Property = Label.FontSizeProperty,
                                   Value = Device.OnPlatform<double>(16, 16, 16)
                               }
                           }
                       };
            }
        }

        public Style EntryStyle
        {
            get
            {
                return new Style(typeof (Entry))
                       {
                           Setters =
                           {
                               new Setter
                               {
                                   Property = Entry.TextColorProperty,
                                   Value =
                                       Device.OnPlatform(Color.FromHex("646566"), Color.FromHex("646566"),
                                           Color.FromHex("646566"))
                               }
                           }
                       };
            }
        }

        public Thickness GridPadding
        {
            get
            {
                return Device.OnPlatform(new Thickness(10, 5, 10, 5), new Thickness(10, 5, 10, 5),
                    new Thickness(10, 5, 10, 5));
            }
        }

        public double TextFontSizeMedium
        {
            get { return _textfontSizeMedium; }
        }

        public double TextFontSizeSmall
        {
            get { return _textFontSizeSmall; }
        }

        public double TextFontSizeLarge
        {
            get { return _textfontSizeLarge; }
        }

        public Color ColorGreenDroidBlueSapphireIos
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("126180"), Color.FromHex("77D065"), Color.FromHex("77D065"));
            }
        }

        public Color ColorWhiteDroidBlueIos
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("354B60"), Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"));
            }
        }

        public Color MenuTextOptionsColor
        {
            get
            {
                //646566	
                return Device.OnPlatform
                    (Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"));
            }
        }

        //details page color

        public Color UploadBackgroundColor
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("E6E6E6"), Color.FromHex("E6E6E6"), Color.FromHex("E6E6E6"));
            }
        }

        //Navigation Background color
        public Color NavigationBackgroundColor
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("E6E6E6"), Color.FromHex("E6E6E6"), Color.FromHex("E6E6E6"));
            }
        }

        //main page color
        public Color PageContentBackgroundColor
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("E6E6E6"), Color.FromHex("E6E6E6"), Color.FromHex("E6E6E6"));
            }
        }

        public Style PageStyle
        {
            get
            {
                var style = new Style(typeof (Page))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value =
                                            Device.OnPlatform(Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"),
                                                Color.FromHex("FFFFFF"))
                                    },
                                    //new Setter {Property = Page.PaddingProperty, Value =  Device.OnPlatform(new Thickness(10, 5, 10, 5), new Thickness(10, 5, 10, 5),new Thickness(10, 5, 10, 5))}                                                                                  
                                }
                            };

                return style;
            }
        }

        public Style InitialPageStyle
        {
            get
            {
                var style = new Style(typeof (Page))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value =
                                            Device.OnPlatform(Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"),
                                                Color.FromHex("FFFFFF"))
                                    },
                                    //new Setter {Property = Page.PaddingProperty, Value =  Device.OnPlatform(new Thickness(50, 5, 50, 5),
                                    //    new Thickness(50, 5, 50, 5),
                                    //    new Thickness(50, 5, 50, 5))}                                                                                  
                                }
                            };

                return style;
            }
        }

        public Style PickerStyle
        {
            get
            {
                var style = new Style(typeof (Picker))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value =
                                            Device.OnPlatform(Color.FromHex("E6E6E6"), Color.FromHex("E6E6E6"),
                                                Color.FromHex("E6E6E6"))
                                    },
                                    new Setter {Property = Picker.TitleProperty, Value = Color.Black}
                                }
                            };

                return style;
            }
        }

        public Style ListViewItemButton
        {
            get
            {
                var style = new Style(typeof (Button))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value = Color.FromHex("646567")
                                    },
                                    new Setter
                                    {
                                        Property = VisualElement.HeightRequestProperty,
                                        Value = Device.OnPlatform<double>(35, 35, 35)
                                    },
                                    new Setter {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Start},
                                    new Setter {Property = Button.TextColorProperty, Value = Color.FromHex("EFEFF0")},
                                    new Setter {Property = Button.BorderRadiusProperty, Value = 2},
                                    new Setter {Property = Button.FontFamilyProperty, Value = FontFamily}
                                }
                            };
                style.Setters.Add(new Setter
                                  {
                                      Property = Button.FontSizeProperty,
                                      Value = Device.OnPlatform<double>(12, 12, 12)
                                  });
                return style;
            }
        }

        public Color ApplicationGreenColor
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("43BB88"), Color.FromHex("43BB88"), Color.FromHex("43BB88"));
            }
        }

        public Color ApplicationPageColor
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("E6E6E6"), Color.FromHex("E6E6E6"), Color.FromHex("E6E6E6"));
            }
        }

        public Color ColorLine
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("354B60"), Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"));
            }
        }

        public Color ColorGrayDroidDSkyIos
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("DBE1ED"), Color.FromHex("6D7075"), Color.FromHex("6D7075"));
            }
        }

        public Color ColorWhiteBackground
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"));
            }
        }

        public Color DefaultColorButton
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("646567"), Color.FromHex("646567"), Color.FromHex("646567"));
            }
        }

        public Color TabSelectedTextColor
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("000000"), Color.FromHex("000000"), Color.FromHex("000000"));
            }
        }

        public Color ColorBrown
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("444444"), Color.FromHex("444444"), Color.FromHex("444444"));
            }
        }
        public Color ColorWhite
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"));
            }
        }

        public Color DefaultProfileMeBannerColor
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("42595C"), Color.FromHex("42595C"), Color.FromHex("42595C"));
            }
        }
      
        public Style TabUnderLine
        {
            get
            {
                return new Style(typeof (BoxView))
                       {
                           Setters =
                           {
                               new Setter
                               {
                                   Property = BoxView.ColorProperty,
                                   Value =
                                       Device.OnPlatform(Color.FromHex("4EBD8C"), Color.FromHex("4EBD8C"),
                                           Color.FromHex("4EBD8C"))
                               },
                               new Setter {Property = VisualElement.HeightRequestProperty, Value = 5},
                               new Setter {Property = VisualElement.WidthRequestProperty, Value = 100}
                           }
                       };
            }
        }

        public Style TabUnderLineInactive
        {
            get
            {
                return new Style(typeof(BoxView))
                {
                    Setters =
                           {
                               new Setter
                               {
                                   Property = BoxView.ColorProperty,
                                   Value =
                                       Device.OnPlatform(Color.FromHex("aaaaaa"), Color.FromHex("aaaaaa"),
                                           Color.FromHex("aaaaaa"))
                               },
                               new Setter {Property = VisualElement.HeightRequestProperty, Value = 5},
                               new Setter {Property = VisualElement.WidthRequestProperty, Value = 100}
                           }
                };
            }
        }

        public Style MessageContentStyle
        {
            get
            {
                var style = new Style(typeof (InputView))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value =
                                            Device.OnPlatform(Color.FromHex("ffffff"), Color.FromHex("ffffff"),
                                                Color.FromHex("ffffff")),
                                    }
                                }
                            };
                return style;
            }
        }


        public Style MessageNavigationButton
        {
            get
            {
                var style = new Style(typeof (Button))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value = Color.Transparent
                                    },
                                    new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                                    new Setter {Property = Button.FontFamilyProperty, Value = FontFamily},
                                    new Setter {Property = Button.FontSizeProperty, Value = _fontSizeButtonSmall},
                                    new Setter
                                    {
                                        Property = Button.TextColorProperty,
                                        Value = Device.OnPlatform
                                            (Color.FromHex("354B60"), Color.White, Color.White)
                                    },
                                }
                            };

                return style;
            }
        }


        public Thickness GridOfListView
        {
            get
            {
                return Device.OnPlatform(new Thickness(15, 5, 0,5), new Thickness(15, 5, 0, 5),
                    new Thickness(15, 5, 0, 5));
            }
        }

        #region "Wall page styles"
        /*
         * Wall page Style
         */
        public Style ListTitleWallStyle
        {
            get
            {

                return new Style(typeof(Label))
                {
                    Setters =
                           {
                               new Setter {Property = Label.FontFamilyProperty, Value = FontFamily},
                               new Setter
                               {
                                   Property = Label.FontSizeProperty,
                                   Value = Device.OnPlatform<double>(16, 16, 16)
                               },
                               new Setter
                                {
                                    Property = Label.TextColorProperty,
                                    Value =
                                        Device.OnPlatform(Color.FromHex("444444"),
                                            Color.FromHex("444444"), Color.FromHex("444444"))
                                },
                                new Setter
                                {
                                    Property = Label.FontAttributesProperty,
                                    Value = FontAttributes.Bold
                                }
                           }
                };
            }
        }

        public Style ListDescriptionWallStyle
        {
            get
            {
                return new Style(typeof(Label))
                {
                    Setters =
                           {
                               new Setter {Property = Label.FontFamilyProperty, Value = FontFamily},
                               new Setter
                               {
                                   Property = Label.FontSizeProperty,
                                   Value = Device.OnPlatform<double>(16, 16, 16)
                               },
                               new Setter
                                {
                                    Property = Label.TextColorProperty,
                                    Value =
                                        Device.OnPlatform(Color.FromHex("C3C3C3"),
                                            Color.FromHex("C3C3C3"), Color.FromHex("C3C3C3"))
                                },
                                new Setter
                                {
                                    Property = Label.FontAttributesProperty,
                                    Value = FontAttributes.Bold
                                }
                           }
                };
            }
        }

        public Thickness WallPageGridRowListView
        {
            get
            {
                return Device.OnPlatform(new Thickness(8, 0, 0, 0), new Thickness(8, 0, 0, 0),
                    new Thickness(8, 0, 0, 0));
            }
        }

        #endregion

        /*
         * Standard Styles based on design
         */
        public Thickness PageStandardThickness
        {
            get
            {
                return Device.Idiom == TargetIdiom.Phone
                    ? new Thickness(10, 0, 10, 0)
                    : new Thickness(10, 0, 10, 0);

            }
        }

        public Style PageCircleImageCommon
        {
            get
            {
                var style = new Style(typeof(CircleImage))
                {
                    Setters =
                                {
                                    new Setter {Property = Image.AspectProperty, Value = Aspect.AspectFit},
                                    new Setter {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Start},
                                    new Setter {Property = View.VerticalOptionsProperty, Value = LayoutOptions.Start},
                                    new Setter
                                    {
                                        Property = VisualElement.HeightRequestProperty,
                                        Value =
                                            Device.Idiom == TargetIdiom.Phone
                                                ? Device.OnPlatform(40, 80, 70)
                                                : Device.OnPlatform(100, 110, 70)
                                    },
                                    new Setter
                                    {
                                        Property = VisualElement.WidthRequestProperty,
                                        Value =
                                            Device.Idiom == TargetIdiom.Phone
                                                ? Device.OnPlatform(40, 80, 70)
                                                : Device.OnPlatform(100, 110, 70)
                                    },
                                    new Setter
                                    {
                                        Property = CircleImage.BorderColorProperty,
                                        Value =
                                            Device.OnPlatform(Color.FromHex("646567"), Color.FromHex("646567"),
                                                Color.FromHex("646567"))
                                    },
                                    new Setter
                                    {
                                        Property = CircleImage.BorderThicknessProperty,
                                        Value = Device.OnPlatform(2, 2, 2)
                                    }
                                }
                };

                return style;
            }
        }
        // => End 

        #region Images Icons

        public Style CircleImageCommon
        {
            get
            {
                var style = new Style(typeof (CircleImage))
                            {
                                Setters =
                                {
                                    new Setter {Property = Image.AspectProperty, Value = Aspect.AspectFit},
                                    new Setter {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Start},
                                    new Setter {Property = View.VerticalOptionsProperty, Value = LayoutOptions.Start},
                                    new Setter
                                    {
                                        Property = VisualElement.HeightRequestProperty,
                                        Value =
                                            Device.Idiom == TargetIdiom.Phone
                                                ? Device.OnPlatform(70, 70, 70)
                                                : Device.OnPlatform(100, 100, 100)
                                    },
                                    new Setter
                                    {
                                        Property = VisualElement.WidthRequestProperty,
                                        Value =
                                            Device.Idiom == TargetIdiom.Phone
                                                ? Device.OnPlatform(70, 70, 70)
                                                : Device.OnPlatform(100, 100, 100)
                                    },
                                    new Setter
                                    {
                                        Property = CircleImage.BorderColorProperty,
                                        Value =
                                            Device.OnPlatform(Color.FromHex("646567"), Color.FromHex("646567"),
                                                Color.FromHex("646567"))
                                    },
                                    new Setter
                                    {
                                        Property = CircleImage.BorderThicknessProperty,
                                        Value = Device.OnPlatform(0, 0, 0)
                                    }
                                }
                            };

                return style;
            }
        }

        public Style CircleImageForDetails
        {
            get
            {
                var style = new Style(typeof (CircleImage))
                            {
                                Setters =
                                {
                                    new Setter {Property = Image.AspectProperty, Value = Aspect.AspectFit},
                                    new Setter {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Center},
                                    new Setter {Property = View.VerticalOptionsProperty, Value = LayoutOptions.Center},
                                    new Setter
                                    {
                                        Property = VisualElement.HeightRequestProperty,
                                        Value =
                                            Device.Idiom == TargetIdiom.Phone
                                                ? Device.OnPlatform(125, 125, 125)
                                                : Device.OnPlatform(300, 300, 300)
                                    },
                                    new Setter
                                    {
                                        Property = VisualElement.WidthRequestProperty,
                                        Value =
                                            Device.Idiom == TargetIdiom.Phone
                                                ? Device.OnPlatform(125, 125, 125)
                                                : Device.OnPlatform(300, 300, 300)
                                    },
                                    new Setter
                                    {
                                        Property = CircleImage.BorderColorProperty,
                                        Value =
                                            Device.OnPlatform(Color.FromHex("646567"), Color.FromHex("646567"),
                                                Color.FromHex("646567"))
                                    },
                                    new Setter
                                    {
                                        Property = CircleImage.BorderThicknessProperty,
                                        Value = Device.OnPlatform(1, 1, 1)
                                    }
                                }
                            };

                return style;
            }
        }

        public Style CircleImageUpload
        {
            get
            {
                var style = new Style(typeof (CircleImage))
                            {
                                Setters =
                                {
                                    new Setter {Property = Image.AspectProperty, Value = Aspect.AspectFit},
                                    new Setter {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Center},
                                    new Setter {Property = View.VerticalOptionsProperty, Value = LayoutOptions.Center},
                                    new Setter
                                    {
                                        Property = VisualElement.HeightRequestProperty,
                                        Value =
                                            Device.Idiom == TargetIdiom.Phone
                                                ? Device.OnPlatform(100, 150, 100)
                                                : Device.OnPlatform(150, 200, 150)
                                    },
                                    new Setter
                                    {
                                        Property = VisualElement.WidthRequestProperty,
                                        Value =
                                            Device.Idiom == TargetIdiom.Phone
                                                ? Device.OnPlatform(100, 150, 100)
                                                : Device.OnPlatform(150, 200, 150)
                                    },
                                    new Setter
                                    {
                                        Property = CircleImage.BorderColorProperty,
                                        Value =
                                            Device.OnPlatform(Color.FromHex("646567"), Color.FromHex("646567"),
                                                Color.FromHex("646567"))
                                    },
                                    new Setter
                                    {
                                        Property = CircleImage.BorderThicknessProperty,
                                        Value = Device.OnPlatform(5, 5, 5)
                                    }
                                }
                            };

                return style;
            }
        }

        public Style CircleImageLogo
        {
            get
            {
                var style = new Style(typeof (CircleImage))
                            {
                                Setters =
                                {
                                    new Setter {Property = Image.AspectProperty, Value = Aspect.AspectFit},
                                    new Setter {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Center},
                                    new Setter {Property = View.VerticalOptionsProperty, Value = LayoutOptions.Start},
                                    new Setter
                                    {
                                        Property = VisualElement.HeightRequestProperty,
                                        Value =
                                            Device.Idiom == TargetIdiom.Phone
                                                ? Device.OnPlatform(100, 150, 100)
                                                : Device.OnPlatform(320, 320, 320)
                                    },
                                    new Setter
                                    {
                                        Property = VisualElement.WidthRequestProperty,
                                        Value =
                                            Device.Idiom == TargetIdiom.Phone
                                                ? Device.OnPlatform(100, 150, 100)
                                                : Device.OnPlatform(320, 320, 320)
                                    },
                                    new Setter
                                    {
                                        Property = CircleImage.BorderThicknessProperty,
                                        Value = Device.OnPlatform(0, 0, 0)
                                    }
                                }
                            };

                return style;
            }
        }

        public Style SquareImageStyle
        {
            get
            {
                var style = new Style(typeof (Image))
                            {
                                Setters =
                                {
                                    new Setter {Property = Image.AspectProperty, Value = Aspect.Fill},
                                    new Setter
                                    {
                                        Property = View.HorizontalOptionsProperty,
                                        Value = LayoutOptions.FillAndExpand
                                    },
                                    new Setter
                                    {
                                        Property = View.VerticalOptionsProperty,
                                        Value = LayoutOptions.FillAndExpand
                                    },
                                    new Setter
                                    {
                                        Property = VisualElement.HeightRequestProperty,
                                        Value = Device.Idiom == TargetIdiom.Phone
                                            ? Device.OnPlatform(200, 200, 200)
                                            : Device.OnPlatform(300, 300, 300)
                                    },
                                    new Setter
                                    {
                                        Property = VisualElement.WidthRequestProperty,
                                        Value =
                                            Device.Idiom == TargetIdiom.Phone
                                                ? Device.OnPlatform(500, 500, 500)
                                                : Device.OnPlatform(800, 800, 800)
                                    }
                                }
                            };

                return style;
            }
        }


        public string DefaultProfileMeBannerImage
        {
            get { return "Icon.png"; }
        }

        public string DefaultProfileUserIconName
        {
            get { return "userprofile.png"; }
        }

        public string ProfileMenuItemIcon
        {
            get { return Device.OS == TargetPlatform.iOS ? "Contact.png" : "userprofile.png"; }
        }

        public string KnocksMenuITemIcon
        {
            get { return "padlock.png"; }
        }

        public string MessageIcon
        {
            get { return "mail.png"; }
        }

        public string DefaultGroupIcon
        {
            get { return "igroup.png"; }
        }

        public string DefaultWallIcon
        {
            get { return "ifeeds.png"; }
        }

        public string DefaultEventIcon
        {
            get { return "a_event.png"; }
        }

        public string DefaultNotificationIcon
        {
            get { return "userprofile.png"; }
        }

        public string DefaultContactIcon
        {
            get { return "userprofile.png"; }
        }

        public string DefaultActivityIcon
        {
            get { return "activity.png"; }
        }

        public string DefaultShopIcon
        {
            get { return "shop.png"; }
        }

        public string RatinGoffIcon
        {
            get { return "ratingoff.png"; }
        }


        //icons
        public string FilterIcon
        {
            get { return "search.png"; }
        }

        public string FilterCloseIcon
        {
            get { return "ratingon.png"; }
        }

        public string AboutUsIcon
        {
            get { return "about_us.png"; }
        }

        public string HelpCenterIcon
        {
            get { return "call_center.png"; }
        }

        public string LogoutIcon
        {
            get { return "logout.png"; }
        }

        public string PrivacyIcon
        {
            get { return "privacy.png"; }
        }

        public string TermsAndConditionsIcon
        {
            get { return "terms_conditions.png"; }
        }

        public string OfferServicesIcon
        {
            get { return "services.png"; }
        }

        public string SplashImage
        {
            get { return "splash.PNG"; }
        }

        //Complete icons
        public string CompletePhotoIcon
        {
            get { return "complete_photo.png"; }
        }

        public string CompleteLocationIcon
        {
            get { return "complete_location.png"; }
        }

        public string CompleteJobIcon
        {
            get { return "complete_job.png"; }
        }

        //General Iconsratingon
        public string RankingAddIcon
        {
            get { return "ratingon.png"; }
        }
        public string RankingDefaultIcon
        {
            get { return "ratingoff.png"; }
        }
        public string WriteIcon
        {
            get { return "write.png"; }
        }

        public string SearchIcon
        {
            get { return "search.png"; }
        }

        public string ContactAddIcon
        {
            get { return "contact_add.png"; }
        }

        public string ContactAddedIcon
        {
            get { return "contact_added.png"; }
        }

        public string ContactOfflineIcon
        {
            get { return "offline.png"; }
        }

        public string ContactOnlineIcon
        {
            get { return "online.png"; }
        }

        public string CompleteBlackCircle
        {
            get { return "blackcircle.jpg"; }
        }

        public string CompleteGreenCircle
        {
            get { return "greencircle.jpg"; }
        }

        public string LogoIcon
        {
            get { return "logo.png"; }
        }

        #endregion
    }
}