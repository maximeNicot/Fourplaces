using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storm.Mvvm;
using Xamarin.Forms;

namespace Td1.ViewModels
{
	public class InscriptionViewModel : ViewModelBase
	{
       
        private string _mdp;
        private string _firstName;
        private string _lastName;
        private string _email;
        public Command EffectuerInscriptionCommand { get; }


        public string Mdp
        {
            get => _mdp;
            set => SetProperty(ref _mdp, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public async Task RegisterAPI()
        {

            bool res = await App.restService.Register(Email, FirstName,LastName,Mdp);
            // bool res = await App.restService.Login("maximenicot@outlook.com", "maxime");
            if(res)
                Console.WriteLine(" Enregistrement reussi");

        }


        public InscriptionViewModel ()
		{
            FirstName = "";
            LastName = "";
            Mdp = "";
            Email = "";

            EffectuerInscriptionCommand = new Command(async () => {
                await RegisterAPI();


            });
        }
	}
}