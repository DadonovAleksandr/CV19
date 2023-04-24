using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19;

class Program
{
    [STAThread]
    public static void Main()
    {
        var app = new CV19.App();
        app.InitializeComponent();
        app.Run();
    }
}
