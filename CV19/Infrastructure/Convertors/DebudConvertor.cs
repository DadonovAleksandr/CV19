using System;
using System.Diagnostics;
using System.Globalization;

namespace CV19.Infrastructure.Convertors;

class DebudConvertor : Convertor
{
    public override object Convert(object value, Type t, object p, CultureInfo c)
    {
        Debugger.Break();
        return value;
    }

    public override object ConvertBack(object value, Type t, object p, CultureInfo c)
    {
        Debugger.Break();
        return value;
    }
}