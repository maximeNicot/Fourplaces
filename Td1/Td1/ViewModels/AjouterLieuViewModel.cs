using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Td1.ViewModels
{
	public class AjouterLieuViewModel : ViewModelBase
    {
        private string _nomLieu;
        private string _detailLieu;

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
       
        public AjouterLieuViewModel ()
		{
            NomLieu = "a";
            DetailLieu = "";
        }
	}
}