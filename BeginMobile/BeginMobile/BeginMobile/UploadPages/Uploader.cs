using BeginMobile.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using BeginMobile.Accounts;
using Xamarin.Forms;

namespace BeginMobile.UploadPages
{
    public class Uploader:CarouselPage
    {
        private readonly ContentPage _pictureUploader;
        private readonly ContentPage _offerYourServices;
        public Uploader()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            _pictureUploader = new PictureUploader();
            _offerYourServices = new OfferYourServices();

            Children.Add(_pictureUploader);
            Children.Add(_offerYourServices);

            MessagingCenter.Subscribe<ContentPage>(this, "PictureUploader", sender =>
            {
                CurrentPage = _pictureUploader;
            });

            MessagingCenter.Subscribe<ContentPage>(this, "OfferYourServices", sender =>
            {
                CurrentPage = _offerYourServices;
            });
        }
    }
}
