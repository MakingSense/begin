using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XLabs.Forms.Controls;
using ExtendedEntryRenderer = BeginMobile.iOS.Renderers.ExtendedEntryRenderer;

[assembly: ExportRenderer(typeof(Entry), typeof(ExtendedEntryRenderer))]
namespace BeginMobile.iOS.Renderers
{
    class ExtendedEntryRenderer: EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
			var view = Element as ExtendedEntry;

            if (view != null)
            {
                //SetBorder(view);
            }
        }

        private void SetBorder(ExtendedEntry view)
        {
            
            Control.Layer.CornerRadius = 15f;
            Control.RightViewMode = UITextFieldViewMode.WhileEditing;
            Control.BorderStyle = view.HasBorder ? UITextBorderStyle.Bezel : UITextBorderStyle.None;
        }
    }
}