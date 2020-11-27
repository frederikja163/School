using System;

namespace TicTacToe
{
    public sealed class TicTacToe
    {
        private const int Width = 3, Height = 3, RowCount = 3;
        private readonly BoardState _state;
        private readonly IBoardRenderer _renderer;
        private readonly IMover[] _movers;
        private readonly char[] _symbols;

        public TicTacToe()
        {
            _symbols = new[] {'X', 'O'};
            _state = new BoardState(Width, Height, RowCount, _symbols.Length);
            _renderer = new BoardConsoleRenderer(_symbols, _state);
            _movers = new[] {new Player(_state), new Player(_state)};
        }

        public void Run()
        {
            var turn = -1;
            _state.Clear();
            int winner;
            while (!_state.CheckWinner(out winner))
            {
                turn++;
                int playerIndex = turn % _movers.Length;
                var player = _movers[playerIndex];

                Position move;
                do
                {
                    move = player.GetMove();
                } while (!_state.Set(move, playerIndex));
            }
            Console.SetCursorPosition(0, (Height + 1) * 2);
            Console.WriteLine($"{_symbols[turn % _symbols.Length]} is the winner!                                                 ");
        }
    }
}