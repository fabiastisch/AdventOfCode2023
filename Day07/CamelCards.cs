namespace AdventOfCode2023.Day07;

public class CamelCards: AdventOfCodeBase
{
    private Dictionary<string, int> _cardsAndBits = new();
    public void SolvePartOne()
    {
        foreach (var line in Input)
        {
            var splitted = line.Split(' ');
            _cardsAndBits.Add(splitted[0], Convert.ToInt32(splitted[1]));
        }


        var keys = _cardsAndBits.Keys.ToList();
        keys.Sort((a, b) =>
        {

            var aPairs = getPairs(a);
            var bPairs = getPairs(b);

            aPairs.Sort();
            bPairs.Sort();
            aPairs.Reverse();
            bPairs.Reverse();

            for (int i = 0; i < aPairs.Count; i++)
            {
                if (aPairs[i] > bPairs[i])
                {
                    return 1;
                }
                else if (aPairs[i] < bPairs[i])
                {
                    return -1;
                }
            }

            return CompareHighCard(a, b);

        });


        var total = 0;
        for (var i = 0; i < keys.Count; i++)
        {
            var bits = _cardsAndBits[keys[i]];

            Console.WriteLine($"{keys[i]} : {bits}");

            total += bits * (i + 1);
        }

        Console.Out.WriteLine("Total: " + total);


    }

    private int CompareHighCard(string s, string s1)
    {
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i].CompareTo(s1[i]) != 0)
            {
                return GetCharValue(s[i]).CompareTo(GetCharValue(s1[i]));
            }

        }
        return 0;
    }

    private List<int> getPairs(string s)
    {
        Dictionary<char, int> pairs = new();
        foreach (var c in s)
        {
            if (pairs.ContainsKey(c))
            {
                pairs[c]++;
            }
            else
            {
                pairs.Add(c, 1);
            }
        }

        // remove jokers | PART 2
        var jokerCount = 0;
        if (pairs.ContainsKey('J'))
        {
            jokerCount = pairs['J'];
            pairs.Remove('J');
        }


        var values = new[] { 0, 0, 0, 0, 0 };
        pairs.Values.CopyTo(values, 0);

        var val = values.ToList();
        val.Sort();
        val.Reverse();

        // remove jokers | PART 2
        val[0] += jokerCount;

        return val;
    }

    private int GetCharValue(char c)
    {
        switch (c)
        {
            case 'A':
                return 14;
            case 'K':
                return 13;
            case 'Q':
                return 12;
            case 'J':
                return 1; // now Joker , normal 11  | PART 2
            case 'T':
                return 10;
            default:
                return Convert.ToInt32(c.ToString());

        }
    }

}
