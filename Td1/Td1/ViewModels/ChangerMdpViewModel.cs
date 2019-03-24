using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Td1.ViewModels
{
	public class ChangerMdpViewModel : ViewModelBase
    {
        private string _ancienMdp;
        private string _nouveauMdp;  
        public Command ConfirmerChangerMdpCommand { get; }

        public string AncienMdp
        {
            get => _ancienMdp;
            set => SetProperty(ref _ancienMdp, value);
        }

        public string NouveauMdp
        {
            get => _nouveauMdp;
            set => SetProperty(ref _nouveauMdp, value);
        }

        public async Task ChangerMdpAPI()
        {
            bool res = await App.restService.ModificationMdp(AncienMdp, NouveauMdp);
        }


        public ChangerMdpViewModel ()
		{
            AncienMdp = "";
            NouveauMdp = "";

            ConfirmerChangerMdpCommand = new Command(async () => {

                await ChangerMdpAPI();
            });

        }
	}
}