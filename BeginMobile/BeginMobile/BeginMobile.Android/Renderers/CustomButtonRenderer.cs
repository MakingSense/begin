using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using BeginMobile.Android.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(CustomButtonRenderer))]
namespace BeginMobile.Android.Renderers
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        private int _iterationCount = 1;

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            base.DispatchDraw(canvas);

            if (Control != null)
            {
                if (_iterationCount <= 10)
                {
                    ++_iterationCount;
                    Control.Text = Control.Text;
                }
                else
                {
                    _iterationCount = 1;
                }
                //Control.TextAlignment = global::Android.Views.TextAlignment.Center;
            }
        }
    }
}