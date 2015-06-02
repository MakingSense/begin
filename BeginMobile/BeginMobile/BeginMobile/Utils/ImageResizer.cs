using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

#if __IOS__
using System.Drawing;
using UIKit;
using CoreGraphics;
#endif

#if __ANDROID__
using Android.Graphics;
#endif

namespace BeginMobile.Utils
{
    public class ImageResizer
    {
        #if __IOS__
                public static string ResourcePrefix = "BeginMobile.iOS.Resources.";
        #endif
        #if __ANDROID__
        public static string ResourcePrefix = "BeginMobile.Android.Resources.Drawable.";
        #endif
        static ImageResizer()
        {
        }

        public async static Task<byte[]> ResizeImage(byte[] imageData, float width, float height)
        {
            Task<byte[]> result = null;
#if __IOS__
                result = ResizeImageIOS ( imageData, width, height );
#endif
#if __ANDROID__
            result = ResizeImageAndroid(imageData, width, height);
#endif

            return await result;
        }

#if __IOS__
		public async static Task<byte[]> ResizeImageIOS (byte[] imageData, float width, float height)
		{
			UIImage originalImage = ImageFromByteArray (imageData);

			//create a 24bit RGB image
			using (CGBitmapContext context = new CGBitmapContext (IntPtr.Zero,
				(int)width, (int)height, 8,
				(int)(4 * width), CGColorSpace.CreateDeviceRGB (),
				CGImageAlphaInfo.PremultipliedFirst)) {

				RectangleF imageRect = new RectangleF (0, 0, width, height);

				// draw the image
				context.DrawImage (imageRect, originalImage.CGImage);

				UIKit.UIImage resizedImage = UIKit.UIImage.FromImage (context.ToImage ());

				// save the image as a jpeg
				return resizedImage.AsJPEG().ToArray ();
			}
		}

		public static UIKit.UIImage ImageFromByteArray(byte[] data)
		{
			if (data == null) {
				return null;
			}

			UIKit.UIImage image;
			try {
				image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
			} catch (Exception e) {
				Console.WriteLine ("Image load failed: " + e.Message);
				return null;
			}
			return image;
		}
#endif

#if __ANDROID__

        public async static Task<byte[]> ResizeImageAndroid(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

        public async static Task<byte[]> GetResizeImage(string resource) 
        {
            var assembly = typeof(BeginApplication).GetTypeInfo().Assembly;
            byte[] imageData;

            Stream stream = assembly.GetManifestResourceStream(ResourcePrefix + resource);

            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                imageData = ms.ToArray();
            }

            return await ImageResizer.ResizeImage(imageData, 80, 80);
        }

#endif
    }
}
