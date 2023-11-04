using Xamarin.Essentials;

using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

using App2.Models;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void AddFileButton_Clicked(object sender, System.EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Выберите файл"
            });

            if (result != null)
            {
                // Сохранение файла в памяти приложения 
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), result.FileName);
                var stream = await result.OpenReadAsync();
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream);
                }

                EstimateFile estimate = new EstimateFile();

                estimate.FileName = result.FileName;
                estimate.FilePath = filePath;
                estimate.CreatedDate = DateTime.Now;

                await App.EstimateFileDB.SavaEstimateFile(estimate);
            }
        }
    }
}