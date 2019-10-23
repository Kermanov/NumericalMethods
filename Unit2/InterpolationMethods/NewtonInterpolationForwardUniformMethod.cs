using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit2.Interfaces;

namespace Unit2.InterpolationMethods
{
    public class NewtonInterpolationForwardUniformMethod : IInterpolationMethod
    {
        readonly double[] xValues;
        readonly double[] yValues;
        readonly double step;

        public NewtonInterpolationForwardUniformMethod(double[] xValues, double[] yValues)
        {
            this.xValues = xValues;
            this.yValues = yValues;
            step = xValues[1] - xValues[0];
        }

        public double Polynom(double x)
        {
            int n = xValues.Length;
            double result = Cj(0);
            for (int j = 1; j < n; j++)
            {
                double coef = Cj(j);
                for (int k = 0; k < j; k++)
                {
                    coef *= x - xValues[k];
                }
                result += coef;
            }

            return result;
        }

        double Cj(int j)
        {
            if (j == 0)
            {
                return yValues[0];
            }
            double delta = Delta(j, j);
            return delta / (Factorial(j) * Math.Pow(step, j));
        }

        double Delta(int p, int i)
        {
            if (p == 1)
            {
                return yValues[i] - yValues[i - 1];
            }
            return Delta(p - 1, i) - Delta(p - 1, i - 1);
        }

        double Factorial(double value)
        {
            if (value == 0)
            {
                return 1;
            }
            return value * Factorial(value - 1);
        }
    }
}
