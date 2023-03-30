using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels;

internal class MainWindowViewModel : ViewModel 
{
    /* ------------------------------------------------------------------------------------------------------------ */

    #region Номер выбранной вкладки
    private int _selectedPageindex = 0;

    public int SelectedPageindex
    {
        get => _selectedPageindex;
        set => Set(ref _selectedPageindex, value); 
    }
    #endregion

    #region OxyPlot
    public PlotModel PlotModel { get; set; }

    private IEnumerable<OxyPlot.DataPoint> _testDataPoints;

    public IEnumerable<OxyPlot.DataPoint> TestDataPoints
    {
        get => _testDataPoints;
        set => Set(ref _testDataPoints, value);
    }

    #endregion

    #region Заголовок окна
    private string _title = "Анализ статиcтики CV19";
    /// <summary>
    /// Заголовок окна
    /// </summary>
    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
    }
    #endregion

    #region Статус
    private string _status = "Готов!";
    /// <summary>
    /// Статус программы
    /// </summary>
    public string Status
    {
        get => _status;
        set => Set(ref _status, value);
    }
    #endregion

    /* ------------------------------------------------------------------------------------------------------------ */

    #region Команды

    public ICommand CloseApplicationCommand { get; }
    private void OnCloseApplicationCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;


    public ICommand ChangeTabindexCommand { get; }
    private void OnChangeTabindexCommandExecuted(object p)
    {
        if (p is null) return;
        SelectedPageindex += Convert.ToInt32(p);
    }
    private bool CanChangeTabindexCommandExecute(object p) => _selectedPageindex >= 0;

    #endregion

    /* ------------------------------------------------------------------------------------------------------------ */

    public MainWindowViewModel()
    {
        var data_points = new List<OxyPlot.DataPoint>((int)(360/0.1));    
        for(var x = 0d; x <= 360; x += 0.1)
        {
            const double to_rad = Math.PI / 180;
            var y = Math.Sin(x * to_rad);
            data_points.Add(new OxyPlot.DataPoint(x,y));
        }
        
        TestDataPoints = data_points;

        PlotModel = new PlotModel() { Title = "Пример 1"};
        //PlotModel.Series.Add(new FunctionSeries(Math.Sin, 0, 10, 0.1, "sin(x)"));
        PlotModel.Series.Add(new LineSeries() { ItemsSource = data_points});


        #region Команды
        CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
        ChangeTabindexCommand = new LambdaCommand(OnChangeTabindexCommandExecuted, CanChangeTabindexCommandExecute);
        #endregion
        
        var students = Enumerable.Range(1, 10).Select(i => new Student()
        {
            Name = $"Имя {i}",
            Surname = $"Фамилия {i}",
            Patronymic = $"Отчество {i}",
            Birthday = DateTime.Now,
            Rating = 0
        });
        var groups = Enumerable.Range(1, 20).Select(i => new Group()
        {
            Name = $"Группа {i}",
            Students = new ObservableCollection<Student>(students)
        }); 

        Groups = new ObservableCollection<Group>(groups);
    }

    /* ------------------------------------------------------------------------------------------------------------ */

    public ObservableCollection<Group> Groups { get; set; }


    #region Выбранная группа
    private Group _selectedGroup;
    /// <summary>
    /// Выбранная группа
    /// </summary>
    public Group SelectedGroup
    {
        get => _selectedGroup;
        set => Set(ref _selectedGroup, value);
    }
    #endregion

}