using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit3.Interfaces;

namespace Unit3
{
    public class RectangleMethod : IIntegrationMethod
    {
        public double Calculate(Func<double, double> function, double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0;
            for (int i = 0; i < n; ++i)
            {
                sum += function(a + ((2 * i + 1) / 2.0) * h);
            }
            double result = sum * h;

            return result;
        }
    }
}
