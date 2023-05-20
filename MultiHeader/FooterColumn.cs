using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GgasiControls
{
    public class FooterColumn : Border
    {
        public FooterColumn() 
        {
            this.Child = new DataGridCell();
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
                      typeof(FooterColumn),
                       new FrameworkPropertyMetadata(string.Empty, TitleChanged));

        private static void TitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FooterColumn fc = (FooterColumn)d;
            DataGridCell dc = (DataGridCell)fc.Child;
            dc.Content = fc.Title;
        }

        public new Style Style
        {
            get { return (Style)GetValue(StyleProperty); }
            set { SetValue(StyleProperty, value); }
        }

        internal new static readonly DependencyProperty StyleProperty =
              DependencyProperty.Register(
                      "Style",
                      typeof(Style),
                      typeof(FooterColumn),
                       new FrameworkPropertyMetadata(null, StyleChanged));

        private static void StyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Style style = (Style)e.NewValue;
            FooterColumn fc = (FooterColumn)d;
            DataGridCell dc = (DataGridCell)fc.Child;
            dc.Style = style;
        }

        internal new int Column
        {
            get { return (int)GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }

        internal new static readonly DependencyProperty ColumnProperty =
              DependencyProperty.Register(
                      "Column",
                      typeof(int),
                      typeof(FooterColumn),
                       new FrameworkPropertyMetadata(-1, ColumnChanged));

        private static void ColumnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FooterColumn fc = (FooterColumn)d;
            if (fc.Column < fc.Parent.FrozenColumnCount) fc.IsFrozen = true;
            //hc.ColumnSpan = (hc.ColumnSpan == 0 ? 1 : hc.ColumnSpan);

            if (fc.IsFrozen)
            {
                fc.Parent.ctrl_PART_grdFrozenFooter.Children.Add(fc);
                Grid.SetColumn(fc, fc.Column + 1);
            }
            else
            {
                fc.Parent.ctrl_PART_grdScrollFooter.Children.Add(fc);
                Grid.SetColumn(fc, fc.Column - fc.Parent.FrozenColumnCount);
            }
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
                      typeof(FooterColumn),
                       new FrameworkPropertyMetadata(1, ColumnSpanChanged));

        private static void ColumnSpanChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FooterColumn fc = (FooterColumn)d;
            Grid.SetColumnSpan(fc, fc.ColumnSpan);
        }

        public bool IsFrozen
        {
            get { return (bool)GetValue(IsFrozenProperty); }
            set { SetValue(IsFrozenProperty, value); }
        }

        public new static readonly DependencyProperty IsFrozenProperty =
              DependencyProperty.Register(
                      "IsFrozen",
                      typeof(bool),
                      typeof(FooterColumn),
                       new FrameworkPropertyMetadata(false));

        MultiHeader _parent;
        public new MultiHeader Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                DataGridCell dc = (DataGridCell)this.Child;
                if (dc.Style == null) { dc.Style = _parent.FooterStyle; }
            }
        }
    }
}
