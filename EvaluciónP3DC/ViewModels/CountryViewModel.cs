using EvaluciónP3DC.Models;
using EvaluciónP3DC.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EvaluciónP3DC.ViewModels
{
    public class CountryViewModel : INotifyPropertyChanged
    {
        private readonly ApiServiceDC _apiService;
        private readonly DatabaseService _databaseService;

        public ObservableCollection<Country> Countries { get; set; }

        public CountryViewModel()
        {
            _apiService = new ApiServiceDC();
            _databaseService = new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "countries.db3"));

            Countries = new ObservableCollection<Country>();
            LoadCountriesCommand = new Command(async () => await LoadCountriesAsync());
            SaveCountryCommand = new Command<Country>(async (country) => await SaveCountryAsync(country));
            UpdateCountryCommand = new Command<Country>(async (country) => await UpdateCountryAsync(country));
            DeleteCountryCommand = new Command<Country>(async (country) => await DeleteCountryAsync(country));
        }

        public Command LoadCountriesCommand { get; }
        public Command SaveCountryCommand { get; }
        public Command UpdateCountryCommand { get; }
        public Command DeleteCountryCommand { get; }

        private async Task LoadCountriesAsync()
        {
            var countries = await _apiService.GetCountriesAsync();
            foreach (var country in countries)
            {
                await _databaseService.SaveCountryAsync(country);
                Countries.Add(country);
            }
        }

        private async Task SaveCountryAsync(Country country)
        {
            await _databaseService.SaveCountryAsync(country);
        }

        private async Task UpdateCountryAsync(Country country)
        {
            await _databaseService.UpdateCountryAsync(country);
        }

        private async Task DeleteCountryAsync(Country country)
        {
            await _databaseService.DeleteCountryAsync(country);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
