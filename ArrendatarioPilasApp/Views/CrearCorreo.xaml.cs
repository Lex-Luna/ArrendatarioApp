using ArrendatarioPilasApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ArrendatarioPilasApp.ViewModels;

namespace ArrendatarioPilasApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CrearCorreo : ContentPage
	{
		public CrearCorreo ()
		{
			InitializeComponent ();
            BindingContext = new VMCrearCorreo(Navigation);
        }
	}
}