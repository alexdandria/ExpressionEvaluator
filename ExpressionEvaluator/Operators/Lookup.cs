namespace ExpressionEvaluator.Operators
{
    abstract class Lookup<T> where T : Operator
    {
        readonly LookupDictionary<T> _lookup;

        protected Lookup(LookupDictionary<T> lookup)
        {
            _lookup = lookup;
        }

        public bool Contains(string symbol) => _lookup.ContainsKey(symbol);

        public T Get(string symbol) => _lookup[symbol];
    }
}
