using System;

namespace ExpressionEvaluator.Operators
{
    sealed class Function : Operator
    {
        readonly Func<double, double> _compute;

        public Function(string symbol, Func<double, double> compute)
            : base(symbol)
        {
            _compute = compute;
        }

        public double Compute(double operand) => _compute(operand);
    }
}
