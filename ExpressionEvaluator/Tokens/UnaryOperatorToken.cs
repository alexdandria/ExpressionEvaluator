using ExpressionEvaluator.Operators;

namespace ExpressionEvaluator.Tokens
{
    sealed class UnaryOperatorToken : Token
    {
        public override TokenType Type => TokenType.UnaryOperator;

        public UnaryOperator Operator { get; }

        public UnaryOperatorToken(UnaryOperator @operator)
        {
            Operator = @operator;
        }
    }
}
