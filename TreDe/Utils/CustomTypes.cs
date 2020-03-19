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

    public enum  Directions { FORWARD, BACKWARD, LEFT, RIGHT, UP, DOWN }
    [Serializable]
    public struct Point3
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        
        public static Point3 Forward { get { return new Point3(0, 0, 1); } }
        public static Point3 Backward { get { return new Point3(0, 0, -1); } }
        public static Point3 Left { get { return new Point3(-1, 0, 0); } }
        public static Point3 Right { get { return new Point3(1, 0, 0); } }
        public static Point3 Up { get { return new Point3(0, 1, 0); } }
        public static Point3 Down { get { return new Point3(0, -1, 0); } }


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
