<Query Kind="Statements" />

int[][] LoadGrid (string path) => File.ReadAllLines(path)
	.Select(i => Enumerable.Range(0, i.Length).Select(c => int.Parse(i[c].ToString())).ToArray()).ToArray();

int Step(int[][] grid)
{
	var affected = new Queue<(int x, int y)>(Enumerable.Range(0, 10).Select(y => Enumerable.Range(0, 10).Select(x => (x, y))).SelectMany(i => i));

	do
	{
		var (x, y) = affected.Dequeue();
		if (grid[y][x] == 9)
		{
			if (y > 0 && x > 0)
			{
				affected.Enqueue((x - 1, y - 1));
			}
			if (y > 0)
			{
				affected.Enqueue((x, y - 1));
			}
			if (y > 0 && x < 9)
			{
				affected.Enqueue((x + 1, y - 1));
			}
			if (x < 9)
			{
				affected.Enqueue((x + 1, y));
			}
			if (x < 9 && y < 9)
			{
				affected.Enqueue((x + 1, y + 1));
			}
			if (y < 9)
			{
				affected.Enqueue((x, y + 1));
			}
			if (y < 9 && x > 0)
			{
				affected.Enqueue((x - 1, y + 1));
			}
			if (x > 0)
			{
				affected.Enqueue((x - 1, y));
			}
		}

		grid[y][x] += 1;
	} while (affected.Any());


	// handle flashes
	var flashes = 0;
	for (int y = 0; y < 10; y++)
	{
		for (int x = 0; x < 10; x++)
		{
			if (grid[y][x] > 9)
			{
				flashes++;
				grid[y][x] = 0;
			}
		}
	}


	return flashes;
}

string PrettyPrint(int[][] grid) => string.Join("\n", grid.Select(y => string.Join(string.Empty, y.Select(i => i.ToString()))));

var sample = LoadGrid(@"C:\Projects\GitHub\advent-of-code-2021\day11.sample.dat");

var iterations = 100;
var flashes = Enumerable.Range(0,iterations).Aggregate(0, (acc,_) => acc + Step(sample));

Console.WriteLine($"[SAMPLE] After {iterations} iterations, {flashes} flashes");
PrettyPrint(sample).Dump();


var input = LoadGrid(@"C:\Projects\GitHub\advent-of-code-2021\day11.dat");
flashes = Enumerable.Range(0, iterations).Aggregate(0, (acc, _) => acc + Step(input));
Console.WriteLine($"[INPUT] After {iterations} iterations, {flashes} flashes");
PrettyPrint(input).Dump();

int FindFirstSimultanousFlash(int[][] grid)
{
	iterations = 0;
	do
	{
		flashes = Step(grid);
		iterations++;
	} while (flashes != 100);
	return iterations;
}


sample = LoadGrid(@"C:\Projects\GitHub\advent-of-code-2021\day11.sample.dat");
var first = FindFirstSimultanousFlash(sample);
Console.WriteLine($"[SAMPLE] After {iterations} iterations simultaneously flash");

input = LoadGrid(@"C:\Projects\GitHub\advent-of-code-2021\day11.dat");
first = FindFirstSimultanousFlash(input);
Console.WriteLine($"[INPUT] After {iterations} iterations simultaneously flash");
