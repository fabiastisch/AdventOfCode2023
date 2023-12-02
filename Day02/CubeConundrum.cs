namespace AdventOfCode2023.Day02;

public class CubeConundrum: AdventOfCodeBase
{

    public void Run()
    {
        var maxRed = 12;
        var maxGreen = 13;
        var maxBlue = 14;

        var isGamePossible = true;
        var countPossibleGames = 0;
        var counter = 0;
        foreach (var line in Input)
        {
            var splitGame = line.Split(": ");
            var sets = splitGame[1].Split("; ");
            var redCubes = 0;
            var greenCubes = 0;
            var blueCubes = 0;
            foreach (var set in sets)
            {
                var colors = set.Split(", ");
                foreach (var color in colors)
                {


                    var numberColor = color.Split(" ");
                    var number = int.Parse(numberColor[0]);
                    var colorName = numberColor[1];
                    switch (colorName)
                    {
                        case "red":
                            redCubes = redCubes < number ? number: redCubes;
                            break;
                        case "green":
                            greenCubes = greenCubes < number ? number: greenCubes;
                            break;
                        case "blue":
                            blueCubes = blueCubes < number ? number: blueCubes;
                            break;
                        default:
                            Console.Out.WriteLine($"Unknown color {colorName}");
                            break;
                    }
                    if (redCubes > maxRed || greenCubes > maxGreen || blueCubes > maxBlue)
                    {
                        isGamePossible = false;
                        //break;
                    }

                }

            }

            counter += redCubes * greenCubes * blueCubes;

            Console.Out.WriteLine($"{splitGame[0]}: " +
                                  $"Red: {redCubes} " +
                                  $"Green: {greenCubes} " +
                                  $"Blue: {blueCubes} " +
                                  $"Total: {redCubes * greenCubes * blueCubes} ");
        }
        //Console.Out.WriteLine($"Possible games: {countPossibleGames}");
        Console.Out.WriteLine($"Total cubes: {counter}");
    }

}
