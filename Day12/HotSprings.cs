using System.Text;

namespace AdventOfCode2023.Day12;

public class HotSprings : AdventOfCodeBase
{
    public void SolvePartOne()
    {
        var sum = 0;
        foreach (var line in Input)
        {

            var arrangements = ComputeArrangements(line, 0);
            var sumForLine = arrangements.Where(IsValidArrangement).Count();
            sum += sumForLine;
            Console.WriteLine($"Sum for line {line}: {sumForLine}");
        }
        Console.WriteLine($"Sum of all arrangements: {sum}");
    }

    private bool IsValidArrangement(string line)
    {
        var split = line.Split(' ');
        var groups = split[1].Split(',').Select(int.Parse).ToArray();
        var gears = split[0];

        var groupsOfArrangement = gears.Split(".", StringSplitOptions.RemoveEmptyEntries);

        if (groupsOfArrangement.Length != groups.Length) return false;

        for (int i = 0; i < groups.Length; i++)
        {
            var group = groups[i];
            var groupOfArrangement = groupsOfArrangement[i];
            if (groupOfArrangement.Length != group) return false;
        }

        return true;
    }

    private List<string> ComputeArrangements(string line, int index)
    {
        if (index == line.Length) return new List<string>() { line };

        var arrangements = new List<string>();

        if (line[index] == '?')
        {
            var left = line.Substring(0, index);
            var right = line.Substring(index + 1);
            arrangements.AddRange(ComputeArrangements(left + '#' + right, index + 1));
            arrangements.AddRange(ComputeArrangements(left + '.' + right, index + 1));
        }
        else
        {
            arrangements.AddRange(ComputeArrangements(line, index + 1));
        }

        return arrangements;
    }

}
