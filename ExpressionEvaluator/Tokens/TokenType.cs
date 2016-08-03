namespace ExpressionEvaluator.Tokens
{
    enum TokenType
    {
        Operand,
        VariableOperand,
        Function,
        UnaryOperator,
        HighPriorityBinaryOperator,
        MidPriorityBinaryOperator,
        LowPriorityBinaryOperator,
        OpeningParenthesis,
        ClosingParenthesis,
    }
}
