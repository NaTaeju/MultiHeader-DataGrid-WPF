using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace GgasiControls
{
    public class HeaderRowCollection : ObservableCollection<HeaderRow>
    {
        MultiHeader _parent;
        //HeaderColumn hcRowHeader;
        DataGridRowHeader dgrhRowHeader;
        internal HeaderRowCollection(MultiHeader multiHeader)
        {
            _parent = multiHeader;
            dgrhRowHeader = new DataGridRowHeader() { Height = double.NaN };
            dgrhRowHeader.IsEnabled = false;
            Parent.ctrl_PART_grdFrozenHeader.Children.Add(dgrhRowHeader);
        }

        protected override void InsertItem(int index, HeaderRow item)
        {
            base.InsertItem(index, item);
            item.Parent = _parent;
        }

        protected override void SetItem(int index, HeaderRow item)
        {
            base.SetItem(index, item);
            item.Parent = _parent;
            
        }

        public MultiHeader Parent
        {
            get { return _parent; }
        }


        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            if (e.NewItems != null)
            {
                foreach (HeaderRow hr in e.NewItems)
                {
                    hr.Row = Parent.ctrl_PART_grdFrozenHeader.RowDefinitions.Count;
                    GridLength grl = double.IsNaN(hr.Height) ? new GridLength(1,GridUnitType.Star) : new GridLength(hr.Height);
                    //Parent.ctrl_PART_grdFrozenHeader.Background = new SolidColorBrush(Colors.Red);
                    Parent.ctrl_PART_grdFrozenHeader.RowDefinitions.Add(new RowDefinition() { Height = grl });
                    Parent.ctrl_PART_grdScrollHeader.RowDefinitions.Add(new RowDefinition() { Height = grl });

                    //Grid.SetRowSpan(dgrhRowHeader, hr.Row < 1 ? 1 : hr.Row + 1);
                    Grid.SetRowSpan(dgrhRowHeader, 4);
                }
            }
        }
    }
}
