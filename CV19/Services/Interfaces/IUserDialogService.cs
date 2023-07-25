using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Services.Interfaces
{
    internal interface IUserDialogService
    {
        bool Edit(object item);

        void ShowInformation(string information, string caption);
        void ShowWarning(string message, string caption);
        void ShowError(string message, string caption);
        bool Confirm(string message, string  caption, bool exclamation = false);

        string GetStringValue(string message, string caption, string defaultValue = null);
    }
}
