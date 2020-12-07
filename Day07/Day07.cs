using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var lines = File.ReadAllLines("day07.txt");

var regex = new Regex(@"(\w+ \w+) bags contain (((\d+) (\w+ \w+) bags?(?:, )?)+)\.");
var graph = new Dictionary<string, List<(int Num, string Color)>>();
foreach (var line in lines)
{
	var match = regex.Match(line);
	graph[match.Groups[1].Value] = match.Groups[3].Captures
		.Select((x, i) => (int.Parse(match.Groups[4].Captures[i].Value), match.Groups[5].Captures[i].Value))
		.ToList();
}

bool ContainsShinyGold(string color)
{
	var contains = graph.GetValueOrDefault(color);
	return contains != null && (contains.Any(x => x.Color == "shiny gold") || contains.Any(x => ContainsShinyGold(x.Color)));
}

void Part1()
{
	Console.WriteLine($"Part 1: {graph.Keys.Count(ContainsShinyGold)}");
}

int CountDescendants(string color)
{
	var contains = graph.GetValueOrDefault(color);
	return contains?.Sum(x => x.Num + (x.Num * CountDescendants(x.Color))) ?? 0;
}

void Part2()
{
	Console.WriteLine($"Part 2: {CountDescendants("shiny gold")}");
}

Part1();
Part2();
