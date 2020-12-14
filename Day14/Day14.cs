using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var instructions = File.ReadAllLines("day14.txt");

(long OnMask, long OffMask) GetMasksV1(string maskString)
{
    // 1010X101010010101X00X00011XX11011111
    long on = 0, off = 0;
    for (int i = 0; i < 36; i++)
    {
        if (maskString[i] != 'X')
        {
            long bit = maskString[i] == '1' ? 1 : 0;
            on |= (bit & 1) << (35 - i);
            off |= (~bit & 1) << (35 - i);
        }
    }
    return (on, off);
}

void Part1()
{
    long onMask = 0, offMask = 0;
    var mem = new Dictionary<int, long>();
    foreach (var instr in instructions)
    {
        if (instr.StartsWith("mask"))
        {
            // mask = 1010X101010010101X00X00011XX11011111
            (onMask, offMask) = GetMasksV1(instr[7..]);
        }
        else
        {
            // mem[1303] = 728
            var endBracket = instr.IndexOf(']');
            var addr = int.Parse(instr[4..endBracket]);
            var value = long.Parse(instr[(endBracket+4)..]);
            var maskedValue = (value | onMask) & ~offMask;
            mem[addr] = maskedValue;
        }
    }

    var memorySum = mem.Select(kvp => kvp.Value).Sum();
    Console.WriteLine($"Part 1: {memorySum}");
}

(long OnMask, long FloatingMask) GetMasksV2(string maskString)
{
    // 1010X101010010101X00X00011XX11011111
    long on = 0, floating = 0;
    for (int i = 0; i < 36; i++)
    {
        if (maskString[i] == '1')
        {
            on |= 1L << (35 - i);
        }
        else if (maskString[i] == 'X')
        {
            floating |= 1L << (35 - i);
        }
    }
    return (on, floating);
}

long[] ApplyFloatingMask(long addr, long floatingMask)
{
    var addrs = new List<long> { addr };

    for (int i = 0; i < 36; i++)
    {
        if ((floatingMask & (1L << i)) != 0)
        {
            for (int j = 0, len = addrs.Count; j < len; j++)
            {
                addrs.Add(addrs[j] | (1L << i));
                addrs.Add(addrs[j] & ~(1L << i));
            }
        }
    }

    return addrs.Distinct().ToArray();
}

void Part2()
{
    long onMask = 0, floatingMask = 0;
    var mem = new Dictionary<long, long>();
    foreach (var instr in instructions)
    {
        if (instr.StartsWith("mask"))
        {
            // mask = 1010X101010010101X00X00011XX11011111
            (onMask, floatingMask) = GetMasksV2(instr[7..]);
        }
        else
        {
            // mem[1303] = 728
            var endBracket = instr.IndexOf(']');
            var addr = long.Parse(instr[4..endBracket]);
            var value = long.Parse(instr[(endBracket + 4)..]);
            addr |= onMask;

            foreach (var floatingAddr in ApplyFloatingMask(addr, floatingMask))
            {
                mem[floatingAddr] = value;
            }
        }
    }

    var memorySum = mem.Select(kvp => kvp.Value).Sum();
    Console.WriteLine($"Part 2: {memorySum}");
}

Part1();
Part2();
