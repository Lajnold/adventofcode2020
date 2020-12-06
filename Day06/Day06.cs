using System;
using System.IO;
using System.Linq;

var groups = File.ReadAllText("day06.txt")
	.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
	.Select(g => g.Split("\n", StringSplitOptions.RemoveEmptyEntries))
	.ToList();

void Part1()
{
	var sum = 0;
	foreach (var group in groups)
	{
		sum += group.SelectMany(g => g).Distinct().Count();
	}
	Console.WriteLine($"Part 1: {sum}");
}

void Part2()
{
	var sum = 0;
	foreach (var group in groups)
	{
		sum += group.SelectMany(g => g).Distinct().Count(c => group.All(g => g.Contains(c)));
	}
	Console.WriteLine($"Part 2: {sum}");
}

Part1();
Part2();
