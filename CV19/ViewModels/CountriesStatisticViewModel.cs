using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CV19.ViewModels;

internal class CountriesStatisticViewModel : ViewModel
{
    private MainWindowViewModel _mainVm { get; }

    private DataService dataService;

    private IEnumerable<CountryInfo> _countries ;

    public IEnumerable<CountryInfo> Countries
    {
        get => _countries;
        set => Set(ref _countries, value);
    }



    #region Команды

    public ICommand RefreshDataCommand { get; }

    private void OnRefreshDataCommandExecuted(object p)
    {
        Countries = dataService.GetData();
    }

    private bool CanRefreshDataCommandExecute(object p) => true;

    #endregion

    /// <summary>
    /// Отладочный конструктор
    /// </summary>
    public CountriesStatisticViewModel() : this(null)
    {
        if(!App.IsDesignMode)
            throw new InvalidOperationException("Вызов конструктора, непредназначенного для использования в обычном режиме");

        _countries = Enumerable.Range(1, 10).Select(i => new CountryInfo
        {
            Name = $"Country {i}",
            ProvinceCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo
            {
                Name = $"Province {j}",
                Location = new System.Windows.Point(i, j),
                Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
                {
                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
                    Count = k
                }).ToArray()
            }).ToArray()
        }).ToArray();
    }

    public CountriesStatisticViewModel(MainWindowViewModel mainVm)
    {
        _mainVm = mainVm;
        dataService = new DataService();

        #region Команды
        RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute);
        #endregion


    }
}
