using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class BoardConsoleRenderer : IBoardRenderer<Player>
    {
        private readonly int _width;
        private readonly int _height;

        public BoardConsoleRenderer(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public void Set(Position pos, Player player)
        {
            Console.SetCursorPosition((pos.X + 1) * 2, (_height - pos.Y - 1) * 2 + 1);
            Console.Write(player.Symbol);
        }

        public void Clear()
        {
            var sb = new StringBuilder();

            void AddRow()
            {
                for (int x = 0; x < _width + 1; x++) sb.Append("-+");
                sb.AppendLine();
            }

            AddRow();
            for (int y = 0; y < _height; y++)
            {
                sb.Append(_height - y);
                sb.Append("|");
                for (int x = 0; x < _width; x++)
                {
                    sb.Append(" |");
                }
                sb.AppendLine();
                AddRow();
            }
            sb.Append(" |");
            for (int x = 0; x < _width; x++)
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