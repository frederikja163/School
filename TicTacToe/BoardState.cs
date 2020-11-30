using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public sealed class BoardState
    {
        private readonly int[,] _board;
        private readonly int _rowCount;

        public BoardState(int width, int height, int rowCount, int playerCount)
        {
            Width = width;
            Height = height;
            _rowCount = rowCount;
            PlayerCount = playerCount;
            _board = new int[Width, Height];
        }

        private BoardState(int[,] board, int width, int height, int rowCount, int playerCount)
        {
            Width = width;
            Height = height;
            _rowCount = rowCount;
            PlayerCount = playerCount;
            _board = board;
        }
        
        public int Width { get; }
        public int Height { get; }

        public int PlayerCount { get; }

        public Action? OnClear;
        public Action<Position, int>? OnSet;

        public bool CheckWinner(out int player)
        {
            for (int y = 0; y <= Height - _rowCount; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    //Vertical check
                    player = _board[x, y];
                    for (int i = 1; i < _rowCount && player != -1; i++)
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
                    for (int i = 1; i < _rowCount && player != -1; i++)
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
                    for (int i = 1; i < _rowCount && player != -1; i++)
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
                    for (int i = 1; i < _rowCount && player != -1; i++)
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

            player = -1;
            return false;
        }

        public void Clear()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _board[x, y] = -1;
                }
            }
            OnClear?.Invoke();
        }

        public bool Set(Position pos, int player)
        {
            if (!IsClear(pos) || player > PlayerCount || player < 0)
            {
                return false;
            }

            OnSet?.Invoke(pos, player);
            _board[pos.X, pos.Y] = player;
            return true;
        }

        public bool IsClear(Position pos)
        {
            return pos.X >= 0 && pos.X < Width &&
                   pos.Y >= 0 && pos.Y < Height &&
                   _board[pos.X, pos.Y] == -1;
        }

        public BoardState CloneWith(Position pos, int player)
        {
            var clone = _board.Clone() as int[,];
            clone[pos.X, pos.Y] = player;
            return new BoardState(clone, Width, Height, _rowCount, PlayerCount);
        }

        public Position[] GetValidMoves()
        {
            var moves = new List<Position>();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (_board[x, y] == -1)
                    {
                        moves.Add(new Position(x, y));
                    }
                }
            }
            return moves.ToArray();
        }
    }
}