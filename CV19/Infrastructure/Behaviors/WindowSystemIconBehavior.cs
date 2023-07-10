using CV19.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CV19.Infrastructure.Behaviors;

internal class WindowSystemIconBehavior : Behavior<Image>
{
    protected override void OnAttached()
    {
        AssociatedObject.MouseLeftButtonDown += OnMouseDown;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseLeftButtonDown -= OnMouseDown;
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var window = AssociatedObject.FindVisualRoot() as Window;
        if (window == null) return;

        e.Handled = true;

        if(e.ClickCount > 1 ) 
        { 
            window.Close(); 
        }
        else
        {
            window.SendMessage(WM.SYSCOMMAND, SC.KEYMENU);
        }

    }

}
