using System;
using org.mariuszgromada.math.mxparser;

namespace Common
{
    public static class ExpressionParser
    {
        public static Func<double, double> GetFunction(string stringExpr)
        {
            if (!stringExpr.Contains("x"))
            {
                throw new ArgumentException("Expression must contains variable x");
            }

            var expression = new Expression(stringExpr);
            expression.addArguments(new Argument("x"));

            if (!expression.checkSyntax())
            {
                throw new ArgumentException("Invalid expression");
            }

            return x =>
            {
                expression.setArgumentValue("x", x);
                return expression.calculate();
            };
        }
    }
}
