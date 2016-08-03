using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpressionEvaluator.Operators;
using ExpressionEvaluator.Tokens;

namespace ExpressionEvaluator
{
    sealed class Tokenizer
    {
        public static Token Tokenize(
            string expression,
            Dictionary<string, double> variables) => new Tokenizer(expression, variables).Tokenize();

        readonly StringBuilder _buffer = new StringBuilder();
        readonly char[] _expression;
        readonly Dictionary<string, double> _variables;
        Token _root;
        int _index;
        char _char;

        Tokenizer(
            string expression,
            Dictionary<string, double> variables)
        {
            _expression = expression.ToCharArray();
            _variables = variables;
        }

        Token Tokenize()
        {
            for (; _index < _expression.Length; _index++)
            {
                _char = _expression[_index];

                var token = TokenizeChar();

                if (token != null)
                {
                    _root = _root == null ? token : _root.ReplaceNext(token);
                }
            }

            return _root.FindRoot();
        }

        Token TokenizeChar()
        {
            if (char.IsDigit(_char) || _char == '.')
            {
                return TokenizeOperand();
            }

            if (char.IsLetter(_char))
            {
                return TokenizeFunctionOrVariable();
            }

            if (Globals.Operators.Contains(_char))
            {
                return TokenizeOperator();
            }

            if (_char == '(')
            {
                return new OpeningParenthesisToken();
            }

            if (_char == ')')
            {
                return new ClosingParenthesisToken();
            }

            if (_char != ' ')
            {
                throw new EvaluationException(Inv($"Unrecognized character \"{_char}\"."));
            }

            return null;
        }

        Token TokenizeOperand()
        {
            do
            {
                _buffer.Append(_char);
            }
            while (++_index < _expression.Length && (char.IsDigit(_char = _expression[_index]) || _char == '.'));

            _index--;

            var s = _buffer.Flush();
            var value = s.ToInvariantDoubleOrNull();

            if (value == null)
            {
                throw new EvaluationException(Inv($"Invalid operand \"{_char}\"."));
            }

            return new OperandToken(value.Value);
        }

        Token TokenizeFunctionOrVariable()
        {
            do
            {
                _buffer.Append(_char);
            }
            while (++_index < _expression.Length && char.IsLetter(_char = _expression[_index]));

            _index--;

            while (++_index < _expression.Length && (_char = _expression[_index]) == ' ') { }

            _index--;

            var value = _buffer.Flush();

            if (_char == '(')
            {
                var function = FunctionLookup.Instance.Get(value);

                if (function == null)
                {
                    throw new EvaluationException("");
                }

                return new FunctionToken(function);
            }

            if (!_variables.ContainsKey(value))
            {
                throw new EvaluationException("");
            }

            return new OperandToken(_variables[value]);
        }

        Token TokenizeOperator()
        {
            var symbol = _char.ToString();

            if (UnaryOperatorCache.Contains(symbol) && (_root == null || (_root.Type != TokenType.Operand && _root.Type != TokenType.ClosingParenthesis)))
            {
                return new UnaryOperatorToken(UnaryOperatorCache.Get(symbol));
            }

            if (HighPriorityBinaryOperatorLookup.Instance.Contains(symbol))
            {
                return new HighPriorityBinaryOperatorToken(HighPriorityBinaryOperatorLookup.Instance.Get(symbol));
            }

            if (MidPriorityBinaryOperatorLookup.Instance.Contains(symbol))
            {
                return new MidPriorityBinaryOperatorToken(MidPriorityBinaryOperatorLookup.Instance.Get(symbol));
            }

            if (LowPriorityBinaryOperatorLookup.Instance.Contains(symbol))
            {
                return new LowPriorityBinaryOperatorToken(LowPriorityBinaryOperatorLookup.Instance.Get(symbol));
            }

            return null;
        }

        public static string Inv(FormattableString formattable) => FormattableString.Invariant(formattable);
    }
}
