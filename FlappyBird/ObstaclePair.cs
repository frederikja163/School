using SFML.Graphics;
using SFML.System;

namespace FlappyBird
{
    public sealed class ObstaclePair
    {
        private readonly RenderWindow _window;
        private readonly Sprite _spriteTop;
        private readonly Sprite _spriteBottom;

        public ObstaclePair(RenderWindow window, Sprite spriteTop, Sprite spriteBottom, float gapHeight, float gapSize)
        {
            _window = window;
            _spriteTop = spriteTop;
            _spriteBottom = spriteBottom;
            
            _spriteTop.Origin = new Vector2f(_spriteTop.TextureRect.Width / 2f, _spriteTop.TextureRect.Height);
            _spriteTop.Position = new Vector2f(window.Size.X + _spriteTop.TextureRect.Width, gapHeight - gapSize);
            _spriteTop.Scale = new Vector2f(4, 4);
            _spriteBottom.Origin = new Vector2f(_spriteTop.TextureRect.Width / 2f, 0);
            _spriteBottom.Position = new Vector2f(window.Size.X + _spriteBottom.TextureRect.Width, gapHeight + gapSize);
            _spriteBottom.Scale = new Vector2f(4, 4);
        }

        public void Draw()
        {
            _window.Draw(_spriteTop);
            _window.Draw(_spriteBottom);
        }

        public float XPosition
        {
            get => _spriteTop.Position.X;
            set
            {
                _spriteTop.Position = new Vector2f(value, _spriteTop.Position.Y);
                _spriteBottom.Position = new Vector2f(value, _spriteBottom.Position.Y);
            }
        }

        public float Top => _spriteTop.Position.Y;
        public float Bottom => _spriteBottom.Position.Y;
        public float Left => _spriteBottom.Position.X - (_spriteTop.Scale.X * _spriteTop.TextureRect.Width) / 2;
        public float Right => _spriteBottom.Position.X + (_spriteTop.Scale.X * _spriteTop.TextureRect.Width) / 2;
    }
}