using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var instructions = File.ReadLines("day08.txt")
	.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
	.Select(x => (Instr: x[0], Param: int.Parse(x[1])))
	.ToList();

(bool Completed, int Acc) TryRunUntilCompletion(List<(string Instr, int Param)> instructions)
{
	int acc = 0, ip = 0;
	var visited = new HashSet<int>();

	while (ip < instructions.Count && !visited.Contains(ip))
	{
		var (instr, param) = instructions[ip];
		visited.Add(ip);

		switch (instr)
		{
			case "acc":
				acc += param;
				ip++;
				break;
			case "jmp":
				ip += param;
				break;
			case "nop":
				ip++;
				break;
		}
	}

	return (ip >= instructions.Count, acc);
}

void Part1()
{
	var (_, acc) = TryRunUntilCompletion(instructions);
	Console.WriteLine($"Part 1: {acc}");
}

void Part2()
{
	var instructionsCopy = instructions.ToList();
	int nextIp = instructions.FindIndex(x => x.Instr == "jmp" || x.Instr == "nop");
	var (completed, acc) = TryRunUntilCompletion(instructionsCopy);
	while (!completed)
	{
		var (instr, param) = instructions[nextIp];
		instructionsCopy[nextIp] = (instr == "jmp" ? "nop" : "jmp", param);
		(completed, acc) = TryRunUntilCompletion(instructionsCopy);
		instructionsCopy[nextIp] = (instr == "jmp" ? "jmp" : "nop", param);
		nextIp = instructions.FindIndex(nextIp + 1, x => x.Instr == "jmp" || x.Instr == "nop");
	}

	Console.WriteLine($"Part 2: {acc}");
}

Part1();
Part2();
