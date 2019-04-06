using MonkeyCache.SQLite;
using Plugin.Geolocator;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Td1.ViewModels
{
	public class DetailLieuViewModel : ViewModelBase
    {
       
        private string _nomLieu;
        private string _descriptionLieu;
        private string _imageLieu;
        private string _nouveauCommentaireAuteur;
        private string _nouveauCommentaireContenu;
        private Map _map;
        private double _maLatitude;
        private double _maLongitude;
        private Plugin.Geolocator.Abstractions.Position _maPosition;

        public Command NouveauCommentaireCommand { get; }
        private List<CommentItem> _listeCommentaire;


        public Double MaLatitude
        {
            get => _maLatitude;
            set => SetProperty(ref _maLatitude, value);
        }
        public Double MaLongitude
        {
            get => _maLongitude;
            set => SetProperty(ref _maLongitude, value);
        }
        public Plugin.Geolocator.Abstractions.Position MaPosition
        {
            get => _maPosition;
            set => SetProperty(ref _maPosition, value);
        }

        public Map Map
        {
            get => _map;
            set => SetProperty(ref _map, value);
        }

        public List<CommentItem> ListeCommentaire
        {
            get => _listeCommentaire;
            set => SetProperty(ref _listeCommentaire, value);
        }

        public string NouveauCommentaireAuteur
        {
            get => _nouveauCommentaireAuteur;
            set => SetProperty(ref _nouveauCommentaireAuteur, value);
        }

        public string NouveauCommentaireContenu
        {
            get => _nouveauCommentaireContenu;
            set => SetProperty(ref _nouveauCommentaireContenu, value);
        }

        public string NomLieu
        {
            get => _nomLieu;
            set => SetProperty(ref _nomLieu, value);
        }

        public string DescriptionLieu
        {
            get => _descriptionLieu;
            set => SetProperty(ref _descriptionLieu, value);
        }
        public string ImageLieu
        {
            get => _imageLieu;
            set => SetProperty(ref _imageLieu, value);
        }


        public async void SetLocation()
        {

            //var locator = CrossGeolocator.Current;

            
            if (CrossGeolocator.IsSupported && CrossGeolocator.Current.IsGeolocationEnabled && CrossGeolocator.Current.IsGeolocationAvailable)
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync();
                MaLatitude = position.Latitude;
                MaLongitude = position.Longitude;
                Console.WriteLine("Latitude est " + MaLatitude + " Longitude est " + MaLongitude);
                Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(MaLatitude, MaLongitude), Distance.FromMiles(1)).WithZoom(20));
            }
            else
            {
              
            }
            
        }
       

        public DetailLieuViewModel (int idLieu)
		{

            PlaceItem placeItem = Barrel.Current.Get<PlaceItem>("Lieu" + idLieu);
            NomLieu = placeItem.Title;
            DescriptionLieu = placeItem.Description;
            ImageLieu = "https://td-api.julienmialon.com/images/" + placeItem.ImageId;
            ListeCommentaire = placeItem.Comments;

            
           

            Map = new Map();
            Position positionLieu = new Position(placeItem.Latitude, placeItem.Longitude);
            var pin = new Pin()
            {
                Position = positionLieu,
                Label = placeItem.Title
            };
            Map.Pins.Add(pin);




            SetLocation();
         

            NouveauCommentaireAuteur = "";
            NouveauCommentaireContenu = "";
            NouveauCommentaireCommand = new Command(async () => {
                await App.restService.NouveauCommentaire(NouveauCommentaireContenu, idLieu);
                await App.restService.GetPlaceId(idLieu);
                PlaceItem placeItem2 = Barrel.Current.Get<PlaceItem>("Lieu" + idLieu);
                ListeCommentaire = placeItem2.Comments;
            });

            //var uriNewYork = new Uri(@"bingmaps:?cp=40.726966~-74.006076");

        }
	}
}