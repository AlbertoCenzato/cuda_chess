namespace Chess.Utils
{
   // TODO: improve this struct to be used as a true Chessboard cell 
   // with a letter as first index and a number a second index. 
   // The "letter-number" usage should flawlessly integrate with 
   // the cartesian point usage.
   public struct Cell
   {
      public int X;
      public int Y;

      public Cell(int x, int y)
      {
         X = x;
         Y = y;
      }

      public Cell NewWithOffset(int dx, int dy)
      {
         return new Cell(this.X + dx, this.Y + dy);
      }

      public ulong ToULong()
      {
         return 1uL << this.Y * 8 + this.X;
      }

      public string MyToString()
      {
         return (char)(this.X + 65) + (this.Y + 1).ToString();
      }
   }
}
