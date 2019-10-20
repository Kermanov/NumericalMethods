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
        Func<double, double> function;
        Func<double, double> derivative;
        double a;
        double b;
        double eps;

        public MainWindow()
        {
            InitializeComponent();
        }

        private double SimpleIterativeMethodRun()
        {
            var method = new SimpleIterativeMethod(function, derivative, a, b, eps);
            double root = method.Calculate();
            Plot.DrawSimpleMethodFunction(method.Roots);
            return root;
        }

        private double NewtonIterativeMethodRun()
        {
            var method = new NewtonIterativeMethod(function, derivative, a, b, eps);
            double root = method.Calculate();
            Plot.DrawNewtonMethodFunction(method.Roots, function);
            return root;
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            function = ExpressionParser.GetFunction(functionInput.Text);
            derivative = ExpressionParser.GetFunction(derivativeInput.Text);
            a = double.Parse(intervalInputA.Text);
            b = double.Parse(intervalInputB.Text);
            eps = double.Parse(epsilonInput.Text);

            Plot.PlotModel.Series.Clear();
            Plot.DrawInterval(a, b);
            Plot.DrawFunction(function, a, b);

            double root = 0;
            if (methodSelect.SelectedIndex == 0)
            {
                root = SimpleIterativeMethodRun();
            }
            else if (methodSelect.SelectedIndex == 1)
            {
                root = NewtonIterativeMethodRun();
            }

            Plot.PlotModel.InvalidatePlot(true);

            resultTextBox.Text = $"x = {root}";
        }
    }
}
