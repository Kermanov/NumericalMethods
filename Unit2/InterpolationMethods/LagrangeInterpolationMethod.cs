using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit2.Interfaces;

namespace Unit2.InterpolationMethods
{
    public class LagrangeInterpolationMethod : IInterpolationMethod
    {
        readonly double[] xValues;
        readonly double[] yValues;

        public LagrangeInterpolationMethod(double[] xValues, double[] yValues)
        {
            this.xValues = xValues;
            this.yValues = yValues;
        }

        public double Polynom(double x)
        {
            double result = 0;
            for (int i = 0; i < xValues.Length; ++i)
            {
                double li = 1;
                for (int j = 0; j < xValues.Length; ++j)
                {
                    if (i != j)
                    {
                        li *= (x - xValues[j]) / (xValues[i] - xValues[j]);
                    }
                }
                result += li * yValues[i];
            }

            return result;
        }
    }
}
