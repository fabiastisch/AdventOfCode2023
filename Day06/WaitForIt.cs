namespace AdventOfCode2023.Day06;

public class WaitForIt : AdventOfCodeBase
{

    public void SolveFirst()
    {
        var times = Input[0].Replace("Time: ", "")
            .Split(" ")
            .Where(x => x.Length > 0)
            .Select(long.Parse)
            .ToArray();

        var distances = Input[1].Replace("Distance: ", "")
            .Split(" ")
            .Where(x => x.Length > 0)
            .Select(long.Parse)
            .ToArray();

        // Solve
        var result = 1;

        for (int i = 0; i < times.Length; i++)
        {
            var time = times[i];
            var distance = distances[i];
            Console.Out.WriteLine("Time: " + time + " Distance: " + distance);

            result *= CalculateDifferentWays(time, distance);

        }
        Console.Out.WriteLine("Result: " + result);

    }

    private int CalculateDifferentWays(long time, long distance)
    {
        var count = 0;
        for (long tToCheck = 0; tToCheck < time; tToCheck++)
        {
            if ((time - tToCheck) * tToCheck > distance)
                count++;
        }
        return count;
    }

    public void SolveTwo()
    {
        var time = long.Parse(Input[0].Replace("Time: ", "")
            .Replace(" ", ""));

        var distance = long.Parse(Input[1].Replace("Distance: ", "")
            .Replace(" ", ""));

        Console.Out.WriteLine("Time: " + time + " Distance: " + distance);
        Console.Out.WriteLine("Result: " + CalculateDifferentWays(time, distance));
    }


}
