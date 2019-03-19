using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Td1.Views;
using Xamarin.Forms;

namespace Td1.ViewModels
{

    public class ProfilViewModel : ViewModelBase
    {
        private string _pseudo;
        private string _mdp;
        private string _resume;
        public Command ChangerMotDePasseCommand { get; }
        public Command EditerProfilCommand { get; }

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

        public string Resume
        {
            get => _resume;
            set => SetProperty(ref _resume, value);
        }

        public ProfilViewModel()
        {
            Pseudo = "";
            Resume = "";
            Mdp = "";

            ChangerMotDePasseCommand = new Command(async () => {

                await Application.Current.MainPage.Navigation.PushAsync(new ChangerMdp());
            });

            EditerProfilCommand = new Command(async () => {

                await Application.Current.MainPage.Navigation.PushAsync(new ProfilEditPage());
            });

        }
    }
}