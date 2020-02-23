using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreDe.TextFileParser
{
    public static class ReadFromRaw
    {
        public static void Read()
        {
            string filename = @"C: \Users\kroll\source\repos\TreDe\TreDe\Items.txt";
            string source = File.ReadAllText(filename);


            Lexer l = new Lexer(source);

            Parser p = new Parser(l);
            ItemDescription IDesc = p.Run();

        }
    }
}
