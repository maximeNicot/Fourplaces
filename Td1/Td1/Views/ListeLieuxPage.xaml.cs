using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Td1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Td1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListeLieuxPage : ContentPage
	{
		public ListeLieuxPage ()
		{
			InitializeComponent ();
            BindingContext = new ListeLieuxViewModel();

             
		}
       
    }
}