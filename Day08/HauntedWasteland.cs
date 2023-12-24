namespace AdventOfCode2023.Day08;

public class HauntedWasteland : AdventOfCodeBase
{
    public void SolvePartOne()
    {
        Dictionary<string, string[]> tree = new Dictionary<string, string[]>();

        var instructions = Input[0];

        foreach (var line in Input.Skip(2))
        {
            var first = line.Split('=');

            var parent = first[0].Trim();
            var childs = first[1]
                .Replace("(", "")
                .Replace(")", "")
                .Split(',')
                .Select(v => v.Trim())
                .ToArray();

            tree.Add(parent, childs);
        }

        string current = "AAA";
        int stepCount = 0;

        while (current != "ZZZ")
        {
            foreach (char instruction in instructions)
            {
                stepCount++;
                int ins = instruction == 'L' ? 0 : 1;

                current = tree[current][ins];


               // Console.Out.WriteLine("Current: " + current);
            }
        }
        Console.Out.WriteLine("StepCount: " + stepCount);


    }

}
