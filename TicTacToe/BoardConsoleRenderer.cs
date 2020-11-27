using System;
using System.Text;

namespace TicTacToe
{
    public class BoardConsoleRenderer : IBoardRenderer
    {
        private readonly BoardState _state;
        private readonly char[] _players;

        public BoardConsoleRenderer(char[] players, BoardState state)
        {
            _players = players;
            _state = state;
            
            _state.OnSet += Set;
            _state.OnClear = Clear;
        }

        private void Set(Position pos, int player)
        {
            Console.SetCursorPosition((pos.X + 1) * 2, (_state.Height - pos.Y - 1) * 2 + 1);
            Console.Write(_players[player]);
        }

        private void Clear()
        {
            var sb = new StringBuilder();

            void AddRow()
            {
                for (int x = 0; x < _state.Width + 1; x++) sb.Append("-+");
                sb.AppendLine();
            }

            AddRow();
            for (int y = 0; y < _state.Height; y++)
            {
                sb.Append(_state.Height - y);
                sb.Append("|");
                for (int x = 0; x < _state.Width; x++)
                {
                    sb.Append(" |");
                }
                sb.AppendLine();
                AddRow();
            }
            sb.Append(" |");
            for (int x = 0; x < _state.Width; x++)
            {
                sb.Append(x + 1);
                sb.Append("|");
            }
            sb.AppendLine();
            AddRow();
            Console.WriteLine(sb);
        }
    }
}