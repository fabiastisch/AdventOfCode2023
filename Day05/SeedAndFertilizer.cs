namespace AdventOfCode2023.Day05;

public class SeedAndFertilizer : AdventOfCodeBase
{
    // Part 1
    Dictionary<CurrentParseLocation, List<long[]>> maps = new();
    public void CalculateLowestLocationNumber()
    {
        List<long[]> seeds = new();
        CurrentParseLocation? currentParseLocation = null;


        Enum.GetValues(typeof(CurrentParseLocation))
            .Cast<CurrentParseLocation>()
            .ToList()
            .ForEach(x => maps.Add(x, new List<long[]>()));

        foreach (var line in Input)
        {
            if (line.Length <= 0)
                continue;

            if (line.StartsWith("seeds: "))
            {
                var numbers = line.Replace("seeds: ", "").Split(" ");

                /*// Part 1
                foreach (var number in numbers)
                {
                    seeds.Add(long.Parse(number));
                }*/

                // Part 2
                for (var i = 0; i < numbers.Length; i++)
                {
                    var number = long.Parse(numbers[i]);
                    var range = long.Parse(numbers[(i + 1)]);

                    seeds.Add(new[] { number, range });
                    Console.Out.WriteLine("Added " + seeds.Count);
                    i++;
                }
                // Part 2 end
            }

            if (line.StartsWith("seed-to-soil"))
            {
                currentParseLocation = CurrentParseLocation.SeedToSoil;
                continue;
            }

            if (line.StartsWith("soil-to-fertilizer"))
            {
                currentParseLocation = CurrentParseLocation.SoilToFertilizer;
                continue;
            }

            if (line.StartsWith("fertilizer-to-water"))
            {
                currentParseLocation = CurrentParseLocation.FertilizerToWater;
                continue;
            }

            if (line.StartsWith("water-to-light"))
            {
                currentParseLocation = CurrentParseLocation.WaterToLight;
                continue;
            }

            if (line.StartsWith("light-to-temperature"))
            {
                currentParseLocation = CurrentParseLocation.LightToTemperature;
                continue;
            }

            if (line.StartsWith("temperature-to-humidity"))
            {
                currentParseLocation = CurrentParseLocation.TemperatureToHumidity;
                continue;
            }

            if (line.StartsWith("humidity-to-location"))
            {
                currentParseLocation = CurrentParseLocation.HumidityToLocation;
                continue;
            }

            if (currentParseLocation is {} cParseLoc)
            {
                maps[cParseLoc].Add(ParseLine(line));
            }

        }

        // Calculate the lowest location number

        long lowestLocationNumber = long.MaxValue;

        long outerIndex = 0;

        foreach (var seed in seeds)
        {
            var sync = new object();
            Parallel.For(0, seed[1], i =>
            {
                var number = CalculateNextNumber(seed[0] + i, 0);

                if (number <= 389056265) // Part1 result
                {
                    lock (sync)
                    {
                        lowestLocationNumber = Math.Min(number, lowestLocationNumber);
                    }
                }

            });

            Console.Out.WriteLine("Seed " + outerIndex + " done");

            outerIndex++;
        }

        // Print the results
        Console.Out.WriteLine("Lowest location number: " + lowestLocationNumber);

        /*foreach (var map in maps)
        {
            Console.Out.WriteLine("Map: " + map.Key);
            foreach (var mapLine in map.Value)
            {
                Console.Out.WriteLine("MapLine: " + string.Join(" ", mapLine));
            }
        }*/
    }

    private long CalculateNextNumber(long seedOrNumber, int position)
    {
        var nextNumber = seedOrNumber;
        var keys = maps.Keys.ToList();
        if (position >= keys.Count)
        {
            return seedOrNumber;
        }
        CurrentParseLocation currentCalculatePosition = keys[position];

        foreach (var x in maps[currentCalculatePosition])
        {
            // Console.Out.WriteLine("Check: " + soil + " || " + x[0] + " " + x[1] + " " + x[2]);
            // x[0] = destination
            // x[1] = start
            // x[2] = range
            if (seedOrNumber >= x[1] && seedOrNumber < x[1] + x[2])
            {
                nextNumber = x[0] + (seedOrNumber - x[1]);
            }

        }
        //Console.Out.WriteLine(" " + currentCalculatePosition + ": " + nextNumber + " || ");
        return CalculateNextNumber(nextNumber, position + 1);

    }

    private long[] ParseLine(string line)
    {
        var numbers = line.Split(" ");
        var result = new long[numbers.Length];

        for (int i = 0; i < numbers.Length; i++)
        {
            result[i] = long.Parse(numbers[i]);
        }

        return result;
    }

}

public enum CurrentParseLocation
{
    SeedToSoil,
    SoilToFertilizer,
    FertilizerToWater,
    WaterToLight,
    LightToTemperature,
    TemperatureToHumidity,
    HumidityToLocation
}
