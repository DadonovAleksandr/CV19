using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Convertors;

internal abstract class Convertor : MarkupExtension, IValueConverter
{
    public abstract object Convert(object value, Type t, object p, CultureInfo c);


    public virtual object ConvertBack(object value, Type t, object p, CultureInfo c) 
        => throw new NotSupportedException("Обратное преобразование не поддерживается");

    public override object ProvideValue(IServiceProvider serviceProvider) => this;
    
}

