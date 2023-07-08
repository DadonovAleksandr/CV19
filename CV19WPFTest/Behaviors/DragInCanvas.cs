using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    #region PositionX : double - Горизонтальное смещение

    public static readonly DependencyProperty PositionXProperty =
        DependencyProperty.Register(
            nameof(PositionX),
            typeof(double),
            typeof(DragInCanvas),
            new PropertyMetadata(default(double)));

    [Description("Горизонтальное смещение")]
    public double PositionX
    {
        get => (double)GetValue(PositionXProperty);
        set => SetValue(PositionXProperty, value);
    }

    #endregion

    #region PositionY : double - Вертикальное смещение

    public static readonly DependencyProperty PositionYProperty =
        DependencyProperty.Register(
            nameof(PositionY),
            typeof(double),
            typeof(DragInCanvas),
            new PropertyMetadata(default(double)));

    [Description("Вертикальное смещение")]
    public double PositionY
    {
        get => (double)GetValue(PositionYProperty);
        set => SetValue(PositionYProperty, value);
    }

    #endregion


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

        PositionX = delta.X;
        PositionY = delta.Y;
    }
}
