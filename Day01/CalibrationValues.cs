namespace AdventOfCode2023.Day01;

public class CalibrationValues: AdventOfCodeBase
{

    public void FindCalibrationValues()
    {
        int totalCalibrationValues = 0;
        foreach (var line in Input)
        {
            Console.Out.WriteLine("Line: " + line);
            totalCalibrationValues += int.Parse(GetCalibrationValue(line));

        }
        Console.Out.WriteLine("Total Calibration Values: " + totalCalibrationValues);
    }

    private string GetCalibrationValue(string s)
    {
        int firstNumber = -1;
        int lastNumber = -1;
        string previous = "";

        List<char> chars = s.ToList();

        foreach (var currentChar in chars)
        {
            if (char.IsLetter(currentChar))
            {
                previous += currentChar;
                if (CheckNumberInNumbersList(previous, out firstNumber))
                {
                    break;
                }
            }else
            {
                previous = "";
            }
            if (char.IsNumber(currentChar))
            {
                firstNumber = int.Parse(currentChar.ToString());
                break;
            }

        }
        previous = "";
        chars.Reverse();
        foreach (char currentChar in chars)
        {
            if (char.IsLetter(currentChar))
            {
                previous += currentChar;
                var reversed = new string(previous.Reverse().ToArray());
                if (CheckNumberInNumbersList(reversed, out lastNumber))
                {
                    break;
                }
            }else
            {
                previous = "";
            }
            if (char.IsNumber(currentChar))
            {
                lastNumber = int.Parse(currentChar.ToString());
                break;
            }
        }
        return firstNumber + "" + lastNumber;
    }


    private bool CheckNumberInNumbersList(string s, out int num)
    {
        string[] numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        for (var i = 0; i < numbers.Length; i++)
        {
            var number = numbers[i];
            if (s.Contains(number))
            {
                num = i + 1;
                return true;
            }
        }
        num = -1;
        return false;

    }

}
