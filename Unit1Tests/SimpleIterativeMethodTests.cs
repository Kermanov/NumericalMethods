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
            Func<double, double> func = x => 3 * x - 2;
            double a = 1;
            double b = 10;
            double eps = 0.0001;

            var simpleIterMethod = new SimpleIterativeMethod(func, a, b, eps);
            double root = simpleIterMethod.Calculate();

            Assert.AreEqual(0.6666, root, eps);
        }
    }
}
