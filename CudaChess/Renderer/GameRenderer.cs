using System;
using System.Collections.Generic;

using Chess.Pieces;
using Chess.Utils;

namespace Chess.Renderer
{
   /**
    * Simple console graphic engine 
    */
   class GameRenderer
   {

      RenderState state;        //stato in cui si trova il gioco
      const string hor = "\u2500"; //trattino orizzontale
      const string tl  = "\u250c"; //top left corner
      const string tr  = "\u2510"; //top right corner
      const string bl  = "\u2514"; //bottom left corner
      const string br  = "\u2518"; //bottom right corner
      const string cd  = "\u252c"; //central down

      private static GameRenderer singleton = null;

      //---costruttore---
      private GameRenderer()
      {
         state = RenderState.intro;
         Console.Title = "ChessMaster";
      }

      static readonly Action<Int32> a = null;
      static readonly Func<Int32, String, DateTime> b = null;

      /*
      private static Lazy<GameRenderer> lazyCurrent = new Lazy<GameRenderer>(() => new GameRenderer());
      public static GameRenderer Current => lazyCurrent.Value;
      */

      private static GameRenderer instance;
      public  static GameRenderer Instance => instance ?? (instance = new GameRenderer());

      /*
      public static GameRenderer GetRenderer()
      {
         if (singleton == null)
            singleton = new GameRenderer();
         
         return singleton;
      }
      */

      public void DrawChessboard(Chessboard board, Team team)
      {
         var p = new Cell(8, 8);
         List<Cell> lP = null;
         DrawChessboard(board, team, p, lP);
      }

      public void DrawChessboard(Chessboard board, Team team, Cell selected, List<Cell> avMov)
      {
         Console.Clear();
         Console.WriteLine($"\nTurno dei {0}: che pezzo vuoi muovere?\n", team == Team.white ? "bianchi" : "neri");
         
         string line = " " + tl;
         for (int i = 0; i < 8; i++)
            line += hor + hor + hor + hor + hor + cd;
         line = line.Substring(0, line.Length - 1) + tr + "\n";
         Write(line, ConsoleColor.DarkGray, ConsoleColor.Gray);

         for (int i = 7; i >= 0; i--)
         {
            Write(" |     |     |     |     |     |     |     |     |\n", ConsoleColor.DarkGray, ConsoleColor.Gray);
            Write((i + 1).ToString(), ConsoleColor.White, ConsoleColor.Black);
            for (int j = 0; j < 8; j++)
            {
               String s = " ";
               var position = new Cell(j, i);
               var pz = board.GetPiece(position);
               var color = ConsoleColor.Gray;
               var backColor = ConsoleColor.Gray;
               if (pz != null)
               {
                  if (position.Equals(selected)) backColor = ConsoleColor.DarkRed;
                  if (pz is King) s = "+";
                  else if (pz is Queen) s = "*";
                  else s = pz.ToString().Substring(0, 1);
                  if (pz.team == Team.black) color = ConsoleColor.Black;
                  else color = ConsoleColor.White;
               }
               Write("| ", ConsoleColor.DarkGray, ConsoleColor.Gray);
               Write(" {0} ", s, color, backColor);
               Write(" ", ConsoleColor.DarkGray, ConsoleColor.Gray);
            }
            Write("|\n", ConsoleColor.DarkGray, ConsoleColor.Gray);
            Write(" |     |     |     |     |     |     |     |     |\n", ConsoleColor.DarkGray, ConsoleColor.Gray);
            Write("  _____ _____ _____ _____ _____ _____ _____ _____ \n", ConsoleColor.DarkGray, ConsoleColor.Gray);
         }
         Console.WriteLine("    A     B     C     D     E     F     G     H");
         Console.WriteLine("\n");
         return;
      }

      public static void Write(String s, ConsoleColor foreground, ConsoleColor background)
      {
         Console.BackgroundColor = background;
         Console.ForegroundColor = foreground;
         Console.Write(s);
         Console.ForegroundColor = ConsoleColor.White;
         Console.BackgroundColor = ConsoleColor.Black;
         return;
      }
      public static void Write(String s, String par, ConsoleColor c, ConsoleColor b)
      {
         Console.BackgroundColor = b;
         Console.ForegroundColor = c;
         Console.Write(s, par);
         Console.ForegroundColor = ConsoleColor.White;
         Console.BackgroundColor = ConsoleColor.Black;
         return;
      }

   }

   enum RenderState
   {
      intro,
      playing,
      win
   }
}
