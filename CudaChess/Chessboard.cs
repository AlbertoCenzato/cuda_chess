using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Chess.Pieces;
using Chess.Utils;

namespace Chess
{
   public enum CellState
   {
      enemy = -1,
      empty,
      ally,
      invalidPos
   }

   public enum Team
   {
      white = 1,
      black
   }
   
   public struct Move
   {
      public Piece piece;
      public Cell from;
      public Cell to;
      public int val;
   }

   public class Chessboard
   {
      private Piece[,] grid;                          //scacchiera
      private List<Cell> white;  //lista dei pezzi in gioco (squadra 1)
      private List<Cell> black;  //  "    "    "    "  "    (squadra 2)
      
      public ulong WMCells { get; private set; }  //celle micacciate dai bianchi
      public ulong BMCells { get; private set; }  //celle minacciate dai neri

      public const int dim = 8;  //dimensione della scacchiera

      /*
      private struct MoveExtended
      {
         public Piece piece;
         public Piece dead;
         public Cell from;
         public Cell to;
         public Team team;    //TODO useless, piece contains the information about the team
      }
      */


      //----------------------------------------------------------------------------------

      public Chessboard()
      {
         white = new List<Cell>();
         black = new List<Cell>();
         WMCells = 0x0000000000FFFF7E;
         BMCells = 0xE7FFFF0000000000;
         grid = new Piece[dim, dim];

         for (int i = 0; i < dim; i++) {
            SetPiece(new Pawn(Team.white), new Cell( i, 1 ));
            SetPiece(new Pawn(Team.black), new Cell(i, 6));
         }

         SetPiece(new Rook  (Team.white), new Cell(0, 0));
         SetPiece(new Knight(Team.white), new Cell(1, 0));
         SetPiece(new Bishop(Team.white), new Cell(2, 0));
         SetPiece(new King  (Team.white), new Cell(3, 0));
         SetPiece(new Queen (Team.white), new Cell(4, 0));
         SetPiece(new Bishop(Team.white), new Cell(5, 0));
         SetPiece(new Knight(Team.white), new Cell(6, 0));
         SetPiece(new Rook  (Team.white), new Cell(7, 0));

         SetPiece(new Rook  (Team.black), new Cell(0, 7));
         SetPiece(new Knight(Team.black), new Cell(1, 7));
         SetPiece(new Bishop(Team.black), new Cell(2, 7));
         SetPiece(new King  (Team.black), new Cell(3, 7));
         SetPiece(new Queen (Team.black), new Cell(4, 7));
         SetPiece(new Bishop(Team.black), new Cell(5, 7));
         SetPiece(new Knight(Team.black), new Cell(6, 7));
         SetPiece(new Rook  (Team.black), new Cell(7, 7));
      }

      //restituisce il pezzo contenuto nella cella p
      public Piece GetPiece(Cell p)
      {
         if (!isOnBoard(p)) throw new IndexOutOfRangeException();
         return grid[p.X, p.Y];
      }

      /// <summary>
      /// Gets the list of pieces of the playing team
      /// </summary>
      public List<Piece> GetTeamPieces(Team team)
      {
         List<Piece> pieces = new List<Piece>();
         var cells = GetTeamCells(team);
         foreach (var cell in cells)
            pieces.Add(GetPiece(cell));

         return pieces;
      }

      /// <summary>
      /// Gets the list of cells occupied by pieces of the team that is playing this round
      /// </summary>
      public List<Cell> GetTeamCells(Team team)
      {
         return team == Team.white ? white : black;
      }

      //valuta lo stato della cella "cell" rispetto alla squadra "team"
      public CellState GetCellState(Cell cell, Team team)
      {
         if (!isOnBoard(cell)) return CellState.invalidPos;
         var piece = GetPiece(cell);
         if (piece == null) return CellState.empty;    //torna 0 se la cella è vuota
         if (piece.team != team) return CellState.enemy;    //torna -1 se è occupata da un pezzo dell'altra squadra
         return CellState.ally;                                         //torna 1 se è occupata da un pezzo della stessa squadra
      }

      /// <summary>
      /// Riceve una posizione e la squadra per la quale la casella è minacciata
      /// </summary>
      /// <param name="pos"></param>
      /// <param name="team"></param>
      public bool IsThreatened(Cell pos, Team team)
      {
         var cells = team == Team.black ? WMCells : BMCells;
         return cells.Contains(pos);
      }

      /// <summary>
      /// Sposta il pezzo piece nella cella newPosition, restituisce l'eventuale 
      /// pezzo mangiato e aggiunge i punti dovuti alla squadra che ha mosso
      /// </summary>
      /// <param name="oldPosition"></param>
      /// <param name="newPosition"></param>
      /// <returns></returns>
      public Piece Move(Cell oldPosition, Cell newPosition)
      {
         // remove dead piece
         var deadPiece = grid[newPosition.X, newPosition.Y];
         if (deadPiece != null) {
            removePiece(newPosition);
            //addPoints(deadPiece);
         }

         var piece = movePiece(oldPosition, newPosition);

         // manage Pawn's unique behavior upon reaching the end of the chessboard
         if (piece is Pawn) {
            if (newPosition.Y == 7 && piece.team == Team.white) {
               removePiece(newPosition);
               piece = InputManager.getSubstitutivePiece(this, newPosition, piece.team);
               SetPiece(piece, newPosition);
            }
            else if (newPosition.Y == 0 && piece.team == Team.black) {
               removePiece(newPosition);
               piece = new Queen(/*this, newPosition,*/ Team.black);   // FIXME: if playing 1vs1 black player can't choose
               SetPiece(piece, newPosition);
            }
         }
         
         updateMenaced();

         return deadPiece;
      }


      /// <summary>
      /// Puts "piece" on the grid and in the team's list
      /// </summary>
      public bool SetPiece(Piece piece, Cell position)
      {
         if (piece == null)
            return false;
         if (piece.team == Team.white)
            white.Add(position); // TODO: change this function, name and semantic are incoherent
         else
            black.Add(position);
         grid[position.X, position.Y] = piece;
         return true;
      }


      public List<Cell> GetNeighborCells(Cell position)
      {
         var neighbors = new List<Cell>();

         var cell = position.NewWithOffset(0, 1);
         if (isOnBoard(cell))
            neighbors.Add(cell);
         cell = position.NewWithOffset(1, 1);
         if (isOnBoard(cell))
            neighbors.Add(cell);
         cell = position.NewWithOffset(1, 0);
         if (isOnBoard(cell))
            neighbors.Add(cell);
         cell = position.NewWithOffset(1, -1);
         if (isOnBoard(cell))
            neighbors.Add(cell);
         cell = position.NewWithOffset(0, -1);
         if (isOnBoard(cell))
            neighbors.Add(cell);
         cell = position.NewWithOffset(-1, -1);
         if (isOnBoard(cell))
            neighbors.Add(cell);
         cell = position.NewWithOffset(-1, 0);
         if (isOnBoard(cell))
            neighbors.Add(cell);
         cell = position.NewWithOffset(-1, 1);
         if (isOnBoard(cell))
            neighbors.Add(cell);

         return neighbors;
      }

      // --------------------------------------------------------------------------------------------------------------



      /// <summary>
      /// Remove piece from chessboard
      /// </summary>
      /// <param name="toRemove"></param>
      private void removePiece(Cell position)
      {
         var piece = grid[position.X, position.Y];
         grid[position.X, position.Y] = null;
         if (piece.team == Team.white) white.Remove(position);
         else black.Remove(position);

         return;
      }


      //verifica che p sia una cella della scacchiera
      private bool isOnBoard(Cell p)
      { return p.Y >= 0 && p.Y < dim && p.X >= 0 && p.X < dim; }


      /// <summary>
      /// Moves a piece and returns it
      /// </summary>
      /// <param name="oldPosition">Piece position</param>
      /// <param name="newPosition">Piece destination</param>
      private Piece movePiece(Cell oldPosition, Cell newPosition)
      {
         // move piece to new position
         var piece = grid[oldPosition.X, oldPosition.Y];
         grid[newPosition.X, newPosition.Y] = piece;
         grid[oldPosition.X, oldPosition.Y] = null;

         // update set of occupied cells
         var team = piece.team == Team.white ? white : black;
         team.Remove(oldPosition); // remove oldPosition
         team.Add(newPosition); // add    newPosition

         return piece;
      }



      /// <summary>
      /// Updates cells threatened by both teams
      /// </summary>
      private void updateMenaced()
      {
         updateThreatened(Team.white);
         updateThreatened(Team.black);
      }

      /// <summary>
      /// Updates cells threatened by one team
      /// </summary>
      /// <param name="team"></param>
      private void updateThreatened(Team team)
      {
         ulong threatened  = 0x0000000000000000;
         var cells = GetTeamCells(team);
         foreach (var position in cells) {
            var piece = grid[position.X, position.Y];
            var menacing = piece.CouldMenace(this, position);
            foreach (Cell pt in menacing)
               threatened.Insert(pt);
         }

         if (team == Team.white) WMCells = threatened ;
         else BMCells = threatened ;
      }

      
      /*
      private void addPoints(Piece deadPiece)
      {
         if (deadPiece.team == Team.white) scoreBlack += deadPiece.value;
         else scoreWhite += deadPiece.value;
         return;
      }

      private void subPoints(Piece piece)
      {
         if (piece == null) return;
         if (piece.team == Team.white) scoreBlack -= piece.value;
         else scoreWhite -= piece.value;
         return;
      }
      */

   }
}
