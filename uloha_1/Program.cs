using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




//tvorba hor a lavin



//tabulka kde cesta, atd

class Program
{
    static string[,] Vytvoritpole()
    { 
        string[,] pole = new string[4, 4];

        for (int i = 0; i< 4; i++)
        {
            for (int j = 0; j< 4; j++)
            {
                pole[i, j] = "cesta";
            }
        }
        return pole;
        
    }
    static string Vypispole(string[,] pole)
    {
        string vypis = " ";
        for (int i = 0;i< pole.GetLength(0); i++)
        {
            for (int j = 0; j < pole.GetLength(1); j++)
            {
                vypis += pole[i, j] + "\t";
            }
            vypis += "\n";    
        }
        return vypis;
    }
    static void Main()
    {
        string[,]polehry = Vytvoritpole();
        Console.WriteLine("obsah pole: ");
        Console.WriteLine(Vypispole(polehry));
    }
}

