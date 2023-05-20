using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace GgasiControls
{
    public class BindingHelper
    {
        public static void SetLinkProperty(DependencyObject source, string sourceProperty, DependencyObject target, DependencyProperty targetProperty, BindingMode mode)
        {
            BindingOperations.ClearBinding(target, targetProperty);
            Binding bind = new Binding(sourceProperty) { Source = source, Mode = mode };
            BindingOperations.SetBinding(target, targetProperty, bind);
        }

        public static void SetLinkProperty(DependencyObject source, string sourceProperty, DependencyObject target, DependencyProperty targetProperty, BindingMode mode, IValueConverter converter)
        {
            BindingOperations.ClearBinding(target, targetProperty);
            Binding bind = new Binding(sourceProperty) { Source = source, Mode = mode , Converter = converter};
            BindingOperations.SetBinding(target, targetProperty, bind);
        }
    }
}
