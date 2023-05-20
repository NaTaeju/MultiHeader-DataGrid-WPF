using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace GgasiControls
{
    public class HeaderColumn : DataGridColumnHeader
    {
        public HeaderColumn() 
        {
            //this.IsEnabled= false;
            
        }
        

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
              DependencyProperty.Register(
                      "Title",
                      typeof(string),
                      typeof(HeaderColumn),
                       new FrameworkPropertyMetadata(string.Empty, TitleChanged));

        private static void TitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HeaderColumn hc = (HeaderColumn)d;
            hc.Content = hc.Title;
        }

        public new int Column
        {
            get { return (int)GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }

        internal static readonly DependencyProperty ColumnProperty =
              DependencyProperty.Register(
                      "Column",
                      typeof(int),
                      typeof(HeaderColumn),
                       new FrameworkPropertyMetadata(-1, ColumnChanged));

        private static void ColumnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HeaderColumn hc = (HeaderColumn)d;
            if(hc.Parent.FrozenColumnCount!=0 && hc.Column < hc.Parent.FrozenColumnCount) hc.IsFrozen= true;
            //hc.ColumnSpan = (hc.ColumnSpan == 0 ? 1 : hc.ColumnSpan);

            if(hc.IsFrozen) 
            {
                hc.Parent.ctrl_PART_grdFrozenHeader.Children.Add(hc);
                Grid.SetColumn(hc, hc.Column + 1);
            }
            else
            {
                hc.Parent.ctrl_PART_grdScrollHeader.Children.Add(hc);
                Grid.SetColumn(hc, hc.Column - hc.Parent.FrozenColumnCount);
            }
            
        }

        internal int Row
        {
            get { return (int)GetValue(RowProperty); }
            set { SetValue(RowProperty, value); }
        }

        internal static readonly DependencyProperty RowProperty =
              DependencyProperty.Register(
                      "Row",
                      typeof(int),
                      typeof(HeaderColumn),
                       new FrameworkPropertyMetadata(-1, RowChanged));

        private static void RowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HeaderColumn hc = (HeaderColumn)d;
            Grid.SetRow(hc, hc.Row);
        }

        public int ColumnSpan
        {
            get { return (int)GetValue(ColumnSpanProperty); }
            set { SetValue(ColumnSpanProperty, value); }
        }

        public static readonly DependencyProperty ColumnSpanProperty =
              DependencyProperty.Register(
                      "ColumnSpan",
                      typeof(int),
                      typeof(HeaderColumn),
                       new FrameworkPropertyMetadata(1, ColumnSpanChanged));

        private static void ColumnSpanChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HeaderColumn hc = (HeaderColumn)d;
            Grid.SetColumnSpan(hc, hc.ColumnSpan);
        }

        public new bool IsFrozen
        {
            get { return (bool)GetValue(IsFrozenProperty); }
            set { SetValue(IsFrozenProperty, value); }
        }

        public new static readonly DependencyProperty IsFrozenProperty =
              DependencyProperty.Register(
                      "IsFrozen",
                      typeof(bool),
                      typeof(HeaderColumn),
                       new FrameworkPropertyMetadata(false));

        public int RowSpan
        {
            get { return (int)GetValue(RowSpanProperty); }
            set { SetValue(RowSpanProperty, value); }
        }

        public static readonly DependencyProperty RowSpanProperty =
              DependencyProperty.Register(
                      "RowSpan",
                      typeof(int),
                      typeof(HeaderColumn),
                       new FrameworkPropertyMetadata(1, RowSpanChanged));

        private static void RowSpanChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HeaderColumn hc = (HeaderColumn)d;
            Grid.SetRowSpan(hc, hc.RowSpan);
        }

        MultiHeader _parent;
        public new MultiHeader Parent
        {
            get { return _parent; }
            set 
            { 
                _parent = value;  
                if(this.Style == null) { this.Style = _parent.HeaderStyle; }
                this.IsEnabled = _parent.IsHeaderEnable;
            }
        }
    }
}
