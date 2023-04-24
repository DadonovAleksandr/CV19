using MapControl;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Convertors;

[MarkupExtensionReturnType(typeof(PointToMapLocation))]
[ValueConversion(typeof(Point), typeof(Location))]
internal class PointToMapLocation : Convertor
{
    public override object Convert(object value, Type t, object p, CultureInfo c)
    {
        if (!(value is Point point)) return null;
        return new Location(point.X, point.Y);
    }

    public override object ConvertBack(object value, Type t, object p, CultureInfo c)
    {
        if (!(value is Location location)) return null;
        return new Point(location.Latitude, location.Longitude);
    }
}
