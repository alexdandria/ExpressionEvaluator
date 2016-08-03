namespace ExpressionEvaluator.Tokens
{
    sealed class OperandToken : Token
    {
        public override TokenType Type => TokenType.Operand;

        public double Value { get; }

        public OperandToken(double value)
        {
            Value = value;
        }
    }
}
