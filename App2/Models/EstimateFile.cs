using SQLite;
using System;
using App2.Services;
using System.Collections.ObjectModel;
using App2.Data;

namespace App2.Models
{
    public class EstimateFile
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }

        public ObservableCollection<FileDataItem> GetTableData()
        {
            return FileManager.MakeExelFileIntoMatrix(filePath: FilePath);
        }
    }
}

    
