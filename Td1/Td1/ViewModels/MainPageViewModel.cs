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

        private string _pseudo;
        private string _mdp;
        public Command PasEncoreInscritCommand { get; }
        public Command ConnexionCommand { get; }
        

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
        }

        public MainPageViewModel ()
		{
            //PasEncoreInscritCommand = new Command(LaunchInscriptionView);
            Pseudo = "";
            Mdp = "";

            PasEncoreInscritCommand = new Command(async () => {

                await Application.Current.MainPage.Navigation.PushAsync(new InscriptionPage());
            });

            ConnexionCommand = new Command(async () => {

                await Application.Current.MainPage.Navigation.PushAsync(new ProfilPage());
            });
        }
	}
}