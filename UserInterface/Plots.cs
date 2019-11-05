using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Annotations;
using OxyPlot.Axes;

namespace UserInterface
{
    public class Plots
    {
        public static PlotModel PlotUnit1Model { get; set; }
        public static PlotModel PlotUnit2Model { get; set; }
        public static PlotModel PlotInterpol2Model { get; set; }

        static Plots()
        {
            PlotUnit1Model = CreatePlot();
            PlotUnit2Model = CreatePlot();
            PlotInterpol2Model = CreatePlot();
        }

        static PlotModel CreatePlot()
        {
            var model = new PlotModel();
            model.PlotMargins = new OxyThickness(1);
            model.PlotType = PlotType.Cartesian;
            model.PlotAreaBorderThickness = new OxyThickness(0);

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                PositionAtZeroCrossing = true,
                AxislineStyle = LineStyle.Solid,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(50, 0, 0, 0),
                FilterMaxValue = 50

            });
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                PositionAtZeroCrossing = true,
                AxislineStyle = LineStyle.Solid,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(50, 0, 0, 0),
                FilterMaxValue = 50
            });

            return model;
        }

        public static void DrawInterval(PlotModel model, double a, double b)
        {
            var lineSeries = new LineSeries();
            lineSeries.Color = OxyColor.FromArgb(200, 200, 50, 50);
            lineSeries.StrokeThickness = 5;

            lineSeries.Points.Add(new DataPoint(a, 0));
            lineSeries.Points.Add(new DataPoint(b, 0));

            model.Series.Add(lineSeries);
        }

        public static void DrawFunction(PlotModel model, Func<double, double> func, double a, double b, string color = "255,100,200,100", string title = null)
        {
            var funcSeries = new FunctionSeries(func, a, b, 0.01, title);
            funcSeries.Color = OxyColor.Parse(color);
            model.Series.Add(funcSeries);

            double outerIntervalLength = (b - a) * 5;
            var outerFuncSeriesLeft = new FunctionSeries(func, a - outerIntervalLength, a, 0.1);
            outerFuncSeriesLeft.LineStyle = LineStyle.Dash;
            outerFuncSeriesLeft.Color = OxyColor.FromArgb(80, 0, 0, 0);
            model.Series.Add(outerFuncSeriesLeft);

            var outerFuncSeriesRight = new FunctionSeries(func, b, b + outerIntervalLength, 0.1);
            outerFuncSeriesRight.LineStyle = LineStyle.Dash;
            outerFuncSeriesRight.Color = OxyColor.FromArgb(80, 0, 0, 0);
            model.Series.Add(outerFuncSeriesRight);
        }

        public static void DrawSimpleMethodFunction(List<double> roots)
        {
            var lineSeries = new LineSeries();
            lineSeries.Color = OxyColor.FromRgb(0, 0, 150);

            for (int i = 1; i < roots.Count; ++i)
            {
                lineSeries.Points.Add(new DataPoint(roots[i - 1], roots[i]));

                var rootLine = new LineSeries();
                rootLine.LineStyle = LineStyle.Dash;
                rootLine.StrokeThickness = 1;
                rootLine.Color = OxyColor.FromRgb(0, 0, 0);

                rootLine.Points.Add(new DataPoint(roots[i - 1], roots[i]));
                rootLine.Points.Add(new DataPoint(roots[i - 1], 0));

                PlotUnit1Model.Series.Add(rootLine);
            }
            PlotUnit1Model.Series.Add(lineSeries);

            var funcSeries = new FunctionSeries(x => x, 0, roots.First(), 0.5, "y = x");
            funcSeries.Color = OxyColor.FromArgb(80, 0, 100, 100);
            funcSeries.LineStyle = LineStyle.Dot;
            PlotUnit1Model.Series.Add(funcSeries);
        }

        public static void DrawNewtonMethodFunction(List<double> roots, Func<double, double> func)
        {
            for (int i = 0; i < roots.Count; ++i)
            {
                var rootLine = new LineSeries();
                rootLine.LineStyle = LineStyle.Dash;
                rootLine.StrokeThickness = 1;
                rootLine.Color = OxyColor.FromRgb(0, 0, 0);

                rootLine.Points.Add(new DataPoint(roots[i], func(roots[i])));
                rootLine.Points.Add(new DataPoint(roots[i], 0));

                PlotUnit1Model.Series.Add(rootLine);
            }

            for (int i = 1; i < roots.Count; ++i)
            {
                var tangent = new LineSeries();
                tangent.LineStyle = LineStyle.Solid;
                tangent.StrokeThickness = 1;
                tangent.Color = OxyColor.FromRgb(200, 0, 0);

                tangent.Points.Add(new DataPoint(roots[i], 0));
                tangent.Points.Add(new DataPoint(roots[i - 1], func(roots[i - 1])));

                PlotUnit1Model.Series.Add(tangent);
            }
        }

        public static void DrawChordMethodFunction(List<double> roots, double b, Func<double, double> func)
        {
            for (int i = 0; i < roots.Count; ++i)
            {
                var chord = new LineSeries();
                chord.LineStyle = LineStyle.Solid;
                chord.StrokeThickness = 1;
                chord.Color = OxyColor.FromRgb(200, 0, 0);

                chord.Points.Add(new DataPoint(roots[i], func(roots[i])));
                chord.Points.Add(new DataPoint(b, func(b)));

                PlotUnit1Model.Series.Add(chord);
            }

            for (int i = 1; i < roots.Count; ++i)
            {
                var rootLine = new LineSeries();
                rootLine.LineStyle = LineStyle.Dash;
                rootLine.StrokeThickness = 1;
                rootLine.Color = OxyColor.FromRgb(0, 0, 0);

                rootLine.Points.Add(new DataPoint(roots[i], 0));
                rootLine.Points.Add(new DataPoint(roots[i], func(roots[i])));

                PlotUnit1Model.Series.Add(rootLine);
            }
        }

        public static void DrawPoints(PlotModel model, double[] xValues, double[] yValues)
        {
            var points = new LineSeries();
            points.Color = OxyColor.FromArgb(0, 0, 0, 0);
            points.MarkerType = MarkerType.Circle;
            points.MarkerStroke = OxyColor.FromRgb(255, 132, 0);
            points.MarkerFill = OxyColor.FromRgb(255, 255, 255);
            points.MarkerSize = 4;
            points.MarkerStrokeThickness = 3;

            for (int i = 0; i < xValues.Length; ++i)
            {
                points.Points.Add(new DataPoint(xValues[i], yValues[i]));
            }

            model.Series.Add(points);
        }
    }
}
