using MonkeyCache.SQLite;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Td1.ViewModels
{
	public class DetailLieuViewModel : ViewModelBase
    {
        private string _nomLieu;
        private string _descriptionLieu;
        private string _imageLieu;

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

        

        public DetailLieuViewModel (int idLieu)
		{
            PlaceItem placeItem = Barrel.Current.Get<PlaceItem>("Lieu" + idLieu);
            NomLieu = placeItem.Title;
            
            DescriptionLieu = placeItem.Description;
            ImageLieu = "https://td-api.julienmialon.com/images/" + placeItem.ImageId;
            
        }
	}
}