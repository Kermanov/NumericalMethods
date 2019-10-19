using System;
using System.Collections.Generic;
using System.Text;

namespace Unit1.IterativeMethods
{
    public class ChordIterativeMethod: IterativeMethodBase
    {
        public ChordIterativeMethod(Func<double, double> function, double a, double b, double eps)
        {
            this.function = function;
            this.a = a;
            this.b = b;
            this.eps = eps;
            this.x0 = a;
        }

        protected override double Phi(double x)
        {
            return (x - b) / (function(x) - function(b));
        }
    }
}
