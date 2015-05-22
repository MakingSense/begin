using BeginMobile.Pages.PhotoUpload;
using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace BeginMobile.UploadPages
{
    
    public class PictureUploader: ContentPage
    {
        private const string SelectFromFolder = "Select from Folder";
        private const string AccessYourCamera = "Access your Camera";
        private const string Cancel = "Cancel";
        private Label _labelUploadPicture;
        private Label _labelReplacePicture;
        private Button _buttonNextStep;

        //
        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private CircleImage _imageUploaded;
        private ImageSource _imageSourceAvatar;
        private IMediaPicker _mediaPicker = null;

        private Label _labelUploadYourPicture;
        private Label _labelNicePicture;
        private Label _labelUploadSubtitle;
        private Button _buttonTakePicture;
        private Button _buttonSelectFromFolder;
        private StackLayout _stackLayoutButtons;

        public string Status
        { set; get; }

        //private CircleImage _imageUploaded;
        public PictureUploader()
        {
            Title = "Upload avatar";

            var tapGestureRecognizer = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1,
            };

            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                var action = await DisplayActionSheet(null, Cancel, null, SelectFromFolder, AccessYourCamera);

                switch (action)
                {
                    case SelectFromFolder:
                        var photoPageFolder = new PhotoPage(this, false);
                        await Navigation.PushAsync(photoPageFolder);
                        //Setup();
                        //var options = new CameraMediaStorageOptions();
                        //var test = await _mediaPicker.SelectPhotoAsync(options);

                        //var stream = test.Source;

                        //if (stream != null)
                        //{
                        //    _imageSourceAvatar = StreamImageSource.FromStream(() => stream);
                        //}
                        break;
                    case AccessYourCamera:
                        var photoPageCamera = new PhotoPage(this, true);
                        await Navigation.PushAsync(photoPageCamera);
                        //await TakePicture();
                        var test1 = "";
                        //var tes = new PictureUploader();
                        break;
                    case Cancel:
                        return;
                        break;
                    default:
                        return;
                }
                
            };

            _imageUploaded = new CircleImage
            {
                Source = BeginApplication.Styles.CompletePhotoIcon,
                Style = BeginApplication.Styles.CircleImageUpload,
                HorizontalOptions =  LayoutOptions.CenterAndExpand,
            };

            _imageUploaded.GestureRecognizers.Add(tapGestureRecognizer);

            _labelUploadPicture = new Label
                                {
                                    Text = "Upload Picture",
                                    Style = BeginApplication.Styles.PickerStyle,
                                    XAlign = TextAlignment.Center,
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    IsVisible = true
                                };
            _labelUploadPicture.GestureRecognizers.Add(tapGestureRecognizer);
            

            _buttonNextStep = new Button()
            {
                Text = "Next step!",
                Style = BeginApplication.Styles.UploadLinkLabelButton,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 16,
                IsVisible = false
            };
            _buttonNextStep.Clicked += (e, s) => MessagingCenter.Send<ContentPage>(this, "OfferYourServices");

            var stackLayoutPicture = new StackLayout()
            {
                BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                Spacing = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 20, 0, 0),
                Children =
                {
                    _imageUploaded,
                }
            };

            

            _labelUploadYourPicture = new Label()
            {
                Text = "Upload your Picture",
                Style = BeginApplication.Styles.TitleStyle,
                XAlign = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                IsVisible = true
            };

            _labelUploadSubtitle = new Label()
            {
                Text = "the best shot you have!",
                Style = BeginApplication.Styles.SubtitleStyle,
                XAlign = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                IsVisible = true,
            };

            _labelNicePicture = new Label()
            {
                Text = "Nice Picture!",
                Style = BeginApplication.Styles.TitleStyle,
                XAlign = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                IsVisible = false
            };

            _labelReplacePicture = new Label
                                {
                                    Text = "Replace",
                                    Style = BeginApplication.Styles.SubtitleStyle,
                                    XAlign = TextAlignment.Center,
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    IsVisible =  false
                                };
            _labelReplacePicture.GestureRecognizers.Add(tapGestureRecognizer);


            var stackLayoutTitleSubTitle = new StackLayout()
            {
                Padding = new Thickness(0, 20, 0, 50),
                Children =
                {
                    _labelUploadYourPicture,
                    _labelUploadSubtitle,
                    _labelNicePicture,
                    _labelReplacePicture
                }
            };

            var gridMain = new Grid()
            {
                Padding = new Thickness(0 , 0, 0 , 20),
                BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = 6,
                RowDefinitions =
                {
                    new RowDefinition(){Height = GridLength.Auto},
                    new RowDefinition(){Height = GridLength.Auto}
                }
            };

            _buttonTakePicture = new Button
            {
                Text = "Take a Picture", 
                Style = BeginApplication.Styles.DefaultButton,
            };
            _buttonTakePicture.Clicked += async (s, e) =>
            {
                var photoPageCamera = new PhotoPage(this, true);
                await Navigation.PushAsync(photoPageCamera);
            };

            _buttonSelectFromFolder = new Button
            {
                Text = "Select from Folder",
                Style = BeginApplication.Styles.DefaultButton,
            };
            _buttonSelectFromFolder.Clicked += async (s, e) =>
            {
                var photoPageFolder = new PhotoPage(this);
                await Navigation.PushAsync(photoPageFolder);
            };

            _stackLayoutButtons = new StackLayout()
            {
                Spacing = 10,
                HorizontalOptions =  LayoutOptions.FillAndExpand,
                Children =
                {
                    _buttonTakePicture,
                    _buttonSelectFromFolder
                }
            };


            var stackCircleButtons = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    GreenCircle(),
                    BlackCircle(),
                }
            };

            gridMain.Children.Add(stackLayoutPicture, 0, 1);
            gridMain.Children.Add(stackLayoutTitleSubTitle, 0, 2);
            gridMain.Children.Add(_stackLayoutButtons, 0, 3);
            gridMain.Children.Add(_buttonNextStep, 0, 4);



            Content = new StackLayout()
            {
                BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                Padding = new Thickness(32, Device.OnPlatform(20, 20, 20), 32, 10),
                Children = {gridMain,stackCircleButtons}
            };
        }

        private async Task<MediaFile> TakePicture()
        {
            Setup();

            _imageSourceAvatar = null;

            return await _mediaPicker.TakePhotoAsync(new CameraMediaStorageOptions
            {
                DefaultCamera = CameraDevice.Rear, 
                MaxPixelDimension = 400,
            
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Status = t.Exception.InnerException.ToString();
                }
                else if (t.IsCanceled)
                {
                    Status = "Canceled";
                }
                else
                {
                    var mediaFile = t.Result;

                    _imageSourceAvatar = ImageSource.FromStream(() => mediaFile.Source);

                    return mediaFile;
                }

                return null;
            });
        }

        private void Setup()
        {
            if (_mediaPicker != null)
            {
                return;
            }

            var device = Resolver.Resolve<IDevice>();

            _mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
        }

        public async void UpdatePhoto(ImageSource imageSource)
        {
            this._imageUploaded.Source = imageSource;

            this._stackLayoutButtons.IsVisible = false;
            this._buttonNextStep.IsVisible = true;

            this._labelUploadYourPicture.IsVisible = false;
            this._labelUploadSubtitle.IsVisible = false;
            this._labelNicePicture.IsVisible = true;
            this._labelReplacePicture.IsVisible = true;
            
        }

        private BoxView BoxViewLine()
        {
            return new BoxView
            {
                Color = BeginApplication.Styles.ColorLine, 
                WidthRequest = 100, 
                HeightRequest = 1,
                HorizontalOptions = LayoutOptions. FillAndExpand,
                
            };
        }

        private Image BlackCircle()
        {
            return new Image()
            {
                Source = BeginApplication.Styles.CompleteBlackCircle
            };
        }

        private Image GreenCircle()
        {
            return new Image()
            {
                Source = BeginApplication.Styles.CompleteGreenCircle,
            };
        }
    }
}
