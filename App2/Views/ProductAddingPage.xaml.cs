using App2.Models;
using App2.ViewModels;
using Xamarin.Forms;

namespace App2.Views
{
    public partial class ProductAddingPage : ContentPage
    {
        public ProductAddingPage(EstimateFile estimate)
        {
            InitializeComponent();
            BindingContext = new ProductAddingViewModel(estimate, this);
        }
    }
}