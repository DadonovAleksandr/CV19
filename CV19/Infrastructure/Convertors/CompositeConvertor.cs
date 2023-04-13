using System;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Convertors;

internal class CompositeConvertor : Convertor
{

    public IValueConverter First { get; set; }
    public IValueConverter Second { get; set; }


    public override object Convert(object value, Type t, object p, CultureInfo c)
    {
        var result1 = First?.Convert(value, t, p, c) ?? value;
        var result2 = Second?.Convert(result1, t, p, c) ?? result1;
        return result2;
    }

    public override object ConvertBack(object value, Type t, object p, CultureInfo c)
    {
        var result2 = Second?.ConvertBack(value, t, p, c) ?? value;
        var result1 = First?.ConvertBack(result2, t, p, c) ?? result2;
        return result1;
    }

}

