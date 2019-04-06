using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using Td1.Views;
using Td1.Data;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;

namespace Td1.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{

        private string _email;
        private string _mdp;
        public Command PasEncoreInscritCommand { get; }
        public Command ConnexionCommand { get; }
        

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Mdp
        {
            get => _mdp;
            set => SetProperty(ref _mdp, value);
        }


        public MainPageViewModel ()
		{
           
            Email = "";
            Mdp = "";

            PasEncoreInscritCommand = new Command(async () => {

                await Application.Current.MainPage.Navigation.PushAsync(new InscriptionPage());
            });

            ConnexionCommand = new Command(async () => {


                if (await App.restService.Login(Email, Mdp))
                {
                    await App.restService.GetPlaces();
                    await Application.Current.MainPage.Navigation.PushAsync(new ListeLieuxPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Mauvais identifiant", "", "Ok");
                }
                
            });
        }
	}
}