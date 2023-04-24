using System;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Convertors;

internal abstract class MultiConvertor : IMultiValueConverter
{
    public abstract object Convert(object[] values, Type t, object p, CultureInfo c);
        
    public virtual object[] ConvertBack(object value, Type[] t, object p, CultureInfo c) =>
        throw new NotSupportedException("Обратное преобразование не поддерживается");
}
