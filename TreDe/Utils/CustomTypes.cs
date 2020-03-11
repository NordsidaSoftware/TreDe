using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreDe
{

    [Serializable]
    public struct FPoint
        {
            public float X;
            public float Y;

            public FPoint(float X, float Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }
    [Serializable]
    public struct Point3
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Point3 (int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public override string ToString()
        {
            return "(" + X.ToString() + "," + Y.ToString() + "," + Z.ToString() + ")";
        }
    }
}
