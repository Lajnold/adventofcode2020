using System;
using System.IO;
using System.Linq;

(char C, int N)[] LoadInstructions()
{
    return File.ReadAllLines("day12.txt").Select(x => (x[0], int.Parse(x[1..]))).ToArray();
}

void Part1()
{
    var instructions = LoadInstructions();
    int x = 0, y = 0, dir = 1;

    foreach (var (c, n) in instructions)
    {
        switch (c)
        {
            case 'N':
                y += n;
                break;
            case 'S':
                y -= n;
                break;
            case 'E':
                x += n;
                break;
            case 'W':
                x -= n;
                break;
            case 'L':
                dir -= (n / 90);
                if (dir < 0) dir += 4;
                break;
            case 'R':
                dir = (dir + (n / 90)) % 4;
                break;
            case 'F':
                switch (dir)
                {
                    case 0:
                        y += n;
                        break;
                    case 1:
                        x += n;
                        break;
                    case 2:
                        y -= n;
                        break;
                    case 3:
                        x -= n;
                        break;
                }
                break;
        }
    }

    Console.WriteLine($"Part 1: {Math.Abs(x) + Math.Abs(y)}");
}

void Part2()
{
    var instructions = LoadInstructions();
    int x = 0, y = 0;
    int wpx = 10, wpy = 1;

    foreach (var (c, n) in instructions)
    {
        switch (c)
        {
            case 'N':
                wpy += n;
                break;
            case 'S':
                wpy -= n;
                break;
            case 'E':
                wpx += n;
                break;
            case 'W':
                wpx -= n;
                break;
            case 'L':
                switch (n)
                {
                    case 90:
                        if (wpx >= 0 && wpy >= 0)
                        {
                            var swap = wpx;
                            wpx = -wpy;
                            wpy = swap;
                        }
                        else if (wpx >= 0 && wpy < 0)
                        {
                            var swap = wpx;
                            wpx = -wpy;
                            wpy = swap;
                        }
                        else if (wpx < 0 && wpy < 0)
                        {
                            var swap = wpx;
                            wpx = -wpy;
                            wpy = swap;
                        }
                        else
                        {
                            var swap = wpx;
                            wpx = -wpy;
                            wpy = swap;
                        }
                        break;
                    case 180:
                        wpx = -wpx;
                        wpy = -wpy;
                        break;
                    case 270:
                        if (wpx >= 0 && wpy >= 0)
                        {
                            var swap = wpx;
                            wpx = wpy;
                            wpy = -swap;
                        }
                        else if (wpx >= 0 && wpy < 0)
                        {
                            var swap = wpx;
                            wpx = wpy;
                            wpy = -swap;
                        }
                        else if (wpx < 0 && wpy < 0)
                        {
                            var swap = wpx;
                            wpx = wpy;
                            wpy = -swap;
                        }
                        else
                        {
                            var swap = wpx;
                            wpx = wpy;
                            wpy = -swap;
                        }
                        break;
                }
                break;
            case 'R':
                switch (n)
                {
                    case 90:
                        if (wpx >= 0 && wpy >= 0)
                        {
                            var swap = wpx;
                            wpx = wpy;
                            wpy = -swap;
                        }
                        else if (wpx >= 0 && wpy < 0)
                        {
                            var swap = wpx;
                            wpx = wpy;
                            wpy = -swap;
                        }
                        else if (wpx < 0 && wpy < 0)
                        {
                            var swap = wpx;
                            wpx = wpy;
                            wpy = -swap;
                        }
                        else
                        {
                            var swap = wpx;
                            wpx = wpy;
                            wpy = -swap;
                        }
                        break;
                    case 180:
                        wpx = -wpx;
                        wpy = -wpy;
                        break;
                    case 270:
                        if (wpx >= 0 && wpy >= 0)
                        {
                            var swap = wpx;
                            wpx = -wpy;
                            wpy = swap;
                        }
                        else if (wpx >= 0 && wpy < 0)
                        {
                            var swap = wpx;
                            wpx = -wpy;
                            wpy = swap;
                        }
                        else if (wpx < 0 && wpy < 0)
                        {
                            var swap = wpx;
                            wpx = -wpy;
                            wpy = swap;
                        }
                        else
                        {
                            var swap = wpx;
                            wpx = -wpy;
                            wpy = swap;
                        }
                        break;
                }
                break;
            case 'F':
                x += wpx * n;
                y += wpy * n;
                break;
        }
    }

    Console.WriteLine($"Part 2: {Math.Abs(x) + Math.Abs(y)}");
}

Part1();
Part2();
