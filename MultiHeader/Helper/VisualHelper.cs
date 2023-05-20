using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Reflection;

namespace GgasiControls
{
    public class VisualHelper
    {
        #region 컨트롤 찾는 함수
        #region 하위컨트롤 찾는 함수

        public static T? GetVisualChild<T>(DependencyObject? parent) where T : Visual
        {
            T? child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public static T? GetVisualChild<T>(DependencyObject? parent, string name) where T : Visual
        {
            T? child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    if(child is FrameworkElement)
                    {
                        if ((child as FrameworkElement).Name == name) break; 
                    }
                    continue;
                }
            }
            return child;
        }

        public static ScrollBar? GetDataGridInScrollBar(DependencyObject? parent, Orientation org)
        {
            ScrollBar? sb = null;
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                sb = v as ScrollBar;
                if (sb == null)
                {
                    sb = GetVisualChild<ScrollBar>(v);
                }
                if (sb != null && sb.Orientation == org)
                {
                    break;
                }
            }
            return sb;
        }

        #endregion

        #region 부모 컨트롤 찾는 함수
        public static T FindParent<T>(FrameworkElement element) where T : FrameworkElement
        {
            FrameworkElement parent = element.TemplatedParent as FrameworkElement;

            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = parent.TemplatedParent as FrameworkElement;
            }

            return null;
        }

        public static T FindParent<T>(FrameworkElement element, string name) where T : FrameworkElement
        {
            FrameworkElement parent = element.TemplatedParent as FrameworkElement;

            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null && correctlyTyped.Name == name)
                {
                    return correctlyTyped;
                }

                parent = parent.TemplatedParent as FrameworkElement;
            }

            return null;
        }

        #endregion
        #endregion

        public static DataGrid GetDataGridParent(DataGridColumn column)
        {
            PropertyInfo propertyInfo = column.GetType().GetProperty("DataGridOwner", BindingFlags.Instance | BindingFlags.NonPublic);
            return propertyInfo.GetValue(column, null) as DataGrid;
        }
    }
}
