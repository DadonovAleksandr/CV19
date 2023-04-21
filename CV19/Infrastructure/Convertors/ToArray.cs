using System;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Convertors;

internal class ToArray : MultiConvertor
{
    public override object Convert(object[] values, Type t, object p, CultureInfo c)
    {
        var collection = new CompositeCollection();
        foreach(var item in values) 
        {
            collection.Add(item);
        }
        return collection;
    }

}
