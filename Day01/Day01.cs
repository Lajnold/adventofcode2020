using System;
using System.IO;
using System.Linq;

var lines = File.ReadLines("day01.txt");
var numbers = lines.Select(int.Parse).OrderBy(x => x).ToList();

void Part1()
{
	for (int i = 0; i < numbers.Count - 1; i++)
	{
		for (int j = numbers.Count - 1; j > i; j--)
		{
			if (numbers[i] + numbers[j] == 2020)
			{
				Console.WriteLine($"Part 1: {numbers[i] * numbers[j]}");
				return;
			}
			else if (numbers[i] + numbers[j] < 2020)
			{
				break;
			}
		}
	}
}

void Part2()
{
	for (int i = 0; i < numbers.Count - 2; i++)
	{
		for (int j = i + 1; j < numbers.Count - 1; j++)
		{
			for (int k = numbers.Count - 1; k > j; k--)
			{
				if (numbers[i] + numbers[j] + numbers[k] == 2020)
				{
					Console.WriteLine($"Part 2: {numbers[i] * numbers[j] * numbers[k]}");
					return;
				}
				else if (numbers[i] + numbers[j] + numbers[k] < 2020)
				{
					break;
				}
			}
		}
	}
}

Part1();
Part2();
