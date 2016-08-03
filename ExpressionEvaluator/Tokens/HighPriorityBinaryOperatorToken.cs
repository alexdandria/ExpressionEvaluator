using ExpressionEvaluator.Operators;

namespace ExpressionEvaluator.Tokens
{
    sealed class HighPriorityBinaryOperatorToken : BinaryOperatorToken
    {
        public override TokenType Type => TokenType.HighPriorityBinaryOperator;

        public HighPriorityBinaryOperatorToken(BinaryOperator @operator)
            : base(@operator) { }
    }
}
