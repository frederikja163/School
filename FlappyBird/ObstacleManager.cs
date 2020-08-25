using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace FlappyBird
{
    public sealed class ObstacleManager
    {
        private readonly RenderWindow _window;
        private readonly Player _player;
        private readonly SpriteSheet _sheet;
        private readonly List<ObstaclePair> _pairs = new List<ObstaclePair>();
        private int _pipeType = 0;
        private float _timeForNextObstacle = 5;
        private readonly Random _random = new Random(DateTime.UtcNow.Millisecond + DateTime.UtcNow.Hour * 1000);
        public event Action OnPlayerHitObstacle;

        public ObstacleManager(RenderWindow window, Player player, SpriteSheet sheet)
        {
            _window = window;
            _player = player;
            _sheet = sheet;
        }

        public void Update(float deltaT)
        {
            for (int i = 0; i < _pairs.Count; i++)
            {
                _pairs[i].XPosition -= deltaT * 250;
                if (_pairs[i].Right < 0)
                {
                    _pairs.RemoveAt(i);
                }
                if (_pairs[i].Right > _player.Position.X && _pairs[i].Left < _player.Position.X &&
                    !(_pairs[i].Top < _player.Position.Y && _pairs[i].Bottom > _player.Position.Y))
                {
                    OnPlayerHitObstacle?.Invoke();
                }
            }
            _timeForNextObstacle -= deltaT;
            if (_timeForNextObstacle < 0)
            {
                SpawnObstacles();
            }
        }

        public void Draw()
        {
            for (int i = 0; i < _pairs.Count; i++)
            {
                _pairs[i].Draw();
            }
        }

        private void SpawnObstacles()
        {
            var gapSize = _random.Next(50, 75);
            _pairs.Add(new ObstaclePair(_window, 
                _sheet, _pipeType,
                _random.Next(gapSize + 20, (int)_window.Size.Y - gapSize - 20), gapSize));
            _timeForNextObstacle = 1.5f;
        }
    }
}