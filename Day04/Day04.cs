using System;
using System.IO;
using System.Linq;

var passportsRaw = File.ReadAllText("day04.txt").Split("\n\n");
var passports = passportsRaw
	.Select(pass => pass.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries)
		.ToDictionary(field => field.Split(":")[0], field => field.Split(":")[1]))
	.ToList();

void Part1()
{
	var requiredFields = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
	var valid = passports.Count(fields => requiredFields.All(f => fields.ContainsKey(f)));
	Console.WriteLine($"Part 1: {valid}");
}

bool ValidateField(string name, string value)
{
	switch (name)
	{
		case "byr":
			return int.TryParse(value, out var byr) && byr >= 1920 && byr <= 2002;
		case "iyr":
			return int.TryParse(value, out var iyr) && iyr >= 2010 && iyr <= 2020;
		case "eyr":
			return int.TryParse(value, out var eyr) && eyr >= 2020 && eyr <= 2030;
		case "hgt":
			if (value.EndsWith("cm"))
				return int.TryParse(value[0..^2], out var cm) && cm >= 150 && cm <= 193;
			else if (value.EndsWith("in"))
				return int.TryParse(value[0..^2], out var inch) && inch >= 59 && inch <= 76;
			else
				return false;
		case "hcl":
			return value.Length == 7
				&& value.StartsWith("#")
				&& value.Substring(1).All(c => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f'));
		case "ecl":
			var validEyeColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
			return validEyeColors.Contains(value);
		case "pid":
			return value.Length == 9 && int.TryParse(value, out var _);
		case "cid":
			return true;
	}

	throw new Exception("What???");
}

void Part2()
{
	var requiredFields = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
	var valid = passports.Count(fields =>
		requiredFields.All(f => fields.ContainsKey(f)) &&
		fields.All(field => ValidateField(field.Key, field.Value)));
	Console.WriteLine($"Part 2: {valid}");
}

Part1();
Part2();
