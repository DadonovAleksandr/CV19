using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace CV19.ViewModels;

internal class MainWindowViewModel : ViewModel 
{
    /* ------------------------------------------------------------------------------------------------------------ */

    public CountriesStatisticViewModel CountriesStatistic { get; }

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

    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get; }
    private void OnCloseApplicationCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;
    #endregion

    #region ChangeTabindexCommand
    public ICommand ChangeTabindexCommand { get; }
    private void OnChangeTabindexCommandExecuted(object p)
    {
        if (p is null) return;
        SelectedPageindex += Convert.ToInt32(p);
    }
    private bool CanChangeTabindexCommandExecute(object p) => _selectedPageindex >= 0;
    #endregion

    #region CreateNewGroupCommand
    public ICommand CreateNewGroupCommand { get; }
    private void OnCreateNewGroupCommandExecuted(object p)
    {
        var group_max_index = Groups.Count + 1;
        var newGroup = new Group
        {
            Name = $"Группа {group_max_index}",
            Students = new ObservableCollection<Student>()
        };
        Groups.Add(newGroup);
    }
    private bool CanCreateNewGroupCommandExecute(object p) => true;
    #endregion

    #region DeleteNewGroupCommand
    public ICommand DeleteNewGroupCommand { get; }
    private void OnDeleteNewGroupCommandExecuted(object p)
    {
        if (!(p is Group group)) return;
        var index = Groups.IndexOf(group);
        Groups.Remove(group);
        if(index < Groups.Count)
            SelectedGroup = Groups[index];
    }
    private bool CanDeleteNewGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);
    #endregion

    #endregion

    /* ------------------------------------------------------------------------------------------------------------ */

    public MainWindowViewModel()
    {
        CountriesStatistic = new CountriesStatisticViewModel(this);


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
        CreateNewGroupCommand = new LambdaCommand(OnCreateNewGroupCommandExecuted, CanCreateNewGroupCommandExecute);
        DeleteNewGroupCommand = new LambdaCommand(OnDeleteNewGroupCommandExecuted, CanDeleteNewGroupCommandExecute);
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

        var dataList = new List<object>();

        dataList.Add("Hello, word");
        dataList.Add(42);
        var group = Groups[1];
        dataList.Add(group);
        dataList.Add(group.Students.ToArray()[0]);

        _SelectedGroupStudents.Filter += OnStudentsFiltred;
        //_SelectedGroupStudents.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
        //_SelectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
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
        set
        {
            if (!Set(ref _selectedGroup, value)) return;

            _SelectedGroupStudents.Source = value?.Students;
            OnPropertyChanged(nameof(SelectedGroupStudents));
        }
    }
    #endregion

    private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();
    public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;

    private void OnStudentsFiltred(object sender, FilterEventArgs e)
    {
        if(!(e.Item is Student student))
        {
            e.Accepted = false;
            return;
        }
        var filter_text = _StudentFilterText;
        if(string.IsNullOrWhiteSpace(filter_text)) { return; }

        if(student.Name is null || student.Surname is null || student.Patronymic is null)
        {
            e.Accepted = false;
            return;
        }
        if (student.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
        if (student.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
        if (student.Patronymic.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

        e.Accepted = false;
    }

    #region Текст фильтра студентов
    private string _StudentFilterText;
    public string StudentFilterText
    {
        get => _StudentFilterText;
        set
        {
            if(!Set(ref _StudentFilterText, value)) return;
            _SelectedGroupStudents.View.Refresh();

        }
    }
    #endregion

    public IEnumerable<Student> TestStudents => Enumerable.Range(1, App.IsDesignMode ? 10 : 100000).Select(i => new Student()
    {
        Name = $"Имя {i}",
        Surname = $"Фамилия {i}"
    });



}