using ExpressionEvaluator.Operators;

namespace ExpressionEvaluator.Tokens
{
    abstract class BinaryOperatorToken : Token
    {
        public BinaryOperator Operator { get; }

        protected BinaryOperatorToken(BinaryOperator @operator)
        {
            Operator = @operator;
        }
    }
}
