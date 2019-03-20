using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Td1.Views;
using Xamarin.Forms;

namespace Td1.ViewModels
{
    public class ListeLieuxViewModel : ViewModelBase
    {
        public Command ProfilPageCommand { get; }
        private ObservableCollection<ItemListe> _listeLieux;
        private ItemListe _selectedLieu;
        public Command AjouterUnLieuCommand { get; }

        public ItemListe SelectedLieu
        {
            get => _selectedLieu;
            set
            {
                if (_selectedLieu != value)
                {
                    _selectedLieu = value;
                    OnCliqueItem();
                }
                SetProperty(ref _selectedLieu, value);
            }
        }
        public ObservableCollection<ItemListe> ListeLieux
        {
            get => _listeLieux;
            set => SetProperty(ref _listeLieux, value);
        }

        public async void OnCliqueItem()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new DetailLieuPage());
        }

        public ListeLieuxViewModel()
        {
            ListeLieux = new ObservableCollection<ItemListe>
            {
                new ItemListe () {NomLieu = "Paris", DetailLieu = "Une belle capital", ImageLieu = ""},
                new ItemListe () {NomLieu = "Orléans", DetailLieu = "Une belle ville", ImageLieu = ""},
            };

            AjouterUnLieuCommand = new Command(async () => {

                await Application.Current.MainPage.Navigation.PushAsync(new AjouterLieuPage());
            });

            ProfilPageCommand = new Command(async () => {
                await Application.Current.MainPage.Navigation.PushAsync(new ProfilPage());
            });
        }
    }

    public class ItemListe : ViewModelBase
    {
        private string _nomLieu;
        private string _detailLieu;
        private string _imageLieu;

        public string NomLieu
        {
            get => _nomLieu;
            set => SetProperty(ref _nomLieu, value);
        }
        public string DetailLieu
        {
            get => _detailLieu;
            set => SetProperty(ref _detailLieu, value);
        }
        public string ImageLieu
        {
            get => _imageLieu;
            set => SetProperty(ref _imageLieu, value);
        }
    }

}