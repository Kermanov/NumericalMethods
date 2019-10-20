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
    public class Plot
    {
        public static PlotModel PlotModel { get; private set; }

        static Plot()
        {
            PlotModel = new PlotModel { Title = "Simple iteration plot" };
            PlotModel.PlotMargins = new OxyThickness(1);
            PlotModel.PlotType = PlotType.Cartesian;
            PlotModel.PlotAreaBorderThickness = new OxyThickness(0);

            PlotModel.Axes.Add(new LinearAxis 
            { 
                Position = AxisPosition.Bottom, 
                PositionAtZeroCrossing = true,
                AxislineStyle = LineStyle.Solid,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(50, 0, 0, 0)

            });
            PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                PositionAtZeroCrossing = true,
                AxislineStyle = LineStyle.Solid,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(50, 0, 0, 0)
            });
        }

        public static void DrawInterval(double a, double b)
        {
            var lineSeries = new LineSeries();
            lineSeries.Color = OxyColor.FromArgb(200, 200, 50, 50);
            lineSeries.StrokeThickness = 5;

            lineSeries.Points.Add(new DataPoint(a, 0));
            lineSeries.Points.Add(new DataPoint(b, 0));

            PlotModel.Series.Add(lineSeries);
        }

        public static void DrawFunction(Func<double, double> func, double a, double b)
        {
            var funcSeries = new FunctionSeries(func, a, b, 0.1);
            funcSeries.Color = OxyColor.FromRgb(100, 200, 100);
            PlotModel.Series.Add(funcSeries);

            double outerIntervalLength = (b - a) * 5;
            var outerFuncSeriesLeft = new FunctionSeries(func, a - outerIntervalLength, a, 0.1);
            outerFuncSeriesLeft.LineStyle = LineStyle.Dash;
            outerFuncSeriesLeft.Color = OxyColor.FromArgb(80, 0, 0, 0);
            PlotModel.Series.Add(outerFuncSeriesLeft);

            var outerFuncSeriesRight = new FunctionSeries(func, b, b + outerIntervalLength, 0.1);
            outerFuncSeriesRight.LineStyle = LineStyle.Dash;
            outerFuncSeriesRight.Color = OxyColor.FromArgb(80, 0, 0, 0);
            PlotModel.Series.Add(outerFuncSeriesRight);
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

                PlotModel.Series.Add(rootLine);
            }
            PlotModel.Series.Add(lineSeries);

            var funcSeries = new FunctionSeries(x => x, 0, roots.First(), 0.5, "y = x");
            funcSeries.Color = OxyColor.FromArgb(80, 0, 100, 100);
            funcSeries.LineStyle = LineStyle.Dot;
            PlotModel.Series.Add(funcSeries);
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

                PlotModel.Series.Add(rootLine);
            }

            for (int i = 1; i < roots.Count; ++i)
            {
                var tangent = new LineSeries();
                tangent.LineStyle = LineStyle.Solid;
                tangent.StrokeThickness = 1;
                tangent.Color = OxyColor.FromRgb(200, 0, 0);

                tangent.Points.Add(new DataPoint(roots[i], 0));
                tangent.Points.Add(new DataPoint(roots[i - 1], func(roots[i - 1])));

                PlotModel.Series.Add(tangent);
            }
        }

    }
}
