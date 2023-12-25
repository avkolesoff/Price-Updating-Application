using ClosedXML.Excel;
using System.Linq;

using System.Collections.ObjectModel;
using App2.Data;

namespace App2.Services
{
    public class FileManager
    {
        public static void UpdateExcelFile (string filePath)
        {
            var workBook = new XLWorkbook(file: filePath);
            var sheet = workBook.Worksheets.First();

            var rows = sheet.RowsUsed();
            foreach (var row in rows)
            {
                int rowNumber = row.RowNumber();
                string productURL = sheet.Cell($"C{rowNumber}").ToString();

                sheet.Cell($"B{rowNumber}").Value = WebServices.Parse(productURL);
            }
            workBook.SaveAs(filePath);
        }

        public static ObservableCollection<FileDataItem> MakeExelFileIntoMatrix (string filePath)
        {
            var workbook = new XLWorkbook(file: filePath);
            var worksheet = workbook.Worksheets.First();

            int rows = worksheet.RowsUsed().Count();
            var collection = new ObservableCollection<FileDataItem>();

            for (int row = 1; row <= rows; row++)
            {
                collection.Add( new FileDataItem
                {
                    rowNumber = row,
                    Name = worksheet.Cell($"A{row}").Value.ToString(),
                    Price = worksheet.Cell($"B{row}").Value.ToString(),
                    Link = worksheet.Cell($"C{row}").Value.ToString(),
                } );
            }
            return collection;
        }

        public static void UpdateProductLink (string productLink, string filePath, int rowNumber)
        {
            var workbook = new XLWorkbook(file: filePath);
            var worksheet = workbook.Worksheets.First();

            worksheet.Cell($"C{rowNumber}").SetValue(productLink);

            workbook.SaveAs(filePath);
        }

        public static void RemoveProduct(int rowNumber, string filePath)
        {
            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheets.First();

            worksheet.Row(rowNumber).Delete();

            workbook.SaveAs(filePath);
        }

        public static void AddNewProduct(string filePath, FileDataItem product)
        {
            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheets.First();

            int rowNumber = worksheet.RowsUsed().Count() + 1;

            worksheet.Cell($"A{rowNumber}").SetValue(product.Name);
            worksheet.Cell($"B{rowNumber}").SetValue(product.Price);
            worksheet.Cell($"C{rowNumber}").SetValue(product.Link);

            workbook.SaveAs(filePath);
        }
    }
}
