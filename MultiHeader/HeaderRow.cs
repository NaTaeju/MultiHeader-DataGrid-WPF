using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace GgasiControls
{
    [ContentProperty("Cols")]
    public class HeaderRow : FrameworkElement
    {
        private HeaderColumnCollection _cols;

        private MultiHeader _parent;

        public HeaderRow()
        {
            _cols = new HeaderColumnCollection();
            _cols.CollectionChanged += new NotifyCollectionChangedEventHandler(OnColumnsChanged);
        }

        public HeaderRow(MultiHeader mh )
        {
            _parent = mh;
            _cols = new HeaderColumnCollection(mh);
            
        }

        public HeaderColumnCollection Cols { get { return _cols; } }

        public MultiHeader Parent 
        { 
            get 
            { 
                return _parent; 
            } 
            set 
            { 
                _parent = value; 
                _cols.Parent = _parent;
                
            } 
        }

        private int _row = 0;
        public int Row 
        { 
            get 
            { 
                return _row; 
            } 
            set 
            { 
                _row = value;
                Cols.Row = value;
            } 
        }

        private void OnColumnsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }
    }
}
