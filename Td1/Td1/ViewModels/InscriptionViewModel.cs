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


        public InscriptionViewModel ()
		{
            FirstName = "";
            LastName = "";
            Mdp = "";
            Email = "";

            EffectuerInscriptionCommand = new Command(async () => {
                if (await App.restService.Register(Email, FirstName, LastName, Mdp))
                {
                    Console.WriteLine("L'enregistrement a reussi");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    Console.WriteLine("L'enregistrement a echoué");
                    Email = "Erreur";
                }
                    
            });
        }
	}
}