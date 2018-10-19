using System.Collections.Generic;

using Chess.Utils;

//using Emgu.CV.Structure;

namespace Chess
{
    public static class ExtensionMthd
    {
        //--- ulong ---
        public static List<Cell> ToPointList(this ulong l)
        {
            List<Cell> points = new List<Cell>();
            ulong mask = 0x0000000000000001;
            for (int i = 0; i < 64; i++)
            {
                mask = mask << i;
                if ((l & mask) != 0)
                {
                    int x = i % 8;
                    int y = i / 8;
                    points.Add(new Cell(x, y));
                }
            }


            return points;
        }

        public static ulong Insert(this ref ulong l, ulong newElem)
        {
            return l |= newElem;
        }

        public static ulong Insert(this ulong l, Cell newElem)
        {
            return l.Insert(newElem.ToULong());
        }

        public static ulong Remove(this ulong l, ulong oldElement)
        {
            return l &= ~oldElement;
        }

        public static ulong Remove(this ulong l, Cell oldElem)
        {
           return l.Remove(oldElem.ToULong());
        }

        public static bool Contains(this ulong l, ulong cell)
        {
           return (l & cell) != 0;
        }

        public static bool Contains(this ulong l, Cell cell)
        {
           return l.Contains(cell.ToULong());
        }

        //--- List<Point> ---
        public static string MyToString(this List<Cell> list)
        {
            string s = "";
            foreach (Cell p in list)
            {
                s += p.MyToString() + " ";
            }
            return s;
        }

      /*
        //--- LineSegment2D ---

        //torna l'angolo di inclinazione della retta (in gradi) rispetto all'asse x
        public static double GetAngleDeg(this LineSegment2D line) {
            var rad = line.GetAngleRad();
            return 180 * rad / Math.PI; ;
        }

        public static double GetAngleRad(this LineSegment2D line) {
            var m = GetM(line);
            if (Double.IsPositiveInfinity(m))
                return Math.PI / 2;
            return Math.Atan(m);
        }

        //returns the inclination coeff. m of the line (y = mx + q)
        public static double GetM(this LineSegment2D line) {
            double dx = (line.P1.X - line.P2.X);
            if (dx == 0)
                return Double.PositiveInfinity;
            return (line.P1.Y - line.P2.Y) / dx;
        }

        public static double GetOrthDeg(this LineSegment2D line) {
            var deg = line.GetAngleDeg();
            if (deg <= 0)
                return deg + 90;
            return deg - 90;
        }

        public static double GetOrthRad(this LineSegment2D line)
        {
            var rad = line.GetAngleRad();
            if (rad <= 0)
                return rad + Math.PI/2;
            return rad - Math.PI / 2;
        }
        */
    }
}
