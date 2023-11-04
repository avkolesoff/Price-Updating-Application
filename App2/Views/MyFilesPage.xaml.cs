using App2.Models;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using App2.Services;

namespace App2.Views
{
    public partial class MyFilesPage : ContentPage
    {
        public MyFilesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.EstimateFileDB.GetEstimateFilesList();
            BindingContext = this;
            
            base.OnAppearing();
        }

        private void OnFileSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                EstimateFile estimateFile = (EstimateFile)e.CurrentSelection.First();
            }
        }

        private async void OnDeleteButton_Clicked(object sender, System.EventArgs e)
        {
            var estimateFiles = await App.EstimateFileDB.GetEstimateFilesList();

            for (int i = 0; i < estimateFiles.Count; i++) 
            {
                EstimateFile estimatedFile = (EstimateFile)estimateFiles[i];

                if (estimatedFile != null)
                {
                    if (File.Exists(estimatedFile.FilePath))
                    {
                        File.Delete(estimatedFile.FilePath);
                    }
                    await App.EstimateFileDB.DeleteEstimateFile(estimateFiles[i]);

                }
            }
            OnAppearing();
        }

        private async void PricesUpdatingButton_Clicked(object sender, System.EventArgs e)
        {
            int changesResult = await FileManager.UpdatePricesInAllFiles();
            
            if (changesResult == 1)
            {
                await DisplayAlert("Готово!", "Цены обновлены во всех файлах", "OK");
                OnAppearing();
            }
            else if (changesResult == 0) 
            {
                await DisplayAlert("Упс...", "При обновлении цен протзошда ошибка!", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}