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
            double sum = yValues[0];
            double t = (x - xValues[0]) / step;

            for (int i = 1; i < xValues.Length; ++i)
            {
                double numerator = 1;
                for (int k = 0; k < i; ++k)
                {
                    numerator *= (t - k);
                }

                double delta = 0;
                for (int k = 0; k <= i; ++k)
                {
                    delta += Math.Pow(-1, i - k) * Combination(i, k) * yValues[k];
                }

                numerator *= delta;
                sum += numerator / Factorial(i);
            }

            return sum;
        }

        int Factorial(int n)
        {
            if (n > 1)
            {
                return n * Factorial(n - 1);
            }
            else
            {
                return 1;
            }
        }

        int Combination(int n, int k)
        {
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }
    }
}
