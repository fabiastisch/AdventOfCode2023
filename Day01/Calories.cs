namespace AdventOfCode2023.Day01;

/*
 * Example of 2022
 */
public class Calories: AdventOfCodeBase
{
    private int[] _topElves = new []{0,0,0};

    public void FindMostCalories()
    {

        int currentCalories = 0;

        foreach (var line in Input)
        {
            if (line.Length <= 0)
            {
                if (currentCalories > _topElves[0])
                {
                    _topElves[0] = currentCalories;
                    Array.Sort(_topElves);
                }
                currentCalories = 0;
            }
            else
            {
                currentCalories += int.Parse(line);
            }

        }
        Console.Out.WriteLine($"Top elves: {_topElves[0]}, {_topElves[1]}, {_topElves[2]}");
        Console.Out.WriteLine($"Total: {_topElves[0] + _topElves[1] + _topElves[2]}");
    }


}
