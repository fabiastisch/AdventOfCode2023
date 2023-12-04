namespace AdventOfCode2023;

public static class DictionaryExtensions
{

    public static void AddUpValue(this Dictionary<int, int> dict, int key, int value)
    {
        if (dict.ContainsKey(key))
        {
            dict[key] += value;
        }
        else
        {
            dict.Add(key, value);
        }
    }

}
