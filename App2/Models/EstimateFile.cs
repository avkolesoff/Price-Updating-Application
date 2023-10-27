using SQLite;

namespace App2.Models
{
    class EstimateFile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FileName { get; set; }

    }
}
