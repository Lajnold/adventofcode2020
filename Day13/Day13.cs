using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

void Part1()
{
    var input = LoadInput();
    var shortestWait = input.BusIds
        .Where(id => id != null)
        .Select(id =>
        {
            var div = input.EarliestLeave / id;
            var wait = div * id == input.EarliestLeave
                ? 0
                : ((div + 1) * id) - input.EarliestLeave;
            return (id, wait);
        })
        .OrderBy(x => x.wait)
        .First();

    Console.WriteLine($"Part 1: {shortestWait.id * shortestWait.wait}");
}

void Part2()
{
    var input = LoadInput();
    var offsets = input.BusIds
        .Select((id, i) => (id, i))
        .Where(x => x.id != null)
        .Select(x => (Id: x.id.Value, Offset: x.i))
        .ToList();

    var maxIdOffset = offsets.OrderByDescending(x => x.Id).First();
    var tmax = 100000000000000L / maxIdOffset.Id * maxIdOffset.Id;
    for (; ; tmax += maxIdOffset.Id)
    {
        var match = true;
        var t = tmax - maxIdOffset.Offset;
        for (var i = 0; i < offsets.Count && match; i++)
        {
            if ((t + offsets[i].Offset) % offsets[i].Id != 0)
            {
                match = false;
            }
        }

        if (match) break;
    }

    Console.WriteLine($"Part 2: {tmax - maxIdOffset.Offset}");
}

Input LoadInput()
{
    var lines = File.ReadAllLines("day13.txt");
    var earliestLeave = int.Parse(lines[0]);
    var busIds = lines[1].Split(',').Select(x => x != "x" ? int.Parse(x) : (int?)null).ToList();
    return new Input(earliestLeave, busIds);
}


Part1();
Part2();


record Input(int EarliestLeave, IReadOnlyList<int?> BusIds);
