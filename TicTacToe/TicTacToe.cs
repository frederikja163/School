using System;

namespace TicTacToe
{
    public sealed class TicTacToe
    {
        private const int Width = 3, Height = 3, RowCount = 3;
        private readonly Board _board;
        private readonly Player[] _players;

        public TicTacToe()
        {
            _players = new[] {new Player('X', Width, Height),
                new Player('O', Width, Height)};
            
            _board = new Board(Width, Height, RowCount, _players);
        }

        public void Run()
        {
            var turn = 0;
            Player winner;
            while (!_board.CheckWinner(out winner))
            {
                int playerIndex = turn % _players.Length;
                var player = _players[playerIndex];

                Position move;
                do
                {
                    move = player.GetMove();
                } while (!_board.Set(move, playerIndex));

                turn++;
            }
            Console.SetCursorPosition(0, (Height + 1) * 2);
            Console.WriteLine($"{winner.Symbol} is the winner!                                                 ");
        }
    }
}