using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class TextAnalyzer : StreamReader
{
    private Dictionary<string, int> slova; // Slovník pro frekvenci slov
    public int PocetSlov { get; private set; } // Počet slov
    public int PocetZnakuBezMezer { get; private set; } // Počet znaků bez bílých znaků
    public int PocetZnakuSMezerama { get; private set; } // Počet znaků s bílými znaky

    public TextAnalyzer(string soubor) : base(soubor) // Konstruktor třídy
    {
        try
        {
            slova = new Dictionary<string, int>();
            string obsah = ReadToEnd(); // Načtení celého souboru jako string

            // Počet znaků s bílými znaky
            PocetZnakuSMezerama = obsah.Length;

            // Počet znaků bez bílých znaků
            PocetZnakuBezMezer = obsah.Count(znak => !char.IsWhiteSpace(znak));

            // Rozdělení textu na jednotlivá slova podle bílých znaků
            var vsechnaslova = obsah.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            PocetSlov = vsechnaslova.Length;

            // Počítání výskytu slov
            foreach (var slovo in vsechnaslova)
            {
                string slovomale = slovo.ToLower(); // Převod na malá písmena
                if (slova.ContainsKey(slovomale))
                {
                    slova[slovomale]++;
                }
                else
                {
                    slova[slovomale] = 1;
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Soubor '{soubor}' neexistuje.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba při zpracování souboru: {ex.Message}");
        }
    }

    // Metoda pro získání frekvence slov
    public Dictionary<string, int> ZiskatFrekvenciSlov()
    {
        return new Dictionary<string, int>(slova);
    }

    // Metoda pro vrácení slov oddělených jednou mezerou, zachovávající řádky
    public string ZiskatSlovaSOddelovacem()
    {
        BaseStream.Seek(0, SeekOrigin.Begin);
        string obsah = ReadToEnd();

        var radky = obsah.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
        var sb = new System.Text.StringBuilder();

        foreach (var radek in radky)
        {
            var slovavRadku = radek.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            sb.AppendLine(string.Join(" ", slovavRadku)); // Slova v řádku oddělená jednou mezerou
        }

        return sb.ToString().Trim(); // Odstranění prázdného řádku na konci
    }
}

class Program
{
    static void Main(string[] args)
    {
        string vstupniSoubor = "vstup.txt";  // Název vstupního souboru
        string vystupniSoubor = "vystup.txt"; // Název výstupního souboru

        try
        {
            TextAnalyzer analyzer = new TextAnalyzer(vstupniSoubor);

            using (StreamWriter writer = new StreamWriter(vystupniSoubor))
            {
                writer.WriteLine($"Počet slov: {analyzer.PocetSlov}");
                writer.WriteLine($"Počet znaků bez bílých znaků: {analyzer.PocetZnakuBezMezer}");
                writer.WriteLine($"Počet znaků s bílými znaky: {analyzer.PocetZnakuSMezerama}");
                writer.WriteLine("\nFrekvence slov:");

                foreach (var slovo in analyzer.ZiskatFrekvenciSlov())
                {
                    writer.WriteLine($"{slovo.Key}: {slovo.Value}");
                }

                writer.WriteLine("\nSlova s oddělovači:");
                writer.WriteLine(analyzer.ZiskatSlovaSOddelovacem());
            }

            Console.WriteLine("Výstup byl úspěšně uložen.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba: {ex.Message}");
        }
    }
}
