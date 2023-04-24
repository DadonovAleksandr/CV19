using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Convertors;

/// <summary>
/// Реализация линейного преобразования f(x) = k*x + b
/// </summary>
[ValueConversion(typeof(double), typeof(double))]
[MarkupExtensionReturnType(typeof(Linear))]
internal class Linear : Convertor
{
    [ConstructorArgument("K")]
    public double K { get; set; } = 1;
    [ConstructorArgument("B")]
    public double B { get; set; } = 0;

    public Linear() { }

    public Linear(double k) => K = k;

    public Linear(double k, double b) : this(k) => B = b;
    

    public override object Convert(object value, Type t, object p, CultureInfo c)
    {
        if (value == null) return null;

        var x = System.Convert.ToDouble(value, c);
        return K * x + B;
    }

    public override object ConvertBack(object value, Type t, object p, CultureInfo c)
    {
        if (value == null) return null;

        var y = System.Convert.ToDouble(value, c);

        return (y - B) / K;
    }
}
