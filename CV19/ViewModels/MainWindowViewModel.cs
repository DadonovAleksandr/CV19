using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Series;
using System.Collections.ObjectModel;
using CV19.Models.Decanat;
using System.Linq;
using System.Windows.Data;
using System.ComponentModel;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        /*--------------------------------------------------------------------------------*/
        public ObservableCollection<Group> Groups { get; }

        public object[] CompositeCollection { get; }

        #region Выбранный непонятный элемент
        private object _selectedCompositeValue;

        public object SelectedCompositeValue
        {
            get => _selectedCompositeValue;
            set => Set(ref _selectedCompositeValue, value);
        }
        #endregion

        #region Выбранная группа студентов
        private Group _SelectedGroup;

        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set
            {
                if(!Set(ref _SelectedGroup, value)) return;
                _SelectedGroupStudents.Source = value?.Students;
                OnPropertyChanged(nameof(SelectedGroupStudents));
            }
        }
        #endregion


        #region StudentFilterText

        private string _StudentFilterText;

        /// <summary>
        /// Текст фильтра студентов
        /// </summary>
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

        #region SelectedGroupStudents
        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();

        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;

        private void OnStudenFiltred(object sender, FilterEventArgs e)
        {
            if(!(e.Item is Student student))
            {
                e.Accepted = false;
                return;
            }
            var filtredText = _StudentFilterText;
            if(string.IsNullOrWhiteSpace(filtredText)) return;

            if (student.Name is null || student.Suname is null) 
            {
                e.Accepted = false;
                return;
            }

            if (student.Name.Contains(filtredText, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Suname.Contains(filtredText, StringComparison.OrdinalIgnoreCase)) return;
            if (student.Patronymic.Contains(filtredText, StringComparison.OrdinalIgnoreCase)) return;

            e.Accepted = false;

        }
        #endregion

        #region SelectedPageIndex
        private int _SelectedPageIndex = 2;
        /// <summary>
        /// Номер выбранной вкладки
        /// </summary>
        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }

        #endregion

        #region График
        public PlotModel TestOxyModel { get; private set;}

        private IEnumerable<Models.DataPoint> _TestDataPoints;
        /// <summary>
        /// Тестовый набор данных для визуализации грфиков
        /// </summary>
        public IEnumerable<Models.DataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }
        #endregion

        #region Window title
        private string _Title = "Анализ статистики CV19";
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => _Title;
            //set
            //{
            //    //if (Equals(_Title, value)) return;
            //    //_Title = value;
            //    //OnPropertyChanged();

            //    Set(ref _Title, value);
            //}
            set => Set(ref _Title, value);
        }

        #endregion

        #region Status
        private string _Status = @"Готов!";
        /// <summary>
        /// Статус программы
        /// </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }
        #endregion

        public IEnumerable<Student> TestStudents => Enumerable.Range(1, App.IsDesighnMode ? 10 : 100_000).Select(i=> new Student
        {
            Name = $"Имя {i}",
            Suname = $"Фамилия {i}"
        });

        /*--------------------------------------------------------------------------------*/

        #region Commands

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        #endregion

        #region ChangeTabIndexCommand
        public ICommand ChangeTabIndexCommand { get; }

        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            SelectedPageIndex += Convert.ToInt32(p);
        }

        private bool CanChangeTabIndexCommandExecute(object p) => SelectedPageIndex >= 0;
        #endregion

        #region CreateNewGroupCommand
        public ICommand CreateNewGroupCommand { get; }

        private void OnCreateNewGroupCommandExecuted(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        }

        private bool CanCreateNewGroupCommandExecute(object p) => true;
        #endregion

        #region DeleteGroupCommand
        public ICommand DeleteGroupCommand { get; }

        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;
            var group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if(group_index < Groups.Count) SelectedGroup = Groups[group_index];

        }

        private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);
        #endregion


        #endregion

        /*--------------------------------------------------------------------------------*/

        public MainWindowViewModel()
        {
            #region Commands
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
            CreateNewGroupCommand = new LambdaCommand(OnCreateNewGroupCommandExecuted, CanCreateNewGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);
            #endregion

            var data_points = new List<Models.DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x+=0.1)
            {
                const double to_rad = Math.PI / 108;
                var y = Math.Sin(x * to_rad);

                data_points.Add(new Models.DataPoint { XValue = x, YValue = y });

            }

            TestDataPoints = data_points;

            TestOxyModel = new PlotModel { Title = "Example 1" };
            TestOxyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));

            var students = Enumerable.Range(1, 20).Select(i => new Student
            {
                Name = $"Name {i}",
                Suname = $"Suname {i}",
                Patronymic = $"Patronymic {i}",
                Birthday = DateTime.Now,
                Rating = 0
            }) ;
            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);

            var data_list = new List<object>();
            data_list.Add("Hello word");
            data_list.Add(42);
            var group = Groups[1];
            data_list.Add(group);
            data_list.Add(group.Students[0]);

            CompositeCollection = data_list.ToArray();

            _SelectedGroupStudents.Filter += OnStudenFiltred;
            //_SelectedGroupStudents.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
            //_SelectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));

        }

        

        /*--------------------------------------------------------------------------------*/


    }
}
