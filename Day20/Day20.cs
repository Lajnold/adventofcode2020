using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("day20.txt");
var tiles = ParseTiles(lines);

Part1();

void Part1()
{
    var sideLooup = tiles
        .SelectMany(t => t.Sides.Concat(t.FlippedSides).Select(s => (s, t.TileId)))
        .ToLookup(x => x.s, x => x.TileId);

    var corners = tiles.Where(t =>
        t.Sides.Count(s => sideLooup[s].Count() == 1) == 2 ||
        t.FlippedSides.Count(s => sideLooup[s].Count() == 1) == 2);

    var cornerIdsMultiplied = corners.Select(t => (long) t.TileId).Aggregate((acc, val) => acc * val);
    Console.WriteLine($"Part 1: {cornerIdsMultiplied}");
}

Tile[] ParseTiles(string[] lines)
{
    var tiles = new List<Tile>();
    for (int i = 0; i < lines.Count(); i += 12)
    {
        var tileId = int.Parse(lines[i].Split(' ')[1][..^1]);
        var pixels = lines.Skip(i + 1).Take(10).Select(x => x.ToArray()).ToArray();
        tiles.Add(new Tile(tileId, pixels));
    }

    return tiles.ToArray();
}

record Tile(int TileId, char[][] Pixels)
{
    public string[] Sides
    {
        get
        {
            if (_sides == null)
            {
                // Up, Down, Left, Right
                _sides = new string[]
                {
                    new string(Pixels[0]),
                    new string(Pixels[^1]),
                    new string(Pixels.Select(row => row[0]).ToArray()),
                    new string(Pixels.Select(row => row[^1]).ToArray())
                };
            }

            return _sides;
        }
    }
    private string[] _sides;

    public string[] FlippedSides
    {
        get
        {
            if (_flippedSides == null)
            {
                _flippedSides = Sides.Select(s => string.Join("", s.Reverse())).ToArray();
            }

            return _flippedSides;
        }
    }
    private string[] _flippedSides;

    public override string ToString()
        => string.Join(Environment.NewLine, Pixels.Select(row => string.Join("", row)));
}
