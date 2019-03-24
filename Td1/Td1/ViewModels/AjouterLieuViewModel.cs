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
        private string _latitude;
        private string _longitude;
        public Command AjouterLieuCommand { get; }

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

        public async Task NouveauLieuAPI(string title, string description, string idImage, string latitude, string longitude)
        {
            bool res = await App.restService.NouveauLieu(title, description, Int32.Parse(idImage), Double.Parse(latitude), Double.Parse(longitude));
        }

        public AjouterLieuViewModel ()
		{

            AjouterLieuCommand = new Command(async () => {
                await NouveauLieuAPI(Title, Description, IdImage, Latitude, Longitude);


            });
        }
	}
}