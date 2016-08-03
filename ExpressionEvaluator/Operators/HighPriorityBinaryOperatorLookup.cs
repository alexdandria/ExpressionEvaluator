using System;

namespace ExpressionEvaluator.Operators
{
    sealed class HighPriorityBinaryOperatorLookup : Lookup<HighPriorityBinaryOperator>
    {
        public static readonly HighPriorityBinaryOperatorLookup Instance = new HighPriorityBinaryOperatorLookup(new LookupDictionary<HighPriorityBinaryOperator>());

        HighPriorityBinaryOperatorLookup(LookupDictionary<HighPriorityBinaryOperator> lookup)
            : base(lookup)
        {
            lookup.Add("^", new HighPriorityBinaryOperator("^", (x, y) => Math.Pow(x, y)));
        }
    }
}
