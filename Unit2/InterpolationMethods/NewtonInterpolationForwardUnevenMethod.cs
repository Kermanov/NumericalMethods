using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit2.Interfaces;

namespace Unit2.InterpolationMethods
{
    public class NewtonInterpolationForwardUnevenMethod : IInterpolationMethod
    {
        readonly double[] xValues;
        readonly double[] yValues;

        public NewtonInterpolationForwardUnevenMethod(double[] xValues, double[] yValues)
        {
            this.xValues = xValues;
            this.yValues = yValues;
        }

        public double Polynom(double x)
        {
            double sum = yValues[0];
            for (int i = 1; i < xValues.Length; ++i)
            {
                double term = 1;
                for (int k = 0; k < i; ++k)
                {
                    term *= (x - xValues[k]);
                }

                term *= Diff(i);
                sum += term;
            }

            return sum;
        }

        double Diff(int k)
        {
            double sum = 0;
            for (int i = 0; i <= k; ++i)
            {
                double denom = 1;
                for (int j = 0; j <= k; ++j)
                {
                    if (j != i)
                    {
                        denom *= (xValues[i] - xValues[j]);
                    }
                }

                sum += yValues[i] / denom;
            }

            return sum;
        }
    }
}
