using MonkeyCache.SQLite;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Td1.Views;
using Xamarin.Forms;

namespace Td1.ViewModels
{

    public class ProfilViewModel : ViewModelBase
    {
        private string _email;
        private string _firstName;
        private string _lastName;
        public Command ChangerMotDePasseCommand { get; }
        public Command EditerProfilCommand { get; }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

   
       

        public ProfilViewModel()
        {
            UserItem me = Barrel.Current.Get<UserItem>("Me");
            Email = me.Email;
            FirstName = me.FirstName;
            LastName = me.LastName;
           


            ChangerMotDePasseCommand = new Command(async () => {

                await Application.Current.MainPage.Navigation.PushAsync(new ChangerMdp());
            });

            EditerProfilCommand = new Command(async () => {

                await Application.Current.MainPage.Navigation.PushAsync(new ProfilEditPage());
            });

        }
    }
}