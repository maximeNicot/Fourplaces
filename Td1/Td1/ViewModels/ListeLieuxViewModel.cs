using MonkeyCache.SQLite;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Td1.Views;
using Xamarin.Forms;

namespace Td1.ViewModels
{
    public class ListeLieuxViewModel : ViewModelBase
    {
        private string _imageUrl;
        private List<PlaceItemSummary> _listeLieux;
        private PlaceItemSummary _selectedLieu;
        public Command ProfilPageCommand { get; }
        public Command AjouterUnLieuCommand { get; }
        private double _maLatitude;
        private double _maLongitude;



        public double MaLatitude
        {
            get => _maLatitude;
            set => SetProperty(ref _maLatitude, value);
        }
        public double MaLongitude
        {
            get => _maLongitude;
            set => SetProperty(ref _maLongitude, value);
        }
        public PlaceItemSummary SelectedLieu
        {
            get => _selectedLieu;
            set
            {
                if (_selectedLieu != value)
                {
                    _selectedLieu = value;
                    OnCliqueItem( SelectedLieu.Id);
                }
                SetProperty(ref _selectedLieu, value);
            }
        }

        public List<PlaceItemSummary> ListeLieux
        {
            get => _listeLieux;
            set => SetProperty(ref _listeLieux, value);
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }



         public async void OnCliqueItem(int idLieu)
        {
            await App.restService.GetPlaceId(idLieu);
            await Application.Current.MainPage.Navigation.PushAsync(new DetailLieuPage(idLieu));
        }

        public async void GetLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetLastKnownLocationAsync();
            if (position != null)
            {
                MaLatitude = position.Latitude;
                MaLongitude = position.Longitude;
            }
            else
            {
                position = await locator.GetPositionAsync();
                MaLatitude = position.Latitude;
                MaLongitude = position.Longitude;
            }
        }


        public ListeLieuxViewModel()
        {
            GetLocation();
           
            ListeLieux = new List<PlaceItemSummary>{ };
            ListeLieux = Barrel.Current.Get<List<PlaceItemSummary>>("ListeLieux");

            //double distance;
            //Position maPos = new Position(MaLatitude,MaLongitude);

            foreach (PlaceItemSummary placeItemSummary in ListeLieux)
            {

                //distance = maPos.CalculateDistance(new Position(placeItemSummary.Latitude, placeItemSummary.Longitude));


                placeItemSummary.ImageUrl = "https://td-api.julienmialon.com/images/" + placeItemSummary.ImageId;
               
            }

            //ListeLieux.Sort();


            AjouterUnLieuCommand = new Command(async () => {

                await Application.Current.MainPage.Navigation.PushAsync(new AjouterLieuPage());
            });

            ProfilPageCommand = new Command(async () => {
                await App.restService.GetMe();
                await Application.Current.MainPage.Navigation.PushAsync(new ProfilPage());
            });
        }

       
    }


}