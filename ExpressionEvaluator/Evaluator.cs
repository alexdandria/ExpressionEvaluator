using System;
using System.Collections.Generic;
using ExpressionEvaluator.Tokens;

namespace ExpressionEvaluator
{
    public static class Evaluator
    {
        static readonly Dictionary<string, double> _emptyDictionary = new Dictionary<string, double>();

        public static double Eval(
            string expression)
        {
            Guard.NotNull(expression, nameof(expression));

            return Eval(expression, _emptyDictionary);
        }

        public static double Eval(
            string expression,
            Dictionary<string, double> variables)
        {
            Guard.NotNull(expression, nameof(expression));
            Guard.NotNull(variables, nameof(variables));

            var root = Tokenizer.Tokenize(expression, variables);

            root = EvalParenthesis(root);
            root = EvalFunctionsAndOperators(root);

            if (root.Next != null)
            {
                throw new EvaluationException("Invalid expression.");
            }

            return root.As<OperandToken>().Value;
        }

        static Token EvalParenthesis(
            Token root)
        {
            var openingParenthesis = root.FindLast(TokenType.OpeningParenthesis);

            while (openingParenthesis != null)
            {
                var closingParenthesis = openingParenthesis.Find(TokenType.ClosingParenthesis);

                if (closingParenthesis == null)
                {
                    throw new EvaluationException("Missing closing parenthesis.");
                }

                var previous = openingParenthesis.TruncatePrevious();
                var next = closingParenthesis.TruncateNext();

                openingParenthesis = openingParenthesis.TruncateNext();
                closingParenthesis = closingParenthesis.TruncatePrevious();

                var token = EvalFunctionsAndOperators(openingParenthesis);
                var value = token.As<OperandToken>().Value;

                root = new OperandToken(value);

                root.ReplacePrevious(previous);
                root.ReplaceNext(next);

                openingParenthesis = root.Find(TokenType.OpeningParenthesis, true);
            }

            return root.FindRoot();
        }

        static Token EvalFunctionsAndOperators(
            Token root)
        {
            root = EvalUnaryOperators(root);
            root = EvalFunctions(root);
            root = EvalBinaryOperators(root, x => x.FindHighPriorityBinaryOperator());
            root = EvalBinaryOperators(root, x => x.FindMidPriorityBinaryOperator());
            root = EvalBinaryOperators(root, x => x.FindLowPriorityBinaryOperator());

            return root;
        }

        static Token EvalFunctions(
            Token root)
        {
            while (true)
            {
                var function = root.FindFunction();

                if (function == null)
                {
                    return root.FindRoot();
                }

                if (function.Previous == null || function.Previous.Type != TokenType.Operand)
                {
                    if (function.Next == null || function.Next.Type != TokenType.Operand)
                    {
                        throw new EvaluationException(Inv($"Missing operand for function \"{function.Function.Symbol}\"."));
                    }

                    var operand = function.Next.As<OperandToken>().Value;
                    var computed = function.Function.Compute(operand);

                    root = new OperandToken(computed);

                    root.ReplacePrevious(function.Previous);
                    root.ReplaceNext(function.Next.Next);
                }
                else
                {
                    if (function.Next == null)
                    {
                        return function.FindRoot();
                    }

                    root = function.Next;
                }
            }
        }

        static Token EvalUnaryOperators(
            Token root)
        {
            while (true)
            {
                var unaryOperator = root.FindUnaryOperator();

                if (unaryOperator == null)
                {
                    return root.FindRoot();
                }

                if (unaryOperator.Previous == null || unaryOperator.Previous.Type != TokenType.Operand)
                {
                    if (unaryOperator.Next == null || unaryOperator.Next.Type != TokenType.Operand)
                    {
                        throw new EvaluationException(Inv($"Missing operand for unary operator \"{unaryOperator.Operator.Symbol}\"."));
                    }

                    var operand = unaryOperator.Next.As<OperandToken>().Value;
                    var computed = unaryOperator.Operator.Compute(operand);

                    root = new OperandToken(computed);

                    root.ReplacePrevious(unaryOperator.Previous);
                    root.ReplaceNext(unaryOperator.Next.Next);
                }
                else
                {
                    if (unaryOperator.Next == null)
                    {
                        return unaryOperator.FindRoot();
                    }

                    root = unaryOperator.Next;
                }
            }
        }

        static Token EvalBinaryOperators(
            Token root,
            Func<Token, BinaryOperatorToken> selector)
        {
            while (true)
            {
                var binaryOperator = selector(root);

                if (binaryOperator == null)
                {
                    return root.FindRoot();
                }

                if (binaryOperator.Previous == null || binaryOperator.Previous.Type != TokenType.Operand)
                {
                    throw new EvaluationException(Inv($"Missing left operand for binary operator \"{binaryOperator.Operator.Symbol}\"."));
                }

                if (binaryOperator.Next == null || binaryOperator.Next.Type != TokenType.Operand)
                {
                    throw new EvaluationException(Inv($"Missing right operand for binary operator \"{binaryOperator.Operator.Symbol}\"."));
                }

                var leftOperand = binaryOperator.Previous.As<OperandToken>().Value;
                var rightOperand = binaryOperator.Next.As<OperandToken>().Value;
                var computed = binaryOperator.Operator.Compute(leftOperand, rightOperand);

                root = new OperandToken(computed);

                root.ReplacePrevious(binaryOperator.Previous.Previous);
                root.ReplaceNext(binaryOperator.Next.Next);
            }
        }

        public static string Inv(FormattableString formattable) => FormattableString.Invariant(formattable);
    }
}
