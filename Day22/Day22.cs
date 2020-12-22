using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

Part1();

void Part1()
{
    var decks = LoadPlayerDecks();

    while (decks.All(d => d.Any()))
    {
        var topCards = decks.Select(d => d[0]).ToList();
        var maxPlayer = topCards.Select((c, i) => (c, i)).OrderByDescending(ci => ci.c).First().i;
        foreach (var deck in decks)
        {
            deck.RemoveAt(0);
        }
        decks[maxPlayer].AddRange(topCards.OrderByDescending(c => c));
    }

    var winnerDeck = decks.First(d => d.Any());
    var winnerScore = winnerDeck.Reverse<int>().Select((c, i) => c * (i + 1)).Sum();
    Console.WriteLine($"Part 1: {winnerScore}");
}

List<int>[] LoadPlayerDecks()
{
    var lines = File.ReadLines("day22.txt");
    var deck1 = lines.Skip(1).TakeWhile(x => x != "").Select(int.Parse).ToList();
    var deck2 = lines.SkipWhile(x => x != "Player 2:").Skip(1).Select(int.Parse).ToList();
    return new [] { deck1, deck2 };
}
