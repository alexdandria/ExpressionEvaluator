namespace ExpressionEvaluator.Operators
{
    sealed class LowPriorityBinaryOperatorLookup : Lookup<LowPriorityBinaryOperator>
    {
        public static readonly LowPriorityBinaryOperatorLookup Instance = new LowPriorityBinaryOperatorLookup(new LookupDictionary<LowPriorityBinaryOperator>());

        LowPriorityBinaryOperatorLookup(LookupDictionary<LowPriorityBinaryOperator> lookup)
            : base(lookup)
        {
            lookup.Add("+", new LowPriorityBinaryOperator("+", (x, y) => x + y));
            lookup.Add("-", new LowPriorityBinaryOperator("-", (x, y) => x - y));
        }
    }
}
