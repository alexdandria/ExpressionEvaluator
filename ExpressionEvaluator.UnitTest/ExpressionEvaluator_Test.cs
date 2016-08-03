using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionEvaluator
{
    [TestClass]
    public class ExpressionEvaluator_Test
    {
        [TestMethod]
        public void TestMethod_EX()
        {
            try
            {
                // bad operand
                Evaluator.Eval("1..0");
                Assert.Fail("Exception expected.");
            }
            catch (EvaluationException ex) { }
        }

        [TestMethod]
        public void TestMethod1()
        {
            Test(128.0 + 256.0, "128.0 + 256.0");
            Test(128.0 - 256.0, "128.0 - 256.0");
            Test(128.0 * 256.0, "128.0 * 256.0");
            Test(128.0 / 256.0, "128.0 / 256.0");
            Test(128.0 % 256.0, "128.0 % 256.0");
            Test(Math.Pow(128.0, 256.0), "128.0 ^ 256.0");
            Test(1.0 + 2.0 - 3.0 * 4.0 / 5.0 % Math.Pow(6.0, 7.0), "1.0 + 2.0 - 3.0 * 4.0 / 5.0 % 6.0 ^ 7.0");
            Test(
                128.0 + (256.0 - (512.0 * (1024.0 / (2048.0 % 4096) / 2048.0) * 512.0) - 256.0) + 128.0,
                "128.0 + (256.0 - (512.0 * (1024.0 / (2048.0 % 4096) / 2048.0) * 512.0) - 256.0) + 128.0");
        }

        private static void Test(double expected, string expression)
        {
            Assert.AreEqual(expected, Evaluator.Eval(expression));
        }

        [TestMethod]
        public void TestMethod2()
        {
            var variables = new Dictionary<string, double> { { "x", 128.0 } };
            var result = 0.0;

            result = Evaluator.Eval("x + 256.0", variables);

            Assert.AreEqual(128.0 + 256.0, result);

            result = Evaluator.Eval("x - 256.0", variables);

            Assert.AreEqual(128.0 - 256.0, result);

            result = Evaluator.Eval("x * 256.0", variables);

            Assert.AreEqual(128.0 * 256.0, result);

            result = Evaluator.Eval("x / 256.0", variables);

            Assert.AreEqual(128.0 / 256.0, result);

            result = Evaluator.Eval("x % 256.0", variables);

            Assert.AreEqual(128.0 % 256.0, result);

            result = Evaluator.Eval("x ^ 256.0", variables);

            Assert.AreEqual(Math.Pow(128.0, 256.0), result);

            result = Evaluator.Eval("x + (256.0 - (512.0 * (1024.0 / (2048.0 % 4096) / 2048.0) * 512.0) - 256.0) + x", variables);

            Assert.AreEqual(128.0 + (256.0 - (512.0 * (1024.0 / (2048.0 % 4096) / 2048.0) * 512.0) - 256.0) + 128.0, result);
        }
    }
}
