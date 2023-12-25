using App2.Models;
using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using App2.Services;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using App2.Views;
using Xamarin.Forms;
using System.IO;

namespace App2.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Page _page;
        public string AddButtonText { get; set; }
        public ObservableCollection<EstimateFile> Files { get; set; }
        public AsyncCommand AddFileCommand { get; set; }
        public AsyncCommand<EstimateFile> RemoveFileCommand { get; set; }
        public AsyncCommand<EstimateFile> SelectedCommand { get; set; }

        public MainViewModel(Page page)
        {
            Title = "Мои файлы";
            AddButtonText = "Добавить файл";
            _page = page;

            Files = new ObservableCollection<EstimateFile>();
            LoadEstimateFises();

            AddFileCommand = new AsyncCommand(AddFile);
            RemoveFileCommand = new AsyncCommand<EstimateFile>(RemoveFile);
            SelectedCommand = new AsyncCommand<EstimateFile>(Selected);
            _page = page;
        }

        private async void LoadEstimateFises ()
        {
            var files = await EstimateFileService.GetEstimates();

            foreach (var file in files)
            {
                Files.Add(file);
            }
        }

        private async Task AddFile ()
        {
            var result = await FilePicker.PickAsync(new PickOptions { PickerTitle = "Выберите файл" });

            if (result != null)
            {
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), result.FileName); var stream = await result.OpenReadAsync();

                if (!File.Exists(filePath))
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        stream.CopyTo(fileStream);
                    }
                }

                var estimate = new EstimateFile
                {
                    FileName = result.FileName,
                    FilePath = filePath,
                    CreatedDate = DateTime.Now,
                };
                Files.Add(estimate);

                await EstimateFileService.AddEstimate(estimate);
            }
        }

        private async Task RemoveFile (EstimateFile estimate)
        {
            bool answer = await _page.DisplayAlert(string.Empty, $"Удалить файл\n{estimate.FileName} ?", "Удалить", "Отмена");

            if (answer)
            {
                await EstimateFileService.RemoveEstimate(estimate.Id);
                Files.Remove(estimate);
            }
        }

        private async Task Selected(EstimateFile estimate)
        {
            _page.IsEnabled = false;
            await Application.Current.MainPage.Navigation.PushAsync(new DitalFilePage(estimate));
            _page.IsEnabled = true;
        }
    }
}
