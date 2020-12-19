using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RuleDict = System.Collections.Generic.Dictionary<int, Rule>;

Part1();
Part2();

void Part1()
{
    var lines = File.ReadLines("day19.txt");
    RuleDict rules = ParseRules(lines.TakeWhile(x => x != ""));
    var messages = lines.SkipWhile(x => x != "").Skip(1).ToArray();

    var startRule = rules[0];
    var valid = messages.Count(m =>
    {
        var (match, next) = MatchRule(rules, startRule, m, 0);
        return match && next == m.Length;
    });

    Console.WriteLine($"Part 1: {valid}");
}

void Part2()
{

}

(bool Match, int Next) MatchRule(RuleDict rules, Rule rule, string message, int fromIdx)
{
    foreach (var pattern in rule.Patterns)
    {
        var (match, next) = MatchPattern(rules, pattern, message, fromIdx);
        if (match)
        {
            return (match, next);
        }
    }

    return (false, fromIdx);
}

(bool Match, int Next) MatchPattern(RuleDict rules, Pattern pattern, string message, int fromIdx)
{
    int idx = fromIdx;

    foreach (var symbol in pattern.Symbols)
    {
        if (idx >= message.Length)
        {
            return (false, fromIdx);
        }

        if (symbol.Character != null)
        {
            if (message[idx] != symbol.Character.Value)
            {
                return (false, fromIdx);
            }

            idx++;
        }
        else
        {
            var nextRule = rules[symbol.RuleId.Value];
            var (nextRuleMatch, nextIdx) = MatchRule(rules, nextRule, message, idx);
            if (!nextRuleMatch)
            {
                return (false, fromIdx);
            }

            idx = nextIdx;
        }
    }

    return (true, idx);
}

RuleDict ParseRules(IEnumerable<string> strings)
{
    var dict = new RuleDict();

    foreach (var str in strings)
    {
        var (ruleId, rule) = ParseRule(str);
        dict[ruleId] = rule;
    }

    return dict;
}

(int, Rule) ParseRule(string str)
{
    var idSplit = str.Split(':', 2);
    var ruleId = int.Parse(idSplit[0]);

    var patterns = new List<Pattern>();
    var subRulesSplit = idSplit[1].Split('|');
    foreach (var patternStr in subRulesSplit)
    {
        var symbols = new List<Symbol>();
        var symbolsSplit = patternStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var symbolStr in symbolsSplit)
        {
            int? symbolRuleId = null;
            char? symbolCharacter = null;
            if (symbolStr[0] == '"')
            {
                symbolCharacter = symbolStr[1];
            }
            else
            {
                symbolRuleId = int.Parse(symbolStr);
            }

            symbols.Add(new Symbol(symbolRuleId, symbolCharacter));
        }

        patterns.Add(new Pattern(symbols));
    }

    return (ruleId, new Rule(patterns));
}


record Rule(IReadOnlyList<Pattern> Patterns)
{
    public override string ToString() => string.Join(" | ", Patterns);
}

record Pattern(IReadOnlyList<Symbol> Symbols)
{
    public override string ToString() => string.Join(" ", Symbols);
}

record Symbol(int? RuleId, char? Character)
{
    public override string ToString() => RuleId?.ToString() ?? "\"" + Character.Value + "\"";
}
