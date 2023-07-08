using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
using CV19.Infrastructure.Extensions;

namespace CV19.Infrastructure.Behaviors;

class CloseWindow : Behavior<Button>
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

        var window = button.FindVisualRoot() as Window;
        window?.Close();
    }

    
}