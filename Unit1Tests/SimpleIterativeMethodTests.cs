using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unit1.IterativeMethods;

namespace Unit1Tests
{
    [TestClass]
    public class SimpleIterativeMethodTests
    {
        [TestMethod]
        public void LinearTest()
        {
            double func(double x) => 3 * x - 2;
            double deriv(double x) => 3;
            double a = 1;
            double b = 10;
            double eps = 0.00001;

            var simpleIterMethod = new SimpleIterativeMethod(func, deriv, a, b, eps);
            double root = simpleIterMethod.Calculate();

            Assert.AreEqual(0.6667, Math.Round(root, 4));
        }

        [TestMethod]
        public void QuadraticTest()
        {
            double func(double x) => 2 * x * x - 4;
            double deriv(double x) => 4 * x;
            double a = 0;
            double b = 5;
            double eps = 0.00001;

            var simpleIterMethod = new SimpleIterativeMethod(func, deriv, a, b, eps);
            double root = simpleIterMethod.Calculate();

            Assert.AreEqual(1.4142, Math.Round(root, 4));
        }
    }
}
