namespace TicTacToe
{
    public interface IBoardRenderer<in TPlayer> where TPlayer : class, IPlayer
    {
        void Set(Position pos, TPlayer player);
        void Clear();
    }
}