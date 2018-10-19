using System.Collections.Generic;

using Chess.Utils;

namespace Chess.Pieces
{
    public class Knight : Piece
    {
        public override int value { get { return 300; } }

        public Knight(Team team) : base(team) { }

        public override List<Move> Moves(Chessboard board, Cell position)
        {
            var moves = new List<Move>();

            var nP = position.NewWithOffset(-2, -1);
            var cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos && cs != CellState.ally)
                moves.Add(new Move { piece = this, from = position, to = nP });

            nP = position.NewWithOffset(-1, -2);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos && cs != CellState.ally)
                moves.Add(new Move { piece = this, from = position, to = nP });

            nP = position.NewWithOffset(+2, -1);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos && cs != CellState.ally)
                moves.Add(new Move { piece = this, from = position, to = nP });

            nP = position.NewWithOffset(-1, +2);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos && cs != CellState.ally)
                moves.Add(new Move { piece = this, from = position, to = nP });

            nP = position.NewWithOffset(+1, -2);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos && cs != CellState.ally)
                moves.Add(new Move { piece = this, from = position, to = nP });

            nP = position.NewWithOffset(-2, +1);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos && cs != CellState.ally)
                moves.Add(new Move { piece = this, from = position, to = nP });

            nP = position.NewWithOffset(+1, +2);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos && cs != CellState.ally)
                moves.Add(new Move { piece = this, from = position, to = nP });

            nP = position.NewWithOffset(+2, +1);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos && cs != CellState.ally)
                moves.Add(new Move { piece = this, from = position, to = nP });

            return moves;
        }

        public override List<Cell> CouldMenace(Chessboard board, Cell point)
        {
            var menacing = new List<Cell>();

            var nP = point.NewWithOffset(-2, -1);
            var cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos)
                menacing.Add(nP);

            nP = point.NewWithOffset(-1, -2);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos)
                menacing.Add(nP);

            nP = point.NewWithOffset(+2, -1);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos)
                menacing.Add(nP);

            nP = point.NewWithOffset(-1, +2);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos)
                menacing.Add(nP);

            nP = point.NewWithOffset(+1, -2);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos)
                menacing.Add(nP);

            nP = point.NewWithOffset(-2, +1);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos)
                menacing.Add(nP);

            nP = point.NewWithOffset(+1, +2);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos)
                menacing.Add(nP);

            nP = point.NewWithOffset(+2, +1);
            cs = board.GetCellState(nP, team);
            if (cs != CellState.invalidPos)
                menacing.Add(nP);

            return menacing;
        }

      public override string ToString()
      {
         return "Knight";
      }
   }
}
