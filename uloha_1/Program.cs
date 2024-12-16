using System;
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
    }




    static void Main()
    {
        string[,]polehry = Vytvoritpole();
        Console.WriteLine("obsah pole: ");
        Console.WriteLine(Vypispole(polehry));
    }
}

