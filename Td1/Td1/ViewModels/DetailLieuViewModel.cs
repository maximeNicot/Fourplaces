using MonkeyCache.SQLite;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Td1.ViewModels
{
	public class DetailLieuViewModel : ViewModelBase
    {
        private string _nomLieu;
        private string _descriptionLieu;
        private string _imageLieu;
        private string _nouveauCommentaireAuteur;
        private string _nouveauCommentaireContenu;
        public Command NouveauCommentaireCommand { get; }
        private List<CommentItem> _listeCommentaire;


        public List<CommentItem> ListeCommentaire
        {
            get => _listeCommentaire;
            set => SetProperty(ref _listeCommentaire, value);
        }

        public string NouveauCommentaireAuteur
        {
            get => _nouveauCommentaireAuteur;
            set => SetProperty(ref _nouveauCommentaireAuteur, value);
        }

        public string NouveauCommentaireContenu
        {
            get => _nouveauCommentaireContenu;
            set => SetProperty(ref _nouveauCommentaireContenu, value);
        }

        public string NomLieu
        {
            get => _nomLieu;
            set => SetProperty(ref _nomLieu, value);
        }

        public string DescriptionLieu
        {
            get => _descriptionLieu;
            set => SetProperty(ref _descriptionLieu, value);
        }
        public string ImageLieu
        {
            get => _imageLieu;
            set => SetProperty(ref _imageLieu, value);
        }


        public async Task NouveauCommentaireAPI(string commentaire, int idLieu)
        {
            bool res = await App.restService.NouveauCommentaire(commentaire, idLieu);
        }


        public DetailLieuViewModel (int idLieu)
		{

            PlaceItem placeItem = Barrel.Current.Get<PlaceItem>("Lieu" + idLieu);
            NomLieu = placeItem.Title;
            DescriptionLieu = placeItem.Description;
            ImageLieu = "https://td-api.julienmialon.com/images/" + placeItem.ImageId;
            ListeCommentaire = placeItem.Comments;

            /*foreach (CommentItem commentItem in ListeCommentaire)
            {
              
            }*/

            NouveauCommentaireAuteur = "";
            NouveauCommentaireContenu = "";

            NouveauCommentaireCommand = new Command(async () => {
                await NouveauCommentaireAPI(NouveauCommentaireContenu, idLieu);
            });
        }
	}
}