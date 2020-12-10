using System;
using System.IO;
using System.Linq;

var adapters = File.ReadLines("day10.txt").Select(int.Parse).ToList();

void Part1()
{
	var sorted = adapters.OrderBy(x => x).ToList();
	int diff1 = 0, diff3 = 0;

	if (sorted[0] == 1) diff1++;
	if (sorted[0] == 3) diff3++;

	for (int i = 1; i < sorted.Count; i++)
	{
		if (sorted[i] == sorted[i-1] + 1) diff1++;
		if (sorted[i] == sorted[i-1] + 3) diff3++;
	}

	diff3++;

	Console.WriteLine($"Part 1: {diff1 * diff3}");
}

void Part2()
{
	var sorted = adapters.OrderBy(x => x).ToList();
	var maxAdapter = sorted.Max();
	sorted.Insert(0, 0);
	sorted.Add(maxAdapter + 3);

	var ways = Enumerable.Repeat(0L, sorted.Count).ToList();
	ways[0] = 1;

	for (int i = 1; i < sorted.Count; i++)
	{
		for (int j = i - 1; j >= 0 && sorted[j] >= sorted[i] - 3; j--)
		{
			ways[i] += ways[j];
		}
	}

	Console.WriteLine($"Part 2: {ways.Last()}");
}

Part1();
Part2();
