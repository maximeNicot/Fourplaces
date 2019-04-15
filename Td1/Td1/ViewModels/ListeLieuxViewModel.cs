using MonkeyCache.SQLite;
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


        public ListeLieuxViewModel()
        {
            ListeLieux = new List<PlaceItemSummary>{ };
            ListeLieux = Barrel.Current.Get<List<PlaceItemSummary>>("ListeLieux");

            
             foreach (PlaceItemSummary placeItemSummary in ListeLieux)
             {
                placeItemSummary.ImageUrl = "https://td-api.julienmialon.com/images/" + placeItemSummary.ImageId;
                //ListeLieux.OrderBy<double,PlaceItemSummary.>
             }
           
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