using System;
using System.Collections.Generic;
using System.Text;

namespace Unit1.IterativeMethods
{
    public class SimpleIterativeMethod : IterativeMethodBase
    {
        private readonly double phi;
        public SimpleIterativeMethod(Func<double, double> func, Func<double, double> deriv, double a, double b, double eps):
            base(func, deriv, a, b, eps)
        {
            phi = 1.0 / derivative(x0);
        }

        protected override double Phi(double x)
        {
            return phi;
        }
    }
}
