using System;
using System.Globalization;
using System.Windows.Markup;

namespace CV19.Infrastructure.Convertors;

internal class Ratio : Convertor
{
    [ConstructorArgument("K")]
    public double K { get; set; } = 1;

    public Ratio() { }

    public Ratio(double k) { K = k; }

    public override object Convert(object value, Type t, object p, CultureInfo c)
    {
        if (value is null) return null;
        var x = System.Convert.ToDouble(value, c);

        return x * K;
    }

    public override object ConvertBack(object value, Type t, object p, CultureInfo c)
    {
        if (value is null) return null;
        if (value is string str && String.IsNullOrEmpty(str)) return null;

        var x = System.Convert.ToDouble(value, c);

        return x / K;
    }
}
