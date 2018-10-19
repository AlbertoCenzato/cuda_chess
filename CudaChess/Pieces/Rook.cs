using System.Collections.Generic;

using Chess.Utils;

namespace Chess.Pieces
{
    class Rook : Piece 
    {    
        public override int value {get { return 500; }}

        public Rook(Team team) : base(team) {}

        public override List<Move> Moves(Chessboard board, Cell position)
        {
            var moves = new List<Move>();
            moves.AddRange(moveStraight(board, position, new Cell( 0,  1)));
            moves.AddRange(moveStraight(board, position, new Cell( 1,  0)));
            moves.AddRange(moveStraight(board, position, new Cell( 0, -1)));
            moves.AddRange(moveStraight(board, position, new Cell(-1,  0)));

            return moves;
        }

        public override List<Cell> CouldMenace(Chessboard board, Cell point)
        {
            var menacing = new List<Cell>();
            menacing.AddRange(menaceStraight(board, point, new Cell( 0,  1)));
            menacing.AddRange(menaceStraight(board, point, new Cell( 1,  0)));
            menacing.AddRange(menaceStraight(board, point, new Cell( 0, -1)));
            menacing.AddRange(menaceStraight(board, point, new Cell(-1,  0)));

            return menacing;
        }

        public override string ToString()
        {
            return "Rook";
        }
    }
}
