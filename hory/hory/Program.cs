using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();

       
        Console.WriteLine("Před dávnými časy byl do vesnice doručen důležitý dopis. Tento dopis může zachránit celou vesnici od blížící se katastrofy. " +
                          "Jen ty, nejodvážnější posel, se vydáváš na nebezpečnou cestu přes zrádné hory a laviny, abys doručil tento dopis na místo určení. " +
                          "Čas běží... dokážeš to?");

        
        Console.WriteLine("\nZadejte velikost mapy (n):");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Chyba: Neplatná velikost mapy.");
            return;
        }

        char[,] mapa = new char[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                mapa[i, j] = '.';

       
        int pocetHor = random.Next(1, n * n / 3);
        for (int i = 0; i < pocetHor; i++)
        {
            int x = random.Next(0, n);
            int y = random.Next(0, n);
            if ((x == 0 && y == 0) || (x == n - 1 && y == n - 1)) continue;
            mapa[x, y] = 'H';
        }

        
        mapa[n - 1, n - 1] = 'C';

       
        int hracX = 0, hracY = 0;

        // Fráze pro různé situace
        string[] cestaFraze = { "Tramtaram!", "Oh, ještě jeden krok blíže k cíli.", "Nezapomněl jsem si mapu? *hrabhrab* a tady je!" };
        string horaFraze = "Nemůžeš vstoupit na horu.";
        string lavinaFraze = "Lavina zasáhla cestu!";

        string posledniHlaseni = ""; 

        // Hra
        while (true)
        {
            // Zobrazení mapy
            Console.Clear();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == hracX && j == hracY)
                        Console.Write('P'); // Pozice hráče
                    else
                        Console.Write(mapa[i, j]);
                }
                Console.WriteLine();
            }

            
            if (!string.IsNullOrEmpty(posledniHlaseni))
                Console.WriteLine($"\n{posledniHlaseni}");

            
            if (hracX == n - 1 && hracY == n - 1)
            {
                Console.WriteLine("\nGratulujeme! Doručil jsi důležitý dopis a zachránil vesnici!");
                break;
            }

            // Výběr směru
            Console.WriteLine("\nKam se chceš vydat? (W = nahoru, S = dolů, A = vlevo, D = vpravo)");
            char smer = Console.ReadKey().KeyChar;
            int novyX = hracX, novyY = hracY;

            if (smer == 'W' || smer == 'w') novyX--;
            else if (smer == 'S' || smer == 's') novyX++;
            else if (smer == 'A' || smer == 'a') novyY--;
            else if (smer == 'D' || smer == 'd') novyY++;
            else
            {
                posledniHlaseni = "Neplatný směr! Zadejte W, S, A nebo D.";
                continue;
            }

            
            if (novyX < 0 || novyX >= n || novyY < 0 || novyY >= n)
            {
                posledniHlaseni = "Nemůžeš jít mimo mapu! Zkus jiný směr.";
                continue;
            }

           
            if (mapa[novyX, novyY] == 'H')
            {
                posledniHlaseni = horaFraze;
                continue;
            }

            // Lavina - šance, že spadne při každém tahu
            if (random.Next(0, 10) < 2) // 20% šance na lavinu
            {
                int lavinaX = random.Next(0, n);
                int lavinaY = random.Next(0, n);
                int velikostLaviny = random.Next(1, 4);

                for (int i = Math.Max(0, lavinaX - velikostLaviny); i <= Math.Min(n - 1, lavinaX + velikostLaviny); i++)
                {
                    for (int j = Math.Max(0, lavinaY - velikostLaviny); j <= Math.Min(n - 1, lavinaY + velikostLaviny); j++)
                    {
                        if (mapa[i, j] == '.' && !(i == 0 && j == 0) && !(i == n - 1 && j == n - 1))
                            mapa[i, j] = 'L';
                    }
                }

                if (mapa[hracX, hracY] == 'L')
                {
                    Console.Clear();
                    Console.WriteLine("\nLavina tě zasáhla! Hra skončila!");
                    break;
                }

                posledniHlaseni = lavinaFraze;
            }

            hracX = novyX;
            hracY = novyY;

            posledniHlaseni = cestaFraze[random.Next(cestaFraze.Length)];
        }
    }
}
