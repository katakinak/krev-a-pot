﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




//tvorba hor a lavin



//tabulka kde cesta, atd

class Program
{
    static string[,] Vytvoritpole(int velikost)
    {
        string[,] pole = new string[velikost, velikost];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                pole[i, j] = "cesta";
            }
        }
        return pole;

    }
    static string Vypispole(string[,] pole)
    {
        string vypis = " ";
        for (int i = 0; i < pole.GetLength(0); i++)
        {
            for (int j = 0; j < pole.GetLength(1); j++)
            {
                vypis += pole[i, j] + "\t";
            }
            vypis += "\n";
        }
        return vypis;
    }
    //BFS
    static (int, List<(int, int)>) najdicestu(
        int n,
        List<(int x, int y, int t)> laviny,
        List<(int dx, int dy)> tahy
) 
{
    
    Queue<(int x, int y, int time, List<(int, int)> Path)> fronta
        = new Queue<(int, int, int, List<(int, int)>)>();


    fronta.Enqueue((0,0,0, new List<(int, int)> {(0,0)}));

    HashSet<(int, int, int)> navstiveno
        = new HashSet<(int, int, int)>();

    while (fronta.Count > 0)
    { 
        var (x, y, time, path) = fronta.Dequeue();

        if (x == n - 1 && y == n - 1)
        {
            return (time, path);
        }
            bool jeblokovana = laviny.Any(
                lavina => lavina.x == x && lavina.y == y && lavina.t <= time
                );
            if (jeblokovana)
            {
                continue;
            }
            if (navstiveno.Contains(x, y, time))
            {
                continue ;
            }    

            navstiveno.Add((x,y, time));
            foreach (var(dx,dy) in tahy)
            {
                int novyX = x + dx;
                int novyY = y + dy;
                if novyX >= 0 && novyX < n && novyY >= 0 && novyY <n)
                {
                    bool jeBlokovanyTah = laviny.Any(
                        lavina => lavina.x == novyX && lavina.y == novyY && lavina.t == time +1
                    );
                    if (!jeBlokovanyTah)
                    {
                        var novaCesta = new List<(int, int)>(path);
                        novaCesta.Add((novyX, novyY));
                        fronta.Enqueue((novyX, novyY, time + 1, novaCesta));
                    }
                }
            }
        }
        return (-1, null);
    }




    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int m = int.Parse(Console.ReadLine());
        List<(int x, int y, int t)> laviny = new List<(int, int, int)>();

        for (int i = 0; i < m; i++)
        {
            var data = Console.ReadLine().Split().Select(int.Parse).ToArray();
            laviny.Add((data[0], data[1], data[2]));
        }

        int k = int.Parse(Console.ReadLine());
        List<(int dx, int dy)> tahy = new List<(int, int)>();

        for (int i = 0; i < k; i++)
        {
            var data = Console.ReadLine().Split().Select(int.Parse).ToArray();
            tahy.Add((data[0], data[1]));
        }

        // Inicializace šachovnice (pro ilustraci)
        string[,] sachovnice = VytvoritPole(n);
        foreach (var lavina in laviny)
        {
            sachovnice[lavina.x, lavina.y] = "hora";
        }

        // Najít nejkratší cestu
        var (delka, cesta) = NajdiCestu(n, laviny, tahy);

        // Výstup
        if (delka == -1)
        {
            Console.WriteLine("-1");
        }
        else
        {
            Console.WriteLine(delka);
            Console.WriteLine(string.Join(" ", cesta.Select(p => $"[{p.Item1},{p.Item2}]")));
        }

        // Výpis šachovnice
        Console.WriteLine("Šachovnice:");
        Console.WriteLine(VypisPole(sachovnice));
    }
}

