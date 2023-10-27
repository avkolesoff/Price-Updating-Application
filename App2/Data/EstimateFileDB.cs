using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

using App2.Models;

namespace App2.Data
{
    class EstimateFileDB
    {
        readonly SQLiteAsyncConnection _connection;

        public EstimateFileDB(string connectionString)
        {
            _connection = new SQLiteAsyncConnection(connectionString);

            _connection.CreateTableAsync<EstimateFile>().Wait();

        }

        public Task<List<EstimateFile>> GetEstimateFilesList()
        {
            return _connection.Table<EstimateFile>().ToListAsync();
        }

        public Task<EstimateFile> GetEstimateFile(int id)
        {
            return _connection.Table<EstimateFile>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SavaEstimateFile(EstimateFile estimate)
        {
            if (estimate.Id != 0)
            {
                return _connection.UpdateAsync(estimate);
            }
            else
            {
                return _connection.InsertAsync(estimate);
            }
        }

        public Task<int> DeleteEstimateFile(EstimateFile estimate)
        {
            return _connection.DeleteAsync(estimate);
        }
    }
}