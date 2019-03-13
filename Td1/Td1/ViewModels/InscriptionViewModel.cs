using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Storm.Mvvm;
using Xamarin.Forms;

namespace Td1.ViewModels
{
	public class InscriptionViewModel : ViewModelBase
	{
        private string _pseudo;
        private string _mdp;
        private string _email;

        public string Pseudo
        {
            get => _pseudo;
            set => SetProperty(ref _pseudo, value);
        }

        public string Mdp
        {
            get => _mdp;
            set => SetProperty(ref _mdp, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public InscriptionViewModel ()
		{
            Pseudo = "";
            Mdp = "";
            Email = "";
		}
	}
}