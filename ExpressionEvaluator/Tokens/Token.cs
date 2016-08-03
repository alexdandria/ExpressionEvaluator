namespace ExpressionEvaluator.Tokens
{
    abstract class Token
    {
        public abstract TokenType Type { get; }

        public Token Previous { get; private set; }

        public Token Next { get; private set; }

        public override string ToString()
        {
            return Type.ToString();
        }

        public Token FindRoot()
        {
            var token = this;

            while (token.Previous != null)
            {
                token = token.Previous;
            }

            return token;
        }

        public Token ReplacePrevious(Token token)
        {
            TruncatePrevious();

            Previous = token;

            if (token != null)
            {
                token.Next = this;
            }

            return token;
        }

        public Token TruncatePrevious()
        {
            if (Previous == null)
            {
                return null;
            }

            var token = Previous;

            token.Next = null;
            Previous = null;

            return token;
        }

        public Token ReplaceNext(Token token)
        {
            TruncateNext();

            Next = token;

            if (token != null)
            {
                token.Previous = this;
            }

            return token;
        }

        public Token TruncateNext()
        {
            if (Next == null)
            {
                return null;
            }

            var token = Next;

            token.Previous = null;
            Next = null;

            return token;
        }

        public UnaryOperatorToken FindUnaryOperator(bool backward = false) => Find(TokenType.UnaryOperator, backward).As<UnaryOperatorToken>();

        public FunctionToken FindFunction(bool backward = false) => Find(TokenType.Function, backward).As<FunctionToken>();

        public HighPriorityBinaryOperatorToken FindHighPriorityBinaryOperator(bool backward = false) => Find(TokenType.HighPriorityBinaryOperator, backward).As<HighPriorityBinaryOperatorToken>();

        public MidPriorityBinaryOperatorToken FindMidPriorityBinaryOperator(bool backward = false) => Find(TokenType.MidPriorityBinaryOperator, backward).As<MidPriorityBinaryOperatorToken>();

        public LowPriorityBinaryOperatorToken FindLowPriorityBinaryOperator(bool backward = false) => Find(TokenType.LowPriorityBinaryOperator, backward).As<LowPriorityBinaryOperatorToken>();

        public Token Find(TokenType type, bool backward = false)
        {
            var token = this;

            while (token != null)
            {
                if (token.Type == type)
                {
                    return token;
                }

                token = backward ? token.Previous : token.Next;
            }

            return null;
        }

        public Token FindLast(TokenType type, bool backward = false)
        {
            Token found = null;
            var token = this;

            while (token != null)
            {
                if (token.Type == type)
                {
                    found = token;
                }

                token = backward ? token.Previous : token.Next;
            }

            return found;
        }
    }
}
