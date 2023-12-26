namespace AdventOfCode2023.Day10;

public class PipeMaze : AdventOfCodeBase
{
    private long _steps = 0;
    public void SolvePartOne()
    {
        // Find S
        for (var i = 0; i < Input.Count; i++)
        {
            for (var j = 0; j < Input[i].Length; j++)
            {
                if (Input[i][j] == 'S')
                {
                    Console.WriteLine($"Found S at {i},{j}");
                    FindStartPipes(i, j);
                    return;
                }
            }

        }

    }

    private void FindStartPipes(int startX, int startY)
    {
        // find next pipes
        var topChar = Input[startX + 1][startY];
        if (topChar is '|' or '7' or 'F')
        {
            Console.Out.WriteLine("Found top pipe");
            BuildPipeLoop(startX + 1, startY, 'U');
            return;
        }

        var bottomChar = Input[startX - 1][startY];
        if (bottomChar is '|' or 'J' or 'L')
        {
            Console.Out.WriteLine("Found bottom pipe");
            BuildPipeLoop(startX - 1, startY, 'D');
            return;
        }

        var leftChar = Input[startX][startY - 1];
        if (leftChar is '-' or 'F' or 'L')
        {
            Console.Out.WriteLine("Found left pipe");
            BuildPipeLoop(startX, startY - 1, 'R');
            return;

        }

        var rightChar = Input[startX][startY + 1];
        if (rightChar is '-' or '7' or 'J')
        {
            Console.Out.WriteLine("Found right pipe");
            BuildPipeLoop(startX, startY + 1, 'L');
            return;
        }

    }

    private void BuildPipeLoop(int y, int x, char previousDirection)
    {

        PartTwoPipeLoop(y, x, previousDirection);
        return;

        while (Input[y][x] != 'S' )
        {

            _steps++;

            var currentChar = Input[y][x];
            if (currentChar == 'S')
            {
                Console.Out.WriteLine("Found S at " + y + "," + x + " Step: " + _steps);
                return;
            }
            Console.Out.WriteLine("Current char: " + currentChar + " at " + y + "," + x + " Step: " + _steps + " Previous direction: " + previousDirection);
            var (nextY, nextX) = currentChar switch
            {
                '|' => previousDirection == 'U' ? (1, 0) : (-1, 0),
                '-' => previousDirection == 'L' ? (0, 1) : (0, -1),
                'L' => previousDirection == 'R' ? (-1, 0) : (0, 1),
                'J' => previousDirection == 'U' ? (0, -1) : (-1, 0),
                '7' => previousDirection == 'D' ? (0, -1) : (1, 0),
                'F' => previousDirection == 'R' ? (1, 0) : (0, 1),
                _ => throw new Exception("Invalid pipe" + currentChar + " at Step: " + _steps)
            };

            char nextPreviousDir = nextX switch
            {
                1 => 'L',
                -1 => 'R',
                _ => nextY switch
                {
                    1 => 'U',
                    -1 => 'D',
                    _ => throw new Exception("Invalid pipe" + currentChar)
                }
            };

            y += nextY;
            x += nextX;
            previousDirection = nextPreviousDir;
        }

        Console.Out.WriteLine("Part one Solution: " + Math.Ceiling(((double)_steps)/2));
    }

    private void PartTwoPipeLoop(int y, int x, char previousDirection)
    {
        char[][] grid = new char[Input.Count][];
        for (int i = 0; i < Input.Count; i++)
        {
            grid[i] = new char[Input[i].Length];
            for (var i1 = 0; i1 < grid[i].Length; i1++)
            {
                grid[i][i1] = '0';
            }
        }

        switch (previousDirection)
        {
            case 'U':
                grid[y - 1][x] = 'S';
                break;
            case 'D':
                grid[y + 1][x] = 'S';
                break;
            case 'L':
                grid[y][x + 1] = 'S';
                break;
            case 'R':
                grid[y][x - 1] = 'S';
                break;
        }

        while (Input[y][x] != 'S' )
        {
            _steps++;
            var currentChar = Input[y][x];
            grid[y][x] = currentChar;

            // Console.Out.WriteLine("Current char: " + currentChar + " at " + y + "," + x + " Step: " + _steps + " Previous direction: " + previousDirection);
            var (nextY, nextX) = currentChar switch
            {
                '|' => previousDirection == 'U' ? (1, 0) : (-1, 0),
                '-' => previousDirection == 'L' ? (0, 1) : (0, -1),
                'L' => previousDirection == 'R' ? (-1, 0) : (0, 1),
                'J' => previousDirection == 'U' ? (0, -1) : (-1, 0),
                '7' => previousDirection == 'D' ? (0, -1) : (1, 0),
                'F' => previousDirection == 'R' ? (1, 0) : (0, 1),
                _ => throw new Exception("Invalid pipe" + currentChar + " at Step: " + _steps)
            };

            char nextPreviousDir = nextX switch
            {
                1 => 'L',
                -1 => 'R',
                _ => nextY switch
                {
                    1 => 'U',
                    -1 => 'D',
                    _ => throw new Exception("Invalid pipe" + currentChar)
                }
            };

            y += nextY;
            x += nextX;
            previousDirection = nextPreviousDir;
        }
        Console.Out.WriteLine("Part one Solution: " + Math.Ceiling(((double)_steps)/2));


        int countInLoop = 0;
        for (var index = 0; index < grid.Length; index++)
        {
            var lines = grid[index];
            bool isInLoop = false;
            for (var i = 0; i < lines.Length; i++)
            {
                var b = lines[i];
                if (b is '|' or 'J' or 'L')
                {
                    isInLoop = !isInLoop;
                }
                else if (isInLoop && b == '0')
                {
                    countInLoop++;
                    grid[index][i] = '█';
                }
            }
            // Console.Out.WriteLine(string.Join("", grid[index].Select(b => b /*? "1" : "0"*/)));
        }
        Console.Out.WriteLine("Part two Solution: " + countInLoop);

    }

}
