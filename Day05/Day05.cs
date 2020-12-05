using System;
using System.IO;
using System.Linq;

var boardingPasses = File.ReadAllLines("day05.txt");

(int Row, int Column) ParseRowColumn(string pass)
{
	int HalfSpan(int min, int max) => (int)((max - min) / 2.0 + 0.5);
	int rowMin = 0, rowMax = 127, colMin = 0, colMax = 7;

	foreach (var c in pass)
	{
		switch (c)
		{
			case 'F':
				rowMax -= HalfSpan(rowMin, rowMax);
				break;
			case 'B':
				rowMin += HalfSpan(rowMin, rowMax);
				break;
			case 'L':
				colMax -= HalfSpan(colMin, colMax);
				break;
			case 'R':
				colMin += HalfSpan(colMin, colMax);
				break;
		}
	}

	return (rowMin, colMin);
}

void Part1()
{
	var seatIds = boardingPasses.Select(ParseRowColumn).Select(rc => rc.Row * 8 + rc.Column).ToList();
	Console.WriteLine($"Part 1: {seatIds.Max()}");
}

void Part2()
{
	var seatIds = boardingPasses.Select(ParseRowColumn).Select(rc => rc.Row * 8 + rc.Column).OrderBy(id => id).ToList();
	for (int i = 0; i < seatIds.Count; i++)
	{
		if (i > 0 && seatIds[i] > 8 && seatIds[i] == seatIds[i - 1] + 2)
		{
			Console.WriteLine($"Part 2: {seatIds[i] - 1}");
			return;
		}
	}
}

Part1();
Part2();
