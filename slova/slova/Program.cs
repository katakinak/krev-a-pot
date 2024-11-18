// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

class TextAnalyzer : StreamReader;

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



    }
       
}