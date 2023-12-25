using App2.ViewModels;
using Xamarin.Forms;

namespace App2.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        { 
            InitializeComponent();
            BindingContext = new MainViewModel(this);
        }
    }
}