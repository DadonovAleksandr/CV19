using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xaml;

namespace CV19.ViewModels.Base;

internal abstract class ViewModel : MarkupExtension, INotifyPropertyChanged, IDisposable
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        var handlers = PropertyChanged;
        if (handlers is null) return;

        var invokationList = handlers.GetInvocationList();
        var args = new PropertyChangedEventArgs(propertyName);
        foreach(var action in invokationList)
        {
            if(action.Target is DispatcherObject dispatcherObject)
            {
                dispatcherObject.Dispatcher.Invoke(action, this, args);
            }
            else
            {
                action.DynamicInvoke(this, args);
            }
        }
    }

    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if(Equals(field, value)) return false;
            
        field = value;
        OnPropertyChanged(propertyName); 
        return true;
    }

    //~ViewModel()
    //{
    //    Dispose(false);
    //}

    public void Dispose()
    {
        Dispose(true);
    }

    private bool _disposed;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing || _disposed) return;
        _disposed = true;
        // освобождение управляемых ресурсов
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var value_target_service = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
        var root_object_service = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;

        OnInitialized(
            value_target_service?.TargetObject, 
            value_target_service?.TargetProperty, 
            root_object_service?.RootObject);

        return this;
    }

    private WeakReference _TargetRef;
    private WeakReference _RootRef;

    public object TargetObject => _TargetRef?.Target;
    public object RootObject => _RootRef?.Target;


    protected virtual void OnInitialized(object target, object property, object root)
    {
        _TargetRef = new WeakReference(target);
        _RootRef = new WeakReference(root);
    }
    


}

