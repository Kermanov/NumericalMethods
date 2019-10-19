using System;
using System.Collections.Generic;
using System.Text;

namespace Unit1.IterativeMethods
{
    public class SimpleIterativeMethod : IterativeMethodBase
    {
        readonly double phi;
        public SimpleIterativeMethod(Func<double, double> func, double a, double b, double eps):
            base(func, null, a, b, eps)
        {
            phi = Math.Sign(function(b) - function(a)) * 0.5;
        }

        protected override double Phi(double x)
        {
            return phi;
        }
    }
}
