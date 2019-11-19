using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit3.Interfaces;

namespace Unit3
{
    public class TrapezoidMethod : IIntegrationMethod
    {
        public double Calculate(Func<double, double> function, double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0;
            for (int i = 1; i < n; ++i)
            {
                sum += function(a + i * h);
            }
            sum *= 2;
            sum += function(a) + function(b);

            double result = sum * h / 2;
            return result;
        }
    }
}
