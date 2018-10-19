using System;
using System.Collections.Generic;
using System.Linq;

using Chess.Utils;

namespace Chess.Pieces
{
   public abstract class Piece
   { 
      public Team team { get; private set; }
      public abstract int value { get; }

      public Piece(Team tm)
      { 
         team = tm;
      }
      
      /// <summary>
      /// Returns true if Piece in cell position is menacing target cell
      /// </summary>
      /// <param name="board"></param>
      /// <param name="position"></param>
      /// <param name="target"></param>
      public bool IsMenacing(Chessboard board, Cell position, Cell target)
      {
         var moves = CouldMenace(board, position);
         foreach (Cell p in moves)
            if (target.Equals(p)) return true;
         return false;
      }

      /// <summary>
      /// Returns all the menaced cells.
      /// </summary>
      /// <param name="board"></param>
      /// <param name="point">Position of the menacing piece</param>
      public abstract List<Cell> CouldMenace(Chessboard board, Cell point);

      /// <summary>
      /// Returns all moves available to the piece in cell "position"
      /// </summary>
      public abstract List<Move> Moves(Chessboard board, Cell position);


      public override abstract String ToString();


      //public string MovToString(Chessboard board)
      //{
      //   return Moves(board).Select(p => String.Format("{0}{1}", (Char)(p.X + 65), (Char)(p.Y + 49))).Aggregate((c1, c2) => c1 + " " + c2);
      //}

      public static string MovToString(List<Cell> moves)
      {
         return moves.Select(p => String.Format("{0}{1}", (Char)(p.X + 65), (Char)(p.Y + 49))).Aggregate((c1, c2) => c1 + " " + c2);
      }


      protected List<Move> moveStraight(Chessboard board, Cell position, Cell direction)
      {
         var moves = new List<Move>();
         var nP = position.NewWithOffset(direction.X, direction.Y);
         var cs = board.GetCellState(nP, team);
         while (cs != CellState.invalidPos && cs != CellState.ally)
         {
            moves.Add(new Move { piece = this, from = position, to = nP });
            if (cs == CellState.enemy) break;
            nP = nP.NewWithOffset(direction.X, direction.Y);
            cs = board.GetCellState(nP, team);
         }

         return moves;
      }

      protected List<Cell> menaceStraight(Chessboard board, Cell point, Cell direction)
      {
         var menacing = new List<Cell>();

         var nP = point.NewWithOffset(direction.X, direction.Y);
         var cs = board.GetCellState(nP, team);
         while (cs != CellState.invalidPos)
         {
            menacing.Add(nP);
            if (cs == CellState.enemy || cs == CellState.ally) break;
            nP = nP.NewWithOffset(1, 1);
            cs = board.GetCellState(nP, team);
         }

         return menacing;
      }

   }

}
