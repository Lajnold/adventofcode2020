using System;
using System.IO;
using System.Linq;

var numbers = File.ReadLines("day09.txt").Select(long.Parse).ToList();

long FindFirstInvalid()
{
	int start = 0;
	while (start < numbers.Count - 25)
	{
		bool found = false;
		for (int i = start; i < start + 25 && !found; i++)
		{
			for (int j = i + 1; j < start + 25 && !found; j++)
			{
				if (numbers[i] + numbers[j] == numbers[start + 25])
				{
					found = true;
				}
			}
		}

		if (!found)
		{
			return numbers[start + 25];
		}

		start++;
	}

	throw new Exception("What???");
}

void Part1()
{
	Console.WriteLine($"Part 1: {FindFirstInvalid()}");
}

(long, long) FindContiguousMinMaxWithSum(long target)
{
	for (int i = 0; i < numbers.Count - 1; i++)
	{
		long sum = numbers[i], min = numbers[i], max = numbers[i];
		for (int j = i + 1; j < numbers.Count && sum < target; j++)
		{
			sum += numbers[j];
			min = Math.Min(min, numbers[j]);
			max = Math.Max(max, numbers[j]);
			if (sum == target)
			{
				return (min, max);
			}
		}
	}

	throw new Exception("What???");
}

void Part2()
{
	var invalid = FindFirstInvalid();
	var (min, max) = FindContiguousMinMaxWithSum(invalid);
	Console.WriteLine($"Part 2: {min + max}");
}

Part1();
Part2();
