using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GgasiControls
{
    public class HeaderColumnCollection : ObservableCollection<HeaderColumn>
    {
        MultiHeader _parent;
        public HeaderColumnCollection(MultiHeader mh) : base()
        {
            _parent = mh;
        }

        public HeaderColumnCollection()
        {
            
        }

        protected override void InsertItem(int index, HeaderColumn item)
        {
            base.InsertItem(index, item);
            item.Parent =_parent;
        }

        protected override void SetItem(int index, HeaderColumn item)
        {
            base.SetItem(index, item);
            item.Parent = _parent;
        }

        public MultiHeader Parent
        {
            get { return _parent; }
            set { _parent = value; }
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
            } 
        }

        int cols = 0;
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            foreach(HeaderColumn hc in e.NewItems) 
            { 
                hc.Row = _row;
            }
        }
    }
}
