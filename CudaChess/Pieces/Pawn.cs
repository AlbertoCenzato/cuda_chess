using System.Collections.Generic;

using Chess.Utils;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        public override int value { get { return 100; }}

        public Pawn(Team team) : base(team) {}

        public override List<Move> Moves(Chessboard board, Cell position)
        {
            var moves = new List<Move>();
            int fwd = team == Team.white ? 1 : -1;

            var nP = position.NewWithOffset(-1, fwd);
            if (board.GetCellState(nP, team) == CellState.enemy)
                moves.Add(new Move { piece = this, from = position, to = nP });

            nP = position.NewWithOffset(1, fwd);
            if (board.GetCellState(nP, team) == CellState.enemy)
                moves.Add(new Move { piece = this, from = position, to = nP });

            nP = position.NewWithOffset(0, fwd);
            if (board.GetCellState(nP, team) == CellState.empty)
                moves.Add(new Move { piece = this, from = position, to = nP });

            if (position.Y == 1 || position.Y == Chessboard.dim - 2)
            {
                nP = position.NewWithOffset(0, fwd * 2);
                if (board.GetCellState(nP, team) == CellState.empty && board.GetCellState(position.NewWithOffset(0, fwd), team) == CellState.empty)
                    moves.Add(new Move { piece = this, from = position, to = nP });
            }

            return moves;
        }

        public override List<Cell> CouldMenace(Chessboard board, Cell point)
        {
            var menacing = new List<Cell>();
            int fwd = team == Team.white ? 1 : -1;

            var nP = point.NewWithOffset(-1, fwd);
            if (board.GetCellState(nP, team) != CellState.invalidPos)
                menacing.Add(nP);

            nP = point.NewWithOffset(1, fwd);
            if (board.GetCellState(nP, team) != CellState.invalidPos)
                menacing.Add(nP);

            return menacing;
        }

      public override string ToString()
      {
         return "Pawn";
      }
   }
}
