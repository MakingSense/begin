using BeginMobile.iOS.Renderers;
using BeginMobile.Utils;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LinkButton), typeof(ExtendedButtonRenderer))]

    namespace BeginMobile.iOS.Renderers
    {
        public class ExtendedButtonRenderer : ButtonRenderer
        {
            public ExtendedButtonRenderer() { }

            public override void Draw(CGRect rect)
            {
                base.Draw(rect);
                var item = Control;
                item.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            }
        }
    }