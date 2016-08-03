namespace ExpressionEvaluator.Operators
{
    sealed class MidPriorityBinaryOperatorLookup : Lookup<MidPriorityBinaryOperator>
    {
        public static readonly MidPriorityBinaryOperatorLookup Instance = new MidPriorityBinaryOperatorLookup(new LookupDictionary<MidPriorityBinaryOperator>());

        MidPriorityBinaryOperatorLookup(LookupDictionary<MidPriorityBinaryOperator> lookup)
            : base(lookup)
        {
            lookup.Add("*", new MidPriorityBinaryOperator("*", (x, y) => x * y));
            lookup.Add("/", new MidPriorityBinaryOperator("/", (x, y) => x / y));
            lookup.Add("%", new MidPriorityBinaryOperator("%", (x, y) => x % y));
        }
    }
}
