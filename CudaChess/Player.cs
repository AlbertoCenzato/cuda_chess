using System.Collections.Generic;

using Chess.Utils;

namespace Chess
{
   class User : AI.Player
   {
      Renderer.GameRenderer renderer;

      public User(Team team, Renderer.GameRenderer gRenderer) : base(team)
      {
         renderer = gRenderer;
      }


      public override Move NextMove(Chessboard board)
      {
         var oldPosition = choosePiece(board);
         var piece = board.GetPiece(oldPosition);
         var moves = piece.Moves(board, oldPosition);

         var targets = new List<Cell>();
         foreach (var m in moves)
            targets.Add(m.to);

         renderer.DrawChessboard(board, team, oldPosition, targets);
         
         var newPosition = chooseTargetCell(board, targets);

         return new Move { piece=piece, from=oldPosition, to=newPosition };
      }

      // -----------------------------------------------------------------------------------

      private Cell choosePiece(Chessboard board)
      {
         Cell position = new Cell();
         for (bool ok = false; !ok;) {
            position = InputManager.getPosition();
            var piece = board.GetPiece(position);
            if (piece == null) System.Console.WriteLine("\nIn questa casella non è persente un pezzo, indicarne un'altra");
            else if (piece.team != team) System.Console.WriteLine("\nNon puoi muovere i pezzi dell'altra squadra!");
            else if (piece.Moves(board, position).Count == 0) System.Console.WriteLine("\nIl pezzo {0} non può muoversi", piece.ToString());
            else
               ok = true;
         }
         return position;
      }

      private Cell chooseTargetCell(Chessboard board, List<Cell> moves)
      {
         System.Console.WriteLine("\nPuò muoversi in: {0}", moves.MyToString());

         bool done = false;
         Cell newPosition;
         do {
            System.Console.WriteLine("Dove vuoi muoverlo?");
            newPosition = InputManager.getPosition();
            if (moves.Exists(p => (p.X == newPosition.X) && (p.Y == newPosition.Y))) 
               done = true;
            else
               System.Console.WriteLine("\nMossa non valida");
         } while (!done);

         return newPosition;
      }

   }
}
