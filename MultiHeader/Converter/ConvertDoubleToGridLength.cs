using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GgasiControls
{
    public class ConvertDoubleToGridLength : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double db = (double)value;
            //if (double.IsNaN(db)) return new GridLength(0);
            //return new GridLength(db);
            return double.IsNaN(db) ? new GridLength(0) : new GridLength(db);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GridLength gl = (GridLength)value;
            return gl.IsStar ? double.NaN : gl.Value; 
        }
    }
}
