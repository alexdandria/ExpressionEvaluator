using System;

namespace ExpressionEvaluator.Operators
{
    sealed class LowPriorityBinaryOperator : BinaryOperator
    {
        public LowPriorityBinaryOperator(string symbol, Func<double, double, double> compute)
            : base(symbol, compute) { }
    }
}
