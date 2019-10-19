using System;
using System.Collections.Generic;
using System.Text;

namespace Unit1.IterativeMethods
{
    public class NewtonIterativeMethod: IterativeMethodBase
    {
        public NewtonIterativeMethod(Func<double, double> func, Func<double, double> deriv, double a, double b, double eps):
            base(func, deriv, a, b, eps)
        { }

        protected override double Phi(double x)
        {
            return 1.0 / derivative(x);
        }
    }
}
