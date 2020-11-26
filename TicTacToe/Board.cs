#nullable enable
using System;
using System.Text;

namespace TicTacToe
{
    public sealed class Board
    {
        private readonly Player?[,] _board;
        private readonly int _rowCount;
        private readonly Player[] _players;

        public Board(int width, int height, int rowCount, Player[] players)
        {
            Width = width;
            Height = height;
            _rowCount = rowCount;
            _players = players;
            _board = new Player?[Width, Height];
            Clear();
        }
        
        public int Width { get; }
        public int Height { get; }

        public bool CheckWinner(out Player? player)
        {
            for (int y = 0; y <= Height - _rowCount; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    //Vertical check
                    player = _board[x, y];
                    for (int i = 1; i < _rowCount && player != null; i++)
                    {
                        if (player != _board[x, y + i])
                        {
                            break;
                        }

                        if (i == _rowCount - 1)
                        {
                            return true;
                        }
                    }
                }
            }
            for (int x = 0; x <= Width - _rowCount; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    //Horizontal check
                    player = _board[x, y];
                    for (int i = 1; i < _rowCount && player != null; i++)
                    {
                        if (player != _board[x + i, y])
                        {
                            break;
                        }

                        if (i == _rowCount - 1)
                        {
                            return true;
                        }
                    }
                }
                
                for (int y = 0; y <= Height - _rowCount; y++)
                {
                    //Diagonally up check
                    player = _board[x, y];
                    for (int i = 1; i < _rowCount && player != null; i++)
                    {
                        if (player != _board[x + i, y + i])
                        {
                            break;
                        }

                        if (i == _rowCount - 1)
                        {
                            return true;
                        }
                    }
                }
                for (int y = _rowCount - 1; y < Height; y++)
                {
                    //Diagonally down check
                    player = _board[x, y];
                    for (int i = 1; i < _rowCount && player != null; i++)
                    {
                        if (player != _board[x + i, y - i])
                        {
                            break;
                        }

                        if (i == _rowCount - 1)
                        {
                            return true;
                        }
                    }
                }
            }

            player = null;
            return false;
        }

        public void Clear()
        {
            var sb = new StringBuilder();

            void AddRow()
            {
                for (int x = 0; x < Width + 1; x++) sb.Append("-+");
                sb.AppendLine();
            }

            AddRow();
            for (int y = 0; y < Height; y++)
            {
                sb.Append(Height - y);
                sb.Append("|");
                for (int x = 0; x < Width; x++)
                {
                    _board[x, y] = null;
                    sb.Append(" |");
                }
                sb.AppendLine();
                AddRow();
            }
            sb.Append(" |");
            for (int x = 0; x < Width; x++)
            {
                sb.Append(x + 1);
                sb.Append("|");
            }
            sb.AppendLine();
            AddRow();
            Console.WriteLine(sb);
        }

        public bool Set(Position pos, int player)
        {
            if (!IsClear(pos))
            {
                return false;
            }

            var p = _players[player];
            _board[pos.X, pos.Y] = p;
            Console.SetCursorPosition((pos.X + 1) * 2, (Height - pos.Y - 1) * 2 + 1);
            Console.Write(p.Symbol);
            return true;
        }

        public bool IsClear(Position pos)
        {
            return pos.X >= 0 && pos.X < Width &&
                   pos.Y >= 0 && pos.Y < Height &&
                   _board[pos.X, pos.Y] == null;
        }
    }
}