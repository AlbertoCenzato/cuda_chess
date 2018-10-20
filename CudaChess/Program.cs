using System;

using Chess.Renderer;
using Chess.Vision;


namespace Chess
{
   class Program
   {
      static void Main()
      {
         var gRenderer  = GameRenderer.Instance;
         var board      = new Chessboard();
         var realPlayer = new User(Team.white, gRenderer);
         var pcPlayer   = new AI.CudaBreadthFirstSearchAI(Team.black);
         AI.Player player = realPlayer;

         var newmove = pcPlayer.NextMove(board);
         //VisualChessboardAnalyzer.start();
         //Console.ReadKey();

         try {
            while (!isMatchEnded()) {
               var currTeam = player.team;
               gRenderer.DrawChessboard(board, currTeam);
               var move = player.NextMove(board);
               var deadPiece = board.Move(move.from, move.to);
               gRenderer.DrawChessboard(board, currTeam);

               if (player == realPlayer)
               {
                  Console.WriteLine("\nPress any key to pass turn.");
                  Console.ReadKey();
                  player = pcPlayer;
               }
               else
                  player = realPlayer;
            }
            Console.WriteLine("             TEAM WON!");//, board.scoreWhite > board.scoreBlack ? "WHITE" : "BLACK");
            Console.Read();
         }
         catch (Exception e) {
            var message = e.Message;
            var trace   = e.StackTrace;
            System.Console.Write("{0}\n\n{1}", message, trace);
            Console.Read();
         }

      }

      static bool isMatchEnded()
      {
         return false;
      }
   }
   
}
