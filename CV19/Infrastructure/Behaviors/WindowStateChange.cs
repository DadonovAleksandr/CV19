using Microsoft.Xaml.Behaviors;
using System;
using System.Windows.Controls;
using System.Windows;
using CV19.Infrastructure.Extensions;

namespace CV19.Infrastructure.Behaviors;

internal class WindowStateChange : Behavior<Button>
{
    protected override void OnAttached() => AssociatedObject.Click += OnButtonClick;

    protected override void OnDetaching() => AssociatedObject.Click -= OnButtonClick;

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        var window = AssociatedObject.FindVisualRoot() as Window;
        if (window == null) return;

        window.WindowState = window.WindowState switch
        {
            WindowState.Normal => WindowState.Maximized,
            WindowState.Maximized => WindowState.Normal,
            _ => window.WindowState
        };
    }
}