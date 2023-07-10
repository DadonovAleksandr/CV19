using CV19.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Input;

namespace CV19.Infrastructure.Behaviors;

internal class WindowTitleBarBehavior : Behavior<UIElement>
{
    private Window _window;

    protected override void OnAttached()
    {
        _window = AssociatedObject as Window ?? AssociatedObject.FindVisualParent<Window>();
        _window.MouseLeftButtonDown += OnMouseDown;
    }
    
    protected override void OnDetaching()
    {
        _window.MouseLeftButtonDown += OnMouseDown;
        _window = null;
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount > 1) return;

        var window = AssociatedObject.FindVisualRoot() as Window;
        if (window == null) return;
        window?.DragMove();
    }




}
