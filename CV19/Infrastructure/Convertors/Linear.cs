using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Infrastructure.Convertors;

/// <summary>
/// Реализация линейного преобразования f(x) = k*x + b
/// </summary>
internal class Linear : Convertor
{
    public double K { get; set; } = 1;
    public double B { get; set; } = 0;


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
