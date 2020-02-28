using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TreDe.TextFileParser
{
    public static class ReadFromRaw
    {
        public static void Read()
        {
            string filename = @"C: \Users\kroll\source\repos\TreDe\TreDe\Items.txt";
            string[] rawsource = File.ReadAllLines(filename);
            StringBuilder source = new StringBuilder();
            // strip all full line comments from source :
            foreach (string s in rawsource)
            { if (!s.Trim().StartsWith("#")) { source.Append(s); } }

            Lexer l = new Lexer(source.ToString());

            Parser p = new Parser(l);
            List<ItemDescription> ID = p.ReadSource();

        }
    }
}
