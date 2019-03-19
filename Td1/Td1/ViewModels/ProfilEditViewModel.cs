using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Td1.ViewModels
{
	public class ProfilEditViewModel : ViewModelBase
    {
        private string _resume;

        public string Resume
        {
            get => _resume;
            set => SetProperty(ref _resume, value);
        }

        public ProfilEditViewModel ()
		{
            Resume = "";
		}
	}
}