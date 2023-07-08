using CV19.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows;

namespace CV19.Infrastructure.Behaviors;

internal class MinimazeWindow : Behavior<Button>
{
    protected override void OnAttached() => AssociatedObject.Click += OnButtonClick;

    protected override void OnDetaching() => AssociatedObject.Click -= OnButtonClick;

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        var window = AssociatedObject.FindVisualRoot() as Window;
        if (window == null) return;

        window.WindowState = WindowState.Minimized;
    }
}