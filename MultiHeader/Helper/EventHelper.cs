using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GgasiControls.Helper
{
    internal class EventHelper
    {
        internal static void ClearEvent(Control control, string eventName)
        {
            FieldInfo field_info = typeof(Control).GetField(eventName, BindingFlags.NonPublic | BindingFlags.Static);
            PropertyInfo propertyInfo = control.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            object obj = field_info.GetValue(control);
            EventHandlerList eventHandlers = (EventHandlerList)propertyInfo.GetValue(control, null);
            eventHandlers.RemoveHandler(obj, eventHandlers[obj]);
        }
    }
}
