using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TreDe.TextFileParser
{
    public class ItemDescription
    {
        public Dictionary<string, Token> assignments = new Dictionary<string, Token>();
        public List<string> flags = new List<string>();
    }
}
