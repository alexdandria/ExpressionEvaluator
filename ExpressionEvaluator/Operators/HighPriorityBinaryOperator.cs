using System;

namespace ExpressionEvaluator.Operators
{
    sealed class HighPriorityBinaryOperator : BinaryOperator
    {
        public HighPriorityBinaryOperator(string symbol, Func<double, double, double> compute)
            : base(symbol, compute) { }
    }
}
