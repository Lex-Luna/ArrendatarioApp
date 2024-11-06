using ArrendatarioPilasApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ArrendatarioPilasApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}