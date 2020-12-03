using System;
using System.Linq;
using System.Text.RegularExpressions;

var lines = System.IO.File.ReadLines("day02.txt");

void Part1()
{
	var rg = new Regex(@"^(\d+)-(\d+) (\w): (.*)$");
	var valid = 0;
	foreach (var line in lines)
	{
		var match = rg.Match(line);
		var from = int.Parse(match.Groups[1].Value);
		var to = int.Parse(match.Groups[2].Value);
		var ch = match.Groups[3].Value[0];
		var pass = match.Groups[4].Value;

		var count = pass.Count(x => x == ch);
		if (from <= count && count <= to) valid++;
	}

	Console.WriteLine($"Part 1: {valid}");
}

void Part2()
{
	var rg = new Regex(@"^(\d+)-(\d+) (\w): (.*)$");
	var valid = 0;
	foreach (var line in lines)
	{
		var match = rg.Match(line);
		var first = int.Parse(match.Groups[1].Value);
		var last = int.Parse(match.Groups[2].Value);
		var ch = match.Groups[3].Value[0];
		var pass = match.Groups[4].Value;

		int count = 0;
		if (pass.Length >= first && pass[first - 1] == ch) count++;
		if (pass.Length >= last && pass[last - 1] == ch) count++;

		if (count == 1) valid++;
	}

	Console.WriteLine($"Part 2: {valid}");
}

Part1();
Part2();
