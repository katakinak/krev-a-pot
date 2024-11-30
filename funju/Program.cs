// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

class TextAnalyzer : StreamReader;
{
    private Dictionary<string, int> slova;
    public int PocetSlov { get; set; }
    public int PocetZnakuBezMezer { get; set; }
    public int PocetZnakuSMezeama { get; set; }

    private TextAnalyzer (string soubor) : base(soubor);
    {
        try

        {
            slova = new Dictionary<string, int>();
            string obsah = ReadToEnd();

            PocetZnakuSMezeama = obsah.Length;
            PocetZnakuBezMezer = obsah.Count(znak => !char.bileznaky(znak));

            var vsechnaslova = obsah.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            PocetSlov = vsechnaslova.Length;

            foreach (var slovo in vsechnaslova)
            {
                string slovomale = slovo.ToLower;
                if slova.ContainsKey(slovomale))
                {
                    slova[slovomale]++;
                }

                else
                {
                    slova[slovomale] = 1;
                }

            }



        }
        catch ( FileNotFoundException)
        {
            Console.WriteLine($"soubor {'soubor'} neexistuje");
        }
    
        catch ( Exception ex)
        {
            Console.WriteLine($"chyba při zpracování souboru: {ex.Message}");
        }
    }
    public Dictionary<string, int> ziskatfrekvencislov;
    {
        return new Dictionary<string, int>(slova);
        }
    public string ziskatslovasoddelovacem()
    {
        BaseStream.Seek(0, SeekOrigin.Begin);
        string obsah = ReadToEnd();

        var radky = obsah.Split(new[] { " ", "\r", "\n", "\n" }, StringSplitOptions.None);
        var sb = new System.Text.StringBuilder();
        
        foreach (var r in radky)
        { 
            var slovavradku = radek.Split(new[] { ' ','\r'), StringSplitOptions.RemoveEmptyEntries);
            sb.AppendLine(string.Join(" ", slovavradku));

        }
        return sb.ToString();

    }
    
}
class program
{
    static void main(string[] args)
    {
        string vstupniSoubor = "vstup.txt";
        string vystupniSoubor = "vystup.txt";

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
                writer.WriteLine(analyzer.ziskatslovasoddelovacem());
            }

            Console.WriteLine("Výstup byl úspěšně uložen.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Chyba: {ex.Message}");
        }
    }
}                    
