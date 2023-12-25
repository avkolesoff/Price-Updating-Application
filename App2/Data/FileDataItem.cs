using App2.Models;
using MvvmHelpers.Commands;

namespace App2.Data
{
    public class FileDataItem
    {
        public int rowNumber { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Link { get; set; }
        //public Marketplace Marketplace { get; set; } = SetMarketplace();

        private static Marketplace SetMarketplace()
        {
            string MPname = string.Empty;

            return new Marketplace(MPname);
        }
    }
}
