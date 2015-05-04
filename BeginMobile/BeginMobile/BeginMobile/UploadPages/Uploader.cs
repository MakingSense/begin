using BeginMobile.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BeginMobile.UploadPages
{
    public class Uploader:CarouselPage
    {
        private readonly ContentPage _pictureUploader;
        public Uploader()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            _pictureUploader = new PictureUploader();

            Children.Add(_pictureUploader);

            MessagingCenter.Subscribe<ContentPage>(this, "PictureUploader", sender =>
            {
                CurrentPage = _pictureUploader;
            });
        }
    }
}
