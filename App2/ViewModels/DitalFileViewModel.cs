using App2.Data;
using App2.Models;
using App2.Services;
using App2.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input; 
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace App2.ViewModels
{
    public class DitalFileViewModel : BaseViewModel
    {
        public Page Page;
        public ObservableCollection<FileDataItem> FileRows { get; set; }
        public ICommand UpdatePricesCommand { get; set; }
        public ICommand SelectedCommand { get; set; }
        public AsyncCommand<FileDataItem> AddLinkCommand { get; set; }
        public MvvmHelpers.Commands.Command<FileDataItem> RemoveProductCommand { get; set; }
        public AsyncCommand AddNewProductCommand { get; set; }
        public EstimateFile Estimate { get; set; }


        public DitalFileViewModel(EstimateFile estimate, Page page, FileDataItem product = null)
        {
            DefineCommands();
            DefineVariables(estimate, page);

            if (product != null) SaveAddedProduct(product);
            
        }

        private void DefineCommands()
        {
            UpdatePricesCommand = new Command(UpdatePrices);
            SelectedCommand = new MvvmHelpers.Commands.Command<FileDataItem>(Selected);
            RemoveProductCommand = new MvvmHelpers.Commands.Command<FileDataItem>(RemoveProduct);
            AddLinkCommand = new AsyncCommand<FileDataItem>(AddLink);
            AddNewProductCommand = new AsyncCommand(AddNewProduct);
        }

        private void DefineVariables(EstimateFile estimate, Page page)
        {
            Page = page;
            Estimate = estimate;
            Title = Estimate.FileName;
            FileRows = Estimate.GetTableData();
        }

        private void UpdatePrices ()
        {
            FileRows = PricesUpdater.UpdatePricesInFile(Estimate);
            OnPropertyChanged(nameof(FileRows));
        }
    
        private async void Selected (FileDataItem product)
        {
            Page.IsEnabled = false;
            await WebServices.OpenProductOnWebsite(product.Link);
            Page.IsEnabled = true;
        }

        private async Task AddLink (FileDataItem product)
        {
            string link = await Page.DisplayPromptAsync(
                title: "Ссылка на товар",
                message: string.Empty,
                initialValue: product.Link);

            FileRows[FileRows.IndexOf(product)].Link = link;
            OnPropertyChanged(nameof(FileRows));

            FileManager.UpdateProductLink(link, Estimate.FilePath, product.rowNumber);
        }

        private async void RemoveProduct ( FileDataItem product )
        {
            bool answer = await Page.DisplayAlert(string.Empty, $"Удалить товар \n{product.Name} ?", "Удалить", "Отмена");

            if (answer)
            {
                FileManager.RemoveProduct(product.rowNumber, Estimate.FilePath);
                FileRows.Remove(product);
            }
        }

        private async Task AddNewProduct ()
        {
            await Page.Navigation.PushAsync(new ProductAddingPage(Estimate));
            Page.Navigation.RemovePage(Page);
        }

        private void SaveAddedProduct (FileDataItem product)
        {
            FileRows.Add(product);
            FileManager.AddNewProduct(Estimate.FilePath, product);
        }
    }
}
