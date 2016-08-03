using ExpressionEvaluator.Operators;

namespace ExpressionEvaluator.Tokens
{
    sealed class LowPriorityBinaryOperatorToken : BinaryOperatorToken
    {
        public override TokenType Type => TokenType.LowPriorityBinaryOperator;

        public LowPriorityBinaryOperatorToken(BinaryOperator @operator)
            : base(@operator) { }
    }
}
