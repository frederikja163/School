using System;

namespace TicTacToe
{
    public sealed class Player : IMover
    {
        private readonly BoardState _state;

        public Player(BoardState state)
        {
            _state = state;
        }

        public Position GetMove()
        {
            var top = (_state.Height + 1) * 2;
            Console.SetCursorPosition(0, top);
            Console.WriteLine($"Move for player {_state.PlayerCount} ( , )");
            while (true)
            {
                int x;
                Console.SetCursorPosition(19, top);
                while (!TryGetInt(1, _state.Width, out x)) ;
                Console.SetCursorPosition(21, top);
                if (TryGetInt(1, _state.Height, out var y))
                {
                    return new Position(x - 1, y - 1);
                }
            }
        }

        private bool TryGetInt(int min, int max, out int value)
        {
            while (true)
            {
                var c = Console.ReadKey().KeyChar;
                if (char.IsDigit(c))
                {
                    value = c - '0';
                    if (value >= min && value <= max)
                    {
                        return true;
                    }
                }
                else if (c == 'q')
                {
                    value = -1;
                    return false;
                }
            }
        }
    }
}