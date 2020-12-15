using System;
using System.Linq;

const string SampleInput = "0,3,6";
const string Input = "9,3,1,0,8,4";

void Part1()
{
    var numbers = Input.Split(',').Select(int.Parse).ToList();

    for (int i = numbers.Count; i < 2020; i++)
    {
        var speak = numbers.Last();
        var lastIndex = numbers.LastIndexOf(speak, numbers.Count - 2);
        if (lastIndex >= 0)
        {
            speak = numbers.Count - lastIndex - 1;
        }
        else
        {
            speak = 0;
        }

        numbers.Add(speak);
    }

    Console.WriteLine($"Part 1: {numbers.Last()}");
}

void Part2()
{
    var inputNumbers = Input.Split(',').Select(int.Parse).ToList();
    var numbersDict = inputNumbers.Take(inputNumbers.Count - 1)
        .Select((n, i) => (n, i)).ToDictionary(ni => ni.n, ni => ni.i);
    var speak = inputNumbers.Last();
    var numSpoken = inputNumbers.Count - 1;

    for (int i = inputNumbers.Count; i < 30000000; i++)
    {
        int newSpeak;
        if (numbersDict.TryGetValue(speak, out var lastIndex))
        {
            newSpeak = numSpoken - lastIndex;
        }
        else
        {
            newSpeak = 0;
        }

        numbersDict[speak] = numSpoken;
        speak = newSpeak;
        numSpoken++;
    }

    Console.WriteLine($"Part 2: {speak}");
}

Part1();
Part2();
