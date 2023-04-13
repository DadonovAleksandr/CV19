using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Convertors;

[ValueConversion(typeof(double), typeof(double))]
[MarkupExtensionReturnType(typeof(Add))]
class Add : Convertor
{
    [ConstructorArgument("B")]
    public double B { get; set; } = 0;

    public Add() { }

    public Add(double b) { B = b; }

    public override object Convert(object value, Type t, object p, CultureInfo c)
    {
        if (value is null) return null;
        var x = System.Convert.ToDouble(value, c);

        return x + B;
    }

    public override object ConvertBack(object value, Type t, object p, CultureInfo c)
    {
        if (value is null) return null;
        if (value is string str && String.IsNullOrEmpty(str)) return null;

        var x = System.Convert.ToDouble(value, c);

        return x - B;
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => this;
}
