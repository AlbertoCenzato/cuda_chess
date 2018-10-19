namespace Chess.AI
{
   abstract class Agent
   {
      public readonly Team team;

      public Agent(Team team)
      {
         this.team = team;
      }

      public abstract Move NextMove(Chessboard board);
   }
}
