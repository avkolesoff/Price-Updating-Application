using SQLite;
using System;

namespace App2.Models
{
    public class EstimateFile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
