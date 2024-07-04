using EvaluciónP3DC.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluciónP3DC.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Country>().Wait();
        }

        public Task<List<Country>> GetCountriesAsync()
        {
            return _database.Table<Country>().ToListAsync();
        }

        public Task<int> SaveCountryAsync(Country country)
        {
            return _database.InsertAsync(country);
        }

        public Task<int> UpdateCountryAsync(Country country)
        {
            return _database.UpdateAsync(country);
        }

        public Task<int> DeleteCountryAsync(Country country)
        {
            return _database.DeleteAsync(country);
        }
    }

}
