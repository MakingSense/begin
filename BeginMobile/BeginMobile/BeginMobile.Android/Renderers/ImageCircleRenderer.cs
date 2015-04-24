using System;
using Android.Graphics;
using Android.OS;
using Android.Views;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Logging;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImageCircle.Forms.Plugin.Abstractions.CircleImage), typeof(ImageCircleRenderer))]
namespace BeginMobile.Android.Renderers
{
    public class ImageCircleRenderer: ImageRenderer
    {
        private readonly ILoggingService _log = Logger.Current;

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                //Only enable hardware accelleration on lollipop
                if ((int)Build.VERSION.SdkInt < 21)
                {
                    SetLayerType(LayerType.Software, null);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="child"></param>
        /// <param name="drawingTime"></param>
        /// <returns></returns>
        protected override bool DrawChild(Canvas canvas, global::Android.Views.View child, long drawingTime)
        {
            try
            {
                var radius = Math.Min(Width, Height) / 2;
                const int strokeWidth = 10;
                radius -= strokeWidth / 2;


                var path = new Path();
                path.AddCircle(Width / (float)2.0, Height / (float)2.0, radius, Path.Direction.Ccw);
                canvas.Save();
                canvas.ClipPath(path);

                var result = base.DrawChild(canvas, child, drawingTime);

                canvas.Restore();

                path = new Path();
                path.AddCircle(Width / (float)2.0, Height / (float)2.0, radius, Path.Direction.Ccw);

                var paint = new Paint
                {
                    AntiAlias = true,
                    StrokeWidth = ((ImageCircle.Forms.Plugin.Abstractions.CircleImage)Element).BorderThickness
                };
                paint.SetStyle(Paint.Style.Stroke);
                paint.Color = ((ImageCircle.Forms.Plugin.Abstractions.CircleImage)Element).BorderColor.ToAndroid();

                canvas.DrawPath(path, paint);

                paint.Dispose();
                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                _log.Exception(ex);
                System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}