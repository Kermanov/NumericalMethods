using System;
using org.mariuszgromada.math.mxparser;

namespace Common
{
    public static class ExpressionParser
    {
        public static Func<double, double> GetFunction(string stringExpr)
        {
            var expr = new Expression(stringExpr);
            return x =>
            {
                expr.addArguments(new Argument("x", x));
                return expr.calculate();
            };
        }
    }
}
