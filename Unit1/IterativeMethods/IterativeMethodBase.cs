using System;
using System.Collections.Generic;
using System.Text;
using Unit1.Interfaces;

namespace Unit1.IterativeMethods
{
    public abstract class IterativeMethodBase : IIterativeMethod
    {
        protected Func<double, double> function;
        protected Func<double, double> derivative;
        protected double a;
        protected double b;
        protected double eps;
        protected double x0;

        public List<double> Roots { get; private set; } = new List<double>();

        virtual protected double Phi(double x)
        {
            return 1;
        }

        public IterativeMethodBase()
        {
            function = null;
            derivative = null;
            a = 0;
            b = 0;
            eps = 0;
            x0 = 0;
        }

        public IterativeMethodBase(Func<double, double> func, Func<double, double> deriv, double a, double b, double eps)
        {
            function = func;
            derivative = deriv;
            this.a = a;
            this.b = b;
            this.eps = eps;
            x0 = b;
        }

        public double Calculate()
        {
            Roots.Add(x0);
            int i = 0;
            do
            {
                var nextRoot = Roots[i] - Phi(Roots[i]) * function(Roots[i]);
                Roots.Add(nextRoot);
                ++i;
            }
            while (Math.Abs(Roots[i] - Roots[i - 1]) > eps);

            return Roots[i];
        }
    }
}
