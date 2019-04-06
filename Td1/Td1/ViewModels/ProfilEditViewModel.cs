using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Td1.ViewModels
{
	public class ProfilEditViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private int _imageId;
        public Command ValiderModificationCommand { get; }


        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public int ImageId
        {
            get => _imageId;
            set => SetProperty(ref _imageId, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }


        public ProfilEditViewModel ()
		{
            
            FirstName = "";
            LastName = "";
            ImageId = 0;

            ValiderModificationCommand = new Command(async () => {
                if (await App.restService.ModificationUser(FirstName, LastName, ImageId))
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", "", "Ok");
                }
                
            });
        }


	}
}