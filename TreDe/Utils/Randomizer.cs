using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreDe
{
    public static class Randomizer
    {
        public static Random rnd;

        public static void Initialize (int seed)
        {
            rnd = new Random(seed);
        } 
    }
}
