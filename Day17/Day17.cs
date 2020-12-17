using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

Part1();
Part2();


void Part1()
{
    var state = File.ReadAllLines("day17.txt")
        .SelectMany((line, y) => line.Select((c, x) => (x, y, c)))
        .Where(cell => cell.c == '#')
        .Select(cell => new Cell3D(cell.x, cell.y, 0))
        .ToHashSet();

    for (int i = 0; i < 6; i++)
    {
        state = NextState3D(state);
    }

    Console.WriteLine($"Part 1: {state.Count}");
}

void Part2()
{
    var state = File.ReadAllLines("day17.txt")
        .SelectMany((line, y) => line.Select((c, x) => (x, y, c)))
        .Where(cell => cell.c == '#')
        .Select(cell => new Cell4D(cell.x, cell.y, 0, 0))
        .ToHashSet();

    for (int i = 0; i < 6; i++)
    {
        state = NextState4D(state);
    }

    Console.WriteLine($"Part 2: {state.Count}");
}

HashSet<Cell3D> NextState3D(HashSet<Cell3D> state)
{
    var minX = state.Select(c => c.X).Min();
    var maxX = state.Select(c => c.X).Max();
    var minY = state.Select(c => c.Y).Min();
    var maxY = state.Select(c => c.Y).Max();
    var minZ = state.Select(c => c.Z).Min();
    var maxZ = state.Select(c => c.Z).Max();

    var next = new HashSet<Cell3D>();
    for (var x = minX - 1; x <= maxX + 1; x++)
    {
        for (var y = minY - 1; y <= maxY + 1; y++)
        {
            for (var z = minZ - 1; z <= maxZ + 1; z++)
            {
                var cell = new Cell3D(x, y, z);
                var wasActive = state.Contains(cell);
                var neighbours = CountNeighbours3D(state, x, y, z);
                if ((wasActive && (neighbours == 2 || neighbours == 3))
                    || (!wasActive && neighbours == 3))
                {
                    next.Add(cell);
                }
            }
        }
    }

    return next;
}

int CountNeighbours3D(HashSet<Cell3D> state, int x, int y, int z)
{
    var count = 0;
    for (var nx = x - 1; nx <= x + 1; nx++)
    {
        for (var ny = y - 1; ny <= y + 1; ny++)
        {
            for (var nz = z - 1; nz <= z + 1; nz++)
            {
                if ((nx != x || ny != y || nz != z) && state.Contains(new Cell3D(nx, ny, nz)))
                {
                    count++;
                }
            }
        }
    }
    return count;
}

HashSet<Cell4D> NextState4D(HashSet<Cell4D> state)
{
    var minX = state.Select(c => c.X).Min();
    var maxX = state.Select(c => c.X).Max();
    var minY = state.Select(c => c.Y).Min();
    var maxY = state.Select(c => c.Y).Max();
    var minZ = state.Select(c => c.Z).Min();
    var maxZ = state.Select(c => c.Z).Max();
    var minW = state.Select(c => c.W).Min();
    var maxW = state.Select(c => c.W).Max();

    var next = new HashSet<Cell4D>();
    for (var x = minX - 1; x <= maxX + 1; x++)
    {
        for (var y = minY - 1; y <= maxY + 1; y++)
        {
            for (var z = minZ - 1; z <= maxZ + 1; z++)
            {
                for (var w = minW - 1; w <= maxW + 1; w++)
                { 
                    var cell = new Cell4D(x, y, z, w);
                    var wasActive = state.Contains(cell);
                    var neighbours = CountNeighbours4D(state, x, y, z, w);
                    if ((wasActive && (neighbours == 2 || neighbours == 3))
                        || (!wasActive && neighbours == 3))
                    {
                        next.Add(cell);
                    }
                }
            }
        }
    }

    return next;
}

int CountNeighbours4D(HashSet<Cell4D> state, int x, int y, int z, int w)
{
    var count = 0;
    for (var nx = x - 1; nx <= x + 1; nx++)
    {
        for (var ny = y - 1; ny <= y + 1; ny++)
        {
            for (var nz = z - 1; nz <= z + 1; nz++)
            {
                for (var nw = w - 1; nw <= w + 1; nw++)
                {
                    if ((nx != x || ny != y || nz != z || nw != w) && state.Contains(new Cell4D(nx, ny, nz, nw)))
                    {
                        count++;
                    }
                }
            }
        }
    }
    return count;
}


record Cell3D(int X, int Y, int Z);
record Cell4D(int X, int Y, int Z, int W);
