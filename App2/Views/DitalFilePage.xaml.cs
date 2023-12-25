using Xamarin.Forms;
using App2.ViewModels;
using App2.Models;
using App2.Data;

namespace App2.Views
{
    public partial class DitalFilePage : ContentPage
    {
        public DitalFilePage(EstimateFile estimate, FileDataItem product = null)
        {
            InitializeComponent();

            if (product == null)
                BindingContext = new DitalFileViewModel(estimate, this);
            
            else
                BindingContext = new DitalFileViewModel(estimate, this, product);
        }
    }
}