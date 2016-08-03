using System;

namespace ExpressionEvaluator.Operators
{
    sealed class FunctionLookup : Lookup<Function>
    {
        public static readonly FunctionLookup Instance = new FunctionLookup(new LookupDictionary<Function>());

        FunctionLookup(LookupDictionary<Function> lookup)
            : base(lookup)
        {
            lookup.Add("abs", new Function("abs", x => Math.Abs(x)));
            lookup.Add("ceiling", new Function("ceiling", x => Math.Ceiling(x)));
            lookup.Add("cos", new Function("cos", x => Math.Cos(x)));
            lookup.Add("exp", new Function("exp", x => Math.Exp(x)));
            lookup.Add("floor", new Function("floor", x => Math.Floor(x)));
            lookup.Add("log", new Function("log", x => Math.Log10(x)));
            lookup.Add("sin", new Function("sin", x => Math.Sin(x)));
            lookup.Add("sqrt", new Function("sqrt", x => Math.Sqrt(x)));
            lookup.Add("tan", new Function("tan", x => Math.Tan(x)));
            lookup.Add("truncate", new Function("truncate", x => Math.Truncate(x)));
        }
    }
}
