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

       public async Task LoginAPI()
        {
           bool res = await App.restService.Login(Email, Mdp);
           if(res)
                Console.WriteLine(" Login reussi");
           else
                Console.WriteLine(" Login rate");
        }

        public async Task GetPlacesAPI()
        {
            bool res = await App.restService.GetPlaces();
        }

        public MainPageViewModel ()
		{
           
            Email = "";
            Mdp = "";

            PasEncoreInscritCommand = new Command(async () => {

                await Application.Current.MainPage.Navigation.PushAsync(new InscriptionPage());
            });

            ConnexionCommand = new Command(async () => {

                await LoginAPI();
                await GetPlacesAPI();
                await Application.Current.MainPage.Navigation.PushAsync(new ListeLieuxPage());
                
            });
        }
	}
}