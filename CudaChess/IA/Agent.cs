namespace Chess.AI
{
   abstract class Player
   {
      public readonly Team team;

      public Player(Team team)
      {
         this.team = team;
      }

      public abstract Move NextMove(Chessboard board);
   }
}
