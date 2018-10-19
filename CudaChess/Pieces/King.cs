using System.Collections.Generic;

using Chess.Utils;

namespace Chess.Pieces {
    class King : Piece {

        public override int value { get { return 4000; }}

        public King(Team team) : base(team) {}

        public override List<Move> Moves(Chessboard board, Cell position)
        {
            var moves = new List<Move>();

            var neighbors = board.GetNeighborCells(position);
            foreach (var cell in neighbors)
            {
               var cs = board.GetCellState(cell, team);
               if (cs != CellState.invalidPos && cs != CellState.ally)  // FIXME: redundant check because GetNeighborCells does not return invalid cells
                  moves.Add(new Move { piece = this, from = position, to = cell });
            }
            
            return moves;
        }

        public override List<Cell> CouldMenace(Chessboard board, Cell point)
        {
            return board.GetNeighborCells(point);
        }

        public override string ToString()
        {
           return "King";
        }
   
   }
}
