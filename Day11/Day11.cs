using System;
using System.IO;
using System.Linq;

char[][] LoadOriginalState()
{
	return File.ReadLines("day11.txt").Select(x => x.ToArray()).ToArray();
}

bool SameState(char[][] a, char[][] b)
{
	return a.Zip(b).All(row => row.First.SequenceEqual(row.Second));
}

char[][] NextStateAdjacentSeats(char[][] state)
{
	return state.Select((row, ri) => row.Select((col, ci) => NextSeatAdjacentSeats(state, ri, ci)).ToArray()).ToArray();
}

char NextSeatAdjacentSeats(char[][] state, int row, int col)
{
	var seat = state[row][col];
	var surrounding = OccupiedAdjacentSeats(state, row, col);
	if (seat == 'L' && surrounding == 0) return '#';
	if (seat == '#' && surrounding >= 4) return 'L';
	return seat;
}

int OccupiedAdjacentSeats(char[][] state, int row, int col)
{
	var columns = state[0].Length;
	var occupied = 0;

	if (row > 0)
	{
		if (col > 0 && state[row - 1][col - 1] == '#') occupied++;
		if (state[row - 1][col] == '#') occupied++;
		if (col < columns - 1 && state[row - 1][col + 1] == '#') occupied++;
	}

	if (col > 0 && state[row][col - 1] == '#') occupied++;
	if (col < columns - 1 && state[row][col + 1] == '#') occupied++;

	if (row < state.Length - 1)
	{
		if (col > 0 && state[row + 1][col - 1] == '#') occupied++;
		if (state[row + 1][col] == '#') occupied++;
		if (col < columns - 1 && state[row + 1][col + 1] == '#') occupied++;
	}

	return occupied;
}

void Part1()
{
	var state = LoadOriginalState();
	while (true)
	{
		var nextState = NextStateAdjacentSeats(state);
		if (SameState(state, nextState))
		{
			break;
		}
		state = nextState;
	}

	var occupied = state.SelectMany(x => x).Count(x => x == '#');
	Console.WriteLine($"Part 1: {occupied}");
}

char[][] NextStateVisibleSeats(char[][] state)
{
	return state.Select((row, ri) => row.Select((col, ci) => NextSeatVisibleSeats(state, ri, ci)).ToArray()).ToArray();
}

char NextSeatVisibleSeats(char[][] state, int row, int col)
{
	var seat = state[row][col];
	var surrounding = OccupiedVisibleSeats(state, row, col);
	if (seat == 'L' && surrounding == 0) return '#';
	if (seat == '#' && surrounding >= 5) return 'L';
	return seat;
}

int OccupiedVisibleSeats(char[][] state, int row, int col)
{
	var columns = state[0].Length;
	var occupied = 0;
	int r, c;

	r = row - 1;
	c = col - 1;
	while (r >= 0 && c >= 0)
	{
		if (state[r][c] != '.')
		{
			if (state[r][c] == '#') occupied++;
			break;
		}

		r--;
		c--;
	}

	r = row - 1;
	c = col;
	while (r >= 0)
	{
		if (state[r][c] != '.')
		{
			if (state[r][c] == '#') occupied++;
			break;
		}
		r--;
	}

	r = row - 1;
	c = col + 1;
	while (r >= 0 && c < columns)
	{
		if (state[r][c] != '.')
		{
			if (state[r][c] == '#') occupied++;
			break;
		}

		r--;
		c++;
	}

	r = row;
	c = col - 1;
	while (c >= 0)
	{
		if (state[r][c] != '.')
		{
			if (state[r][c] == '#') occupied++;
			break;
		}
		c--;
	}

	r = row;
	c = col + 1;
	while (c < columns)
	{
		if (state[r][c] != '.')
		{
			if (state[r][c] == '#') occupied++;
			break;
		}
		c++;
	}

	r = row + 1;
	c = col - 1;
	while (r < state.Length && c >= 0)
	{
		if (state[r][c] != '.')
		{
			if (state[r][c] == '#') occupied++;
			break;
		}

		r++;
		c--;
	}

	r = row + 1;
	c = col;
	while (r < state.Length)
	{
		if (state[r][c] != '.')
		{
			if (state[r][c] == '#') occupied++;
			break;
		}
		r++;
	}

	r = row + 1;
	c = col + 1;
	while (r < state.Length && c < columns)
	{
		if (state[r][c] != '.')
		{
			if (state[r][c] == '#') occupied++;
			break;
		}

		r++;
		c++;
	}

	return occupied;
}

void Part2()
{
	var state = LoadOriginalState();
	while (true)
	{
		var nextState = NextStateVisibleSeats(state);
		if (SameState(state, nextState))
		{
			break;
		}
		state = nextState;
	}

	var occupied = state.SelectMany(x => x).Count(x => x == '#');
	Console.WriteLine($"Part 2: {occupied}");
}

Part1();
Part2();
