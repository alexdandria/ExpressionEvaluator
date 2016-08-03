using ExpressionEvaluator.Operators;

namespace ExpressionEvaluator.Tokens
{
    sealed class MidPriorityBinaryOperatorToken : BinaryOperatorToken
    {
        public override TokenType Type => TokenType.MidPriorityBinaryOperator;

        public MidPriorityBinaryOperatorToken(BinaryOperator @operator)
            : base(@operator) { }
    }
}
