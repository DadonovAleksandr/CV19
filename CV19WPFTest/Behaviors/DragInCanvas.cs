using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Xaml.Behaviors;

namespace CV19WPFTest.Behaviors;

public class DragInCanvas : Behavior<UIElement>
{
    private Point _startPoint;
    private Canvas _canvas;

    protected override void OnAttached()
    {
        AssociatedObject.MouseLeftButtonDown += OnLeftButtonDown;
    }
    
    protected override void OnDetaching()
    {
        AssociatedObject.MouseLeftButtonDown -= OnLeftButtonDown;
        AssociatedObject.MouseMove -= OnMouseMove;
        AssociatedObject.MouseLeftButtonUp -= OnLeftButtonUp;
    }

    private void OnLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if((_canvas ??= VisualTreeHelper.GetParent(AssociatedObject) as Canvas) is null) { return; }
        _startPoint = e.GetPosition(AssociatedObject);
        AssociatedObject.CaptureMouse();
        AssociatedObject.MouseMove += OnMouseMove;
        AssociatedObject.MouseLeftButtonUp += OnLeftButtonUp;
    }

    private void OnLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        AssociatedObject.MouseMove -= OnMouseMove;
        AssociatedObject.MouseLeftButtonUp -= OnLeftButtonUp;
        AssociatedObject.ReleaseMouseCapture();
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        
        var obj = AssociatedObject;
        var curPosition = e.GetPosition(_canvas);
        var delta = curPosition - _startPoint;

        obj.SetValue(Canvas.LeftProperty, delta.X);
        obj.SetValue(Canvas.TopProperty, delta.Y);

    }
}
