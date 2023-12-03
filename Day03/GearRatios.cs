namespace AdventOfCode2023.Day03;

public class GearRatios : AdventOfCodeBase
{
    // Part 2
    public void FindGearRatiosByMultiplyGearParts()
    {
        int result = 0;
        for (var i = 0; i < Input.Count; i++)
        {
            var line = Input[i];
            for (int j = 0; j < line.Length; j++)
            {
                var currentChar = Input[i][j];
                if (currentChar.Equals('*'))
                {
                    CheckForExactlyTwoNumbersAsNeighbors(i, j, out int multipliedResult);
                    result += multipliedResult;
                   // Console.Out.WriteLine(result);
                }
            }
        }
        Console.Out.WriteLine("Result: " + result);
    }

    private void CheckForExactlyTwoNumbersAsNeighbors(int i, int j, out int multipliedResult)
    {
        multipliedResult = 0;

        // Check on the line
        int left = j - 1;
        int right = j + 1;
        List<int> foundNumbers = new List<int>();

        if (CheckCharForNumber(i, left, out int number))
        {
            foundNumbers.Add(number);
        }

        if (CheckCharForNumber(i, right, out number))
        {
            foundNumbers.Add(number);
        }

        // Check top
        int top = i - 1;
        for (int offset = -1; offset <= 1; offset++)
        {
            if (CheckCharForNumber(top, j + offset, out number))
            {
                foundNumbers.Add(number);
                while (offset <= 1 && char.IsAsciiDigit(Input[top][j + offset]))
                {
                    offset++; // skip the next one
                }
            }
        }

        // Check bottom
        int bot = i + 1;
        for (int offset = -1; offset <= 1; offset++)
        {
            if (CheckCharForNumber(bot, j + offset, out number))
            {
                foundNumbers.Add(number);
                while (offset <= 1 && char.IsAsciiDigit(Input[bot][j + offset]))
                {
                    offset++; // skip the next one
                }
            }
        }
        if (foundNumbers.Count == 2)
        {
            multipliedResult = foundNumbers[0] * foundNumbers[1];
        }
    }
    private bool CheckCharForNumber(int i, int j, out int number)
    {
        number = -1;
        if (i < 0 || i >= Input.Count)
            return false;
        if (j < 0 || j >= Input[i].Length)
            return false;
        var charToCheck = Input[i][j];
        if (char.IsAsciiDigit(charToCheck))
        {
            number = int.Parse(GetNumbersLeft(i, j) + charToCheck + GetNumbersRight(i, j));
            return true;
        }
        return false;
    }

    private string GetNumbersLeft(int i, int j)
    {
        if (--j >= 0 && char.IsAsciiDigit(Input[i][j]))
        {
            return GetNumbersLeft(i, j) + Input[i][j];
        }
        return "";
    }
    private string GetNumbersRight(int i, int j)
    {
        if (++j < Input[i].Length && char.IsAsciiDigit(Input[i][j]))
        {
            return Input[i][j] + GetNumbersRight(i, j );
        }
        return "";
    }
    // Part 1
    private List<int> _numbers = new List<int>();
    public void AddAdjacentNumbers()
    {
        bool isInNumber = false;
        bool foundSymbolAsNeighbor = false;
        string currentNumber = "";
        for (var i = 0; i < Input.Count; i++)
        {
            if (isInNumber && foundSymbolAsNeighbor)
            {
                _numbers.Add(int.Parse(currentNumber));
            }
            currentNumber = "";
            isInNumber = false;
            foundSymbolAsNeighbor = false;
            var line = Input[i];


            for (int j = 0; j < line.Length; j++)
            {
                var currentChar = Input[i][j];
                if (char.IsAsciiDigit(currentChar))
                {
                    isInNumber = true;
                    currentNumber += currentChar;
                    if (HasSymbolAsNeighbor(i, j))
                    {
                        foundSymbolAsNeighbor = true;
                    }
                }
                else
                {
                    if (isInNumber && foundSymbolAsNeighbor)
                    {
                        _numbers.Add(int.Parse(currentNumber));
                    }
                    currentNumber = "";
                    isInNumber = false;
                    foundSymbolAsNeighbor = false;
                }
                if (currentChar == '.')
                {
                    continue;
                }
            }
        }
        Console.Out.WriteLine("Found " + _numbers.Count + " numbers.");
        int sum = 0;
        foreach (var number in _numbers)
        {
            if (number > 1000)
            {
                Console.Out.WriteLine(number);

            }
            sum += number;
        }
        Console.Out.WriteLine("Sum: " + sum);
    }

    private bool HasSymbolAsNeighbor(int i, int j)
    {
        // top
        int top = j - 1;
        if (top >= 0)
        {
            for (int offset = -1; offset <= 1; offset++)
            {
                if (i + offset < 0 || i + offset >= Input.Count)
                    continue;
                var neighboredChar = Input[i + offset][top];
                if (CheckCharForSymbolExceptDot(neighboredChar))
                {
                    return true;
                }
            }
        }

        // left & right
        if (i > 0 && CheckCharForSymbolExceptDot(Input[i - 1][j]))
            return true;
        if (i + 1 < Input.Count && CheckCharForSymbolExceptDot(Input[i + 1][j]))
            return true;

        // bottom

        int bot = j + 1;
        if (bot < Input[i].Length)
        {
            for (int offset = -1; offset <= 1; offset++)
            {
                if (i + offset < 0 || i + offset >= Input.Count)
                    continue;
                var neighboredChar = Input[i + offset][bot];
                if (CheckCharForSymbolExceptDot(neighboredChar))
                {
                    return true;
                }
            }
        }


        return false;
    }

    private bool CheckCharForSymbolExceptDot(char charToCheck)
    {
        if (char.IsAsciiDigit(charToCheck) || charToCheck.Equals('.'))
        {
            return false;
        }
        return true;
    }

}
