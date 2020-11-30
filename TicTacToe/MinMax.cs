namespace TicTacToe
{
    public sealed class MinMax : IMover
    {
        private readonly BoardState _state;
        private readonly int _player;

        public MinMax(BoardState state, int player)
        {
            _state = state;
            _player = player;
        }

        public Position GetMove()
        {
            var bestScore = float.MinValue;
            Position? bestMove = null;
            var validMoves = _state.GetValidMoves();
            foreach (var move in validMoves)
            {
                var state = _state.CloneWith(move, _player);
                var score = -CalcScore(state, (_player + 1) % 1);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }
            return bestMove;
        }

        private float CalcScore(BoardState state, int player, int depth = 0)
        {
            if (state.CheckWinner(out var winner))
            {
                return -depth;
            }
            
            var bestScore = float.MinValue;
            var validMoves = state.GetValidMoves();
            foreach (var move in validMoves)
            {
                var newState = state.CloneWith(move, player);
                var score = -CalcScore(newState, (player + 1) % newState.PlayerCount, depth + 1);
                if (score > bestScore)
                {
                    bestScore = score;
                }
            }

            if (bestScore == float.MinValue)
            {
                return 0;
            }
            return bestScore;
        }
    }
}