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
        private RenderWindow _window;
        private SpriteSheet _sheet;
        private int _birdType = 0;

        public Player(RenderWindow window, float xPosition, SpriteSheet sheet)
        {
            _window = window;
            _window.KeyReleased += OnKeyReleased;
            _sheet = sheet;
            _sheet.SetBirdSprite(ref _sprite, 0, 0);
            _sprite.Position = new Vector2f(xPosition, window.Size.Y / 2);
            _sprite.Scale = new Vector2f(3, 3);
        }

        public void Update(float deltaT)
        {
            if (_velocity < Gravity * 0.20f)
            {
                _sheet.SetBirdSprite(ref _sprite, _birdType, 0);
            }
            else if (_velocity < Gravity * 0.40f)
            {
                _sheet.SetBirdSprite(ref _sprite, _birdType, 2);
            }
            else
            {
                _sheet.SetBirdSprite(ref _sprite, _birdType, 1);
            }
            _velocity -= Gravity * deltaT;
            _sprite.Rotation = Utility.Lerp(_sprite.Rotation, Utility.Clamp(-_velocity, -75, 80), deltaT * 10);

            var newY = Utility.Clamp(_sprite.Position.Y - _velocity * deltaT, 0, _window.Size.Y);
            _sprite.Position = new Vector2f(_sprite.Position.X, newY);
        }

        public Vector2f Position { get => _sprite.Position; }

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
