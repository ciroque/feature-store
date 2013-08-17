/*****************************************************************
 * Copyright 2008 Microsoft Corporation
 * File    : Chart.cs
 * Created : 2008-06-19
 * Author  : danieln
 * Purpose : Lightweight rendering utility for drawing graphs
 * 
 * Change History :
 *  Date        Author      Change
 *  -------------------------------
 *  2008-06-19  danieln     Created
 *  2008-06-20  danieln     Function complete
 * 
 * Notes :
 *****************************************************************/

using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace ChartUtility
{
    public class Chart : IDisposable
    {
        #region Enumerations
        public enum ChartType
        {
            Line,
            Area,
            Scatter,
            Bar
        }
        #endregion

        #region Memeber Variables
        protected float[] gScale = new float[] { 0.0010f, 0.0020f, 0.0025f, 0.0050f };

        protected ChartType _Type = ChartType.Line;
        public ChartType Type { get { return _Type; } set { _Type = value; } }

        // Data source for the charting data
        protected DataTable _Data = null;
        public DataTable Data { get { return _Data; } set { _Data = value; } }
        // Y-Axis columns, ordered by grouping
        protected ChartColumnCollection _IndependentColumns = new ChartColumnCollection();
        public ChartColumnCollection IndependentColumns { get { return _IndependentColumns; } set { _IndependentColumns = value; } }
        // X-Axis columns, in presentation order
        protected ChartColumnCollection _DependentColumns = new ChartColumnCollection();
        public ChartColumnCollection DependentColumns { get { return _DependentColumns; } set { _DependentColumns = value; } }
        // Ranges used to graph
        protected RangeCollection _Ranges = new RangeCollection();


        // List of variable colors for the chart to use
        protected string[] _VariableColors = new string[] { "#5F4281", "#2A92B2", "#DB843D", "#819BC5", "#FF56F3", "#FF96D8" };
        public string[] VariableColors { get { return _VariableColors; } set { _VariableColors = value; } }

        // Toggle whether to draw the Chart or not
        protected bool _DrawChart = true;
        public bool DrawChart { get { return _DrawChart; } set { _DrawChart = value; } }
        // Toggle whether to draw the vertical grid or not
        protected bool _DrawVerticalGrid = true;
        public bool DrawVerticalGrid { get { return _DrawVerticalGrid; } set { _DrawVerticalGrid = value; } }
        // Toggle whether to draw the horizontal grid or not
        protected bool _DrawHorizontalGrid = true;
        public bool DrawHorizontalGrid { get { return _DrawHorizontalGrid; } set { _DrawHorizontalGrid = value; } }
        // Toggle whether to draw the legend or not
        protected bool _DrawLegend = true;
        public bool DrawLegend { get { return _DrawLegend; } set { _DrawLegend = value; } }
        // Toggle whether to draw the legend or not
        protected bool _DrawLegendTitle = true;
        public bool DrawLegendTitle { get { return _DrawLegendTitle; } set { _DrawLegendTitle = value; } }
        // Toggle whether to draw the axis scaling or not
        protected bool _DrawScale = true;
        public bool DrawScale { get { return _DrawScale; } set { _DrawScale = value; } }

        // Enables the ability to graph variables with different scales on the same graph
        protected bool _EnableMultipleScales = false;
        public bool EnableMultipleScales { get { return _EnableMultipleScales; } set { _EnableMultipleScales = value; } }

        // Size of the chart
        protected Size _Dimensions = new Size(650, 250);
        public Size Dimensions { get { return _Dimensions; } set { _Dimensions = value; } }
        public int Width { get { return _Dimensions.Width; } set { _Dimensions.Width = value; } }
        public int Height { get { return _Dimensions.Height; } set { _Dimensions.Height = value; } }

        protected Rectangle _Margins = new Rectangle(0,0,600, 250);

        protected float _LegendWidth = 145.0f;
        public float LegendWidth { get { return _LegendWidth; } set { _LegendWidth = value; } }
        protected float _ScaleWidth = 55.0f;
        public float ScaleWidth { get { return _ScaleWidth; } set { _ScaleWidth = value; } }

        // Background color
        protected Color _BackgroundColor = Color.White;
        public Color BackgroundColor { get { return _BackgroundColor; } set { _BackgroundColor = value; } }
        protected Color _ForegroundColor = Color.Black;
        public Color ForegroundColor { get { return _ForegroundColor; } set { _ForegroundColor = value; } }
        protected Color _ScaleTextColor = Color.Black;
        public Color ScaleTextColor { get { return _ScaleTextColor; } set { _ScaleTextColor = value; } }
        protected Color _LegendTextColor = Color.Black;
        public Color LegendTextColor { get { return _LegendTextColor; } set { _LegendTextColor = value; } }
        protected Color _DarkGridColor = Color.DarkGray;
        public Color DarkGridColor { get { return _DarkGridColor; } set { _DarkGridColor = value; } }
        protected Color _LightGridColor = Color.LightGray;
        public Color LightGridColor { get { return _LightGridColor; } set { _LightGridColor = value; } }

        protected string _ScaleFontName = "Arial";
        public string ScaleFontName { get { return _ScaleFontName; } set { _ScaleFontName = value; } }
        protected float _ScaleFontHeight = 10;
        public float ScaleFontHeight { get { return _ScaleFontHeight; } set { _ScaleFontHeight = value; } }
        protected string _LegendFontName = "Arial";
        public string LegendFontName { get { return _LegendFontName; } set { _LegendFontName = value; } }
        protected float _LegendFontHeight = 10;
        public float LegendFontHeight { get { return _LegendFontHeight; } set { _LegendFontHeight = value; } }
        protected string _LegendTitleFontName = "Arial";
        public string LegendTitleFontName { get { return _LegendTitleFontName; } set { _LegendTitleFontName = value; } }
        protected float _LegendTitleFontHeight = 12;
        public float LegendTitleFontHeight { get { return _LegendTitleFontHeight; } set { _LegendTitleFontHeight = value; } }

        protected float _PlotWidth = 2.0f;
        public float PlottingLineWidth { get { return _PlotWidth; } set { _PlotWidth = value; } }
        protected float _BarWidth = 2.0f;
        public float BarWidth { get { return _BarWidth; } set { _BarWidth = value; } }
        #endregion

        #region Drawing Objects
        protected Bitmap _Bitmap;
        protected Graphics _Graphics;

        protected Brush _BackgroundBrush;
        protected Brush _ForegroundBrush;
        protected Brush _ScaleTextBrush;
        protected Brush _LegendTextBrush;

        protected Pen _ForegroundPen;
        protected Pen _DarkGridPen;
        protected Pen _LightGridPen;

        protected Font _ScaleFont;
        protected Font _LegendFont;
        protected Font _LegendTitleFont;
        #endregion

        #region ctor/dtor
        public Chart()
        {
        }

        public void Dispose()
        {

        }
        #endregion

        #region Setup
        private void InitializeDrawingObjects()
        {
            _Bitmap = new Bitmap(_Dimensions.Width, _Dimensions.Height);
            _Graphics = Graphics.FromImage(_Bitmap);
            _BackgroundBrush = new SolidBrush(_BackgroundColor);
            _ForegroundBrush = new SolidBrush(_ForegroundColor);
            _ScaleTextBrush = new SolidBrush(_ScaleTextColor);
            _LegendTextBrush = new SolidBrush(_LegendTextColor);
            _ForegroundPen = new Pen(_ForegroundColor);
            _DarkGridPen = new Pen(_DarkGridColor);
            _LightGridPen = new Pen(_LightGridColor);

            _ScaleFont = new Font(_ScaleFontName, _ScaleFontHeight);
            _LegendFont = new Font(_LegendFontName, _LegendFontHeight);
            _LegendTitleFont = new Font(_LegendTitleFontName, _LegendTitleFontHeight, FontStyle.Bold);

            _Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            _Graphics.CompositingQuality = CompositingQuality.HighQuality;
            _Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        }

        protected void Prepare()
        {
            InitializeDrawingObjects();
            CalculateDataBounds(_IndependentColumns);
            CalculateDataBounds(_DependentColumns);
            AdjustDataBounds(_DependentColumns);
            SetMargins();

            SetColumnColors();
        }

        protected void SetColumnColors()
        {
            for (int i = 0; i < _DependentColumns.Count; i++ )
                _DependentColumns[i].LineColor = _VariableColors[i % _VariableColors.Length];
        }

        protected void SetMargins()
        {
            _Margins = new Rectangle(10, 10, Width-20, Height-20);
            if (_DrawScale)
            {
                _Margins.X += (int)_ScaleWidth;
                _Margins.Width -= (int)_ScaleWidth * (_Ranges.Count);
                _Margins.Height -= (int)_ScaleWidth;
            }
            if (_DrawLegend)
            {
                _Margins.Width -= (int)_LegendWidth;
            }
        }

        protected void CalculateDataBounds(ChartColumnCollection columnSet)
        {
            foreach (ChartColumn column in columnSet)
            {
                column.Range.Min = _Data.Compute(string.Format("MIN([{0}])", column.ColumnName), "");
                column.Range.Max = _Data.Compute(string.Format("MAX([{0}])", column.ColumnName), "");
                column.Range = column.Range.SnapToScale();
            }
        }

        protected void AdjustDataBounds(ChartColumnCollection columnSet)
        {
            _Ranges.Clear();
            Range globalRange = new NumberRange(0, 0);
            for (int i = 0; i < columnSet.Count; i++)
            {
                if (columnSet[i].Range.GetType() == typeof(NumberRange))
                    globalRange = globalRange.ExpandTo(columnSet[i].Range);
                // Experimental
                if (_EnableMultipleScales)
                    _Ranges.AppendRange(columnSet[i].Range);
            }
            if (!_EnableMultipleScales)
            {
                _Ranges.AppendRange(globalRange);
            }
        }
        #endregion

        #region Drawing
        public Bitmap GetBitmap()
        {
            Draw();
            return _Bitmap;
        }

        protected void Draw()
        {
            // Prepare the data and drawing objects
            Prepare();

            // Draw the background
            DrawBackground();
            // Draw the grid
            DrawGrid();
            // Draw the legend
            DrawDataLegend();

            // Plot all data
            PlotData();
        }

        #region Draw Background
        // Draw the background
        protected void DrawBackground()
        {
            _Graphics.FillRectangle(_BackgroundBrush, 0, 0, Width, Height);
        }
        #endregion

        #region Draw Grid
        protected void DrawGrid()
        {
            _Graphics.DrawRectangle(_ForegroundPen, _Margins);
            #region Vertical Grid
            if (_DrawVerticalGrid || _DrawScale)
            {
                int rangeCnt = 0;
                foreach (Range range in _Ranges)
                {
                    rangeCnt++;
                    for (int step = 0; step < range.Steps; step++)
                    {
                        float y = range.GetStepFloat(step);
                        float scaledY = (y / range.FloatDiff) * _Margins.Height;
                        if (_DrawVerticalGrid && rangeCnt==1)
                        {
                            _Graphics.DrawLine(_DarkGridPen,
                                new PointF(_Margins.Left, (float)(_Margins.Bottom - scaledY)),
                                new PointF(_Margins.Right, (float)(_Margins.Bottom - scaledY)));
                        }
                        if (_DrawScale)
                        {
                            float textBoxTop = (float)(_Margins.Bottom - scaledY - _ScaleFont.Height);
                            float textBoxLeft = 0;
                            StringFormat labelFmtY = new StringFormat();
                            labelFmtY.LineAlignment = StringAlignment.Center;
                            bool drawThisScale = true;
                            if (rangeCnt == 1) // #1 - left side
                            {
                                labelFmtY.Alignment = StringAlignment.Far;
                                textBoxLeft = 0;
                            }
                            else if (rangeCnt == 2)
                            {
                                labelFmtY.Alignment = StringAlignment.Near;
                                textBoxLeft = _Margins.Right;
                            }
                            else
                            {
                                // Experimental
                                labelFmtY.Alignment = StringAlignment.Near;
                                textBoxLeft = _Margins.Right + _ScaleWidth * (rangeCnt - 2);
                            }
                            /**
                            else // I can only draw two scales, one on either side
                                drawThisScale = false;
                            **/

                            if (drawThisScale)
                            {
                                object mark = range.GetStep(step);
                                string label = "";
                                if (mark.GetType() == typeof(DateTime))
                                    label = ((DateTime)mark).ToString("d");
                                else if (mark.GetType() == typeof(float))
                                    label = ((float)mark).ToString("#,###,###,##0.#####");

                                _Graphics.DrawString(label,
                                    _ScaleFont, _ScaleTextBrush,
                                    new RectangleF(textBoxLeft, textBoxTop, _Margins.Left, _ScaleFont.Height * 2),
                                    labelFmtY);
                            }
                        }
                    }
                }
            }
            #endregion

            #region Horizontal Grid
            if (_DrawHorizontalGrid || _DrawScale)
            {
                // TODO: Support for more than one axis!
                if (_IndependentColumns.Count == 0)
                    throw new Exception("Graph requires at least one independant column");

                ChartColumn column = _IndependentColumns[0];
                Range range = column.Range;
                for (int step = 0; step < range.Steps; step++)
                {
                    float x = range.GetStepFloat(step);
                    float scaledX = (x / range.FloatDiff) * _Margins.Width;
                    if (_DrawHorizontalGrid)
                    {
                        _Graphics.DrawLine(_DarkGridPen,
                            new PointF((float)(_Margins.Right - scaledX), _Margins.Top),
                            new PointF((float)(_Margins.Right - scaledX), _Margins.Bottom));
                    }
                    if (_DrawScale)
                    {
                        float textBoxLeft = (float)(_Margins.Right - scaledX);
                        StringFormat labelFmtX = new StringFormat();
                        labelFmtX.LineAlignment = StringAlignment.Center;
                        labelFmtX.Alignment = StringAlignment.Far;

                        bool diagonal = true;

                        object mark = range.GetStep(step);
                        string label = "";
                        if (range.GetType() == typeof(TimeRange))
                        {
                            if(((TimeRange)range).Interval.Interval == DateInterval.Hourly)
                                label = ((DateTime)mark).ToString("g");
                            else
                                label = ((DateTime)mark).ToString("d");
                        }
                        else if (range.GetType() == typeof(NumberRange))
                            label = ((float)mark).ToString("#,###,###,##0.#####");

                        // Save state
                        GraphicsState gs = _Graphics.Save();
                        // Translate and Rotate
                        _Graphics.ResetTransform();
                        _Graphics.TranslateTransform(textBoxLeft, _Margins.Bottom + 5);
                        if (diagonal)
                            _Graphics.RotateTransform(-45);

                        _Graphics.DrawString(label,
                            _ScaleFont, _ScaleTextBrush,
                            new Point(0,0/*, _Margins.Left, _ScaleFont.Height * 2*/),
                            labelFmtX);

                        _Graphics.Restore(gs);
                    }
                }
            }
            #endregion
        }
        #endregion

        #region Draw Legend
        protected void DrawDataLegend()
        {
            if (_DrawLegend)
            {
                float height = 10;
                float width = _LegendWidth - 20;
                float y = _Margins.Top;
                float x = Width - _LegendWidth + 10;

                if (DrawLegendTitle)
                {
                    StringFormat TitleStrFmt = new StringFormat();
                    TitleStrFmt.Alignment = StringAlignment.Center;
                    TitleStrFmt.LineAlignment = StringAlignment.Near;
                    _Graphics.DrawString("Legend", _LegendTitleFont, _LegendTextBrush,
                        new PointF(x + width / 2, y + height), TitleStrFmt);
                    SizeF legendSize = _Graphics.MeasureString("Legend", _LegendTitleFont, (int)width, TitleStrFmt);
                    height += legendSize.Height + 5;
                }

                SizeF textBounds = new SizeF(width-20,_LegendFont.Height*2);
                for (int i = 0; i < _DependentColumns.Count; i++)
                {
                    _Graphics.DrawString(_DependentColumns[i].ColumnName, _LegendFont, _LegendTextBrush,
                        new RectangleF(new PointF(x + 20, y + height), textBounds));
                    SizeF itemSize = _Graphics.MeasureString(_DependentColumns[i].ColumnName, _LegendFont, textBounds);
                    // Draw the color
                    RectangleF colorRect = new RectangleF(x + 5, y + height + (itemSize.Height - 10) / 2, 10, 10);
                    Brush colorBrush = new SolidBrush(ColorTranslator.FromHtml(_DependentColumns[i].LineColor));
                    _Graphics.FillRectangle(colorBrush, colorRect);
                    height += itemSize.Height + 5;
                }
            }
        }
        #endregion

        #region DataPlotting
        protected void PlotData()
        {
            // TODO: Support for more than one axis!
            if (_IndependentColumns.Count == 0)
                throw new Exception("Graph requires at least one independant column");

            GraphicsState gs = _Graphics.Save();
            _Graphics.TranslateTransform(_Margins.Left, _Margins.Top);

            ChartColumn columnX = _IndependentColumns[0];
            Range rangeX = columnX.Range;
            
            // Sort the data by independent column
            DataView dataView = new DataView(_Data);
            dataView.Sort = string.Format("[{0}] ASC", columnX.ColumnName);
            DataTable data = dataView.ToTable();

            int colNum = 0;
            foreach (ChartColumn columnY in _DependentColumns)
            {
                Range rangeY = columnY.Range;
                List<PointF> points = new List<PointF>(512);
                float lastX=0, lastY=0;
                bool firstPoint = true;
                foreach (DataRow row in data.Rows)
                {
                    if(row[columnY.ColumnName].GetType() == typeof(DBNull))
                        continue;
                    float x = 0, y = 0;

                    float yVal = rangeY.Normalize(row[columnY.ColumnName]);
                    float xVal = rangeX.Normalize(row[columnX.ColumnName]);

                    y = _Margins.Height - yVal * _Margins.Height;
                    x = xVal * _Margins.Width;
                    
                    if(firstPoint)
                        points.Add(new PointF(x, _Margins.Height));
                    firstPoint = false;
                    points.Add(new PointF(x,y));
                    lastX = x; lastY = y;
                }
                points.Add(new PointF(lastX, _Margins.Height));


                Color color = ColorTranslator.FromHtml(columnY.LineColor);
                Brush plotBrush = new SolidBrush(color);
                Pen plotPen = new Pen(color, _PlotWidth);
                GraphicsPath path = new GraphicsPath();
                switch (_Type)
                {
                    case ChartType.Line:
                        {
                            // Line graph
                            points.RemoveAt(0);
                            points.RemoveAt(points.Count-1);
                            path.AddLines(points.ToArray());
                            _Graphics.DrawPath(plotPen, path);
                        }
                        break;
                    case ChartType.Area:
                        {
                            // Area graph
                            path.AddLines(points.ToArray());
                            _Graphics.FillPath(plotBrush, path);
                        }
                        break;
                    case ChartType.Scatter:
                        {
                            // Scatter plot
                            // Skip first and last "anchor" points
                            for (int i = 1; i < points.Count - 1; i++)
                            {
                                path.AddRectangle(new RectangleF(
                                    points[i].X - 2.0f, points[i].Y - 2.0f,
                                    4.0f, 4.0f));
                            }
                            _Graphics.DrawPath(plotPen, path);
                        }
                        break;
                    case ChartType.Bar:
                        {
                            // Scatter plot
                            // Skip first and last "anchor" points
                            float leftOffset = (_DependentColumns.Count * _BarWidth)/2;
                            float offset = leftOffset+colNum * _BarWidth;
                            for (int i = 1; i < points.Count - 1; i++)
                            {
                                path.AddRectangle(new RectangleF(
                                    points[i].X - offset, points[i].Y,
                                    _BarWidth, _Margins.Height - points[i].Y));
                            }
                            _Graphics.FillPath(plotBrush, path);
                        }
                        break;
                }
                colNum++;

            }

            _Graphics.Restore(gs);
        }
        #endregion

        #endregion
    }
}
