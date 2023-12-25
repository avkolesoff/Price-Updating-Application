using App2.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace App2.Services
{
    public class EstimateFileService
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "EstimateDatabase.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<EstimateFile>();
        }

        public static async Task AddEstimate (EstimateFile estimate)
        {
            await Init();
            await db.InsertAsync(estimate);
        }

        public static async Task<EstimateFile> GetEstimate (int id)
        {
            await Init();
            return await db.GetAsync<EstimateFile>(id);
        }

        public static async Task RemoveEstimate (int id)
        {
            await Init();
            await db.DeleteAsync<EstimateFile>(id);
        }

        public static async Task<List<EstimateFile>> GetEstimates ()
        {
            await Init();
            var files = await db.Table<EstimateFile>().ToListAsync();
            return files;
        }
    }
}
