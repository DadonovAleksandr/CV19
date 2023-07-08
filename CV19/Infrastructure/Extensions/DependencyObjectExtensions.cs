using System.Windows.Media;
using System.Windows;

namespace CV19.Infrastructure.Extensions;

static class DependencyObjectExtensions
{
    public static DependencyObject FindVisualRoot(this DependencyObject obj)
    {
        do
        {
            var parent = VisualTreeHelper.GetParent(obj);
            if (parent is null) return obj;
            obj = parent;
        } while (true);
    }

    public static DependencyObject FindLogicalRoot(this DependencyObject obj)
    {
        do
        {
            var parent = LogicalTreeHelper.GetParent(obj);
            if (parent is null) return obj;
            obj = parent;
        } while (true);
    }
}
