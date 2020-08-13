using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird
{
    public sealed class Player
    {
        private const float Gravity = 1500;
        private float _velocity = 0;
        private Sprite _sprite;
        private IntRect[] _animationFrames;
        private RenderWindow _window;

        public Player(RenderWindow window, float xPosition, Texture texture, params IntRect[] areas)
        {
            _window = window;
            _window.KeyReleased += OnKeyReleased;
            _animationFrames = areas;
            _sprite = new Sprite(texture, areas[0]);
            _sprite.Position = new Vector2f(xPosition, window.Size.Y / 2);
            _sprite.Scale = new Vector2f(3, 3);
            _sprite.Origin = new Vector2f(areas[0].Width / 2, areas[0].Height / 2);
        }

        public void Update(float deltaT)
        {
            if (_velocity < Gravity * 0.20f)
            {
                _sprite.TextureRect = _animationFrames[0];
            }
            else if (_velocity < Gravity * 0.40f)
            {
                _sprite.TextureRect = _animationFrames[2];
            }
            else
            {
                _sprite.TextureRect = _animationFrames[1];
            }
            _velocity -= Gravity * deltaT;
            _sprite.Rotation = Utility.Lerp(_sprite.Rotation, Utility.Clamp(-_velocity, -75, 80), deltaT * 15f);

            var newY = Utility.Clamp(_sprite.Position.Y - _velocity * deltaT, 0, _window.Size.Y);
            _sprite.Position = new Vector2f(_sprite.Position.X, newY);
        }

        public void Draw()
        {
            _window.Draw(_sprite);
        }

        private void OnKeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Space)
            {
                _velocity = Gravity * 0.3f;
            }
        }
    }
}
