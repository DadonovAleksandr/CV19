using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using System.Windows.Input;

namespace CV19.ViewModels;
internal class WebServerViewModel : ViewModel
{
    #region Enabled

    private bool _enabled;
    public bool Enabled
    {
        get => _enabled;
        set => Set(ref _enabled, value);
    }

    #endregion

    #region StartCommand
    private ICommand _startCommand;
    public ICommand StartCommand => _startCommand ??= new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);
    private bool CanStartCommandExecute(object arg) => !_enabled;

    private void OnStartCommandExecuted(object obj)
    {
        Enabled = true;
    }
    #endregion

    #region StopCommand
    private ICommand _stopCommand;
    public ICommand StopCommand => _stopCommand ??= new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);
    private bool CanStopCommandExecute(object arg) => _enabled;

    private void OnStopCommandExecuted(object obj)
    {
        Enabled = false;
    }
    #endregion

}

