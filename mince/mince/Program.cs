using System;
using System.Collections.Generic;
using System.Linq;

class Mince
{
    static void FindCombinations(int[] coins, int sum, int index, List<int> current, List<List<int>> results)
    {
        if (sum == 0)
        {
            results.Add(new List<int>(current));
            return;
        }

        if (sum < 0 || index == coins.Length)
        {
            return;
        }

        current.Add(coins[index]);
        FindCombinations(coins, sum - coins[index], index, current, results);
        current.RemoveAt(current.Count - 1);

        FindCombinations(coins, sum, index + 1, current, results);
    }

    static void Main()
    {
        Console.WriteLine("Zadej jaké mincé máš, odděl mezerou:");
        int[] coins = Console.ReadLine()
                            .Split(' ')
                            .Select(int.Parse)
                            .Distinct()
                            .OrderBy(x => x)
                            .ToArray();

        Console.WriteLine("Částka potřebná k zaplacení:");
        int sum = int.Parse(Console.ReadLine());

        List<List<int>> results = new List<List<int>>();
        FindCombinations(coins, sum, 0, new List<int>(), results);

        Console.WriteLine($"\nPočet možností: {results.Count}");
        Console.WriteLine("Možné kombinace:");
        foreach (var combination in results)
        {
            Console.WriteLine(string.Join(" + ", combination));
        }
    }
}
