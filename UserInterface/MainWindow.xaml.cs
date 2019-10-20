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

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            var function = ExpressionParser.GetFunction(functionInput.Text);
            var derivaitve = ExpressionParser.GetFunction(derivativeInput.Text);

            var a = double.Parse(intervalInputA.Text);
            var b = double.Parse(intervalInputB.Text);
            var eps = double.Parse(epsilonInput.Text);

            var method = new SimpleIterativeMethod(function, derivaitve, a, b, eps);
            double root = method.Calculate();

            resultTextBox.Text = $"x = {root}";

            SimpleIterationPlot.SimpleIterationPlotModel.Series.Clear();
            SimpleIterationPlot.DrawInterval(a, b);
            SimpleIterationPlot.DrawFunction(function, a, b);
            SimpleIterationPlot.DrawMethodFunction(method.Roots);
            SimpleIterationPlot.SimpleIterationPlotModel.InvalidatePlot(true);
        }
    }
}
