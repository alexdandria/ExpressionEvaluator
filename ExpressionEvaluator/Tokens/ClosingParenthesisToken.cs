namespace ExpressionEvaluator.Tokens
{
    sealed class ClosingParenthesisToken : Token
    {
        public override TokenType Type => TokenType.ClosingParenthesis;
    }
}
