using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit3.Interfaces;

namespace Unit3
{
    public class SimpsonsMethod : IIntegrationMethod
    {
        public double Calculate(Func<double, double> function, double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0;
            for (int i = 1; i < n; ++i)
            {
                double x = a + h * i;
                sum += (2 + 2 * (i % 2)) * function(x);
            }
            sum += function(a) + function(b);

            double result = sum * h / 3;
            return result;
        }
    }
}
