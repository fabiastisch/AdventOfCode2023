using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2023.Day08;

public class HauntedWasteland : AdventOfCodeBase
{
    [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: Enumerator[System.String]; size: 134MB")]
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

        // Part2 - All Node that Ends With A
        var currentList = tree.Keys.ToList().FindAll(v => v.EndsWith("A"));
        var currentListFirstReachZ = new long[currentList.Count];
        currentListFirstReachZ = currentListFirstReachZ.Select(v => long.MinValue).ToArray();

        string current = "AAA";
        int stepCount = 0;

        foreach (var s in currentList)
        {
            Console.Out.WriteLine("Current: " + s);
        }
        bool foundAll = false;
        //current != "ZZZ"
        while (!foundAll)
        {
            foreach (char instruction in instructions)
            {
                stepCount++;
                int ins = instruction == 'L' ? 0 : 1;

                //current = tree[current][ins];

                for (var i = 0; i < currentList.Count; i++)
                {
                    currentList[i] = tree[currentList[i]][ins];
                    if (currentList[i].EndsWith("Z") && currentListFirstReachZ[i] == long.MinValue)
                    {
                        currentListFirstReachZ[i] = stepCount;
                        if (currentListFirstReachZ.All(v => v != long.MinValue))
                        {
                            Console.Out.WriteLine("Finished First Reach: " + stepCount);
                            foundAll = true;
                            break;
                        }
                    }
                }

               // Console.Out.WriteLine("Current: " + current);
            }
        }

        Console.Out.WriteLine("StepCount: " + currentListFirstReachZ.Aggregate(LCM));


    }

    private long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private long LCM(long a, long b)
    {
        return (a / GCD(a, b)) * b;
    }

}
