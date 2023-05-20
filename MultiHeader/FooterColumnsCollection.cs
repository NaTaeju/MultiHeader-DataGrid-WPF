using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace GgasiControls
{
    public class FooterColumnsCollection : ObservableCollection<FooterColumn>
    {
        MultiHeader _parent;
        //HeaderColumn hcRowHeader;
        DataGridRowHeader dgrhRowHeader;
        public FooterColumnsCollection(MultiHeader mh) : base()
        {
            _parent = mh;

            dgrhRowHeader = new DataGridRowHeader();
            dgrhRowHeader.IsEnabled = false;
            Parent.ctrl_PART_grdFrozenFooter.Children.Add(dgrhRowHeader);
            Grid.SetColumn(dgrhRowHeader, 0);
        }
        protected override void InsertItem(int index, FooterColumn item)
        {
            base.InsertItem(index, item);
            item.Parent = _parent;
            item.Height = _parent.FooterHeight;

        }

        protected override void SetItem(int index, FooterColumn item)
        {
            base.SetItem(index, item);
            item.Parent = _parent;
            item.Height = _parent.FooterHeight;
        }

        public MultiHeader Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
        }
    }
}
