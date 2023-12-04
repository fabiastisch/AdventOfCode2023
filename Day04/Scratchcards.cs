namespace AdventOfCode2023.Day04;

public class Scratchcards : AdventOfCodeBase
{

    // Part 2
    public void AddScratchcards()
    {
        Dictionary<int, int> totalScratchCards = new Dictionary<int, int>();
        for (var i = 0; i < Input.Count; i++)
        {
            totalScratchCards.AddUpValue(i,1);

            if (CheckCardForMatchingNumbers(i, out int matches))
            {
                int winningMultiplier = totalScratchCards[i];

                for (int j = i+1; j <= i+matches; j++)
                {
                    if (totalScratchCards.ContainsKey(j))
                    {
                        totalScratchCards[j] += winningMultiplier;
                    }
                    else
                    {
                        totalScratchCards.Add(j, winningMultiplier);
                    }
                }
            }
        }

        int countScratchCards = 0;
        foreach (var value in totalScratchCards.Values)
        {
            countScratchCards += value;
        }
        Console.Out.WriteLine("Result: " + countScratchCards);
    }
    private bool CheckCardForMatchingNumbers(int lineIndex, out int matches)
    {
        matches = 0;
        var line = Input[lineIndex];

        var cardNumberAndNumbers = line.Split(": ");
        var winningNumbersAndOwningCardNumbers = cardNumberAndNumbers[1].Split(" | ");

        var winningNumbers = winningNumbersAndOwningCardNumbers[0].Split(" ");
        var owningCardNumbers = winningNumbersAndOwningCardNumbers[1].Split(" ");
        HashSet<string> winningNumbersSet = new HashSet<string>();
        foreach (var winningNumber in winningNumbers)
        {
            if (winningNumber.Trim().Length == 0)
                continue;
            winningNumbersSet.Add(winningNumber.Trim());
        }
        foreach (var owningCardNumber in owningCardNumbers)
        {
            if (winningNumbersSet.Contains(owningCardNumber.Trim()))
            {
                matches++;
            }
        }
        return matches > 0;
    }


    // Part 1
    public void AddWinningNumbers()
    {
        var addedResult = 0;
        foreach (var line in Input)
        {

            var cardNumberAndNumbers = line.Split(": ");

            var winningNumbersAndOwningCardNumbers = cardNumberAndNumbers[1].Split(" | ");

            var winningNumbers = winningNumbersAndOwningCardNumbers[0].Split(" ");
            var owningCardNumbers = winningNumbersAndOwningCardNumbers[1].Split(" ");

            HashSet<string> winningNumbersSet = new HashSet<string>();
            foreach (var winningNumber in winningNumbers)
            {
                if (winningNumber.Trim().Length == 0)
                    continue;
                winningNumbersSet.Add(winningNumber.Trim());
            }

            /*
            foreach (var s in winningNumbersSet)
            {
                Console.Out.WriteLine(s);
            }*/

            var countMyWinningNumbers = 0;

            foreach (var owningCardNumber in owningCardNumbers)
            {
                if (winningNumbersSet.Contains(owningCardNumber.Trim()))
                {
                    countMyWinningNumbers++;
                    //Console.Out.WriteLine("Card " + cardNumberAndNumbers[0] + " has " + owningCardNumber.Trim() + " as a winning number.");
                }
            }


            var result = Math.Pow(2, countMyWinningNumbers - 1);

            addedResult += (int)result;
        }

        Console.Out.WriteLine("Result: " + addedResult);
    }

}
