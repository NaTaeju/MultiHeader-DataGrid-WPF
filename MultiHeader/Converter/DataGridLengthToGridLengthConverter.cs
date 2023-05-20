using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GgasiControls.Converter
{
    public class DataGridLengthToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataGridLength dataGridLength = (DataGridLength)value;
            return new GridLength(dataGridLength.DesiredValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GridLength gl = (GridLength)value;
            if(gl.IsStar) return new DataGridLength(1,DataGridLengthUnitType.Star);
            if(gl.IsAuto) return new DataGridLength(1,DataGridLengthUnitType.Auto);
            return new DataGridLength(gl.Value);
        }
    }
}
