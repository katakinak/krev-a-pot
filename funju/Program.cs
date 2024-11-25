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
        catch { FileNotFoundException}
        {
            Console.WriteLine($"soubor {'soubor'} neexistuje");
        }
    
                catch { Exception ex}
        {
            Console.WriteLine($"chyba při zpracování souboru: {ex.Message}");
        }
    }
    public Dictionary<string, int> ziskatfrekvencislov;
    {
        return new Dictionary<string, int>(slova);
    }
    
}