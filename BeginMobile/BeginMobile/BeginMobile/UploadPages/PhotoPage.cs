using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace BeginMobile.Pages.PhotoUpload
{
    public class PhotoPage: ContentPage
    {
        Image img = new Image();
        IMediaPicker picker = null;
        Task<MediaFile> task = null;

        //
        private ImageSource imageSource;
        private string status;

        public PhotoPage()
        {
            Title = "Camera test";
            
            img.WidthRequest = 60;
            img.HeightRequest = 60;
            img.BackgroundColor = Color.Silver;

            var button = new Button()
            {
                Text = "test",
            };

            button.Clicked += async (s, e) =>
            {
                //SelectPicture();
            };

            this.Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Aqua,
                WidthRequest = 250,
                Padding = 40,
                Spacing = 10,
                Children =
                {
                    img,
                    button
                }
            };

            //var device = Resolver.Resolve<IDevice>();
            //task = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;

            /*this.Appearing += (s, e) =>
            {
                var mediaPicker = DependencyService.Get<IMediaPicker>();
                var options = new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Front, MaxPixelDimension = 400 };
                picker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;

                if (picker.IsCameraAvailable)
                {
                    var test = "";
                }
                task = picker.SelectPhotoAsync(options);
            };

            Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
            {
                if (task != null)
                {
                    if (task.Status == TaskStatus.RanToCompletion)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            img.Source = ImageSource.FromStream(() => task.Result.Source);
                        });
                    }

                    return task.Status != TaskStatus.Canceled
                        && task.Status != TaskStatus.Faulted
                        && task.Status != TaskStatus.RanToCompletion;
                }
                return true;
            }); */
        }

        private async Task SelectPicture()
        {
            var device = Resolver.Resolve<IDevice>();
            var mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;

            imageSource = null;

            try
            {
                var mediaFile = await mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400
                });
                imageSource = ImageSource.FromStream(() => mediaFile.Source);
                img.Source = imageSource;
            }
            catch (System.Exception ex)
            {
                this.status = ex.Message;
            }
        }

        /*private async Task TakePicture()
        {
            //Setup();

            ImageSource = null;

            await this.task.TakePhotoAsync(new CameraMediaStorageOptions { DefaultCamera = CameraDevice.Front, MaxPixelDimension = 400 }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    var s = t.Exception.InnerException.ToString();
                }
                else if (t.IsCanceled)
                {
                    var canceled = true;
                }
                else
                {
                    var mediaFile = t.Result;

                    imageSource = ImageSource.FromStream(() => mediaFile.Source);

                    return mediaFile;
                }

                return null;
            }, _scheduler);
        }*/
    }
}
