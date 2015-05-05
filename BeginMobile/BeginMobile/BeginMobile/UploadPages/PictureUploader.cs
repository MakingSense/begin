using BeginMobile.Pages.PhotoUpload;
using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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

        private CircleImage _imageUploaded;
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
                        break;
                    case AccessYourCamera:
                        var photoPageCamera = new PhotoPage(this, true);
                        await Navigation.PushAsync(photoPageCamera);
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
                Source = BeginApplication.Styles.DefaultWallIcon,
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

            _labelReplacePicture = new Label
                                {
                                    Text = "Replace",
                                    Style = BeginApplication.Styles.PickerStyle,
                                    XAlign = TextAlignment.Center,
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    IsVisible =  false
                                };
            _labelReplacePicture.GestureRecognizers.Add(tapGestureRecognizer);

            

            _buttonNextStep = new Button()
            {
                Text = "Next step!",
                Style = BeginApplication.Styles.LinkLabelButton,
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
                Children =
                {
                    _imageUploaded,
                    _labelUploadPicture,
                    _labelReplacePicture,
                    BoxViewLine()
                }
            };

            var labelUploadPicture = new Label()
            {
                Text = "Upload your Picture",
                Style = BeginApplication.Styles.TitleStyle,
                XAlign = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var labelGooLooking = new Label()
            {
                Text = "a Good Looking One....",
                Style = BeginApplication.Styles.SubtitleStyle,
                XAlign = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var gridMain = new Grid()
            {
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

            gridMain.Children.Add(labelUploadPicture, 0, 1);
            gridMain.Children.Add(labelGooLooking, 0, 2);
            gridMain.Children.Add(stackLayoutPicture, 0, 3);
            gridMain.Children.Add(_buttonNextStep, 0, 4);

            Content = new StackLayout()
            {
                BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                Padding = new Thickness(10, Device.OnPlatform(20, 20, 20), 10, 10),
                Children = {gridMain}
            };
        }

        public async void UpdatePhoto(ImageSource imageSource)
        {
            this._imageUploaded.Source = imageSource;
            this._buttonNextStep.IsVisible = true;
            this._labelReplacePicture.IsVisible = true;
            this._labelUploadPicture.IsVisible = false;
        }

        private BoxView BoxViewLine()
        {
            return new BoxView
            {
                Color = BeginApplication.Styles.ColorLine, 
                WidthRequest = 100, 
                HeightRequest = 1,
                HorizontalOptions = LayoutOptions. FillAndExpand
            };
        }
    }
}
