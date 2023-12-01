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
        string[] numbers = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        string previous = "";

        foreach (char c in s.ToList())
        {
            //Console.Out.WriteLine(previous);
            if (char.IsLetter(c))
            {
                previous += c;
                for (var i = 0; i < numbers.Length; i++)
                {
                    if (previous.Contains(numbers[i]))
                    {
                        firstNumber = i + 1;
                        break;
                    }
                }
                if (firstNumber != -1)
                {
                    break;
                }
            }
            else
            {
                previous = "";
            }
            if (char.IsNumber(c))
            {
                firstNumber = int.Parse(c.ToString());
                break;
            }
        }
        previous = "";
        for (var i = s.ToList().Count - 1; i >= 0; i--)
        {
            char c = s[i];
            if (char.IsLetter(c))
            {
                previous += c;
                var reversed = new string(previous.Reverse().ToArray());
                for (var j = 0; j < numbers.Length; j++)
                {
                    if (reversed.Contains(numbers[j]))
                    {
                        lastNumber = j + 1;
                        break;
                    }
                }
                if (lastNumber != -1)
                {
                    break;
                }
            }
            else
            {
                previous = "";
            }
            if (char.IsNumber(s[i]))
            {
                lastNumber = int.Parse(s[i].ToString());
                break;
            }
        }
        Console.Out.WriteLine("First Number: " + firstNumber + " Last Number: " + lastNumber);
        return firstNumber + "" + lastNumber;
    }


}
