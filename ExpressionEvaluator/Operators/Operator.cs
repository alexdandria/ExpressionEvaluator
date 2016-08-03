namespace ExpressionEvaluator.Operators
{
    abstract class Operator
    {
        public string Symbol { get; }

        protected Operator(string symbol)
        {
            Symbol = symbol;
        }

        public override string ToString() => Symbol;
    }
}
