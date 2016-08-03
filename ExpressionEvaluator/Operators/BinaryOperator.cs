using System;

namespace ExpressionEvaluator.Operators
{
    abstract class BinaryOperator : Operator
    {
        readonly Func<double, double, double> _compute;

        protected BinaryOperator(string symbol, Func<double, double, double> compute)
            : base(symbol)
        {
            _compute = compute;
        }

        public double Compute(double leftOperand, double rightOperand) => _compute(leftOperand, rightOperand);
    }
}
