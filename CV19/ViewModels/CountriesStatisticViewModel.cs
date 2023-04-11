using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    private ICommand RefreshDataCommand { get; }

    private void OnRefreshDataCommandExecuted(object p)
    {
        Countries = dataService.GetData();
    }

    private bool CanRefreshDataCommandExecute(object p) => true;

    #endregion

    public CountriesStatisticViewModel(MainWindowViewModel mainVm)
    {
        _mainVm = mainVm;
        dataService = new DataService();

        #region Команды
        RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute);
        #endregion


    }
}
