using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionEvaluator
{
    [TestClass]
    public class ExpressionEvaluator_Test
    {
        [TestMethod]
        public void Throw_when_operand_is_invalid()
        {
            try
            {
                Evaluator.Eval("1..0");
                Assert.Fail("Exception expected.");
            }
            catch (EvaluationException ex)
            {
                Assert.AreEqual(
                    "Invalid operand \"1..0\".",
                    ex.Message);
            }
        }

        [TestMethod]
        public void Throw_when_missing_operand_for_unary_operator()
        {
            try
            {
                Evaluator.Eval("+");
                Assert.Fail("Exception expected.");
            }
            catch (EvaluationException ex)
            {
                Assert.AreEqual(
                    "Missing operand for unary operator \"+\".",
                    ex.Message);
            }
        }

        [TestMethod]
        public void Throw_when_missing_right_operand_for_binary_operator()
        {
            try
            {
                Evaluator.Eval("1.0 +");
                Assert.Fail("Exception expected.");
            }
            catch (EvaluationException ex)
            {
                Assert.AreEqual(
                    "Missing right operand for binary operator \"+\".",
                    ex.Message);
            }
        }

        [TestMethod]
        public void Throw_when_missing_left_operand_for_binary_operator()
        {
            try
            {
                // TODO
                Evaluator.Eval("+ + 1.0");
                Assert.Fail("Exception expected.");
            }
            catch (EvaluationException ex)
            {
                Assert.AreEqual(
                    "Missing right operand for binary operator \"+\".",
                    ex.Message);
            }
        }

        [TestMethod]
        public void Throw_when_missing_closing_parenthesis()
        {
            try
            {
                // TODO
                Evaluator.Eval("(1.0");
                Assert.Fail("Exception expected.");
            }
            catch (EvaluationException ex)
            {
                Assert.AreEqual(
                    "Missing closing parenthesis.",
                    ex.Message);
            }
        }

        [TestMethod]
        public void Throw_when_missing_opening_parenthesis()
        {
            try
            {
                // TODO
                Evaluator.Eval("1.0)");
                Assert.Fail("Exception expected.");
            }
            catch (EvaluationException ex)
            {
                Assert.AreEqual(
                    "Missing closing parenthesis.",
                    ex.Message);
            }
        }

        [TestMethod]
        public void When_expression_is_valid()
        {
            Test(128.0, "128.0");
            Test(128.0, "(128.0)");
            Test(128.0 + 256.0, "128.0 + 256.0");
            Test(128.0 - 256.0, "128.0 - 256.0");
            Test(128.0 * 256.0, "128.0 * 256.0");
            Test(128.0 / 256.0, "128.0 / 256.0");
            Test(128.0 % 256.0, "128.0 % 256.0");
            Test(Math.Pow(128.0, 256.0), "128.0 ^ 256.0");
            Test(1.0 + 2.0 - 3.0 * 4.0 / 5.0 % Math.Pow(6.0, 7.0), "1.0 + 2.0 - 3.0 * 4.0 / 5.0 % 6.0 ^ 7.0");
            Test(128.0 + 256.0, "(128.0 + 256.0)");
            Test(
                Math.Pow(((((1.0 + 2.0) - 3.0) * 4.0) / 5.0) % 6.0, 7.0),
                "(((((1.0 + 2.0) - 3.0) * 4.0) / 5.0) % 6.0) ^ 7.0");
            Test(
                128.0 + (256.0 - (512.0 * (1024.0 / (2048.0 % 4096) / 2048.0) * 512.0) - 256.0) + 128.0,
                "128.0 + (256.0 - (512.0 * (1024.0 / (2048.0 % 4096) / 2048.0) * 512.0) - 256.0) + 128.0");
        }

        private static void Test(double expected, string expression)
        {
            Assert.AreEqual(expected, Evaluator.Eval(expression));
        }
    }
}
