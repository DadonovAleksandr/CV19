using System;
using System.Globalization;
using System.Windows;

namespace CV19.Infrastructure.Convertors;

internal class LocationPointToStr : Convertor
{
    public override object Convert(object v, Type t, object p, CultureInfo c)
    {
        if (!(v is Point point)) return null;

        return $"Lat:{point.X}; Lon:{point.Y}";
    }

    public override object ConvertBack(object v, Type t, object p, CultureInfo c)
    {
        if (!(v is String str)) return null;

        var components = str.Split(";");
        var lat_str = components[0].Split(":")[1];
        var lon_str = components[1].Split(":")[1];

        var lat = double.Parse(lat_str);
        var lon = double.Parse(lon_str);

        return new Point(lat, lon);
    }
}

