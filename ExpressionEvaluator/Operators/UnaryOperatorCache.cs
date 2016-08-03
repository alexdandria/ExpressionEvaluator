using System.Collections.Generic;

namespace ExpressionEvaluator.Operators
{
    static class UnaryOperatorCache
    {
        static readonly Dictionary<string, UnaryOperator> _cache = CreateCache();

        static Dictionary<string, UnaryOperator> CreateCache()
        {
            var cache = new Dictionary<string, UnaryOperator>();

            cache.Add("+", new UnaryOperator("+", x => +x));
            cache.Add("-", new UnaryOperator("-", x => -x));

            return cache;
        }

        public static bool Contains(string symbol) => _cache.ContainsKey(symbol);

        public static UnaryOperator Get(string symbol) => _cache[symbol];
    }
}
