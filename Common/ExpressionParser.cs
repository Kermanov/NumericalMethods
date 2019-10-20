using System;
using org.mariuszgromada.math.mxparser;

namespace Common
{
    public static class ExpressionParser
    {
        public static Func<double, double> GetFunction(string stringExpr)
        {
            var expression = new Expression(stringExpr);
            expression.addArguments(new Argument("x"));

            return x =>
            {
                expression.setArgumentValue("x", x);
                return expression.calculate();
            };
        }
    }
}
