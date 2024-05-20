using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using Xamarin.Forms;
using App2.Views;
using App2.Models;
using App2.Data;

namespace App2.ViewModels
{
    public class ProductAddingViewModel : BaseViewModel
    {
        private readonly Page _page;
        public EstimateFile Estimate;
        public string Name { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public AsyncCommand SaveCommand { get; set; }

        
        public ProductAddingViewModel(EstimateFile estimate, Page page) 
        {
            _page = page;
            SaveCommand = new AsyncCommand(Save);
            Estimate = estimate;
            Title = $"Новый товар в {Estimate.FileName}";
        }

        private bool ValidateForm(FileDataItem product)
        {
            return (
                product.Name != string.Empty &&
                product.Price != string.Empty &&
                product.Link != string.Empty);
        }

        private async Task Save ()
        {
            var product = new FileDataItem
            {
                Name = Name,
                Price = Price,
                Link = Link,
            };

            if (ValidateForm(product))
            {
                await _page.Navigation.PushAsync(new DitalFilePage(Estimate, product));
                _page.Navigation.RemovePage(_page);
            } 
            else
            {
                await _page.DisplayAlert("Форма заполнена некорректно!", string.Empty, "OK");

                Name = string.Empty;
                Price = string.Empty;
                Link = string.Empty;
            }
        }
    }
}
