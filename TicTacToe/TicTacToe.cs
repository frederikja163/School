using System;

namespace TicTacToe
{
    public sealed class TicTacToe
    {
        private const int Width = 3, Height = 3, RowCount = 3;
        private readonly BoardState<Player> _boardState;
        private readonly Player[] _players;

        public TicTacToe()
        {
            _players = new[] {new Player('X', Width, Height),
                new Player('O', Width, Height)};
            
            _boardState = new BoardState<Player>(Width, Height, RowCount, _players, new BoardConsoleRenderer(Width, Height));
        }

        public void Run()
        {
            var turn = 0;
            _boardState.Clear();
            Player winner;
            while (!_boardState.CheckWinner(out winner))
            {
                int playerIndex = turn % _players.Length;
                var player = _players[playerIndex];

                Position move;
                do
                {
                    move = player.GetMove();
                } while (!_boardState.Set(move, playerIndex));

                turn++;
            }
            Console.SetCursorPosition(0, (Height + 1) * 2);
            Console.WriteLine($"{winner.Symbol} is the winner!                                                 ");
        }
    }
}