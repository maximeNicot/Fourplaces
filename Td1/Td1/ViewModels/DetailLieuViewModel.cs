using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Td1.ViewModels
{
	public class DetailLieuViewModel : ViewModelBase
    {
        private string _nomLieu;
        private string _detailLieu;
        private string _imageLieu;

        public string NomLieu
        {
            get => _nomLieu;
            set => SetProperty(ref _nomLieu, value);
        }
        public string DetailLieu
        {
            get => _detailLieu;
            set => SetProperty(ref _detailLieu, value);
        }
        public string ImageLieu
        {
            get => _imageLieu;
            set => SetProperty(ref _imageLieu, value);
        }

        public DetailLieuViewModel ()
		{
            NomLieu = "b";
            DetailLieu = "";
            ImageLieu = "";
        }
	}
}