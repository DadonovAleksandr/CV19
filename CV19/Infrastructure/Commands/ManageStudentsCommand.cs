using CV19.Infrastructure.Commands.Base;
using CV19.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CV19.Infrastructure.Commands
{
    internal class ManageStudentsCommand : Command
    {
        private StudentManagmentWindow _window;

        public override bool CanExecute(object? parameter) => _window == null;
        
        public override void Execute(object? parameter)
        {
            var window = new StudentManagmentWindow
            {
                Owner = App.Current.MainWindow,
            };
            _window = window;
            window.Closed += OnWindowClosed;

            window.ShowDialog();
        }

        private void OnWindowClosed(object? sender, EventArgs e)
        {
            (sender as Window).Closed -= OnWindowClosed;
            _window = null;
        }
    }
}
