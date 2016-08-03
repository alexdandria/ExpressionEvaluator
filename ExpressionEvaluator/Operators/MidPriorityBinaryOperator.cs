using System;

namespace ExpressionEvaluator.Operators
{
    sealed class MidPriorityBinaryOperator : BinaryOperator
    {
        public MidPriorityBinaryOperator(string symbol, Func<double, double, double> compute)
            : base(symbol, compute) { }
    }
}
