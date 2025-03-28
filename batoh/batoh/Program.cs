using System;
using System.Collections.Generic;
using System.Linq;

class batoh
{
    static void Main()
    {
        // Načtení vstupu - seznam vah, cen a kapacita batohu
        int[] weights = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int[] values = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int capacity = int.Parse(Console.ReadLine());

        int n = weights.Length;
        // Tabulka pro dynamické programování
        int[,] dp = new int[n + 1, capacity + 1];

        // Vypočítání maximální hodnoty, kterou lze získat
        for (int i = 1; i <= n; i++)
        {
            for (int w = 0; w <= capacity; w++)
            {
                if (weights[i - 1] <= w)
                    dp[i, w] = Math.Max(dp[i - 1, w], dp[i - 1, w - weights[i - 1]] + values[i - 1]);
                else 
                    dp[i, w] = dp[i - 1, w];
            }
        }

        //dp -> dynamické programování, w -> aktuální kapacita batohu, i-> položky, n-> počet položek

        Console.WriteLine(dp[n, capacity]);

        // Rekonstrukce vybraných položek
        List<int> selectedItems = new List<int>();
        int remainingWeight = capacity;
        for (int i = n; i > 0 && remainingWeight > 0; i--)
        {
            if (dp[i, remainingWeight] != dp[i - 1, remainingWeight]) // Pokud byla položka použita
            {
                selectedItems.Add(i); // Přidáme index položky
                remainingWeight -= weights[i - 1]; // Snížíme zbývající kapacitu
            }
        }

        // Výpis vybraných položek v pořadí, v jakém byly zvoleny
        selectedItems.Reverse();
        Console.WriteLine(string.Join(" ", selectedItems));
    }
}


//v konzoli (řádky) 1.: váha položek, 2.:cena položek, 3: kapacita batohu (oddělujte mezerou bez čárek)