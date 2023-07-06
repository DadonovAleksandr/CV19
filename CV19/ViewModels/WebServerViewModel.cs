using CV19.Infrastructure.Commands;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;
using System.Windows.Input;

namespace CV19.ViewModels;
internal class WebServerViewModel : ViewModel
{
    private readonly IWebServerService _server;

    #region Enabled

    public bool Enabled
    {
        get => _server.Enabled;
        set
        {
            _server.Enabled = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region StartCommand
    private ICommand _startCommand;
    public ICommand StartCommand => _startCommand ??= new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);
    private bool CanStartCommandExecute(object arg) => !Enabled;

    private void OnStartCommandExecuted(object obj)
    {
        _server?.Start();
        OnPropertyChanged(nameof(Enabled));
    }
    #endregion

    #region StopCommand
    private ICommand _stopCommand;
    public ICommand StopCommand => _stopCommand ??= new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);
    private bool CanStopCommandExecute(object arg) => Enabled;

    private void OnStopCommandExecuted(object obj)
    {
        _server.Stop();
        OnPropertyChanged(nameof(Enabled));
    }
    #endregion


    public WebServerViewModel(IWebServerService server)
    {
        _server = server;
    }
}

