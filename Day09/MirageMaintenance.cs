namespace AdventOfCode2023.Day09;

public class MirageMaintenance : AdventOfCodeBase
{
    public void SolvePartOne()
    {
        // find next value and add them up

        var addedValues = 0;
        foreach (var line in Input)
        {
            var pyramid = BuildPyramid(line);

            foreach (var l in pyramid)
            {
                Console.Out.WriteLine(string.Join(" ", l));

            }

            //var val = FindNextValue(pyramid, 1);
            var val = FindPreviousValue(pyramid, 0);

            Console.Out.WriteLine("Next Value: " + val);

            addedValues += val;

        }
        Console.Out.WriteLine("Added Values: " + addedValues);

    }

    private List<List<int>> BuildPyramid(string line)
    {
        var first = line.Split(' ').Select(v => int.Parse(v)).ToList();

        List<List<int>> pyramid = new List<List<int>>();
        pyramid.Add(first);
        return BuildPyramid(pyramid);
    }

    private int FindPreviousValue(List<List<int>> pyramid, int index)
    {
        if (index >= pyramid.Count)
        {
            return 0;
        }
        return pyramid[index].First() - FindPreviousValue(pyramid, index +1);
    }
    private int FindNextValue(List<List<int>> pyramid, int index)
    {
        //Console.Out.WriteLine("Index: " + index);
        if (index > pyramid.Count)
        {
            return 0;
        }
        return pyramid[^index].Last() + FindNextValue(pyramid, ++index);
    }
    private List<List<int>> BuildPyramid(List<List<int>> pyramid)
    {

        var current = pyramid.Last();

        var next = new List<int>();
        for (var i = 0; i < current.Count; i++)
        {
            if (i == 0) continue;

            next.Add(current[i] - current[i - 1]);
        }
        if (next.All(v => v == 0))
        {
            return pyramid;
        }
        pyramid.Add(next);
        return BuildPyramid(pyramid);
    }

}
