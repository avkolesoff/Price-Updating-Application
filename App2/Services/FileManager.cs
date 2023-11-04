using App2.Models;
using System;
using ClosedXML.Excel;
using System.Linq;
using System.Threading.Tasks;

namespace App2.Services
{
    public class FileManager
    {
        private static string UpdateProductPrice(string productURL)
        {
            return "$10000";
        }

        private static void UpdateExcelFiles (string filePath)
        {
            var workBook = new XLWorkbook(file: filePath);
            var sheet = workBook.Worksheets.First();

            var rows = sheet.RowsUsed();
            foreach (var row in rows)
            {
                int rowNumber = row.RowNumber();
                string productURL = sheet.Cell($"C{rowNumber}").ToString();

                sheet.Cell($"B{rowNumber}").Value = UpdateProductPrice(productURL);
            }
            workBook.SaveAs(filePath);
        }


        public async static Task<int> UpdatePricesInAllFiles()
        {
            int res = 0;
            
            var estimateFiles = await App.EstimateFileDB.GetEstimateFilesList();

            for (int i = 0; i < estimateFiles.Count; i++)
            {
                EstimateFile estimateFile = estimateFiles[i];

                if (estimateFile != null)
                {
                    res = 1;

                    UpdateExcelFiles(estimateFile.FilePath);

                    estimateFile.CreatedDate = DateTime.Now;
                    await App.EstimateFileDB.SavaEstimateFile(estimateFile);
                }
            }
            return res;
        }

        public static int UpdatePricesInFile (string filePath)
        {
            try
            {
                UpdateExcelFiles(filePath);
                return 1;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
