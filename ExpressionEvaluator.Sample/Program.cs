using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExpressionEvaluator
{
    static class Program
    {
        static readonly Dictionary<string, double> _variables = new Dictionary<string, double>();
        static readonly char[] _assignementSeparator = { '=' };
        static readonly char[] _spaceSeparator = { ' ' };

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Ready.");

                    var cmd = Console.ReadLine();

                    cmd = cmd.Trim();

                    ExecuteCmd(cmd);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static CmdResult ExecuteCmd(string cmd)
        {
            if (Compare(cmd, Commands.Exit))
            {
                return ExecuteExit();
            }

            if (cmd.StartsWith(Commands.Set, StringComparison.OrdinalIgnoreCase))
            {
                return ExecuteSet(cmd);
            }

            if (Compare(cmd, Commands.Clear))
            {
                return ExecuteClear();
            }

            if (Compare(cmd, Commands.List))
            {
                return ExecuteList();
            }

            var eval = Evaluator.Eval(cmd, _variables);

            Console.WriteLine(eval);

            return CmdResult.Continue;
        }

        static bool Compare(string cmd, string reservedWord) => string.Equals(cmd, reservedWord, StringComparison.OrdinalIgnoreCase);

        static CmdResult ExecuteExit() => CmdResult.Exit;

        static CmdResult ExecuteSet(string cmd)
        {
            var split = cmd.Split(_assignementSeparator);

            if (split.Length != 2)
            {
                Console.WriteLine("Syntax error.");
            }
            else
            {
                var subsplit = split[0].Split(_spaceSeparator, StringSplitOptions.RemoveEmptyEntries);

                if (subsplit.Length != 2)
                {
                    Console.WriteLine("Syntax error.");
                }

                var name = subsplit[1];

                if (Compare(name, Commands.Exit) ||
                    Compare(name, Commands.Set) ||
                    Compare(name, Commands.Clear) ||
                    Compare(name, Commands.List))
                {
                    Console.WriteLine($"Cannot use reserved word \"{name}\" as variable name.");

                    return CmdResult.Continue;
                }

                var value = double.Parse(split[1], CultureInfo.InvariantCulture);

                _variables[name] = value;
            }

            return CmdResult.Continue;
        }

        static CmdResult ExecuteClear()
        {
            _variables.Clear();

            return CmdResult.Continue;
        }

        static CmdResult ExecuteList()
        {
            foreach (var variable in _variables)
            {
                Console.Write(variable.Key);
                Console.Write(" = ");
                Console.WriteLine(variable.Value);
            }

            return CmdResult.Continue;
        }
    }
}
