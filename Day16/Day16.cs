using System;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("day16.txt");
var rules = lines.TakeWhile(x => x != "").Select(Rule.Parse).ToArray();
var myTicket = lines.SkipWhile(x => x != "your ticket:").Skip(1).First()
    .Split(',').Select(int.Parse).ToArray();
var otherTickets = lines.SkipWhile(x => x != "nearby tickets:").Skip(1)
    .Select(x => x.Split(',').Select(int.Parse).ToArray())
    .ToArray();

Part1();
Part2();


void Part1()
{
    var invalidSum = otherTickets.SelectMany(t => GetInvalidFields(t, rules)).Sum();
    Console.WriteLine($"Part 1: {invalidSum}");
}

void Part2()
{
    var validOtherTickets = otherTickets.Where(t => !GetInvalidFields(t, rules).Any()).ToArray();
    var numFields = validOtherTickets[0].Length;
    
    var validRulesPerField = validOtherTickets[0].Select(_ => rules.ToList()).ToArray();

    foreach (var ticket in validOtherTickets)
    {
        for (int i = 0; i < numFields; i++)
        {
            validRulesPerField[i].RemoveAll(r => !r.IsValidValue(ticket[i]));
        }
    }

    while (validRulesPerField.Any(rs => rs.Count > 1))
    {
        foreach (var rulesForField in validRulesPerField)
        {
            rulesForField.RemoveAll(r => validRulesPerField.Where(x => x != rulesForField).Any(x => x.Count == 1 && x[0] == r));
        }
    }

    var departureIndices = validRulesPerField
        .Select((r, i) => (Rule: r[0], FieldIndex: i))
        .Where(ri => ri.Rule.Name.StartsWith("departure"))
        .Select(ri => ri.FieldIndex)
        .ToArray();

    var departureProduct = departureIndices.Select(i => myTicket[i]).Aggregate(1L, (acc, val) => acc * val);
    Console.WriteLine($"Part 2: {departureProduct}");
}

int[] GetInvalidFields(int[] ticketFields, Rule[] rules)
{
    return ticketFields.Where(f => !rules.Any(r => r.IsValidValue(f))).ToArray();
}


class Rule
{
    public string Name { get; }
    public Interval Low { get; }
    public Interval High { get; }

    public Rule(string name, Interval low, Interval high)
    {
        Name = name;
        Low = low;
        High = high;
    }

    public static Rule Parse(string ruleString)
    {
        // departure location: 30-828 or 839-971
        var nameEnd = ruleString.IndexOf(':');
        var name = ruleString.Substring(0, nameEnd);

        var split = ruleString.Split(' ');
        var low = Interval.Parse(split[split.Length - 3]);
        var high = Interval.Parse(split[split.Length - 1]);

        return new Rule(name, low, high);
    }

    public bool IsValidValue(int value)
    {
        return Low.Contains(value) || High.Contains(value);
    }

    public override string ToString()
    {
        return $"{Name}: {Low} or {High}";
    }
}

class Interval
{
    public int Min { get; }
    public int Max { get; }

    public Interval(int min, int max)
    {
        Min = min;
        Max = max;
    }

    public static Interval Parse(string intervalString)
    {
        // 44-75
        var split = intervalString.Split('-');
        var min = int.Parse(split[0]);
        var max = int.Parse(split[1]);
        return new Interval(min, max);
    }

    public bool Contains(int value)
    {
        return Min <= value && value <= Max;
    }

    public override string ToString()
    {
        return $"{Min}-{Max}";
    }
}
