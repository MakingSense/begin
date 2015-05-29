using BeginMobile.UploadPages;
using ImageCircle.Forms.Plugin.Abstractions;
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

        public PhotoPage(PictureUploader uploader, bool isCamera = false)
        {
            Title = isCamera ? "Take photo" : "Select photo";
            
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

            var tapGestureRecognizer = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1,
            };
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                var imageSource = "photo.jpg";
                uploader.UpdatePhoto(imageSource);
                await Navigation.PopAsync();
            };

            var gridOptions = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition{Height = GridLength.Auto}
                }

            };

            var buttonCancelar = new Button
            {
                Text = "Cancelar",
                BackgroundColor = Color.Transparent
            };
            buttonCancelar.Clicked += async (s, e) =>
            {
                await Navigation.PopAsync();
            };

            var labelCarrete = new Label
            {
                Text = "Carrete",
                BackgroundColor = Color.Transparent,
                YAlign = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var buttonCerrar = new Button
            {
                Text = "Cerrar",
                BackgroundColor = Color.Transparent
            };

            gridOptions.Children.Add(buttonCancelar, 0, 0);
            gridOptions.Children.Add(labelCarrete, 1, 0);
            gridOptions.Children.Add(buttonCerrar, 2, 0);


            var gridMain = new Grid()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition(){ Height =  40},
                    new RowDefinition(){ Height =  GridLength.Auto},
                    new RowDefinition(){ Height =  60}
                }
            };


            gridMain.Children.Add(gridOptions, 0, 0);

            if (isCamera == true)
            {
                displayMessageAlert();

                var imageCamera = new Image()
                {
                    Source =  "photo.jpg",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };

                var imageCircleTakephoto = new CircleImage()
                {
                    Source = "takephoto.jpg",
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                imageCircleTakephoto.GestureRecognizers.Add(tapGestureRecognizer);

                gridMain.Children.Add(imageCamera, 0, 1);
                gridMain.Children.Add(imageCircleTakephoto, 0, 2);
            }
            else
            {
                var gridPhotos = new Grid()
                {
                    HorizontalOptions =LayoutOptions.FillAndExpand,
                    RowDefinitions =
                    {
                        new RowDefinition(){Height = GridLength.Auto},
                        new RowDefinition(){Height = GridLength.Auto},
                        new RowDefinition(){Height = GridLength.Auto}
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition(){Width = GridLength.Auto},
                        new ColumnDefinition(){Width = GridLength.Auto},
                        new ColumnDefinition(){Width = GridLength.Auto}
                    }
                };

                var imageCamera = new Image()
                {
                    Source =  "photo.jpg",
                    HeightRequest = 100,
                    WidthRequest = 100
                };
                imageCamera.GestureRecognizers.Add(tapGestureRecognizer);

                gridPhotos.Children.Add(imageCamera, 0, 0);

                gridMain.Children.Add(gridPhotos, 0, 1);
            }
            
            Content = gridMain;
            

            /*this.Content = new StackLayout
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
            };*/

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

        /*private async Task SelectPicture()
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
        }*/

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

        private async void displayMessageAlert()
        {
            var cancel = new Label()
            {
                TextColor = Color.Red,
                Style = BeginApplication.Styles.TitleStyle,
                Text = "Don't Allow",
            };

            var description =
                "\"ForSteps\" need access to your camera \nto take a pticture we must access to your camera";
            var answer = await DisplayAlert(null, description, "OK", cancel.Text);

            if (answer == false)
            {
                await Navigation.PopAsync();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Content = null;
        }
    }

}
