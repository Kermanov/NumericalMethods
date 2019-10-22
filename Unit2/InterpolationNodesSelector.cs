using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit2
{
    public static class InterpolationNodesSelector
    {
        public static double[] SelectUniform(double a, double b, int n)
        {
            double[] nodes = new double[n];
            for (int i = 0; i < n; ++i)
            {
                nodes[i] = a + (b - a) / (n - 1) * i;
            }

            return nodes;
        }

        public static double[] SelectChebyshev(double a, double b, int n)
        {
            double[] nodes = new double[n];
            for (int i = 0; i < n; ++i)
            {
                nodes[i] = (a + b) / 2 + ((b - a) / 2) * Math.Cos((2 * i + 1) * Math.PI / (2 * (n + 1)));
            }

            return nodes;
        }
    }
}
