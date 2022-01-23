using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Series;



namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {

        #region SelectedPageIndex
        private int _SelectedPageIndex;
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

        public MainWindowViewModel()
        {
            #region Commands
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);
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
        }

        #region Commands

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        #endregion

        public ICommand ChangeTabIndexCommand { get; }

        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null) return;
            SelectedPageIndex += Convert.ToInt32(p);
        }

        private bool CanChangeTabIndexCommandExecute(object p) => SelectedPageIndex >= 0;




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

    }
}
