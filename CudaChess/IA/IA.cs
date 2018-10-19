using System;
using System.Collections.Generic;

using Chess.Pieces;
using Chess.Utils;

namespace Chess.AI
{
   class SimpleDepthFirstSearchAI : Agent
   {
      private Stack<(Move move, Piece piece)> moveStack = new Stack<(Move, Piece)>();
      public const int RecursionLevel = 2;


      public SimpleDepthFirstSearchAI(Team team) : base(team) { }


      public override Move NextMove(Chessboard board) {
         var moves  = new List<Move>();
         var pieces = board.GetTeamCells(team);

         //make a list with all available moves for the team
         foreach (var position in pieces) {
             var piece  = board.GetPiece(position);
             moves.AddRange(piece.Moves(board, position));
         }
         
         //give a value to each move
         for (int j = 0; j < moves.Count; j++) {
             var mov = moves[j];
             mov.val = simulate(board, mov, team, RecursionLevel);
             moves[j] = mov;
         }

         moves.Sort(HighestScore);
         return moves[0];
      }
      

      
      private int simulate(Chessboard board, Move mov, Team currentTeam, int iter)
      {
         moveStackPush(board, mov);

         int bestMoveScore = -Int32.MaxValue;
         if (iter > 0) {
            iter--;

            // get all available moves for the enemy
            var enemyTeam = currentTeam == Team.white ? Team.black : Team.white;
            var enemyCells = board.GetTeamCells(enemyTeam);
            var enemyMoves = new List<Move>();
            for (int i = 0; i < enemyCells.Count; i++) {
               var cell = enemyCells[i];
               var piece = board.GetPiece(cell);
               enemyMoves.AddRange(piece.Moves(board, cell));
            }

            // choose move for current team
            for (int i = 0; i < enemyMoves.Count; i++) {
               var move = enemyMoves[i];
               moveStackPush(board, move);

               var cells = board.GetTeamCells(currentTeam);
               for (int j = 0; j < cells.Count; j++) {
                  var cell = cells[j];
                  var piece = board.GetPiece(cell);
                  var moves = piece.Moves(board, cell);
                  for (int k = 0; k < moves.Count; k++)
                  {   // recursively compute enemy player's score obtained with 
                     var m = moves[k];
                     int score = simulate(board, m, currentTeam, iter);
                     if (score > bestMoveScore)
                        bestMoveScore = score;
                  }
               }

               moveStackPop(board);
            }
         }
         else {
            bestMoveScore = giveScore(board, currentTeam);
         }

         moveStackPop(board);

         return bestMoveScore;
      }

      
      private void moveStackPush(Chessboard board, Move mov)
      {
         var deadPiece = board.Move(mov.from, mov.to);
         moveStack.Push((mov, deadPiece));
      }

      private void moveStackPop(Chessboard board)
      {
         var (mov, deadPiece) = moveStack.Pop();
         
         board.Move(mov.to, mov.from);
         board.SetPiece(deadPiece, mov.to);
      }
      
      
      private int giveScore(Chessboard board, Team team) {
          int score = 0;
          var positions = board.GetTeamCells(team);
          //valuta ogni pezzo della squadra corrente ancora in gioco
          foreach (var position in positions) {
              var p = board.GetPiece(position);
              score += p.value;
              //se i pedoni avanzano guadagnano più punti
              //TODO: i pedoni dovrebbero stare circa su una riga unica
              //TODO: i pedoni non dovrebbero stare uno dietro l'altro
              if (p is Pawn)
                  score += team == Team.white ? position.Y*10 : (7 - position.Y)*10;
              //il cavallo è incoraggiato ad occupare la parte centrale della scacchiera
              //TODO: aggiungere alfiere e torre
              if (p is Knight && position.X >= 2 && position.X <= 5 && position.Y >= 2 && position.Y <= 5)
                  score += 150;
              //se il pezzo è minacciato perde punti
              if(board.IsThreatened(position, team))
                  score -= p.value;
          }
      
          //aggiunta punti per ogni casella minacciata
          ulong threatenedCells = team == Team.white ? board.WMCells : board.BMCells;
          var tCells = threatenedCells.ToPointList();
          foreach (Cell pt in tCells) {
              var piece = board.GetPiece(pt);
              //50 punti per casella vuota
              if(piece == null)
                  score += 50;
              //metà del valore del pezzo se difendo un pezzo
              else if(piece.team == team)
                  score += (int)(piece.value/2);
              //il valore del pezzo avversario, se ne minaccio uno
              else
                  score += piece.value;
          }
          return score;
      }
      

      private static int HighestScore(Move x, Move y)
      {
         if (x.val < y.val)
            return -1;
         if (x.val > y.val)
            return 1;
         return 0;
      }
   }

   //enum da eliminare, ma l'idea della gerarchia va tenuta...
   //solo che servono molti più livelli intermedi. es: minaccia 1, 2, 3 va bene ma
   //il valore della mossa dipende anche da che pezzi minaccia non solo da quanti ne 
   //minaccia. vale lo stesso anche per difende e mangia
   enum MovValue {
       minacciato  = -20,
       minaccia    = 2,
       mangia,
       difende,
       scacco      = 10
   }

}