using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace CV19.Infrastructure.Behaviors;

internal class WindowStateChange : Behavior<Button>
{
    protected override void OnAttached()
    {
        AssociatedObject.Click += OnButtonClick;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.Click -= OnButtonClick;
    }

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        var button = AssociatedObject;

        var window = FindVisualRoot(button) as Window;
        window?.Close();
    }

    private static DependencyObject FindVisualRoot(DependencyObject obj)
    {
        do
        {
            var parent = VisualTreeHelper.GetParent(obj);
            if (parent is null) return obj;
            obj = parent;
        } while (true);
    }



}