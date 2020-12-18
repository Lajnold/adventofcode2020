using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

var lines = File.ReadAllLines("day18.txt");

Part1();
Part2();


void Part1()
{
    var results = lines.Select(EvaluateSamePrecedence).ToList();
    var sum = results.Sum();
    Console.WriteLine($"Part 1: {sum}");
}

void Part2()
{
    var results = lines.Select(EvaluateDifferentPrecedence).ToList();
    var sum = results.Sum();
    Console.WriteLine($"Part 1: {sum}");
}

long EvaluateSamePrecedence(string expr)
{
    var tokens = Tokenize(expr);
    return EvaluateTokensSamePrecedence(tokens, 0).Result;
}

string[] Tokenize(string expr)
{
    var tokens = new List<string>();
    for (var i = 0; i < expr.Length;)
    {
        if (expr[i] == ' ') { i++; continue; }

        var token = new StringBuilder();
        if (char.IsNumber(expr[i]))
        {
            while (i < expr.Length && char.IsNumber(expr[i]))
            {
                token.Append(expr[i]);
                i++;
            }
        }
        else
        {
            token.Append(expr[i]);
            i++;
        }

        tokens.Add(token.ToString());
        token.Clear();
    }

    return tokens.ToArray();
}

(long Result, int Next) EvaluateTokensSamePrecedence(string[] tokens, int idx)
{
    long result = 0;
    var op = Operator.Set;

    while (idx < tokens.Length)
    {
        switch (tokens[idx])
        {
            case "(":
                var (n, next) = EvaluateTokensSamePrecedence(tokens, idx + 1);
                idx = next;

                switch (op)
                {
                    case Operator.Set:
                        result = n;
                        break;
                    case Operator.Add:
                        result += n;
                        break;
                    case Operator.Multiply:
                        result *= n;
                        break;
                }
                break;
            case ")":
                return (result, idx + 1);
            case "+":
                op = Operator.Add;
                idx++;
                break;
            case "*":
                op = Operator.Multiply;
                idx++;
                break;
            default:
                n = long.Parse(tokens[idx]);
                idx++;

                switch (op)
                {
                    case Operator.Set:
                        result = n;
                        break;
                    case Operator.Add:
                        result += n;
                        break;
                    case Operator.Multiply:
                        result *= n;
                        break;
                }
                break;
        }
    }

    return (result, idx);
}

long EvaluateDifferentPrecedence(string expr)
{
    var tokens = Tokenize(expr);
    var wrapped = WrapPrecedence(tokens);
    return EvaluateTokensSamePrecedence(wrapped, 0).Result;
}

string[] WrapPrecedence(string[] tokens)
{
    var wrapped = tokens.ToList();
    for (int i = 0; i < wrapped.Count; i++)
    {
        if (wrapped[i] == "+")
        {
            int before;
            int beforeNesting = 0;
            bool insertedBefore = false;
            for (before = i - 1; before >= 0; before--)
            {
                if (int.TryParse(wrapped[before], out var _))
                {
                    if (beforeNesting == 0)
                    {
                        wrapped.Insert(before, "(");
                        i++;
                        insertedBefore = true;
                        break;
                    }
                }
                else if (wrapped[before] == ")")
                {
                    beforeNesting++;
                }
                else if (wrapped[before] == "(")
                {
                    if (beforeNesting == 0)
                    {
                        break;
                    }
                    else
                    {
                        beforeNesting--;
                        if (beforeNesting == 0)
                        {
                            wrapped.Insert(before, "(");
                            i++;
                            insertedBefore = true;
                            break;
                        }
                    }
                }
            }

            if (before < 0)
            {
                wrapped.Insert(0, "(");
                insertedBefore = true;
            }

            if (insertedBefore)
            {
                int after;
                int afterNesting = 0;
                for (after = i + 1; after < wrapped.Count; after++)
                {
                    if (int.TryParse(wrapped[after], out var _))
                    {
                        if (afterNesting == 0)
                        {
                            wrapped.Insert(after + 1, ")");
                            break;
                        }
                    }
                    else if (wrapped[after] == "(")
                    {
                        afterNesting++;
                    }
                    else if (wrapped[after] == ")")
                    {
                        if (afterNesting == 0)
                        {
                            break;
                        }
                        else
                        {
                            afterNesting--;
                            if (afterNesting == 0)
                            {
                                wrapped.Insert(after + 1, ")");
                                break;
                            }
                        }
                    }
                }

                if (after == wrapped.Count)
                {
                    wrapped.Add(")");
                }
            }
        }
    }

    return wrapped.ToArray();
}

enum Operator
{
    Set,
    Add,
    Multiply
}
