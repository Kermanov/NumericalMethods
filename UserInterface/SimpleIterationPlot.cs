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
    public class SimpleIterationPlot
    {
        public static PlotModel SimpleIterationPlotModel { get; private set; }

        static SimpleIterationPlot()
        {
            SimpleIterationPlotModel = new PlotModel { Title = "Simple iteration plot" };
            SimpleIterationPlotModel.PlotMargins = new OxyThickness(1);
            SimpleIterationPlotModel.PlotType = PlotType.Cartesian;
            SimpleIterationPlotModel.PlotAreaBorderThickness = new OxyThickness(0);

            SimpleIterationPlotModel.Axes.Add(new LinearAxis 
            { 
                Position = AxisPosition.Bottom, 
                PositionAtZeroCrossing = true,
                AxislineStyle = LineStyle.Solid,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromArgb(50, 0, 0, 0)

            });
            SimpleIterationPlotModel.Axes.Add(new LinearAxis
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

            SimpleIterationPlotModel.Series.Add(lineSeries);
        }

        public static void DrawFunction(Func<double, double> func, double a, double b)
        {
            var funcSeries = new FunctionSeries(func, a, b, 0.1);
            funcSeries.Color = OxyColor.FromRgb(100, 200, 100);
            SimpleIterationPlotModel.Series.Add(funcSeries);

            double outerIntervalLength = (b - a) * 5;
            var outerFuncSeriesLeft = new FunctionSeries(func, a - outerIntervalLength, a, 0.1);
            outerFuncSeriesLeft.LineStyle = LineStyle.Dash;
            outerFuncSeriesLeft.Color = OxyColor.FromArgb(80, 0, 0, 0);
            SimpleIterationPlotModel.Series.Add(outerFuncSeriesLeft);

            var outerFuncSeriesRight = new FunctionSeries(func, b, b + outerIntervalLength, 0.1);
            outerFuncSeriesRight.LineStyle = LineStyle.Dash;
            outerFuncSeriesRight.Color = OxyColor.FromArgb(80, 0, 0, 0);
            SimpleIterationPlotModel.Series.Add(outerFuncSeriesRight);
        }

        public static void DrawMethodFunction(List<double> roots)
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

                SimpleIterationPlotModel.Series.Add(rootLine);
            }
            SimpleIterationPlotModel.Series.Add(lineSeries);

            var funcSeries = new FunctionSeries(x => x, 0, roots.First(), 0.5, "y = x");
            funcSeries.Color = OxyColor.FromArgb(80, 0, 100, 100);
            funcSeries.LineStyle = LineStyle.Dot;
            SimpleIterationPlotModel.Series.Add(funcSeries);
        }
    }
}
