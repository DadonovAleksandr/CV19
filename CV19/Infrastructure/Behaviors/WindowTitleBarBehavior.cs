using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace CV19.Infrastructure.Behaviors;

internal class WindowTitleBarBehavior : Behavior<UIElement>
{
    private Window _window;

    protected override void OnAttached()
    {
        _window = AssociatedObject as Window ?? AssociatedObject;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
    }




}
