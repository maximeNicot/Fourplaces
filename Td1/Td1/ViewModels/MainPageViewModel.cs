using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using Td1.Views;
using Xamarin.Forms;

namespace Td1.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
        /*
        Dictionary<string, Page> dictionnaire = new Dictionary<string, Page>();
        */
        
        private string _pseudo;
        private string _mdp;
        public Command PasEncoreInscritCommand { get; }


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

        public void LaunchInscriptionView()
        {
            Pseudo = "La Pwetass";
           
            NavigationService.PushAsync<Page1>();
        }
        public MainPageViewModel ()
		{
           /* NavigationService.PushAsync(new Page1());
         */
            PasEncoreInscritCommand = new Command(LaunchInscriptionView);
            Pseudo = "";
            Mdp = "";
        }
	}
}