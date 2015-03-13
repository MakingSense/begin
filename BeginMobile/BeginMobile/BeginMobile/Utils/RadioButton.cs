using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.Utils
{
    public class RadioButton : ContentView
    {
        private const string CheckOff = "\u25CB";
        private const string CheckOn = "\u25C9";
        private readonly Label _checkLabel;

        public RadioButton()
        {
            _checkLabel = new Label
            {
                Text = CheckOff
            };
            var textLabel = new Label();

            _checkLabel.BindingContext = this;
            _checkLabel.SetBinding(Label.FontProperty, "Font");

            textLabel.BindingContext = this;
            textLabel.SetBinding(Label.TextProperty, "Text");
            textLabel.SetBinding(Label.FontProperty, "Font");


            Content = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { _checkLabel, textLabel }
            };

            //Taps
            var recognizer = new TapGestureRecognizer
            {
                Parent = this,
                NumberOfTapsRequired = 1,
                //use command instead of TappedCallback
                TappedCallback = (view, args) => { ((RadioButton)view).IsToggled = true; }
            };
            GestureRecognizers.Add(recognizer);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text",
                                    typeof(string),
                                    typeof(RadioButton), null);

        private static readonly BindableProperty FontProperty =
            BindableProperty.Create<RadioButton, Font>(radio => radio.Font,
                                                       Font.SystemFontOfSize(NamedSize.Large));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Font Font
        {
            set { SetValue(FontProperty, value); }
            get { return (Font)GetValue(FontProperty); }
        }

        public static readonly BindableProperty IsToggledProperty =
            BindableProperty.Create("IsToggled",
                                    typeof(bool),
                                    typeof(RadioButton),
                                    false,
                                    BindingMode.TwoWay,
                                    null,
                                    OnIsToggledPropertyChanged);

        public event EventHandler<ToggledEventArgs> Toggled;

        public bool IsToggled
        {
            get { return (bool)GetValue(IsToggledProperty); }
            set { SetValue(IsToggledProperty, value); }
        }

        private static void OnIsToggledPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            var radioButton = (RadioButton)sender;
            radioButton.OnIsToggledPropertyChanged((bool)oldValue, (bool)newValue);
        }

        private void OnIsToggledPropertyChanged(bool oldValue, bool newValue)
        {
            _checkLabel.Text = newValue ? CheckOn : CheckOff;
            if (Toggled != null)
            {
                Toggled(this, new ToggledEventArgs(IsToggled));
            }
            if (IsToggled)
            {
                var parent = ParentView as Layout<View>;
                if (parent != null)
                {
                    foreach (View view in parent.Children)
                    {
                        if (view is RadioButton && view != this)
                        {
                            ((RadioButton)view).IsToggled = false;
                        }
                    }
                }
            }
        }
    }
}
