using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CV19
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesighnMode { get; private set; } = true;

        protected override void OnStartup(StartupEventArgs e)
        {
            IsDesighnMode = false;
            base.OnStartup(e);
        }

    }
}
