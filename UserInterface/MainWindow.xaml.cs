using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common;
using Unit1.IterativeMethods;
using Unit2.InterpolationMethods;
using Unit2;
using Unit2.Interfaces;
using Unit3;
using Unit3.Interfaces;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Func<double, double> function1;
        Func<double, double> derivative;
        double a1;
        double b;
        double eps;

        Func<double, double> function2;
        double a2;
        double b2;

        IInterpolationMethod interpolationMethod2 = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private double SimpleIterativeMethodRun()
        {
            var method = new SimpleIterativeMethod(function1, derivative, a1, b, eps);
            double root = method.Calculate();
            Plots.DrawSimpleMethodFunction(method.Roots);
            return root;
        }

        private double NewtonIterativeMethodRun()
        {
            var method = new NewtonIterativeMethod(function1, derivative, a1, b, eps);
            double root = method.Calculate();
            Plots.DrawNewtonMethodFunction(method.Roots, function1);
            return root;
        }

        public double ChordIterativeMethodRun()
        {
            var method = new ChordIterativeMethod(function1, a1, b, eps);
            double root = method.Calculate();
            Plots.DrawChordMethodFunction(method.Roots, b, function1);
            return root;
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                function1 = ExpressionParser.GetFunction(functionInput.Text);
                derivative = ExpressionParser.GetFunction(derivativeInput.Text);

                a1 = double.Parse(intervalInputA.Text);
                b = double.Parse(intervalInputB.Text);

                if (a1 >= b)
                {
                    throw new Exception();
                }

                eps = double.Parse(epsilonInput.Text);
            }
            catch
            {
                resultTextBox.Text = $"Incorrect input";
                return;
            }

            Plots.PlotUnit1Model.Series.Clear();
            Plots.DrawInterval(Plots.PlotUnit1Model, a1, b);
            Plots.DrawFunction(Plots.PlotUnit1Model, function1, a1, b);

            double root = 0;
            if (methodSelect.SelectedIndex == 0)
            {
                root = SimpleIterativeMethodRun();
            }
            else if (methodSelect.SelectedIndex == 1)
            {
                root = NewtonIterativeMethodRun();
            }
            else if (methodSelect.SelectedIndex == 2)
            {
                root = ChordIterativeMethodRun();
            }

            Plots.PlotUnit1Model.InvalidatePlot(true);

            resultTextBox.Text = $"x = {root}";
        }

        private void interpolateButton_Click(object sender, RoutedEventArgs e)
        {
            function2 = ExpressionParser.GetFunction(functionInput2.Text);
            int nNodes = (int)nodesSlider.Value;
            a2 = double.Parse(intervalInputA2.Text);
            b2 = double.Parse(intervalInputB2.Text);

            if (methodSelect2.SelectedIndex == 1 || methodSelect2.SelectedIndex == 2)
            {
                nodesSelectMode.SelectedIndex = 0;
            }

            double[] xValues = null;
            if (nodesSelectMode.SelectedIndex == 0)
            {
                xValues = InterpolationNodesSelector.SelectUniform(a2, b2, nNodes);
            }
            else if (nodesSelectMode.SelectedIndex == 1)
            {
                xValues = InterpolationNodesSelector.SelectChebyshev(a2, b2, nNodes);
            }

            double[] yValues = new double[nNodes];
            for (int i = 0; i < nNodes; ++i)
            {
                yValues[i] = function2(xValues[i]);
            }

            IInterpolationMethod interpolationMethod = null;
            if (methodSelect2.SelectedIndex == 0)
            {
                interpolationMethod = new LagrangeInterpolationMethod(xValues, yValues);
            }
            else if (methodSelect2.SelectedIndex == 1)
            {
                interpolationMethod = new NewtonInterpolationForwardUniformMethod(xValues, yValues);
            }
            else if (methodSelect2.SelectedIndex == 2)
            {
                interpolationMethod = new NewtonInterpolationBackUniformMethod(xValues, yValues);
            }
            else if (methodSelect2.SelectedIndex == 3)
            {
                interpolationMethod = new NewtonInterpolationForwardUnevenMethod(xValues, yValues);
            }
            else if (methodSelect2.SelectedIndex == 4)
            {
                interpolationMethod = new NewtonInterpolationBackUnevenMethod(xValues, yValues);
            }

            Plots.PlotUnit2Model.Series.Clear();

            Plots.DrawInterval(Plots.PlotUnit2Model, a2, b2);
            Plots.DrawFunction(Plots.PlotUnit2Model, function2, a2, b2, "255,100,200,100", $"y = {functionInput2.Text}");
            Plots.DrawFunction(Plots.PlotUnit2Model, interpolationMethod.Polynom, a2, b2, "255,100,100,200", "Interpolation polynom");
            Plots.DrawPoints(Plots.PlotUnit2Model, xValues, yValues);

            Plots.PlotUnit2Model.InvalidatePlot(true);
        }

        private void interpolateButton2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] xValuesString = xValuesInput.Text.TrimEnd(';').Split(';');
                double[] xValues = new double[xValuesString.Length];
                for (int i = 0; i < xValuesString.Length; ++i)
                {
                    xValues[i] = double.Parse(xValuesString[i]);
                }

                string[] yValuesString = yValuesInput.Text.TrimEnd(';').Split(';');
                double[] yValues = new double[yValuesString.Length];
                for (int i = 0; i < yValuesString.Length; ++i)
                {
                    yValues[i] = double.Parse(yValuesString[i]);
                }

                if (xValues.Length == 0 || yValues.Length == 0)
                {
                    throw new Exception();
                }
                
                if (xValues.Length != yValues.Length)
                {
                    throw new Exception();
                }

                if (methodSelect3.SelectedIndex == 0)
                {
                    interpolationMethod2 = new LagrangeInterpolationMethod(xValues, yValues);
                }
                else if (methodSelect3.SelectedIndex == 1)
                {
                    interpolationMethod2 = new NewtonInterpolationForwardUniformMethod(xValues, yValues);
                }
                else if (methodSelect3.SelectedIndex == 2)
                {
                    interpolationMethod2 = new NewtonInterpolationBackUniformMethod(xValues, yValues);
                }
                else if (methodSelect3.SelectedIndex == 3)
                {
                    interpolationMethod2 = new NewtonInterpolationForwardUnevenMethod(xValues, yValues);
                }
                else if (methodSelect3.SelectedIndex == 4)
                {
                    interpolationMethod2 = new NewtonInterpolationBackUnevenMethod(xValues, yValues);
                }

                Plots.PlotInterpol2Model.Series.Clear();

                Plots.DrawFunction(Plots.PlotInterpol2Model, interpolationMethod2.Polynom, xValues[0], xValues[xValues.Length - 1], "255,100,100,200", "Interpolation polynom");
                Plots.DrawPoints(Plots.PlotInterpol2Model, xValues, yValues);

                Plots.PlotInterpol2Model.InvalidatePlot(true);
            }
            catch { }
        }

        private void calculateCustom_Click(object sender, RoutedEventArgs e)
        {
            if (interpolationMethod2 != null)
            {
                try
                {
                    yValueResult.Text = interpolationMethod2.Polynom(double.Parse(xValueInput.Text)).ToString();
                }
                catch { }
            }
        }

        private void integrateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var function = ExpressionParser.GetFunction(integrationFunctionInput.Text);
                double a = double.Parse(integrationInputA.Text);
                double b = double.Parse(integrationInputB.Text);
                int n = (int)integrNodesSlider.Value;

                IIntegrationMethod integrationMethod = null;
                if (integrationMethodSelect.SelectedIndex == 0)
                {
                    integrationMethod = new RectangleMethod();
                }
                else if (integrationMethodSelect.SelectedIndex == 1)
                {
                    integrationMethod = new TrapezoidMethod();
                }

                double result = integrationMethod.Calculate(function, a, b, n);
                integrationResultText.Text = result.ToString();
            }
            catch
            {

            }
        }
    }
}
