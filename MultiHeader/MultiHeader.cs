using GgasiControls.Converter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GgasiControls
{
    [ContentProperty("Content")]
    public class MultiHeader : Control
    {
        #region 선언
        internal string PART_grdHeader = "PART_grdHeader";
        internal string PART_grdFooter = "PART_grdFooter";
        internal string PART_grdMain = "PART_grdMain";
        internal string PART_svScrollHeader = "PART_svScrollHeader";
        internal string PART_borHeaderOutLine = "PART_borHeaderOutLine";
        internal string PART_borFooterOutLine = "PART_borfooterOutLine";
        internal string PART_svScrollFooter = "PART_svScrollFooter";
        internal string PART_rdDataGrid = "PART_rdDataGrid";

        internal Grid? ctrl_PART_grdMain;
        internal Grid? ctrl_PART_grdHeader;
        internal Grid? ctrl_PART_grdFooter;
        internal Grid ctrl_PART_grdFrozenHeader = new Grid();
        internal Grid ctrl_PART_grdScrollHeader = new Grid();
        internal Grid ctrl_PART_grdFrozenFooter = new Grid();
        internal Grid ctrl_PART_grdScrollFooter = new Grid();
        internal ScrollViewer? ctrl_PART_svScrollHeader;
        internal ScrollViewer? ctrl_PART_svScrollFooter;
        internal Border? ctrl_PART_borHeaderOutLine;
        internal Border? ctrl_PART_borFooterOutLine;
        internal ColumnDefinition? cdRowHedaer;
        internal ColumnDefinition? cdFooterRowHeader;
        internal RowDefinition? ctrl_PART_rdDataGrid;

        private HeaderRowCollection _rows;
        public HeaderRowCollection Rows { get { return _rows; } }

        private FooterColumnsCollection _footerColumns;

        public FooterColumnsCollection FooterCols { get { return _footerColumns; } }
        #endregion

        #region 생성자
        static MultiHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiHeader), new FrameworkPropertyMetadata(typeof(MultiHeader)));
        }

        public MultiHeader()
        {
            _rows = new HeaderRowCollection(this);
            _footerColumns= new FooterColumnsCollection(this);
        }
        #endregion

        #region 속성
        /// <summary>
        /// 확장 하고자 하는 데이터 그리드가 들어감(ContentProperty로 지정 되어 있음)
        /// </summary>
        public DataGrid Content
        {
            get { return (DataGrid)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(DataGrid), typeof(MultiHeader), new FrameworkPropertyMetadata(null, ContentChanged));

        public Style HeaderStyle
        {
            get { return (Style)GetValue(HeaderStyleProperty); }
            set { SetValue(HeaderStyleProperty, value); }
        }

        public static readonly DependencyProperty HeaderStyleProperty =
            DependencyProperty.Register("HeaderStyle", typeof(Style), typeof(MultiHeader), new FrameworkPropertyMetadata(null));

        public Style FooterStyle
        {
            get { return (Style)GetValue(FooterStyleProperty); }
            set { SetValue(FooterStyleProperty, value); }
        }

        public static readonly DependencyProperty FooterStyleProperty =
            DependencyProperty.Register("FooterStyle", typeof(Style), typeof(MultiHeader), new FrameworkPropertyMetadata(null));

        public double FooterHeight
        {
            get { return (double)GetValue(FooterHeightProperty); }
            set { SetValue(FooterHeightProperty, value); }
        }

        public static readonly DependencyProperty FooterHeightProperty =
            DependencyProperty.Register("FooterHeight", typeof(double), typeof(MultiHeader), new FrameworkPropertyMetadata(double.NaN));

        public int FrozenColumnCount
        {
            get { return (int)GetValue(FrozenColumnCountProperty); }
            set { SetValue(FrozenColumnCountProperty, value); }
        }

        public static readonly DependencyProperty FrozenColumnCountProperty =
            DependencyProperty.Register("FrozenColumnCount", typeof(int), typeof(MultiHeader), new FrameworkPropertyMetadata(0));

        public bool IsHeaderEnable
        {
            get { return (bool)GetValue(IsHeaderEnableProperty); }
            set { SetValue(IsHeaderEnableProperty, value); }
        }


        public static readonly DependencyProperty IsHeaderEnableProperty =
            DependencyProperty.Register("IsHeaderEnable", typeof(bool), typeof(MultiHeader), new FrameworkPropertyMetadata(false));

        public bool IsFooterEnable
        {
            get { return (bool)GetValue(IsFooterEnableProperty); }
            set { SetValue(IsFooterEnableProperty, value); }
        }


        public static readonly DependencyProperty IsFooterEnableProperty =
            DependencyProperty.Register("IsFooterEnable", typeof(bool), typeof(MultiHeader), new FrameworkPropertyMetadata(false));

        private static void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiHeader source = (MultiHeader)d;
            DataGrid innerGrid = (DataGrid)e.NewValue;
            source.eventAdd = false;
        }

        internal DataGridHeadersVisibility HeadersVisibility
        {
            get { return (DataGridHeadersVisibility)GetValue(HeadersVisibilityProperty); }
            set { SetValue(HeadersVisibilityProperty, value); }
        }

        public static readonly DependencyProperty HeadersVisibilityProperty =
            DependencyProperty.Register("HeadersVisibility", typeof(DataGridHeadersVisibility), typeof(MultiHeader), new FrameworkPropertyMetadata(DataGridHeadersVisibility.All, DataGridHeadersVisibilityChanged));

        private static void DataGridHeadersVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiHeader source = (MultiHeader)d;
            if (source.HeadersVisibility == DataGridHeadersVisibility.Column || source.HeadersVisibility == DataGridHeadersVisibility.None)
            {
                if(source.cdRowHedaer!=null)
                    source.cdRowHedaer.Width = new GridLength(0);
            }
        }

        #endregion

        #region 헤더 만들기
        private void HeaderGridInit()
        {
            //Border 조정
            BindingHelper.SetLinkProperty(Content, "BorderThickness", ctrl_PART_borHeaderOutLine, Border.BorderThicknessProperty, BindingMode.OneTime, new HeaderBorderConverter());
            BindingHelper.SetLinkProperty(Content, "BorderBrush", ctrl_PART_borHeaderOutLine, Border.BorderBrushProperty, BindingMode.OneWay);

            //헤더에 그리드 넣고 설정
            ctrl_PART_grdHeader.Children.Add(ctrl_PART_grdFrozenHeader); 
            //ctrl_PART_grdFrozenHeader.ShowGridLines = true;
            //ctrl_PART_grdFrozenHeader.Height = double.NaN;
            ctrl_PART_svScrollHeader.Content = ctrl_PART_grdScrollHeader;
            Grid.SetColumn(ctrl_PART_grdFrozenHeader, 0);

            //FrozenColumnCount를 데이터 그리드와 바인딩
            BindingHelper.SetLinkProperty(this, "FrozenColumnCount", Content, DataGrid.FrozenColumnCountProperty, BindingMode.OneWay);

            //MultiHeader에 들어갈 RowHeader를 삽입
            cdRowHedaer = new ColumnDefinition();
            ctrl_PART_grdFrozenHeader.ColumnDefinitions.Add(cdRowHedaer);
            BindingHelper.SetLinkProperty(Content, "RowHeaderActualWidth", cdRowHedaer, ColumnDefinition.WidthProperty, BindingMode.OneWay, new ConvertDoubleToGridLength());

            BindingHelper.SetLinkProperty(Content, "HeadersVisibility", this, MultiHeader.HeadersVisibilityProperty, BindingMode.OneWay);

            //데이터 그리드 컬럼 갯수에 맞춰 MultiHeader에 ColumnDefinition을 추가 하고 Width를 바인딩 하여 데이터 그리드 헤더 사이즈 변경하면 MultiHeader의 컬럼도 사이즈 변경
            foreach (DataGridColumn dc in Content.Columns)
            {
                ColumnDefinition cd = new ColumnDefinition();
                if (Content.FrozenColumnCount != 0 && dc.IsFrozen)
                {
                    ctrl_PART_grdFrozenHeader.ColumnDefinitions.Add(cd);
                }
                else
                {
                    ctrl_PART_grdScrollHeader.ColumnDefinitions.Add(cd);
                }
                BindingHelper.SetLinkProperty(dc, "Width", cd, ColumnDefinition.WidthProperty, BindingMode.TwoWay, new DataGridLengthToGridLengthConverter());
            }

            //마지막 컬럼 우측에 ColumnDefinition을 추가 하여 사이즈 조절
            if (Rows != null && Rows.Count > 0)
            {
                ColumnDefinition cdLast = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
                HeaderColumn hdLast = new HeaderColumn() { IsEnabled = false , Style=this.HeaderStyle};
                ctrl_PART_grdScrollHeader.ColumnDefinitions.Add(cdLast); //컬럼 뒷편
                ctrl_PART_grdScrollHeader.Children.Add(hdLast);
                Grid.SetColumn(hdLast, Content.Columns.Count - Content.FrozenColumnCount);
                Grid.SetRow(hdLast, 0);

                Grid.SetRowSpan(hdLast, Rows.Count);
                //Grid.SetRowSpan(ctrl_PART_grdFrozenHeader.Children[0] ,Rows.Count);
                
            }
            else
            {
                //Rows가 없으면 MultiHeader를 안보이게 함
                ctrl_PART_grdHeader.Height = 0;
            }
            //Grid.SetRowSpan(dgrhRowHeader, hr.Row < 1 ? 1 : hr.Row + 1);


        }
        #endregion

        #region 푸터 만들기
        private void FooterGridInit()
        {
            //Border 조정
            BindingHelper.SetLinkProperty(Content, "BorderThickness", ctrl_PART_borFooterOutLine, Border.BorderThicknessProperty, BindingMode.OneTime);
            BindingHelper.SetLinkProperty(Content, "BorderBrush", ctrl_PART_borFooterOutLine, Border.BorderBrushProperty, BindingMode.OneWay);

            //푸터에 그리드 넣고 설정
            ctrl_PART_grdFooter.Children.Add(ctrl_PART_grdFrozenFooter);
            ctrl_PART_svScrollFooter.Content = ctrl_PART_grdScrollFooter;
            Grid.SetColumn(ctrl_PART_grdFrozenFooter, 0);

            //Footer에 들어갈 RowHeader를 삽입
            cdFooterRowHeader = new ColumnDefinition();
            ctrl_PART_grdFrozenFooter.ColumnDefinitions.Add(cdFooterRowHeader);
            BindingHelper.SetLinkProperty(Content, "RowHeaderActualWidth", cdFooterRowHeader, ColumnDefinition.WidthProperty, BindingMode.OneWay, new ConvertDoubleToGridLength());

            //데이터 그리드 컬럼 갯수에 맞춰 Footer에 ColumnDefinition을 추가 하고 Width를 바인딩 하여 데이터 그리드 헤더 사이즈 변경하면 MultiHeader의 컬럼도 사이즈 변경
            foreach (DataGridColumn dc in Content.Columns)
            {
                ColumnDefinition cd = new ColumnDefinition();
                if (Content.FrozenColumnCount != 0 && dc.IsFrozen)
                {
                    ctrl_PART_grdFrozenFooter.ColumnDefinitions.Add(cd);
                }
                else
                {
                    ctrl_PART_grdScrollFooter.ColumnDefinitions.Add(cd);
                }
                BindingHelper.SetLinkProperty(dc, "Width", cd, ColumnDefinition.WidthProperty, BindingMode.TwoWay, new DataGridLengthToGridLengthConverter());
            }

            int col = 0;
            foreach(FooterColumn fc in this.FooterCols)
            {
                fc.Column = col;
                col += (fc.ColumnSpan < 1 ? 1 : fc.ColumnSpan);
                fc.BorderBrush = Content.VerticalGridLinesBrush;
                fc.BorderThickness = new Thickness(0, 1, 1, 1);
            }

            //마지막 컬럼 우측에 ColumnDefinition을 추가 하여 사이즈 조절
            if (FooterCols != null && FooterCols.Count > 0)
            {
                ColumnDefinition cdLast = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
                FooterColumn fLast = new FooterColumn() { IsEnabled = false };
                ctrl_PART_grdScrollFooter.ColumnDefinitions.Add(cdLast); //컬럼 뒷편
                ctrl_PART_grdScrollFooter.Children.Add(fLast);
                Grid.SetColumn(fLast, Content.Columns.Count - Content.FrozenColumnCount);
                Grid.SetRow(fLast, 0);
                fLast.BorderBrush = Content.VerticalGridLinesBrush;
                fLast.BorderThickness = new Thickness(0, 0, 1, 1);
            }
            else
            {
                //Rows가 없으면 MultiHeader를 안보이게 함
                ctrl_PART_grdFooter.Height = 0;
            }
        }

        //사용안함(DataGrid 행이 적어 스크롤이 생기지 않으면 데이터 그리드 마지막행 바로 아래 푸터가 붙도록 하려함..)
        public void FooterPosition()
        {
            
            if (Content.ActualHeight != 0 && ctrl_PART_rdDataGrid != null && false)
            {
                Content.RowHeight = 25;
                ScrollViewer? sourceViewer = VisualHelper.GetVisualChild<ScrollViewer>(Content) as ScrollViewer;
                ScrollBar sbv = VisualHelper.GetDataGridInScrollBar(Content, Orientation.Vertical);
                
                //Debug.WriteLine(sourceViewer.ExtentHeight.ToString());
                //ItemsPresenter ip = (ItemsPresenter)sourceViewer.Content;

                //Debug.WriteLine(ip.ActualHeight + " : " + sourceViewer.Height);
                

                if (sbv!=null && sbv.Visibility == Visibility.Visible)
                    ctrl_PART_rdDataGrid.Height = GridLength.Auto;
                else
                    ctrl_PART_rdDataGrid.Height = new GridLength(1, GridUnitType.Star);

                //if (sourceViewer.ScrollableHeight == 0)
                //    ctrl_PART_rdDataGrid.Height = new GridLength(1, GridUnitType.Star);
                //else
                //    ctrl_PART_rdDataGrid.Height = GridLength.Auto;
            }
        }
        #endregion

        #region Content(DataGrid) Border Change
        private void DataGridBorderChange()
        {
            Thickness tk = Content.BorderThickness;
            Content.BorderThickness = new Thickness(tk.Left, 0, tk.Right, 0);
        }
        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ctrl_PART_grdMain = (Grid)GetTemplateChild(PART_grdMain);

            ctrl_PART_grdHeader = (Grid)GetTemplateChild(PART_grdHeader);
            ctrl_PART_grdFooter = (Grid)GetTemplateChild(PART_grdFooter);
            ctrl_PART_svScrollHeader = (ScrollViewer)GetTemplateChild(PART_svScrollHeader);
            ctrl_PART_borHeaderOutLine = (Border)GetTemplateChild(PART_borHeaderOutLine);
            ctrl_PART_svScrollFooter = (ScrollViewer)GetTemplateChild(PART_svScrollFooter);
            ctrl_PART_borFooterOutLine = (Border)GetTemplateChild(PART_borFooterOutLine);

            ctrl_PART_rdDataGrid = (RowDefinition)GetTemplateChild(PART_rdDataGrid);

            HeaderGridInit();
            FooterGridInit();
            DataGridBorderChange();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            ScrollToHorizontalOffsetEventAdd(Content);
            FooterPosition();
            return base.ArrangeOverride(arrangeBounds);
        }
        #endregion

        #region 데이터 그리드 스크롤 이동시 헤더와 푸터 스크롤 이동 이벤트 등록
        bool eventAdd = false; 
        public void ScrollToHorizontalOffsetEventAdd(DataGrid source)
        {
            //if (eventAdd) return;
            ScrollViewer sourceViewer = VisualHelper.GetVisualChild<ScrollViewer>(source) as ScrollViewer;
            ScrollViewer targetViewer = ctrl_PART_svScrollHeader;
            ScrollViewer targetViewer2 = ctrl_PART_svScrollFooter;
            if ( !eventAdd && sourceViewer != null && targetViewer != null && targetViewer2 != null )
            {
                sourceViewer.ScrollChanged += (o, e) =>
                {
                    targetViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
                    targetViewer2.ScrollToHorizontalOffset(e.HorizontalOffset);
                };

                eventAdd = true;
            }
        }
        #endregion
    }
}
