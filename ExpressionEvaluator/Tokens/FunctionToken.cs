using ExpressionEvaluator.Operators;

namespace ExpressionEvaluator.Tokens
{
    sealed class FunctionToken : Token
    {
        public override TokenType Type => TokenType.Function;

        public Function Function { get; }

        public FunctionToken(Function function)
        {
            Function = function;
        }
    }
}
