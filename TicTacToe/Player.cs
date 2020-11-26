using System;

namespace TicTacToe
{
    public sealed class Player
    {
        private readonly int _boardWidth;
        private readonly int _boardHeight;

        public Player(char symbol, int boardWidth, int boardHeight)
        {
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
            Symbol = symbol;
        }

        public char Symbol { get; }

        public Position GetMove()
        {
            var top = (_boardHeight + 1) * 2;
            Console.SetCursorPosition(0, top);
            Console.WriteLine($"Move for player {Symbol} ( , )");
            while (true)
            {
                int x;
                Console.SetCursorPosition(19, top);
                while (!TryGetInt(1, _boardWidth, out x)) ;
                Console.SetCursorPosition(21, top);
                if (TryGetInt(1, _boardHeight, out var y))
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