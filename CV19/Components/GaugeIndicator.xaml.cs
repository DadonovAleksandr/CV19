using System;
using System.Windows;
using System.Windows.Controls;

namespace CV19.Components;
/// <summary>
/// Логика взаимодействия для GaugeIndicator.xaml
/// </summary>
public partial class GaugeIndicator : UserControl
{
    //#region ValueProperty
    //public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
    //    nameof(Value), 
    //    typeof(double), 
    //    typeof(GaugeIndicator), 
    //    new PropertyMetadata(
    //        default(double),
    //        OnValuePropertyChanged,
    //        OnCorrectValue),
    //    OnValidateValue);

    //private static bool OnValidateValue(object value) => true;

    //private static object OnCorrectValue(DependencyObject d, object baseValue) => 
    //    Math.Max(0, Math.Min(100, (double)baseValue));

    //private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

    //public double Value
    //{
    //    get => (double)GetValue(ValueProperty);
    //    set => SetValue(ValueProperty, value);
    //}
    //#endregion


    #region AngleProperty
    public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
        nameof(Angle),
        typeof(double),
        typeof(GaugeIndicator),
        new PropertyMetadata(
            default(double)));

   
    public double Angle
    {
        get => (double)GetValue(AngleProperty);
        set => SetValue(AngleProperty, value);
    }
    #endregion


    public GaugeIndicator() => InitializeComponent();
    
}
