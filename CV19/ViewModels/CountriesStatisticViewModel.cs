using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CV19.ViewModels;

internal class CountriesStatisticViewModel : ViewModel
{
    public MainWindowViewModel MainVm { get; internal set; }

    private IDataService _dataService;

    List<OxyPlot.DataPoint> dataPoints = new();

    #region Статистика по странам
    private IEnumerable<CountryInfo> _countries ;

    public IEnumerable<CountryInfo> Countries
    {
        get => _countries;
        set => Set(ref _countries, value);
    }
    #endregion

    #region Выбранная страна
    private CountryInfo _selectedCountry;

    public CountryInfo SelectedCountry
    {
        get => _selectedCountry;
        set
        {
            Set(ref _selectedCountry, value);
            PlotLine.Points.Clear();
            PlotLine.Title = _selectedCountry.Name;
            PlotLine.LegendKey = _selectedCountry.Name;
            PlotLine.LineLegendPosition = LineLegendPosition.Start;
            PlotLine.Points.AddRange(_selectedCountry.Counts.Select(p => new OxyPlot.DataPoint(DateTimeAxis.ToDouble(p.Date), p.Count)));
            PlotModel.InvalidatePlot(true);
            OnPropertyChanged(nameof(PlotModel));
        }
    }
    #endregion



        #region Команды

    public ICommand RefreshDataCommand { get; }

    private void OnRefreshDataCommandExecuted(object p)
    {
        Countries = _dataService.GetData();
    }

    private bool CanRefreshDataCommandExecute(object p) => true;

    #endregion


    public CountriesStatisticViewModel(IDataService dataService)
    {
        _dataService = dataService;

        #region Команды
        RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute);
        #endregion
        
        dataPoints = new List<OxyPlot.DataPoint>((int)(360 / 0.1));
        for (var x = 0d; x <= 360; x += 0.1)
        {
            const double to_rad = Math.PI / 180;
            var y = Math.Sin(x * to_rad);
            dataPoints.Add(new OxyPlot.DataPoint(x, y));
        }

        PlotModel = new PlotModel() 
        { 
            Title = "Статистика Ковид",
            
        };
        PlotLine = new OxyPlot.Series.LineSeries()
        {
            Color = OxyPlot.OxyColors.Red,
            StrokeThickness = 1
        };
        var dateAxis = new DateTimeAxis()  
        { 
            MajorGridlineStyle = LineStyle.Solid, 
            MinorGridlineStyle = LineStyle.Dot, 
            IntervalLength = 80,
            StringFormat = "dd.MM.yyyy",
            Title = "Дата" 
        };
        PlotModel.Axes.Add(dateAxis);
        var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Кол-во заболевших" };
        PlotModel.Axes.Add(valueAxis);
        PlotModel.Series.Add(PlotLine);
    }


    #region OxyPlot
    public PlotModel PlotModel { get; set; }

    private LineSeries PlotLine { get; set; }

    #endregion


}
