using App2.Data;
using App2.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace App2.Services
{
    public class PricesUpdater
    {
        public async static void UpdatePricesInAllFiles()
        {
            var estimateFiles = await EstimateFileService.GetEstimates();

            for (int i = 0; i < estimateFiles.Count; i++)
            {
                EstimateFile estimateFile = estimateFiles[i];

                if (estimateFile != null)
                {
                    FileManager.UpdateExcelFile(estimateFile.FilePath);
                    estimateFile.CreatedDate = DateTime.Now;
                }
            }
           
        }

        public static ObservableCollection<FileDataItem> UpdatePricesInFile(EstimateFile estimate)
        {
            FileManager.UpdateExcelFile(estimate.FilePath);
            estimate.CreatedDate = DateTime.Now;

            return FileManager.MakeExelFileIntoMatrix(estimate.FilePath);
        }
    }
}
