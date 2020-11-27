using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public sealed class BoardState<TPlayer> where TPlayer : class, IPlayer
    {
        private readonly TPlayer?[,] _board;
        private readonly int _rowCount;
        private readonly TPlayer[] _players;
        private readonly IBoardRenderer<TPlayer> _renderer;

        public BoardState(int width, int height, int rowCount, TPlayer[] players, IBoardRenderer<TPlayer> renderer)
        {
            Width = width;
            Height = height;
            _rowCount = rowCount;
            _players = players;
            _renderer = renderer;
            _board = new TPlayer?[Width, Height];
        }
        
        public int Width { get; }
        public int Height { get; }

        public bool CheckWinner(out TPlayer? player)
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
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _board[x, y] = null;
                }
            }
            _renderer.Clear();
        }

        public bool Set(Position pos, int player)
        {
            if (!IsClear(pos))
            {
                return false;
            }

            var p = _players[player];
            _renderer.Set(pos, p);
            _board[pos.X, pos.Y] = p;
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