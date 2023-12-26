namespace AdventOfCode2023.Day11;

public class CosmicExpansion: AdventOfCodeBase
{
    public void SolvePartOne()
    {
        List<int> rowsWithoutGalaxy = new();
        List<int> columnsWithoutGalaxy = new();
        List<(int x, int y)> galaxies = new();

        for (var i = 0; i < Input.Count; i++)
        {
            bool containsGalaxy = false;
            for (var j = 0; j < Input[i].Length; j++)
            {
                if (Input[i][j] == '#')
                {
                    containsGalaxy = true;
                    galaxies.Add((i, j));
                    Console.WriteLine($"Found # at {i},{j}");
                }
            }
            if (!containsGalaxy)
            {
                rowsWithoutGalaxy.Add(i);
                Console.WriteLine($"Found no galaxy at {i}");
            }
        }

        // find columns without # (to expand)
        for (var i = 0; i < Input[0].Length; i++)
        {
            bool containsGalaxy = false;
            foreach (var t in Input.Where(t => t[i] == '#'))
            {
                containsGalaxy = true;
            }
            if (!containsGalaxy)
            {
                columnsWithoutGalaxy.Add(i);
                Console.WriteLine($"Found no galaxy at {i}");
            }
        }

        long sum = 0;

        HashSet<int> completedGalaxies = new();

        for (var i = 0; i < galaxies.Count; i++)
        {
            var current = galaxies[i];

            for (var j = 0; j < galaxies.Count; j++)
            {
                if (i == j)continue;
                if (completedGalaxies.Contains(j)) continue;

                var other = galaxies[j];

                var xDiff = Math.Abs(current.x - other.x);
                var xDiffReal = xDiff + /*Part2*/(1000000-1)*/*EndPart2*/ columnsWithoutGalaxy.Count(v => v > Math.Min(current.y, other.y) && v < Math.Max(current.y, other.y));

                var yDiff = Math.Abs(current.y - other.y);
                var yDiffReal = yDiff + /*Part2*/(1000000-1)*/*EndPart2*/  rowsWithoutGalaxy.Count(v => v > Math.Min(current.x, other.x) && v < Math.Max(current.x, other.x));

                var distance = xDiffReal + yDiffReal;

                sum += distance;
            }

            completedGalaxies.Add(i);
        }

        Console.WriteLine($"Sum: {sum}");
    }

    
}
