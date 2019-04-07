using Plugin.Media;
using Plugin.Media.Abstractions;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Td1.ViewModels
{
	public class AjouterLieuViewModel : ViewModelBase
    {
        private string _title;
        private string _description;
        private string _idImage;
        private string _pathImage;
        private string _latitude;
        private string _longitude;
        public Command AjouterLieuCommand { get; }
        public Command AjouterImageCommand { get; }
        public Command ChoisirImageCommand { get; }


        public string PathImage
        {
            get => _pathImage;
            set => SetProperty(ref _pathImage, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string IdImage
        {
            get => _idImage;
            set => SetProperty(ref _idImage, value);
        }

        public string Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        public string Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        public async Task<MediaFile> PickImageGallery()
        {
            await CrossMedia.Current.Initialize();
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                if(photo != null)
                {
                    PathImage = photo.Path;
                    return photo;
                }
            }
            return null;
        }

        public async Task<MediaFile> PickImageAppareilPhoto()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Receipts",
                    Name = $"{DateTime.UtcNow}.jpg"
                };

                var photo = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                return photo;
            }
            return null;
        }

        public async void ChoisirImage()
        {
            MediaFile file;
            file = await PickImageGallery();
            //file = await PickImageAppareilPhoto();
        }


        public AjouterLieuViewModel ()
		{
            AjouterLieuCommand = new Command(async () => {
                if (await App.restService.NouveauLieu(Title, Description, Int32.Parse(IdImage), Double.Parse(Latitude), Double.Parse(Longitude)))
                {
                    await App.restService.GetPlaces();
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur ", "Veuillez remplir les champs correctement", "Ok");
                }
            });

            AjouterImageCommand = new Command(async () => {
                if (await App.restService.NouvelleImage(PathImage))
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur ", "Veuillez donner un path valide pour l'image", "Ok");
                }
            });

            ChoisirImageCommand = new Command(async () => {
                ChoisirImage(); 
            });
        }
	}
}