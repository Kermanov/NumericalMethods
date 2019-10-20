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

            Func<double, double> function = x => 0.5 * Math.Pow(x - 4, 2) - 4;
            double a = 2;
            double b = 10;

            SimpleIterationPlot.DrawInterval(a, b);
            SimpleIterationPlot.DrawFunction(function, a, b);
        }
    }
}
