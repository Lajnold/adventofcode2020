using System;
using System.IO;

var lines = File.ReadAllLines("day03.txt");

int CountTreesInSlope(int right, int down)
{
	int x = 0, y = 0;
	int hits = 0;
	while (y + down < lines.Length)
	{
		x = (x + right) % lines[0].Length;
		y += down;
		if (lines[y][x] == '#') hits++;
	}

	return hits;
}

void Part1()
{
	Console.WriteLine($"Part 1: {CountTreesInSlope(3, 1)}");
}

void Part2()
{
	var hits = CountTreesInSlope(1, 1)
		* CountTreesInSlope(3, 1)
		* CountTreesInSlope(5, 1)
		* CountTreesInSlope(7, 1)
		* CountTreesInSlope(1, 2);

	Console.WriteLine($"Part 2: {hits}");
}

Part1();
Part2();
